using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrderDetailBs
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);

        bool Insert(OrderDetail obj);
        bool InsertRange(List<OrderDetail> obj);
        bool Update(OrderDetail obj);
        bool Delete(int id);
    }
    public class OrderDetailBs: IOrderDetailBs
    {
        private IOrderDetailDb objDb;
        public OrderDetailBs(IOrderDetailDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return objDb.GetAll();
        }

        public OrderDetail GetById(int id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(OrderDetail obj)
        {
            return objDb.Insert(obj);
        }
        public bool InsertRange(List<OrderDetail> obj)
        {
            return objDb.InsertRange(obj);
        }

        public bool Update(OrderDetail obj)
        {
            return objDb.Update(obj);
        }
    }
}
