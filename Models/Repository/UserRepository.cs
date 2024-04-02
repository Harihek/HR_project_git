using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class UserRepository<T> : Inter_face<T>
    {
        public void AddInfo(T entity)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (entity is infoUser)
                {
                    var userdata = entity as infoUser;
                    entities1.infoUsers.Add(userdata);
                    entities1.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Error send values form SQL Server");
                }
            }
            catch (Exception e)
            {
                e.Equals("Error!!");
            }
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
                var rs = entities1.infoUsers.ToList().Cast<T>();
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