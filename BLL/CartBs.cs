using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICartBs
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        bool Insert(Cart obj);
        bool Update(Cart obj);
        bool Delete(int id);
        bool DeleteRange(List<int> id);
    }
    public class CartBs: ICartBs
    {
        private readonly ICartDb objDb;
        public CartBs(ICartDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }
        public bool DeleteRange(List<int> id)
        {
            return objDb.DeleteRange(id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return objDb.GetAll();
        }

        public Cart GetById(int id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(Cart obj)
        {
            return objDb.Insert(obj);
        }

        public bool Update(Cart obj)
        {
            return objDb.Update(obj);
        }
    }
}
