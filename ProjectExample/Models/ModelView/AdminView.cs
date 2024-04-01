using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class AdminView
    {
        private readonly AdminRepository<Admin> repository;

        public AdminView()
        {
            repository = new AdminRepository<Admin>();
        }
        public IEnumerable<Admin> GetAllVanCy()
        {
            return repository.GetAllValue();
        }
    }
}