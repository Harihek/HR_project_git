
using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class Employ_Vanacy_Repository<T>
    {
        //Inner join Cadidate and Vanacies
        public IEnumerable<Employ_Vanacies> GetEmploy_Vanacy()
        {
            try
            {
                HR_projectEntities entities1 = new HR_projectEntities();
                var rs = from c in entities1.Employees
                         join a in entities1.Vancacies on c.id equals a.id_emp
                         select new Employ_Vanacies
                         {
                             Employee = c,
                             Vancacy = a
                         };
                return rs.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}