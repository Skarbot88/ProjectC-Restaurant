using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private APIDbContext context;
        public ItemsController(APIDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public List<Items> GetAll()
        {
            List<Items> objList = new List<Items>();
            //Get All Items
            context.Items.ToList().ForEach(x =>{objList.Add(new Items() { ItemId = x.ItemId, Name = x.Name, Description = x.Description, Price = x.Price, Course = x.Course, InStock = x.InStock });});
            return objList;
        }

        

        
    }
}
