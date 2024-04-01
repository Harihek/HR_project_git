using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class Candidate_Vanacies_Repository<T>
    {
        //Inner join Cadidate and Vanacies
        public IEnumerable<Cadidate_Vanacies> GetCadidate_Vanacy()
        {
            try
            {
                HR_projectEntities entities1 = new HR_projectEntities();
                var rs = from c in entities1.Cadidates
                         join a in entities1.Vancacies on c.id_vanacy equals a.id
                         select new Cadidate_Vanacies
                         {
                             Cadidate = c,
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