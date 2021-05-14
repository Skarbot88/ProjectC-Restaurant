using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.ViewModels;

namespace UserInterface.Controllers
{
    public class TableBookingController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
        
    }
}
