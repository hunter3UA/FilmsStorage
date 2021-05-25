using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

using FilmsStorage.Login;
using FilmsStorage.SL;

namespace FilmsStorage
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

        protected void Application_PostAuthenticateRequest(Object sender,EventArgs e)
        {
            // отримання кукі
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                
                UserSerializationModel loginCookieModel = serializer.Deserialize<UserSerializationModel>(authenticationTicket.UserData);
                
                CustomPrincipal loggedInUser = new CustomPrincipal(authenticationTicket.Name);
                loggedInUser.UserID = loginCookieModel.UserID;

                HttpContext.Current.User = loggedInUser;
                // продовжуємо життя кукі при кожному надійденому запиту
                _SL.Users.ExtendCookieLife(authenticationTicket);
            }
            
        }
    }
}
