using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class EmployeeView
    {
        private readonly EmployRepository<Employee> repository;

        public EmployeeView()
        {
            repository = new EmployRepository<Employee>();
        }
        public IEnumerable<Employee> GetAllVanCy()
        {
            return repository.GetAllValue();
        }
        public Employee GetValueIDView(string name)
        {
            try
            {
                if (name != null)
                {
                    return repository.GetById(name);
                }
                return null;
            }
            catch (Exception e)
            {
                return null; 
            }

        }
        public Employee GetIDEmp(int? id)
        {
            try
            {
                if (id != null)
                {
                    return repository.GetById1(id);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public void InsetEmp(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    repository.AddInfo(employee);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}