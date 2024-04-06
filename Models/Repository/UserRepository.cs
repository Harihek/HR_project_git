using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Repository
{
    public class UserRepository<T> : Inter_face<T>
    {
        public void AddInfo(T entity)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                if (entity is infoUser)
                {
                    var userdata = entity as infoUser;
                    entities1.infoUsers.Add(userdata);
                    entities1.SaveChanges();
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllValue()
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();
                var rs = entities1.infoUsers.ToList().Cast<T>();
                return rs;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            };
        }

        public void Update(T entity, object id)
        {
            throw new NotImplementedException();
        }
        public void UpdateImage(infoUser user, int id)
        {
            try
            {
                HR_projectEntities2 entities1 = new HR_projectEntities2();

                // Tìm người dùng cần cập nhật thông tin trong cơ sở dữ liệu
                var userToUpdate = entities1.infoUsers.FirstOrDefault(u => u.id == id);

                if (userToUpdate != null)
                {
                    // Cập nhật thông tin người dùng
                    userToUpdate.image_User = user.image_User;
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
                var rs = (from c in entities1.infoUsers
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