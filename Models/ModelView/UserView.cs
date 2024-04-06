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
        public void UpdateUser(infoUser user)
        {
            try
            {
                if (user != null)
                {
                    repository.UpdateImage(user, user.id);
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

    }
}