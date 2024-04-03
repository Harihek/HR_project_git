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
        public Employee GetValueID(string username)
        {
            // Thực hiện truy vấn để lấy thông tin Employee từ username
            // Đây là nơi bạn sẽ truy vấn cơ sở dữ liệu để lấy thông tin Employee
            // Sau đó, trả về đối tượng Employee chứa thông tin tương ứng
            // Dưới đây là một đoạn mã giả để minh họa

            // Kết nối cơ sở dữ liệu
            using (var dbContext = new HR_projectEntities2())
            {
                // Tìm Employee có username trùng khớp
                var employee = dbContext.Employees.FirstOrDefault(e => e.username == username);

                return employee;
            }
        }
        public void UpdateEmp(Employee emp)
        {
            try
            {
                if (emp != null)
                {
                    repository.UpdateImage(emp, emp.id);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}