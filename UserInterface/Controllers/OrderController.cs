using BLL;
using BOL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        UserManager<ApplicationUsers> userManager;
        private IApplicationUsersBs objUserBs;
        private ICartBs objCartBs;
        private IItemsBs objItemBs;
        private IOrderBillBs objOrderBillBs;
        private IOrderDetailBs objOrderDetailBs;
        public OrderController(IApplicationUsersBs _objUserBs,IOrderBillBs _objOrderBillBs, IOrderDetailBs _objOrderDetailBs, UserManager<ApplicationUsers> _userManager, ICartBs _objCartBs, IItemsBs _objItemBs)
        {
            userManager = _userManager;
            objCartBs = _objCartBs;
            objItemBs = _objItemBs;
            objOrderDetailBs = _objOrderDetailBs;
            objOrderBillBs = _objOrderBillBs;
            objUserBs = _objUserBs;
        }
        public  async Task<IActionResult> Index()
        {
            var objOrderVM = new List<OrderBillVM>();
            try
            {
                var objUser = await userManager.FindByNameAsync(User.Identity.Name);

                List<OrderBill> objList = new List<OrderBill>();

                if (User.IsInRole("Manager"))
                    objList = objOrderBillBs.GetAll().ToList();
                else if (User.IsInRole("Customer"))
                    objList = objOrderBillBs.GetAll().Where(x => x.Id == objUser.Id).ToList();


               objList.ForEach(x =>
                {
                    List<string> itemsNameList = new List<string>();
                    foreach (var item in objOrderDetailBs.GetAll().Where(m => m.InvoiceNo == x.InvoiceNo).Select(x => x.ItemsId).ToList())
                    {
                        var itemObj = objItemBs.GetById(item);
                        itemsNameList.Add(itemObj.Name);
                    }

                    var objUser = objUserBs.GetById(x.Id);
                    objOrderVM.Add(new OrderBillVM() {ContactNo = objUser.Contact ?? "" , Address = objUser.Address ?? "" ,InvoiceNo = x.InvoiceNo,Items =string.Join(", ", itemsNameList) , TotalBill = x.TotalBill, OrderStatus = x.Status });
                });

                return View(objOrderVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }
        }

        public IActionResult OrderDetails(int invoiceNo)
        {
            try
            {
                List<OrderDetailVM> objVM = new List<OrderDetailVM>();

                var objOrderDetail = objOrderDetailBs.GetAll().Where(x => x.InvoiceNo == invoiceNo);
                objOrderDetail.ToList().ForEach(x => 
                {
                    var objItem = objItemBs.GetById(x.ItemsId);
                    objVM.Add(new OrderDetailVM(){Course = objItem.Course, ItemName = objItem.Name, Quantity = x.Quantity, Price = x.Price, TotalPrice = x.Quantity * x.Price});
                });
                
                return View(objVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult UpdateOrderStatus(int invoiceNo)
        {
            var objOrderVM = new OrderBillVM();
            try
            {
                List<string> itemsNameList = new List<string>();
                var objOrderBill = objOrderBillBs.GetById(invoiceNo);
                var objOrderDetail = objOrderDetailBs.GetAll().Where(m => m.InvoiceNo == invoiceNo);

                foreach (var item in objOrderDetail.Select(x => x.ItemsId).ToList())
                {
                    var itemObj = objItemBs.GetById(item);
                    itemsNameList.Add(itemObj.Name);
                }

                //Order Status Items
                List<SelectListItem> statusListItems = new List<SelectListItem>();
                statusListItems.Add(new SelectListItem() { Text = "Pending",    Value = "Pending"});
                statusListItems.Add(new SelectListItem() { Text = "Accepted",   Value = "Accepted" });
                statusListItems.Add(new SelectListItem() { Text = "Preparing",  Value = "Preparing" });
                statusListItems.Add(new SelectListItem() { Text = "Delivering", Value = "Delivering" });
                statusListItems.Add(new SelectListItem() { Text = "Delivered",  Value = "Delivered" });
               

                objOrderVM.InvoiceNo = objOrderBill.InvoiceNo;
                objOrderVM.Items = string.Join(", ", itemsNameList);
                objOrderVM.TotalBill = objOrderBill.TotalBill;
                objOrderVM.OrderStatusList = new SelectList(statusListItems, "Value", "Text");

                return View(objOrderVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index", "Order");
            }
        }
        [HttpPost]
        public IActionResult UpdateOrderStatus(OrderBillVM model)
        {
            try
            {
                var objOrderBill = objOrderBillBs.GetById(model.InvoiceNo);

                objOrderBill.Status = model.OrderStatus == null ? "Pending":model.OrderStatus ;
                objOrderBillBs.Update(objOrderBill);

                TempData["SuccessMsg"] = "Order updated successfully.";
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("UpdateOrderStatus", "Order");
            }
            
        }

    }
}
