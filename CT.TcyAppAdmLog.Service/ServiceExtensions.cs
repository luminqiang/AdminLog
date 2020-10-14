using CT.TcyAppAdmLog.Framework.Dependency;
using CT.TcyAppAdmLog.Framework.Mapper;
using CT.TcyAppAdmLog.Repository;
using CT.TcyAppAdmLog.Service.AutoMapper;
using CT.TcyAppAdmLog.ServiceContract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CT.TcyAppAdmLog.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMapperProfiles(typeof(DomainToViewModelMappingProfile).Assembly);

            var currentAssembly = typeof(ServiceExtensions).Assembly;
            return services.AddDependency(currentAssembly, InjectLifeTime.Scoped);
        }

        public static IServiceCollection AddServiceOfRepositorys(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddRepository(configuration);
        }
    }
}