using BLL;
using BOL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.ViewModels;

namespace UserInterface.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class RestaurantController : Controller
    {
        private IRestaurantBs objRestBs;
        public RestaurantController(IRestaurantBs _objRestBs)
        {
            objRestBs = _objRestBs;
        }
        public IActionResult Index()
        {
            var objListVM = new List<RestaurantVM>();
            objRestBs.GetAll().ToList().ForEach(x =>
            {
                objListVM.Add(new RestaurantVM() { RestaurantId = x.RestaurantId, Name = x.Name, Contact = x.Contact, Address = x.Address });
            });

            return View(objListVM);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int id)
        {

            RestaurantVM objVM = new RestaurantVM();

            if (id > 0)
            {

                var obj = objRestBs.GetById(id);
                if (obj != null)
                {
                    objVM.RestaurantId = obj.RestaurantId;
                    objVM.Name = obj.Name;
                    objVM.Contact = obj.Contact;
                    objVM.Address = obj.Address;
                }
            }
            return View(objVM);
        }

        [HttpPost]
        public IActionResult Save(RestaurantVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Mapping
                    Restaurant obj = new Restaurant() { RestaurantId = model.RestaurantId, Name = model.Name, Contact = model.Contact, Address = model.Address };

                    if (model.RestaurantId == 0) //Insert
                        objRestBs.Insert(obj);
                    else //Update
                        objRestBs.Update(obj);

                    TempData["SuccessMsg"] = "Record saved successfully.";
                    return RedirectToAction("Index", "Restaurant");
                }
                else
                {
                    return View("CreateOrEdit");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("CreateOrEdit", "Restaurant");
            }
        }
        public IActionResult Delete(int id)
        {
            if (id != null && id > 0)
            {
                objRestBs.Delete(id);
                TempData["SuccessMsg"] = "Record deleted successfully.";
                return RedirectToAction("Index", "Restaurant");
            }
            else
            {
                TempData["ErrorMsg"] = "Record can not be deleted.";
                return RedirectToAction("Index", "Restaurant");
            }
        }


    }
}
