using Com.Ctrip.Framework.Apollo;
using CT.TcyAppAdmLog.Framework.DbLoadBalance;
using CT.TcyAppAdmLog.Framework.HealthChecks;
using CT.TcyAppAdmLog.Framework.Mapper;
using Log.TcySys.SDKEX;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace CT.TcyAppAdmLog.Framework
{
    public static class FrameworkExtensions
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureCtAppConfiguration(this IHostBuilder hostBuilder, Action<HostBuilderContext, IConfigurationBuilder> configureDelegate = null)
        {
            hostBuilder.ConfigureAppConfiguration((hostBuilderContent, configBuilder) =>
            {
                configBuilder.SetBasePath(AppContext.BaseDirectory);

                var rootConfig = configBuilder.Build();

                configBuilder.AddCtApolloWithZkConfig(rootConfig)
                             .AddCtDotNetConfig()
                             .AddNamespace("logsdkconfig.json", null);
               // configBuilder.AddDbLoadBalance("Config/Complex/DbLoadBalance.config", true, true); //数据库分库分表
                configureDelegate?.Invoke(hostBuilderContent, configBuilder);
            });

            return hostBuilder;
        }

        /// <summary>
        /// 注册框架工具
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureCtAppServices(this IHostBuilder hostBuilder, Action<HostBuilderContext, IServiceCollection> configureDelegate = null)
        {
            hostBuilder.ConfigureServices((hostBuilderContent, services) =>
            {
                services.AddAutoMapperAdapter();               
                //services.AddSingleton<IRedisManager, StackExchangeRedisManager>();

                services.AddLogging(loggerBuilder =>
                {
                    loggerBuilder.ClearProviders();
                    loggerBuilder.AddFilter("Microsoft", LogLevel.Warning)
                                 .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
                                 .AddFilter("System", LogLevel.Warning)
                                 .AddConsole()
                                 .SetMinimumLevel(LogLevel.Debug);
                });

                configureDelegate?.Invoke(hostBuilderContent, services);
            });

            return hostBuilder;
        }

        /// <summary>
        /// 注册框架工具
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureCtWebAppServices(this IHostBuilder hostBuilder, Action<HostBuilderContext, IServiceCollection> configureDelegate = null)
        {
            hostBuilder.ConfigureServices((hostBuilderContent, services) =>
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<IStartupFilter>(new HealthCheckStartupFilter());
            });

            ConfigureCtAppServices(hostBuilder, configureDelegate);
            return hostBuilder;
        }

        /// <summary>
        /// 使用框架功能
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static IServiceProvider UseCtFramework(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IMapProvider>().UseMapProvider();
            var config = serviceProvider.GetService<IConfiguration>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            if (config["LogSDKConfig:Log_TcySys_Host"] != null)
            {
                loggerFactory.AddTcyLog(config, serviceProvider.GetService<IHttpContextAccessor>());
            }

            ServiceProvider = serviceProvider;

            return serviceProvider;
        }

        /// <summary>
        /// 使用框架功能
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseCtWebFramework(this IApplicationBuilder app)
        {
            UseCtFramework(app.ApplicationServices);
            var config = app.ApplicationServices.GetService<IConfiguration>();
            app.UseTcyLogger(config);

            return app;
        }
    }
}