using BOL;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Data;

namespace DAL
{
    public interface IItemsDb
    {
        IEnumerable<Items> GetAll();
        Items GetById(int id);
        bool Insert(Items obj);
        bool Update(Items obj);
        bool Delete(int id);
    }
    public class ItemsDb: IItemsDb
    {
        private readonly RBADbContext context;
        public ItemsDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.Items.Find(id);
            context.Items.Remove(obj);
            context.SaveChanges();
            return true;
        }

       
        public IEnumerable<Items> GetAll()
        {
            return context.Items;
        }

        public Items GetById(int id)
        {
            return context.Items.Find(id);
        }

        public bool Insert(Items obj)
        {
            context.Items.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(Items obj)
        {
            context.Items.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
