using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Global_ASAX
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // BeginRequest += MvcApplication_BeginRequest;
        }
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var uriObject = app.Context.Request.Url;
            var absolute_path = uriObject.AbsolutePath;
            if(HttpContext.Current.Session != null)
            {
                if (absolute_path != null)
                {
                    //check acc go to absolute_path
                }
            }
            else
            {
                //check enable|disable redirect tp action
                //HttpContext.Current.RewritePath("Home/About");
            }
        }
    }
}
