using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
        private void Application_BeginRequest(object sender, EventArgs args)
        {
            var apo = (HttpApplication)sender;
            var uriObject = apo.Context.Request.Url;
            var absolute_path = uriObject.AbsolutePath;
            //var account = apo.Session["acc"];
            if(HttpContext.Current.Session != null)
            {
                var user = HttpContext.Current.Session["username"];
                var pass = HttpContext.Current.Session["password"];

                if (absolute_path != null)
                {
                    //check account gôt absolute paht
                }
            }
            else
            {
                //check enable | disable --> redict page define
                HttpContext.Current.RewritePath("Home");
            }
        }
    }
}
