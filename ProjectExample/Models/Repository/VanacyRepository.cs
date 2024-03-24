using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class VanacyRepository<T> : Inter_face<T>
    {
        public IEnumerable<T> GetAllValue()
        {
            try
            {
                InterViiewDevEntities3 entities1 = new InterViiewDevEntities3();
                var rs = entities1.Vancacies.ToList().Cast<T>();
                return rs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void AddInfo(T entity)
        {
            try
            {
                InterViiewDevEntities3 entities1 = new InterViiewDevEntities3();
                if (entity is Vancacy)
                {
                    var vanacy1 = entity as Vancacy;
                    entities1.Vancacies.Add(vanacy1);
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
          
        }

        public void Update(T entity, object id)
        {
            
        }

        public T GetById(int id)
        {
            try
            {
                InterViiewDevEntities3 entities1 = new InterViiewDevEntities3();
                var rs = (from c in entities1.Cadidates
                          where c.id_vanacy == (int)id
                          select c).FirstOrDefault();
                return (T)(object)rs;
              
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
    }
}