
using ProjectExample.Models.Entities;
using ProjectExample.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using ProjectExample.Models.Repository;
using System.Data.Entity;

namespace ProjectExample.Controllers
{
    public class AdminController : Controller
    {
        //Model View
        private readonly CadidateView cadidateView;
        private readonly VanacyView vanacyView;
        private readonly Cadidate_Vancies_View Views;
        private readonly Employee_Vanacies_View Views2;

        public AdminController()
        {
            cadidateView = new CadidateView();
            vanacyView = new VanacyView();
            Views = new Cadidate_Vancies_View();
            Views2 = new Employee_Vanacies_View();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PageAddCandidate()
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
        //[HttpGet]
        //public ActionResult PageAddVanacy()
        //{
        //    try
        //    {

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}
        [HttpGet]
        public ActionResult PageAddVancacy() {
            try
            {
                Vancacy vancacy = new Vancacy();
                if (vanacyView != null)
                {
                    ViewBag.vanacies = vanacyView.GetAllVanCy();
                    List<Employee> employees;
                    using (HR_projectEntities dbContext = new HR_projectEntities()) // Thay YourEntities bằng tên của DbContext trong dự án của bạn
                    {
                        employees = dbContext.Employees.ToList();
                    }

                    // Tạo SelectList từ danh sách nhân viên để sử dụng trong dropdown list
                    SelectList employeeList = new SelectList(employees, "id", "username");

                    // Gửi SelectList đến View
                    ViewBag.EmployeeList = employeeList;

                }
                return View(vancacy);
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Vancacy model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.quantity = 0;
                    vanacyView.InsertVacancy(model);
                    return RedirectToAction("ViewAllTable", "Admin");
                }
                else
                {
                    return View("PageAddVancacy", model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin");
            }
        }
        [HttpGet]
        public ActionResult ViewAllTable()
        {
            try
            {
                if (cadidateView != null)
                {
                    ViewBag.vanacys = Views2.GetAllValues();
                }
                return View();
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
                if (cadidateView != null && id_vanacy != null)
                {
                    ViewBag.cadidates = cadidateView.GetByIDCadidate(id_vanacy);
                }
                return View("ViewDetails");
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
        public ActionResult CreateCadidate(Cadidate model, HttpPostedFileBase ff)
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
                    cadidateView.InsertCadidate(model);
                }
            }
            return RedirectToAction("ViewAllTable", "Admin");
        }     

        [HttpGet]
        public ActionResult EditVanacy(int id_vanacy)
        {
            try
            {
                Vancacy vancacy = vanacyView.GetVacancyByID(id_vanacy);

                if (vancacy == null)
                {
                    return RedirectToAction("Error", "Admin");
                }

                return View(vancacy);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Vancacy model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vanacyView.UpdateVacancy(model);
                    return RedirectToAction("ViewAllTable", "Admin");
                }
                else
                {
                    return View("EditVanacy", model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult Error()
        {
            return View("ErrorView");
        }
    }
}