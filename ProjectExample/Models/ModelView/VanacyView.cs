using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class VanacyView
    {
        private readonly VanacyRepository<Vancacy> repository;
        public VanacyView()
        {
            repository = new VanacyRepository<Vancacy>();
        }
        public IEnumerable<Vancacy> GetAllVanCy() {
            return repository.GetAllValue();
        }
        public void InsertCadidate(Vancacy vancacys)
        {
            try
            {
                if (vancacys != null)
                {
                    repository.AddInfo(vancacys);
                }
                else
                {
                    throw new ArgumentException("Null Data!!");
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}