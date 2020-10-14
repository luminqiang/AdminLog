using CT.TcyAppAdmLog.Framework.Dependency;
using CT.TcyAppAdmLog.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CT.TcyAppAdmLog.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddBusinessOfApplications(this IServiceCollection services)
        {
            var currentAssembly = typeof(ApplicationExtensions).Assembly;
            return services.AddDependency(currentAssembly, InjectLifeTime.Scoped);
        }

        public static IServiceCollection AddBusinessOfServices(this IServiceCollection services)
        {
            return services.AddServices();
        }

        public static IServiceCollection AddBusinessOfRepositorys(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddServiceOfRepositorys(configuration);
        }
    }
}