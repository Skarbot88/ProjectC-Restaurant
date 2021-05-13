using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrderBillBs
    {
        IEnumerable<OrderBill> GetAll();
        OrderBill GetById(int id);

        bool Insert(OrderBill obj);
        bool Update(OrderBill obj);
        bool Delete(int id);
    }
    public class OrderBillBs: IOrderBillBs
    {
        private IOrderBillDb objDb;
        public OrderBillBs(IOrderBillDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<OrderBill> GetAll()
        {
            return objDb.GetAll();
        }

        public OrderBill GetById(int id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(OrderBill obj)
        {
            return objDb.Insert(obj);
        }

        public bool Update(OrderBill obj)
        {
            return objDb.Update(obj);
        }
    }
}
