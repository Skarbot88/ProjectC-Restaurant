using BLL;
using BOL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.ViewModels;

namespace UserInterface.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUsersBs objUserBs;
        UserManager<ApplicationUsers> userManager;
        
        public ApplicationUsersController(IApplicationUsersBs _objUserBs, UserManager<ApplicationUsers> _userManager)
        {
            objUserBs = _objUserBs;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            RegisterVM objVM = new RegisterVM();
            var hasher = new PasswordHasher<ApplicationUsers>();
            try
            {
                var objUser = await userManager.FindByNameAsync(User.Identity.Name);
                objVM.Id = objUser.Id;
                objVM.Email = objUser.Email;
                objVM.UserName = objUser.UserName;
                objVM.Address = objUser.Address;
                objVM.ContactNo = objUser.Contact;

                return View(objVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Edit", "ApplicationUsers");
            }
        }
        [HttpPost]
        public IActionResult Edit(RegisterVM model)
        {
            try
            {
                ApplicationUsers objUser = objUserBs.GetById(model.Id);
                var hasher = new PasswordHasher<ApplicationUsers>();
               
                objUser.Email = model.Email;
                objUser.UserName = model.UserName;
                objUser.Contact = model.ContactNo;
                objUser.Address = model.Address;

                objUser.PasswordHash = model.Password == "Test" ? objUser.PasswordHash : hasher.HashPassword(null, model.Password);

                objUserBs.Update(objUser);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Edit", "ApplicationUsers");
            }
        }
    }
}
