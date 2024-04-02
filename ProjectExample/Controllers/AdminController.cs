using ProjectExample.Models.Entities;
using ProjectExample.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Xml.Linq;

namespace ProjectExample.Controllers
{
    public class AdminController : Controller
    {
        //Model View
        private readonly CadidateView cadidateView;
        private readonly VanacyView vanacyView;
        private readonly Cadidate_Vancies_View Views;
        private readonly Employee_Vanacies_View Views2;
        private readonly EmailSender emailSender1;
        private readonly EmployeeView employeeView;
        private readonly ScheduleView scheduleView;

        public AdminController()
        {
            cadidateView = new CadidateView();
            vanacyView = new VanacyView();
            Views = new Cadidate_Vancies_View();
            Views2 = new Employee_Vanacies_View();
            emailSender1 = new EmailSender();
            employeeView = new EmployeeView();
            scheduleView = new ScheduleView();
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
            {
                return View();
            }
            else if (HttpContext.Session["role1"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("PageNotFound", "Admin");
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PageAddCandidates()
        {
            //-->Add Cadidate with position job
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
                return View();
            }
        }
        [HttpGet]
        public ActionResult PageAddVanacy()
        {
            try
            { 
                if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
                {
                    Vancacy vancacy = new Vancacy();
                    return View(vancacy);
                }
                else if (HttpContext.Session["role1"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("PageNotFound", "Admin");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateVanacy(Vancacy model)
        {

            try
            {
                string Namevancacy = model.title_job_position;
                DateTime date = model.date_submitted;
                var derciption1 = Request.Form["myData"].ToString();
                if (Namevancacy != null && date != null && derciption1 != null)
                {
                    string account1 = HttpContext.Session["account"] as string;
                    Employee t =employeeView.GetValueIDView(account1);
                    model.id_emp = t.id;
                    model.status = 0;
                    //model.title_job_position = Namevancacy;
                    model.decription = derciption1;
                    //model.date_submitted = date;
                    model.quantity = 0;
                    vanacyView.InsertVancy(model);


                }
                return RedirectToAction("PageAddVanacy", "Admin");

            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult PageEditVancacy(int id_vanacy)
        {
            try
            {

                Vancacy vancacy = new Vancacy();
                int t = id_vanacy;
                HttpContext.Session["id_vancies"] = t;
                if (id_vanacy != null)
                {
                    Vancacy vancacy1 = vanacyView.GetIDVancyView(id_vanacy);
                    ViewBag.MyData = vancacy1.decription;
                }
                return View();
            }
            catch (Exception e)
            {
                return null;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitEditVancacy(Vancacy model)
        {
            try 
            {
                int id1 = (int)HttpContext.Session["id_vancies"];
                var derciption1 = Request.Form["myData"].ToString();

                if (id1 != null && derciption1 != null)
                {  
                    model.decription = derciption1;
                    vanacyView.UpdateVancacy(model, id1);
                }

                return RedirectToAction("ViewAllTable", "Admin");
            }
            catch (Exception e)
            {

            }                     
            return View();
        }
        [HttpGet]
        public ActionResult ViewAllTable()
        {
            try
            {
                if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
                {
                    Vancacy vancacy = new Vancacy();
                    string name = HttpContext.Session["account"] as string;
                    if (cadidateView != null)
                    {
                            ViewBag.vanacys = Views2.GetAllValues();
                        if (name != null)
                        {
                            ViewBag.inters = scheduleView.GetInterViewAccount(name);
                        }
                    }
                    return View(vancacy);
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
        [HttpGet]
        [Route("Admin/ViewDetails")]
        public ActionResult ViewDetails(int id_vanacy)
        {
            try
            {
                if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
                {
                    if (cadidateView != null && id_vanacy != null)
                    {
                        ViewBag.cadidates = cadidateView.GetByIDCadidate(id_vanacy);
                    }
                    return View("ViewDetails");
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
        //->Random Kí tự chữ và số
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
        //-----Insert
        [HttpPost]
        public ActionResult CreateCadidate(Cadidate model, HttpPostedFileBase ff) {
            if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
            {
                try
                {
                    string fileName = "";
                    string randomCode = GenerateRandomCode(6);
                    model.code_cadi = randomCode;
                    model.status = 0;
                    if (ff != null)
                    {
                        fileName = Path.GetFileName(ff.FileName);
                        model.link_cv = fileName;
                        string projectPath = Server.MapPath("~");
                        string filePath = Path.Combine(projectPath, "Content", "Admin", "Cv");
                        string resultPath = filePath + "\\" + fileName;
                        ff.SaveAs(resultPath);
                        if (ff.ContentLength > 0)
                        {
                            //fileName --> cv
                            string from = "dvo31666@gmail.com";
                            //email for Admin
                            string to = "1318thang@gmail.com";
                            string subject = "Hello Admin";
                            emailSender1.SendEmail(from,to,subject,resultPath);
                            cadidateView.InsertCadidate(model);
                        }
                    }
                    return RedirectToAction("ViewAllTable", "Admin");
                }
                catch (Exception ex)
                {
                    ex.Equals("Error Data");
                }
            }
            else if (HttpContext.Session["role1"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("PageNotFound", "Admin");
        }
        public ActionResult UpdateStatus(Vancacy vancacy)
        {
            try
            {
                if (HttpContext.Session["role1"] != null && HttpContext.Session["role1"].Equals("USER_HR"))
                {
                    string id = Request.Params["id_vanca"];
                    string stas = Request.Params["sta_van"];
                    if (id != null &&  vancacy.status != null)
                    {
                        int id_parse = int.Parse(id);
                        int sta_parse = int.Parse(stas);
                        vancacy.status = sta_parse;
                        vanacyView.UpdateStatus(vancacy,id_parse);
                    }
                    return RedirectToAction("ViewAllTable", "Admin");
                }
                else if (HttpContext.Session["role1"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                ex.Equals("Error Data");
            }
            return RedirectToAction("PageNotFound", "Admin");
        }
        public ActionResult PageInterviewSchedule()
        {
            try
            {
                if (employeeView != null && cadidateView != null)
                {
                    ViewBag.vanca_emp = employeeView.GetAllVanCy();
                    ViewBag.cadi = cadidateView.GetAllCandidate();
                }
                return View();
            }
            catch(Exception e)
            {

            }
            return View();
        }
        public ActionResult CreateInteviewSchedule()
        {
            try
            {
                string code = Request.Params["code_cadi"];
                int? id_e1 = null;
                Employee employee = null;
                if (!string.IsNullOrEmpty(Request.Params["id_emp"]))
                {
                    id_e1 = int.Parse(Request.Params["id_emp"]);
                    employee = employeeView.GetIDEmp(id_e1);
                }
                int? id_e2 = null;
                Employee employee1 = null;
                if (!string.IsNullOrEmpty(Request.Params["id_emp1"]))
                {
                    id_e2 = int.Parse(Request.Params["id_emp1"]);
                    employee1 = employeeView.GetIDEmp(id_e2);
                }

                int? id_e3 = null;
                Employee employee3 = null;
                if (!string.IsNullOrEmpty(Request.Params["id_emp2"]))
                {
                    id_e3 = int.Parse(Request.Params["id_emp2"]);
                    employee3 = employeeView.GetIDEmp(id_e3);
                }
                Cadidate cadidate = null;
                if (code != null)
                {
                    cadidate = cadidateView.GetByCodeCadidate(code);
                    int id_cadi1 = cadidate.id_candidate;
                    cadidateView.UpdateStatusCadi(cadidate, id_cadi1);
                }
                //Kiểm tra tất cả giá trị của 3 id, có thể có một biến nhận giá trị 2 hoặc 1 biến còn lại có thể null
                int[] Enable = new int?[] { id_e1, id_e2, id_e3 }
                              .Where(x => x.HasValue)
                              .Select(x => x.Value)
                              .ToArray();              
                DateTime dateTimeStart = DateTime.Parse(Request.Params["date_time_start"]);
                DateTime dateTimeEnd = DateTime.Parse(Request.Params["date_time_end"]);
                
                if (code != null && dateTimeStart != null && dateTimeEnd != null)
                {
                    if (Enable != null)
                    {
                        string from = "dvo31666@gmail.com";
                        string subject = "Tôi gửi bạn tài khoản đăng nhập nhân viên công ty ABC";

                        string email1 = employee?.email;
                        string email2 = employee1?.email;
                        string email3 = employee3?.email;
                      

                        List<string> to= new List<string>();

                        if (employee?.email != null)
                        {
                            to.Add(employee.email);
                        }

                        if (employee1?.email != null)
                        {
                            to.Add(employee1.email);
                        }

                        if (employee3?.email != null)
                        {
                            to.Add(employee3.email);
                        }

                        string emailuser = cadidate.email ?? string.Empty;

                        to.Add(emailuser);
                        string body = "<html><head><title>Email Content</title></head><body>"
                                    + "<h1>Chúng tôi gửi thông báo phỏng vấn cho bạn</h1> </hr>"
                                    +" <h1>Địa điểm: công ty ABC - tầng 3(phòng c305)</h1></hr>"
                                    + " <h1>Thời gian:"+ dateTimeStart +"</h1></hr>"
                                    + " <h1>Thời gian dự kiến kết thúc:" + dateTimeEnd + "</h1></hr>"
                                    + "</body></html>";
                        foreach(string recipient in to)
                        {
                            emailSender1.SendEmailSales(from,recipient,subject,body);
                        }
                        int status = 0;
                        scheduleView.InsertSchedule(id_e1, id_e2, id_e3, code, dateTimeStart, dateTimeEnd,status);
                        return RedirectToAction("PageInterviewSchedule", "Admin");
                    }
                }
                return RedirectToAction("PageInterviewSchedule", "Admin");
            }
            catch (Exception e)
            {
                return View();
            }

        
        }
        public ActionResult ViewResultDetail(int id_cadi, int id_emp)
        {
            try
            {
                if (id_cadi != null && id_emp != null)
                {
                    ViewBag.details = scheduleView.GetResult(id_cadi,id_emp);
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateResult(int id_cadiEmp, int status)
        {
            try
            {
                if (id_cadiEmp != null && status != null)
                {
                    scheduleView.UpdateResultsView(id_cadiEmp,status);
                }
                return RedirectToAction("ViewResultDetail", "Admin");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("ViewResultDetail", "Admin");
        }

        [HttpGet]
        public ActionResult ViewDetailEmp(string name)
        {
            try
            {
                if (name != null)
                {
                    ViewBag.infoemp = employeeView.GetValueIDView(name);
                   
                    return View();
                }
                return RedirectToAction("ViewAllTable","Admin");

            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpGet]
        public ActionResult PageRegisterUser_HR()
        {
            try
            {
                Employee employee = new Employee();
                return View(employee);
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult CreataUser_HR(Employee model, HttpPostedFileBase image)
        {
            try
            {
                string emailUser = Request.Params["email"];
                if (model != null && image != null)
                {
                    var pathUpload = Server.MapPath("~/Content/Admin/Image");
                    var filename = DateTime.Now.Ticks + image.FileName;
                    var newPath = Path.Combine(pathUpload, filename);
                    //pathUpload += "\\" + filename;
                    image.SaveAs(newPath);
                    var fileInFolder = Directory.GetFiles(pathUpload);
                    var isImageMovedSucessfully = Array.Exists(fileInFolder, file => file.Equals(newPath));
                    if (isImageMovedSucessfully)
                    {
                        model.image_Emp = image.FileName.ToString();
                        model.role = "USER_HR";
                        model.Hire_date = DateTime.Now;
                        model.email = emailUser;
                        employeeView.InsetEmp(model);
                        HttpContext.Session["info"] = "tạo thành công";
                        string from = "dvo31666@gmail.com";
                        string subject = "Tôi gửi bạn tài khoản đăng nhập nhân viên công ty ABC";
                        //Creat body
                        string username =model.username;
                        string password = model.password;
                        string postions = model.job_position;
                        string body = "<html><head><title>Email Content</title></head><body>"
                                    + "<h1>Tên đăng nhập: " + username + "</h1> </hr>"
                                    + "<h1>Mật khẩu: " + password + "</h1> </hr>"
                                    + "<h1>Vị trí công việc: " + postions + "</h1> </hr>"
                                    + "</body></html>";
                        emailSender1.SendEmailSales(from,emailUser,subject,body);
                    }
                }
                else
                {
                    HttpContext.Session["info"] = "tạo thất bại";
                }
                return RedirectToAction("PageRegisterUser_HR", "Admin");
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public ActionResult EditStaCadi(int status, int id)
        {
            try
            {
                int t = status;
                int t1 = id;
                if (t != null)
                {
                  
                        Cadidate cadidate = null;
                        if (id != null)
                        {
                            cadidate = cadidateView.GetByIDCadidatestatus(id);
                        }
                        DateTime datetime = DateTime.Now;
                        string to = cadidate.email;
                        string from = "dvo31666@gmail.com";
                    string subject = "Chúng tôi gửi thông báo kết quả phỏng vấn tới bạn";
                        if (status == 2)
                        {
                           
                            string body = "<h1>Chào mừng bạn đã được tuyển dụng vào công ty ABC của chúng tôi</h1></hr>" +
                                            "<h7>Thởi gian làm việc sẽ bắt đầu vào ngày mai</h7></hr>" +
                                            ""+datetime+"";
                            emailSender1.SendEmailSales(from, to, subject, body);
                            return RedirectToAction("ViewAllTable","Admin");
                        }
                        else
                        {
                            string body = "<h1>Chúng tôi rất tiêt bạn không đủ điều kiện vào tuyển dụng, càm ơn bạn đã tham buổi phỏng vấn này</h1></hr>" +
                                        ""+datetime+"";
                            emailSender1.SendEmailSales(from,to, subject, body);
                            return RedirectToAction("ViewAllTable", "Admin");
                        }

                    
                }
                return View();
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}