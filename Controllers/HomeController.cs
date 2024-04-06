using ProjectExample.Models.Entities;
using ProjectExample.Models.ModelView;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using ProjectExample.Helpers;

namespace ProjectExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly Employee_Vanacies_View Views2;
        private readonly UserView userView;
        public readonly CadidateView cadidateView;
        private readonly VanacyView vanacyView;
        private readonly EmployeeView employeeView;
        public HomeController()
        {
            employeeView = new EmployeeView();
            userView = new UserView();
            Views2 = new Employee_Vanacies_View();
            vanacyView = new VanacyView();
            cadidateView = new CadidateView();
        }
        public ActionResult Index()
        {
            string username = HttpContext.Session["account"] as string;
            string imagePath = GetImagePathFromDatabase(username);
            ViewBag.ImagePath = imagePath;
            return View();
        }
        public string GetImagePathFromDatabase(string username)
        {
            try
            {
                infoUser info = userView.GetValueID(username);
                if (info != null && !string.IsNullOrEmpty(info.image_User))
                {
                    return base.Url.Content("~/Content/Home/Image/" + info.image_User);
                }
            }
            catch (Exception)
            {
            }
            return base.Url.Content("~/Content/Admin/assets/img/default-avatar.png");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        //[HttpPost]
        //public ActionResult SubmitLogin(infoUser user, Employee emp)
        //{
        //    try
        //    {
        //        if (user != null && !string.IsNullOrEmpty(user.username) && !string.IsNullOrEmpty(user.pass_word))
        //        {
        //            // Kiểm tra thông tin người dùng trong bảng User
        //            var userFromDB = userView.GetValueID(user.username);

        //            if (userFromDB != null)
        //            {
        //                // Kiểm tra mật khẩu nhập vào
        //                if (EncryptionHelper.VerifyPassword(user.pass_word, userFromDB.pass_word))
        //                {
        //                    // Đăng nhập thành công, lưu thông tin người dùng vào session
        //                    Session["account"] = user.username;

        //                    // Kiểm tra role và redirect tới trang tương ứng
        //                    if (userFromDB.role == "USER")
        //                    {
        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    else if (userFromDB.role == "USER_HR")
        //                    {
        //                        return RedirectToAction("Index", "Admin");
        //                    }
        //                }
        //                else
        //                {
        //                    // Đăng nhập thất bại do sai mật khẩu
        //                    ViewBag.Error = "Invalid password";
        //                    return View("Login");
        //                }
        //            }
        //            else if (emp != null && !string.IsNullOrEmpty(emp.username) && !string.IsNullOrEmpty(emp.password))
        //            {
        //                // Kiểm tra thông tin người dùng trong bảng Employee
        //                var empFromDB = employeeView.GetValueIDView(emp.username);

        //                if (empFromDB != null)
        //                {
        //                    // Kiểm tra mật khẩu nhập vào
        //                    if (EncryptionHelper.VerifyPassword(emp.password, empFromDB.password))
        //                    {
        //                        // Đăng nhập thành công, lưu thông tin người dùng vào session
        //                        Session["account"] = emp.username;

        //                        // Kiểm tra role và redirect tới trang tương ứng
        //                        if (empFromDB.role == "USER")
        //                        {
        //                            return RedirectToAction("Index", "Home");
        //                        }
        //                        else if (empFromDB.role == "USER_HR")
        //                        {
        //                            return RedirectToAction("Index", "Admin");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Đăng nhập thất bại do sai mật khẩu
        //                        ViewBag.Error = "Invalid password";
        //                        return View("Login");
        //                    }
        //                }
        //                else
        //                {
        //                    // Không tìm thấy người dùng trong cơ sở dữ liệu
        //                    ViewBag.Error = "User not found";
        //                    return View("Login");
        //                }
        //            }
        //            else
        //            {
        //                // Thông tin đăng nhập không hợp lệ
        //                ViewBag.Error = "Invalid username or password";
        //                return View("Login");
        //            }
        //        }
        //        else
        //        {
        //            // Đối tượng user là null hoặc thiếu thông tin cần thiết
        //            ViewBag.Error = "Invalid user object or missing credentials";
        //            return View("Login");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ
        //        ViewBag.Error = "An error occurred while processing your request";
        //        return View("Login");
        //    }
        //    return null;
        //}


        public ActionResult Logout()
        {
            HttpContext.Session.Remove("account");
            HttpContext.Session.Remove("role1");
            return RedirectToAction("Login", "Home");
        }
        public ActionResult PartialViewT()
        {
            return View();
        }
        public ActionResult Register()
        {
            try
            {
                infoUser infoUser = new infoUser();
                return View(infoUser);
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult SubmitRegister(infoUser model, HttpPostedFileBase image)
        {
            try
            {
                if (model != null)
                {
                    string confirmPass = Request.Params["confirm_password"];
                    string pass = Request.Params["pass"];
                    if (image !=null && pass.Equals(confirmPass))
                    {
                        string avatarFileName = DateTime.Now.Ticks + image.FileName;
                        string avatarPath = base.Server.MapPath("~/Content/Home/Image");
                        string newPath = Path.Combine(avatarPath, avatarFileName);
                        image.SaveAs(newPath);
                        string[] fileInFolder = Directory.GetFiles(avatarPath);
                        if (Array.Exists(fileInFolder, (string file) => file.Equals(newPath)))
                        {

                            base.HttpContext.Session["info"] = "Create Account Successfully";
                            model.image_User = avatarFileName;
                            model.role = "USER";
                            model.pass_word = EncryptionHelper.EncryptPassword(pass);
                            userView.InsertUser(model);

                        }
                        else
                        {
                            base.HttpContext.Session["info"] = "Create Account Failed";

                        }
                        return RedirectToAction("Login", "Home");
                    }
                    else if (image == null && pass.Equals(confirmPass))
                     {
                        base.HttpContext.Session["info"] = "Create Account Successfully";
                        model.role = "USER";
                        model.pass_word = EncryptionHelper.EncryptPassword(pass);
                        userView.InsertUser(model);
                    }
                    else
                        {
                            base.HttpContext.Session["info"] = "Create Account Failed";

                        }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
            }
            return RedirectToAction("Register", "Home");
        }

        [HttpGet]
        public ActionResult PageVancacyJob()
        {
            try
            {
                if (HttpContext.Session["role1"] != null)
                {

                    if (Views2 != null)
                    {
                        ViewBag.Van = Views2.GetAllValues();
                    }
                    return View();
                }
                else if (HttpContext.Session["role1"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("PageNotFound", "Admin");

            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult SendCv()
        {
            try
            {
                Cadidate cadidate = new Cadidate();
                if (vanacyView != null)
                {
                    ViewBag.vanacies = vanacyView.GetAllVanCy();
                }

                return View(cadidate);

            }
            catch (Exception e)
            {
                return RedirectToAction("PageNotFound", "Admin");

            }
        }
        string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            List<char> usedChars = new List<char>();

            char[] randomCodeChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                char randomChar;
                do
                {
                    randomChar = chars[random.Next(chars.Length)];
                } while (usedChars.Contains(randomChar));

                randomCodeChars[i] = randomChar;
                usedChars.Add(randomChar);
            }
            return new string(randomCodeChars);
        }
        [HttpPost]
        public ActionResult SubmitCv(Cadidate model, HttpPostedFileBase link)
        {
            try
            {
                string n = Request.Params["name"];//tên cadiddate
                string n1 = Path.GetFileName(link.FileName);//link cv -> file pdf
                string n2 = Request.Params["id_vanacy"];//id vanacy
                string n3 = Request.Params["email"];
                if (n != null && n1 != null && n2 != null)
                {
                    string projectPath = Server.MapPath("~");
                    string filePath = Path.Combine(projectPath, "Content", "Admin", "Cv");
                    string resultPath = filePath + "\\" + n1;
                    link.SaveAs(resultPath);
                    if (link.ContentLength > 0)
                    {
                        //Lấy ngày giở hiện tại hệ thống máy tính
                        DateTime currentDatetime = DateTime.Now;
                        string info = "Gửi CV Thành Công";
                        HttpContext.Session["info"] = info;
                        model.Name_cadidate = n;//name cadidate
                        model.link_cv = n1;//link cv
                        model.id_vanacy = int.Parse(n2);//id vancacy
                        model.date_time = currentDatetime;//date time
                        model.code_cadi = GenerateRandomCode(6);
                        model.email = n3;
                        model.status = 0;
                        cadidateView.InsertCadidate(model);
                    }
                }
                else
                {
                    string info = "Gửi CV Thất bại (bạn cần điền đầy đủ thông tin)";
                    HttpContext.Session["info1"] = info;

                }

                return RedirectToAction("SendCv", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("PageNotFound", "Admin");
            }
        }
        [HttpGet]
        public ActionResult UploadImage()
        {
            try
            {
                string username = HttpContext.Session["account"] as string;
                if (!string.IsNullOrEmpty(username))
                {
                    // Lấy thông tin người dùng từ cơ sở dữ liệu
                    infoUser userInfo = userView.GetValueID(username);
                    if (userInfo != null)
                    {
                        ViewBag.UserID = userInfo.username; // Truyền tên người dùng vào ViewBag để sử dụng trong view
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
            }
            return View();
        }

        [HttpPost]
        public ActionResult SubmitImage(HttpPostedFileBase image)
        {
            try
            {
                string username = HttpContext.Session["account"] as string;
                if (image != null && !string.IsNullOrEmpty(username))
                {
                    string avatarFileName = DateTime.Now.Ticks + image.FileName;
                    string avatarPath = Server.MapPath("~/Content/Home/Image");
                    string newPath = Path.Combine(avatarPath, avatarFileName);
                    image.SaveAs(newPath);

                    // Cập nhật đường dẫn ảnh mới vào cơ sở dữ liệu
                    infoUser user = userView.GetValueID(username);
                    if (user != null)
                    {
                        user.image_User = avatarFileName;
                        userView.UpdateUser(user);
                    }

                    // Redirect về trang Index sau khi upload ảnh thành công
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
            }
            // Nếu có lỗi, redirect về trang Index
            return RedirectToAction("Index");
        }

    }
}