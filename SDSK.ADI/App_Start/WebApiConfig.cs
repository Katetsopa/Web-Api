using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using SDSK.API.Constraints;

namespace SDSK.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("jiraid", typeof(JiraIdConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);

            log4net.Config.XmlConfigurator.Configure();

            config.Routes.MapHttpRoute(
            name: "JiraIdConstraintBasedApi",
            routeTemplate: "api/jiraitems/{id:jiraid}",
            defaults: new { controller = "jiraitems" },
            constraints: new { id = new JiraIdConstraint() });

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
