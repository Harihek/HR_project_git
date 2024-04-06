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
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    // Sử dụng repository để lấy thông tin Employee từ username
                    return repository.GetValueByUsername(username);
                }
                else
                {
                    throw new ArgumentException("Username is null or empty!");
                }
            }
            catch (Exception e)
            {
                // Xử lý ngoại lệ nếu cần
                // Ví dụ: log lỗi, thông báo cho người dùng, v.v.
                return null; // Hoặc là throw nếu bạn muốn truyền ngoại lệ ra ngoài
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