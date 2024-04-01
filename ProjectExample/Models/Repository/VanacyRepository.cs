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
                HR_projectEntities2 entities1 = new HR_projectEntities2();
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
                HR_projectEntities2 entities1 = new HR_projectEntities2();
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
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (id != null)
                {
                    if (entity is Vancacy)
                    {
                        var vancacy = entity as Vancacy;
                        var existingVancacy = entities1.Vancacies.Find(id);

                        if (existingVancacy != null)
                        {
                            if (vancacy.status == 0)
                            {
                                existingVancacy.status = 1;
                                entities1.SaveChanges();
                            }
                            else if (vancacy.status == 1)
                            {
                                existingVancacy.status = 0;
                                entities1.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error Update values form SQL Server");
            }
        }
        public void UpdateCancacy(T entity, int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (id != null)
                {
                    if (entity is Vancacy)
                    {
                        var existingVancacy = entities1.Vancacies.Find(id);
                        var vancacy1 = entity as Vancacy;

                        if (existingVancacy != null)
                        {
                            existingVancacy.decription = vancacy1.decription;
                            entities1.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error Update values form SQL Server");
            }
        }
        public T GetById(string name)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Cadidates
                          where c.id_vanacy == int.Parse(name)   
                          select c).FirstOrDefault();
                return (T)(object)rs;
              
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        public T GetByIdVancacy(int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Vancacies
                          where c.id == id
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