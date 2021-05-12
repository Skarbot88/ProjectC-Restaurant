using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IItemsBs
    {
        IEnumerable<Items> GetAll();
        Items GetById(int id);

        bool Insert(Items obj);
        bool Update(Items obj);
        bool Delete(int id);
    }
    public  class ItemsBs: IItemsBs
    {
        private IItemsDb objDb;
        public ItemsBs(IItemsDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<Items> GetAll()
        {
            return objDb.GetAll();
        }

        public Items GetById(int id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(Items obj)
        {
            return objDb.Insert(obj);
        }

        public bool Update(Items obj)
        {
            return objDb.Update(obj);
        }
    }
}
