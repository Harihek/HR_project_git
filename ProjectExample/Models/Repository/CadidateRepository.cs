using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class CadidateRepository<T> : Inter_face<T>
    {
        public IEnumerable<T> GetAllValue()
        {
            try
            {
                HR_projectEntities2  entities1 = new HR_projectEntities2();
                var rs = entities1.Cadidates.ToList().Cast<T>();

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
                if(entity is Cadidate)
                {
                    var cadidate = entity as Cadidate;
                    entities1.Cadidates.Add(cadidate);
                    entities1.SaveChanges();
                }
                else {
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
                    if (entity is Cadidate)
                    {
                        var existingCandi = entities1.Cadidates.Find(id);
                        var vancacy1 = entity as Vancacy;

                        if (existingCandi != null)
                        {
                            existingCandi.status = 1;
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

        public List<Cadidate> GetById(int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                    var rs = (from c in entities1.Cadidates
                              where c.id_vanacy == id
                              select c).ToList();
                if (rs != null)
                {
                    return rs;
                }
                //if (rs != null && rs.Any() && typeof(T).IsAssignableFrom(rs.First().GetType()))
                //{
                //    return rs;
                //}


                throw new ArgumentException("Invalid ID or incompatible types");

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Error occurred while retrieving the entity by ID.", ex);
            }

        }
        public T GetByIdCode(string code)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Cadidates
                          where c.code_cadi == code
                          select c).FirstOrDefault();
                return (T)(object)rs;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        public T GetByIdStatus(int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Cadidates
                          where c.id_candidate == id
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