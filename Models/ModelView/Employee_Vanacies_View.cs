using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class Employee_Vanacies_View
    {
        private readonly Employ_Vanacy_Repository<Employ_Vanacies> repository;
        public Employee_Vanacies_View()
        {
            repository = new Employ_Vanacy_Repository<Employ_Vanacies>();

        }
        public IEnumerable<Employ_Vanacies> GetAllValues()
        {
            return repository.GetEmploy_Vanacy();
        }
    }
}