using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IApplicationUsersBs
    {
        IEnumerable<ApplicationUsers> GetAll();
        ApplicationUsers GetById(string id);

        bool Insert(ApplicationUsers obj);
        bool Update(ApplicationUsers obj);
        bool Delete(int id);
    }
    public class ApplicationUsersBs : IApplicationUsersBs
    {

        private IApplicationUsersDb objDb; 
        public ApplicationUsersBs(IApplicationUsersDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(int id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<ApplicationUsers> GetAll()
        {
            return objDb.GetAll();
        }

        public ApplicationUsers GetById(string id)
        {
            return objDb.GetById(id);
        }

        public bool Insert(ApplicationUsers obj)
        {
            return objDb.Insert(obj);
        }

        public bool Update(ApplicationUsers obj)
        {
            return objDb.Update(obj);
        }
    }
}
