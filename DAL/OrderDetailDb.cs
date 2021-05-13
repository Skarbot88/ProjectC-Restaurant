using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrderDetailDb
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);

        bool Insert(OrderDetail obj);
        bool InsertRange(List<OrderDetail> obj);
        bool Update(OrderDetail obj);
        bool Delete(int id);
    }
    public class OrderDetailDb: IOrderDetailDb
    {
        private RBADbContext context;
        public OrderDetailDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.OrderDetail.Find(id);
            context.OrderDetail.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetail;
        }

        public OrderDetail GetById(int id)
        {
            return context.OrderDetail.Find(id);
        }

        public bool Insert(OrderDetail obj)
        {
            context.OrderDetail.Add(obj);
            context.SaveChanges();
            return true;
        }
        public bool InsertRange(List<OrderDetail> obj)
        {
            context.OrderDetail.AddRange(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(OrderDetail obj)
        {
            context.OrderDetail.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
