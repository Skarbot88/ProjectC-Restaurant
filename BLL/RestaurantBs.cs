using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IRestaurantBs
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant GetById(int id);
        bool Insert(Restaurant obj);
        bool Update(Restaurant obj);
        bool Delete(int id);
    }
    public class RestaurantBs: IRestaurantBs
    {
        private readonly IRestaurantDb objDb;
        public RestaurantBs(IRestaurantDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return objDb.GetAll();
        }

        public Restaurant GetById(int id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(Restaurant obj)
        {
            return objDb.Insert(obj);
        }

        public bool Update(Restaurant obj)
        {
            return objDb.Update(obj);
        }
    }
}
