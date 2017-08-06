using System.Web.Http.Description;
using StackExchange.Profiling;
using Swashbuckle.Swagger;

namespace WebApi_Miniprofiler_Swagger.Filters
{
    public class InjectMiniprofiler : IDocumentFilter
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