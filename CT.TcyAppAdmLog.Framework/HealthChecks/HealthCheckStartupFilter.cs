using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CT.TcyAppAdmLog.Framework.HealthChecks
{
    class HealthCheckStartupFilter: IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<HealthCheckMiddleware>();

                next(app);
            };
        }
    }
}
