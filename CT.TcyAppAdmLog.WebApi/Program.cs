using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CT.TcyAppAdmLog.Framework;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Com.Ctrip.Framework.Apollo;
using CT.TcyAppAdmLog.ApplicationContract;

namespace CT.TcyAppAdmLog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.RegisterApplicationLifetimeEvents();
            host.Run();
        }

        public static IHost BuildWebHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureCtAppConfiguration()
                       .ConfigureCtWebAppServices()
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                       .Build();
        }       
    }

    internal static class RegisterLifetimeEvents
    {
        private static readonly string appCode = ConfigReader.GetAppSetting("AppCode");
        private static readonly string appName = ConfigReader.GetAppSetting("AppName");
        private static readonly string iPV4Address;
        private static readonly string iPV6Address;

        static RegisterLifetimeEvents()
        {
            var interNetworkV6 = AddressFamily.InterNetworkV6;
            var interNetwork = AddressFamily.InterNetwork;
            var ipList = NetworkInterface.GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .Where(p => (p.Address.AddressFamily == interNetwork || p.Address.AddressFamily == interNetworkV6) && !System.Net.IPAddress.IsLoopback(p.Address)).ToList();

            iPV4Address = ipList[1]?.Address.ToString();
            iPV6Address = ipList[0]?.Address.ToString();
        }

        /// <summary>
        /// 注册应用程序生命周期事件
        /// </summary>
        public static void RegisterApplicationLifetimeEvents(this IHost host)
        {
            var hostApplicationLifetime = host.Services.GetService<IHostApplicationLifetime>();
            var appConfigApplication = host.Services.CreateScope().ServiceProvider.GetService<IAppConfigApplication>();
            var logger = host.Services.GetService<ILoggerFactory>().CreateLogger("Program");

            hostApplicationLifetime.ApplicationStarted.Register(() => OnStarted(logger, appConfigApplication));
            hostApplicationLifetime.ApplicationStopping.Register(() => OnStopping(logger));
            hostApplicationLifetime.ApplicationStopped.Register(() => OnStopped(logger));
        }

        private static void OnStarted(ILogger logger, IAppConfigApplication appConfigApplication)
        {
            //加载缓存
            appConfigApplication.AddAppCacheAsync().GetAwaiter().GetResult();
            logger.LogInformation($"OnStarted has been called：{appCode} {appName} {DateTime.Now} {iPV4Address} {iPV6Address}");
        }

        private static void OnStopping(ILogger logger)
        {
            logger.LogInformation($"OnStopping has been called：{appCode} {appName} {DateTime.Now} {iPV4Address} {iPV6Address}");
        }

        private static void OnStopped(ILogger logger)
        {
            logger.LogInformation($"OnStopped has been called：{appCode} {appName} {DateTime.Now} {iPV4Address} {iPV6Address}");
        }
    }
}