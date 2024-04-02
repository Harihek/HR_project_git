using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class UserView
    {
        private readonly UserRepository<infoUser> repository;

        public UserView()
        {
            repository = new UserRepository<infoUser>();
        }
        public IEnumerable<infoUser> GetAllVanCy()
        {
            return repository.GetAllValue();
        }
        public void InsertUser(infoUser user)
        {
            try
            {
                if (user != null)
                {
                    repository.AddInfo(user);
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
        public infoUser GetValueID(string username)
        {
            // Thực hiện truy vấn để lấy thông tin Employee từ username
            // Đây là nơi bạn sẽ truy vấn cơ sở dữ liệu để lấy thông tin Employee
            // Sau đó, trả về đối tượng Employee chứa thông tin tương ứng
            // Dưới đây là một đoạn mã giả để minh họa

            // Kết nối cơ sở dữ liệu
            using (var dbContext = new HR_projectEntities2())
            {
                // Tìm Employee có username trùng khớp
                var info = dbContext.infoUsers.FirstOrDefault(e => e.username == username);

                return info;
            }
        }
    }
}