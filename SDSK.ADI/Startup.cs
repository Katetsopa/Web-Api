using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SDSK.API.Startup))]
namespace SDSK.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);

            Swashbuckle.Bootstrapper.Init(config);

            app.UseWebApi(config);

        }
    }

}