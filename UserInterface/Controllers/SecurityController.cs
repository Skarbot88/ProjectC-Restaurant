using BLL;
using BOL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.ViewModels;

namespace UserInterface.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly IApplicationUsersBs objUserBs;
        UserManager<ApplicationUsers> userManager;
        SignInManager<ApplicationUsers> signInManager;
        public SecurityController(IApplicationUsersBs _objUserBs, UserManager<ApplicationUsers> _userManager, SignInManager<ApplicationUsers> _signInManager)
        {
            objUserBs = _objUserBs;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "UserName or Password is incorrect";
                        return View();
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUsers objUser = new ApplicationUsers()
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };
                    bool flag = false;

                    var resultCreate = await userManager.CreateAsync(objUser, model.Password);

                    if (resultCreate.Succeeded)
                    {
                        var resultRoleAssign = await userManager.AddToRoleAsync(objUser, "Customer");
                        if (resultRoleAssign.Succeeded)
                        {
                            flag = true;
                        }

                    }
                    if (flag)
                    {
                        var resultSignIn = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                        if (resultSignIn.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    if (!flag)
                    {
                        string errorMsg = resultCreate.Errors.FirstOrDefault().Description;
                        TempData["ErrorMsg"] = errorMsg;
                    }
                }
               
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            };
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Security");
        }
    }
}
