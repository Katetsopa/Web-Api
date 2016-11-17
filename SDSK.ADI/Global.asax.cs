﻿using System.Web;
using System.Web.Http;
using SDSK.API.Filters;

namespace SDSK.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
