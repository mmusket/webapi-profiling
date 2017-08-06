using System.Web.Http;
using WebActivatorEx;
using ExampleWebApi1;
using Swashbuckle.Application;
using ExampleWebApi1.Filter;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ExampleWebApi1
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ExampleWebApi1");
                    c.DocumentFilter<AddAuthenticationEndpointUri>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(thisAssembly, "ExampleWebApi1.App_Start.SwaggerUiCustomization.js");
                });
        }
    }
}
