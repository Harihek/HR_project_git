using ProjectExample.Models.Entities;
using ProjectExample.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ProjectExample.Helpers;

namespace ProjectExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(Object sender, EventArgs e)
        {

            Session["username"] = "";

        }
        private void Application_PostAcquireRequestState(object sender, EventArgs args)
        {
                var apo = (HttpApplication)sender;
                var uriObject = apo.Context.Request.Url;
                var absolute_path = uriObject.AbsolutePath;
                string user123 ="";
                string pass123 = "";

                if (Request.Params["user"] != null)
                {
                    user123 = Request.Params["user"];
                    pass123 = Request.Params["pass"];
                    HttpContext.Current.Session["username1"] = user123;
                }   
                if (user123 != "" && pass123 != "")
            {
                if (absolute_path != null)
                {

                    EmployeeView employeeView = new EmployeeView();
                    IEnumerable<Employee> employees = employeeView.GetAllVanCy();
                    foreach (var employe in employees)
                    {
                        //var hash = EncryptionHelper.EncryptPassword(pass123);
                        if (employe.username.Equals(user123) && EncryptionHelper.VerifyPassword(pass123, employe.password))
                        {
                            string role = employe.role;
                            string us = employe.username;
                            string position = employe.job_position;
                            HttpContext.Current.Session["role1"] = role;
                            HttpContext.Current.Session["account"] = us;
                            HttpContext.Current.Session["position"] = position;

                            // nếu role là USERHR VÀ ADMIN
                            if (role != null && role == "USER_HR")
                            {
                                HttpContext.Current.Response.Redirect("/Admin/Index");
                            }
                        }
                    }
                    //iNFO USER
                    UserView userview1 = new UserView();
                    IEnumerable<infoUser> user1 = userview1.GetAllVanCy();
                    foreach (var userdb in user1)
                    {
                        if (userdb.username.Equals(user123) && EncryptionHelper.VerifyPassword(pass123, userdb.pass_word))
                        {
                            string role = userdb.role;
                            string us = userdb.username;
                           
                            HttpContext.Current.Session["role1"] = role;
                            HttpContext.Current.Session["account"] = us;
                           
                            //Nếu role là User
                            if (role != null && role == "USER")
                            {
                                HttpContext.Current.Response.Redirect("/Home/Index");
                            }
                        }
                    }
                    AdminView adminview1 = new AdminView();
                    IEnumerable<Admin> admins = adminview1.GetAllVanCy(); 
                    foreach (Admin admin in admins) {
                        if (admin.username.Equals(user123) && admin.password.Equals(pass123))
                        {
                            string role = admin.role;
                            string ad = admin.username;
                           
                            HttpContext.Current.Session["role1"] = role;
                            HttpContext.Current.Session["account"] = ad;
                                                        if (role != null && role =="ADMIN")
                            {
                                HttpContext.Current.Response.Redirect("/Admin/Index");
                            }

                        }
                        
                    }


                }
            }

            if (absolute_path == "/")
                {
                    HttpContext.Current.Response.Redirect("/Home/Index");
                }

                if (absolute_path == "/Home/Login")
                {
                    HttpContext.Current.RewritePath("/Home/Login");
                }
           
        }
    }
}
