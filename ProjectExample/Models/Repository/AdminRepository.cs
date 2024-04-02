using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class AdminRepository<T> : Inter_face<T>
    {
        public void AddInfo(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllValue()
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = entities1.Admins.ToList().Cast<T>();
                return rs;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            };
        }

        public void Update(T entity, object id)
        {
            throw new NotImplementedException();
        }
    }
}