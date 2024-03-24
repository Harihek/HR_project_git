using ProjectExample.Models.Entities;
using ProjectExample.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

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
        [HttpGet]
        public ActionResult PageAddVanacy()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return null;
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
        public ActionResult CreateCadidate(Cadidate model, HttpPostedFileBase ff) {
            string fileName = "";
            string randomCode = GenerateRandomCode(6);
            model.code_cadi = randomCode;
            model.status = 0;
            if (ff != null)
            {
                fileName = Path.GetFileName(ff.FileName);
                model.link_cv = fileName;
                string projectPath = Server.MapPath("~");
                string filePath = Path.Combine(projectPath, "Content","Admin","Cv");
                string resultPath = filePath +"\\"+ fileName;
                ff.SaveAs(resultPath);
                if (ff.ContentLength > 0)
                {
                    cadidateView.InsertCadidate(model);
                }
            }
            return RedirectToAction("ViewAllTable","Admin");
        }
    }
}