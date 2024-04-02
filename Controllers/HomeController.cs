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

namespace ProjectExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly Employee_Vanacies_View Views2;
        private readonly UserView userView;
        public readonly CadidateView cadidateView;
        private readonly VanacyView vanacyView;
        public HomeController()
        {
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
                if (model != null && image != null)
                {
                    string confirmPass = Request.Params["confirm_password"];
                    string pass = Request.Params["pass"];

                    if (pass.Equals(confirmPass))
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
                            model.pass_word = pass;
                            userView.InsertUser(model);

                        }
                        else
                        {
                            base.HttpContext.Session["info"] = "Create Account Failed";

                        }
                        return RedirectToAction("Login", "Home");
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
    }
}