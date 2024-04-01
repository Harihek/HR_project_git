using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class Schedule_Interview_Repository<T>
    {
        public void AddInfo(int? id1, int? id2, int? id3, string code, DateTime start, DateTime end)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (code != null && start != null && end != null)
                {
                    entities1.sp_BuoiPhongVan(id1,id2,id3,code,start,end);
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
        public List<ShowInterView> GetInterViewByName(string name)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                
                var rs = from c in entities1.Employees
                             join a in entities1.Candidate_Employe on c.id equals a.id_emp
                             join b in entities1.Cadidates on a.code_cadi equals b.code_cadi
                             where c.username == name//Lọc theo Name
                             select new ShowInterView
                             {
                                 Candidate_Employe = a,
                                 Cadidate = b,
                                 Employee = c,
                              };
                   return rs.ToList();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Error occurred while retrieving the entity by ID.", ex);
            }
        }
        public List<ShowInterView> GetResults(int idcadi, int idemp)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();

                var rs = from c in entities1.Employees
                         join a in entities1.Candidate_Employe on c.id equals a.id_emp
                         join b in entities1.Cadidates on a.code_cadi equals b.code_cadi
                         where c.id == idemp && b.id_candidate == idcadi 
                         select new ShowInterView
                         {
                             Candidate_Employe = a,
                             Cadidate = b,
                             Employee = c,
                         };
                return rs.ToList();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Error occurred while retrieving the entity by ID.", ex);
            }
        }
        public void UpdateResult(int id_Cadiemp, int status)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (id_Cadiemp != null)
                {                
                        var existingVancacy = entities1.Candidate_Employe.Find(id_Cadiemp);

                        if (existingVancacy != null)
                        {
                            if (status == 1)
                            {
                                existingVancacy.status = 1;
                                entities1.SaveChanges();
                            }
                            else if (status == 2)
                            {
                                existingVancacy.status = 2;
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

    }
}