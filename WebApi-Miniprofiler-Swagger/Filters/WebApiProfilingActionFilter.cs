using System.Web.Http.Filters;
using Newtonsoft.Json;
using StackExchange.Profiling;

namespace WebApi_Miniprofiler_Swagger.Filters
{

    public class WebApiProfilingActionFilter : ActionFilterAttribute
    {
        public const string MiniProfilerResultsHeaderName = "X-MiniProfiler-Ids";

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var miniProfilerJson = JsonConvert.SerializeObject(new[] {MiniProfiler.Current.Id});
            filterContext.Response.Content.Headers.Add(MiniProfilerResultsHeaderName, miniProfilerJson);
        }

    }

}