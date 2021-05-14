using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IApplicationUsersDb
    {
        IEnumerable<ApplicationUsers> GetAll();
        ApplicationUsers GetById(string id);

        bool Insert(ApplicationUsers obj);
        bool Update(ApplicationUsers obj);
        bool Delete(int id);
    }
    public class ApplicationUsersDb : IApplicationUsersDb
    {
        //Entity Framework
        private RBADbContext context;
        public ApplicationUsersDb(RBADbContext _context)
        {
            context = _context;
        }
        public bool Delete(int id)
        {
            var obj = context.ApplicationUsers.Find(id);
            context.ApplicationUsers.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<ApplicationUsers> GetAll()
        {
            return context.ApplicationUsers;
        }

        public ApplicationUsers GetById(string id)
        {
            return context.ApplicationUsers.Find(id);
        }

        public bool Insert(ApplicationUsers obj)
        {
            context.ApplicationUsers.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Update(ApplicationUsers obj)
        {
            context.ApplicationUsers.Update(obj);
            context.SaveChanges();
            return true;
        }
    }
}
