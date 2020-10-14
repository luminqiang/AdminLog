using CT.TcyAppAdmLog.Application;
using CT.TcyAppAdmLog.Cache;
using CT.TcyAppAdmLog.Domain;
using CT.TcyAppAdmLog.Framework;
using CT.TcyAppAdmLog.Repository;
using CT.TcyAppAdmLog.Service;
using CT.TcyAppAdmLog.WebApi.Filter;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace CT.TcyAppAdmLog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBusinessOfApplications()
                    .AddBusinessOfServices()
                    .AddBusinessOfRepositorys(Configuration);

            services.AddMediatR(typeof(Startup));
            services.AddDomain();

            IMvcBuilder mvcBuilder = services.AddControllers(option =>
            {
                option.Filters.Add<ExceptionFilter>();
                option.Filters.Add<ValidateModelFilter>();
            });
            mvcBuilder.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); //禁用模型验证失败，自动返回400错误
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TcyAppAdmLogApi", Version = "v1" });
            });           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCtWebFramework();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TcyAppAdmLogApi");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}