using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi_Miniprofiler_Swagger.Filters;

namespace WebApi_Miniprofiler_Swagger
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Filters.Add(new WebApiProfilingActionFilter());
        }
    }
}
