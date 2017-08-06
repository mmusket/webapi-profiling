using System.Web.Http;
using WebActivatorEx;
using WebApi_Miniprofiler_Swagger;
using Swashbuckle.Application;
using WebApi_Miniprofiler_Swagger.Filters;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi_Miniprofiler_Swagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WebApi_Miniprofiler_Swagger");
                    c.DocumentFilter<InjectMiniprofiler>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(thisAssembly, "WebApi_Miniprofiler_Swagger.App_Start.SwaggerUiCustomization.js");
                });
        }
    }
}
