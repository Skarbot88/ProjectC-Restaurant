using BLL;
using BOL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UserInterface.ViewModels;

namespace UserInterface.Controllers
{
    public class CartController : Controller
    {
        UserManager<ApplicationUsers> userManager;
        private ICartBs objCartBs;
        private IItemsBs objItemBs;
        private IOrderBillBs objOrderBillBs;
        private IOrderDetailBs objOrderDetailBs;
        public CartController(IOrderBillBs _objOrderBillBs, IOrderDetailBs _objOrderDetailBs,UserManager<ApplicationUsers> _userManager, ICartBs _objCartBs, IItemsBs _objItemBs)
        {
            userManager = _userManager;
            objCartBs = _objCartBs;
            objItemBs = _objItemBs;
            objOrderDetailBs = _objOrderDetailBs;
            objOrderBillBs = _objOrderBillBs;
        }
        public async Task<IActionResult> Index()
        {
            var objListVM = new List<CartVM>();
            try
            {
                var objUser = await userManager.FindByNameAsync(User.Identity.Name);                
                objCartBs.GetAll().ToList().ForEach(x =>
                {
                    var objItem = objItemBs.GetById(x.ItemsId);
                    if (objItem != null)
                        objListVM.Add(new CartVM(){CartId = x.CartId, ItemId = x.ItemsId, ItemName = objItem.Name, Course = objItem.Course, Price = x.Price, Quantity = x.Quantity, TotalPrice = x.TotalPrice                        });
                 });
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Items");
            }
            return View(objListVM);
        }
        //[HttpPost]
        public async Task<IActionResult> AddToCart(int itemId)
        {
            try
            {
                if (itemId > 0)
                {
                    var userObj = await userManager.FindByNameAsync(User.Identity.Name);
                    var itemObj = objItemBs.GetById(itemId);
                    var cartItemObj = objCartBs.GetAll().AsQueryable().AsNoTracking().Where(x => x.Id == userObj.Id && x.ItemsId == itemObj.ItemId).FirstOrDefault();

                    if (cartItemObj != null) //Update Quantity, Total Price
                    {
                        if (cartItemObj.Quantity >= 5)
                        {
                            TempData["ErrorMsg"] = "Quantity can not exceed to 5.";
                            return RedirectToAction("Index", "Items");
                        }
                        
                        Cart objCart = new Cart() { CartId = cartItemObj.CartId, Quantity = cartItemObj.Quantity + 1, ItemsId = itemId, Id = userObj.Id, Price = itemObj.Price, TotalPrice = (cartItemObj.Quantity + 1) * itemObj.Price };
                        objCartBs.Update(objCart);
                        TempData["SuccessMsg"] = "Item addedd successfully.";
                        
                        
                    }
                    else //Insert New
                    {
                        Cart objCart = new Cart() { Quantity = 1, ItemsId = itemId, Id = userObj.Id, Price = itemObj.Price, TotalPrice = itemObj.Price };
                        objCartBs.Insert(objCart);
                        TempData["SuccessMsg"] = "Item addedd successfully.";
                    }
                    
                    return RedirectToAction("Index", "Items");
                }
                else
                {
                    TempData["ErrorMsg"] = "Item Id is not attached. Please contact with your support team.";
                    return RedirectToAction("Index", "Items");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Items");
            }
            
            
        }

        public IActionResult Delete(int id)
        {
            try
            {
                objCartBs.Delete(id);
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        public ActionResult UpdateQuantity(bool isAdded, int cartId)
        {
            try
            {
                var obj = objCartBs.GetById(cartId);

                if (!isAdded && obj.Quantity == 1)
                {
                    TempData["ErrorMsg"] = "Quantity can not be reduced.";
                    return RedirectToAction("Index", "Cart");
                }

                if (isAdded && obj.Quantity >= 5)
                {
                    TempData["ErrorMsg"] = "Please select quantity between 1 to 5.";
                    return RedirectToAction("Index", "Cart");
                }

                obj.Quantity = isAdded ? obj.Quantity + 1 : obj.Quantity - 1;
                obj.TotalPrice = obj.Quantity * obj.Price;

                objCartBs.Update(obj);
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<ActionResult> Checkout()
        {
            try
            {
                List<StockVM> objStockVM = new List<StockVM>();

                var objUser = await userManager.FindByNameAsync(User.Identity.Name);
                bool isQuantityAvailable = true;

                //Get Logged-In User's Cart List
                var objCartList = objCartBs.GetAll().Where(x => x.Id == objUser.Id).ToList();

                //Check if cart item's quantity is available in stock
                objCartList.ForEach(x =>
                {
                    var itemObj = objItemBs.GetById(x.ItemsId);
                    if (x.Quantity > itemObj.InStock)
                    {
                        isQuantityAvailable = false;
                        objStockVM.Add(new StockVM(){ItemName = itemObj.Name, AvailableQty = itemObj.InStock});
                    }
                });

                //If any item quantity is not available in stock => send error message
                string outputString = "Sorry, these items have less quantity in stock.";
                if (!isQuantityAvailable)
                {
                    objStockVM.ForEach(x =>
                    {
                        outputString += "= Item Name: " + x.ItemName + ",  InStock: " + x.AvailableQty;
                    });

                    TempData["ErrorMsg"] = outputString;
                    return RedirectToAction("Index", "Cart");
                }
                else //If all item's quantity is available in stock => proceed order
                {
                    if (objCartBs.GetAll().Where(x => x.Id == objUser.Id).Count() == 0)
                    {
                        return RedirectToAction("Index", "Cart");
                    }

                    decimal totalBill = objCartList.Sum(x => x.TotalPrice);

                    //Used Transaction to save record in multiple tables
                    using (var trans = new TransactionScope())
                    {
                        //Save Record in Order Bill
                        OrderBill objOrderBill = new OrderBill() {Id = objUser.Id,  TotalBill = totalBill, Status = "Pending" };
                        objOrderBillBs.Insert(objOrderBill);


                        //Save Records in Order Detail
                        List<OrderDetail> objOrderDetail = new List<OrderDetail>();
                        objCartList.ForEach(x =>{objOrderDetail.Add(new OrderDetail() {  InvoiceNo = objOrderBill.InvoiceNo, ItemsId = x.ItemsId, Price = x.Price, Quantity = x.Quantity });});
                        objOrderDetailBs.InsertRange(objOrderDetail);

                        //Update Stock for each item
                        objCartList.ForEach(x =>
                        {
                            var objItem = objItemBs.GetById(x.ItemsId);
                            objItem.InStock = objItem.InStock - x.Quantity;
                            objItemBs.Update(objItem);
                        });

                        //Get Id's to remove from cart after order
                        var cartListIds = objCartList.Select(x => x.CartId).ToList();
                        objCartBs.DeleteRange(cartListIds);

                        

                        trans.Complete();
                        TempData["SuccessMsg"] = "Your order has been placed.";
                    }

                    return RedirectToAction("Index", "Order");

                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
            
        }
    }
}
