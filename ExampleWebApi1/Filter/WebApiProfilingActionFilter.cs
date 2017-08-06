using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using StackExchange.Profiling;

namespace ExampleWebApi1.Filter
{
    public class WebApiProfilingActionFilter : ActionFilterAttribute
    {
        public const string MiniProfilerResultsHeaderName = "X-MiniProfiler-Ids";
        private const string StackKey = "ProfilingActionFilterStack";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var mp = MiniProfiler.Current;
            if (mp != null)
            {
                var stack = HttpContext.Current.Items[StackKey] as Stack<IDisposable>;
                if (stack == null)
                {
                    stack = new Stack<IDisposable>();
                    HttpContext.Current.Items[StackKey] = stack;
                }

                var routeData = filterContext.Request.GetRouteData();

                var tokens = routeData.Route.DataTokens;
                var area = tokens.ContainsKey("area") && !string.IsNullOrWhiteSpace((string) tokens["area"])
                    ? tokens["area"] + "."
                    : "";
                var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var action = filterContext.ActionDescriptor.ActionName;

                stack.Push(mp.Step($"Controller: {area} {controller} {action}"));
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var stack = HttpContext.Current.Items[StackKey] as Stack<IDisposable>;
            if (stack != null && stack.Count > 0)
            {
                stack.Pop().Dispose();
            }

            var miniProfilerJson = JsonConvert.SerializeObject(new[] {MiniProfiler.Current.Id});
            filterContext.Response.Content.Headers.Add(MiniProfilerResultsHeaderName, miniProfilerJson);
        }

    }
}