using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrderBillDb
    {
        IEnumerable<OrderBill> GetAll();
        OrderBill GetById(int id);

        bool Insert(OrderBill obj);
        bool Update(OrderBill obj);
        bool Delete(int id);
    }
    public class OrderBillDb: IOrderBillDb
    {
        private RBADbContext context;
        public OrderBillDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.OrderBill.Find(id);
            context.OrderBill.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<OrderBill> GetAll()
        {
            return context.OrderBill;
        }

        public OrderBill GetById(int id)
        {
            return context.OrderBill.Find(id);
        }

        public bool Insert(OrderBill obj)
        {
            context.OrderBill.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(OrderBill obj)
        {
            context.OrderBill.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
