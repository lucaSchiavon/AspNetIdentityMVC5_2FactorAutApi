using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace TFAWebApiAngularJS
{
    //https://bitoftech.net/2014/10/15/two-factor-authentication-asp-net-web-api-angularjs-google-authenticator/
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
