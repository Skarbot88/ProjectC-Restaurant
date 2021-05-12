using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRestaurantDb
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant GetById(int id);
        bool Insert(Restaurant obj);
        bool Update(Restaurant obj);
        bool Delete(int id);
    }
    public class RestaurantDb: IRestaurantDb
    {
        private readonly RBADbContext context;
        public RestaurantDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.Restaurant.Find(id);
            context.Restaurant.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return context.Restaurant;
        }

        public Restaurant GetById(int id)
        {
            return context.Restaurant.Find(id);
        }

        public bool Insert(Restaurant obj)
        {
            context.Restaurant.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(Restaurant obj)
        {
            context.Restaurant.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
