using System.Web.Http.Description;
using StackExchange.Profiling;
using Swashbuckle.Swagger;

namespace ExampleWebApi1.Filter
{
    public class AddAuthenticationEndpointUri : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.info.contact = new Contact()
            {
                name = MiniProfiler.RenderIncludes().ToHtmlString()
            };
        }
    }
}