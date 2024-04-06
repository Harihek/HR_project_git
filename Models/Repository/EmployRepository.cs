using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjectExample.Models.Repository
{
    public class EmployRepository<T> : Inter_face<T>
    {
        public void AddInfo(T entity)
        {
            try
            {
                HR_projectEntities2 entities2 = new HR_projectEntities2 ();
                if (entity is Employee)
                {
                    var emp = entity as Employee;
                    entities2.Employees.Add(emp);
                    entities2.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllValue()
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = entities1.Employees.ToList().Cast<T>();
                return rs;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public void Update(T entity, object id)
        {
            throw new NotImplementedException();
        }
        public T GetById(string name)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Employees
                          where c.username == name
                          select c).FirstOrDefault();
                return (T)(object)rs;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        public void UpdateImage(Employee emp, int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();

                // Tìm người dùng cần cập nhật thông tin trong cơ sở dữ liệu
                var EmpToUpdate = entities1.Employees.FirstOrDefault(u => u.id == id);

                if (EmpToUpdate != null)
                {
                    // Cập nhật thông tin người dùng
                    EmpToUpdate.image_Emp = emp.image_Emp;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    entities1.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
            }
        }
        public T GetValueByUsername(string username)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = (from c in entities1.Employees
                          where c.username == username
                          select c).FirstOrDefault();
                return (T)(object)rs;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


    }
}