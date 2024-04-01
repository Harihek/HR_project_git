
using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                HR_projectEntities entities1 = new HR_projectEntities();
                var rs = entities1.Vancacies.ToList().Cast<T>();
                return rs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void AddVancacy(T entity)
        {
            try
            {
                HR_projectEntities entities1 = new HR_projectEntities();
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
        public void AddInfo(T entity)
        {
            try
            {
                HR_projectEntities entities1 = new HR_projectEntities();
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
                if (entity is Vancacy)
                {
                    var updatedVacancy = entity as Vancacy;

                    using (var entities1 = new HR_projectEntities())
                    {
                        var existingVacancy = entities1.Vancacies.Find(id);

                        if (existingVacancy == null)
                        {
                            throw new ArgumentException("Vacancy not found in the database.");
                        }

                        existingVacancy.title_job_position = updatedVacancy.title_job_position;
                        existingVacancy.decription = updatedVacancy.decription;
                        existingVacancy.status = updatedVacancy.status;
                        existingVacancy.quantity = updatedVacancy.quantity;
                        existingVacancy.date_submitted = updatedVacancy.date_submitted;

                        entities1.SaveChanges();
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid entity type.");
                }
            }
            catch (Exception e)
            {
                // Handle exception
                throw new Exception("Error updating vacancy in the database", e);
            }
        }




        public T GetById(int id)
        {
            try
            {
                HR_projectEntities entities1 = new HR_projectEntities();
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
        public T GetByIdVancacy(int id)
        {
            try
            {
                using (HR_projectEntities entities1 = new HR_projectEntities())
                {
                    var rs = (from v in entities1.Vancacies
                              where v.id == id
                              select v).FirstOrDefault();
                    return (T)(object)rs;
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Error retrieving vacancy by ID", ex);
            }
        }
    }
}