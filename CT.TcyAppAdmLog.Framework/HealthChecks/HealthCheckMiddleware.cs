using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CT.TcyAppAdmLog.Framework.HealthChecks
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate next;

        private static readonly List<string> healthCheckPaths = new List<string>()
        {
            "/cthealthcheck/basicmonitor",
            "/api/cthealthcheck/basicmonitor",
        };

        public HealthCheckMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsHealthCheckRequest(context))
            {
                context.Response.StatusCode = 200;

                context.Response.Headers.Add("content-type", "application/json");
                await context.Response.WriteAsync("200-ok");
                return;
            }

            await next.Invoke(context);
        }

        private bool IsHealthCheckRequest(HttpContext context)
        {
            if (healthCheckPaths.Contains(context.Request.Path.ToString().ToLower()))
            {
                return true;
            }

            return false;
        }
    }

}
