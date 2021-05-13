using BOL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public interface ICartDb
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        bool Insert(Cart obj);
        bool Update(Cart obj);
        bool Delete(int id);
        bool DeleteRange(List<int> id);
    }
    public class CartDb : ICartDb
    {
        private readonly RBADbContext context;
        public CartDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.Cart.Find(id);
            context.Cart.Remove(obj);
            context.SaveChanges();
            return true;
        }
        public bool DeleteRange(List<int> CartId)
        {
            var objList = context.Cart.Where(x => CartId.Contains(x.CartId));
            context.Cart.RemoveRange(objList);
            context.SaveChanges();
            return true;
        }
        public IEnumerable<Cart> GetAll()
        {
            return context.Cart;
        }

        public Cart GetById(int id)
        {
            return context.Cart.Find(id);
        }

        public bool Insert(Cart obj)
        {
            context.Cart.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(Cart obj)
        {
            context.Cart.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
