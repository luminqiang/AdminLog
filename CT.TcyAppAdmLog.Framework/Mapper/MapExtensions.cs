using AutoMapper;
using CT.TcyAppAdmLog.Framework.AutoMapperAdapter;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace CT.TcyAppAdmLog.Framework.Mapper
{
    public static class MapExtensions
    {
        private static IMapProvider autoMapProvider = new NullMapProvider();

        /// <summary>
        /// 注册对象映射关系
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (services.Any(sd => sd.ServiceType == typeof(IMapper)))
                return services;

            if (assemblies == null || assemblies.Length <= 0)
            {
                return services;
            }

            var toScan = assemblies.ToList();

            var allTypes = toScan
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var allProfileTypes = allTypes.Where(t => t.IsSubclassOf(typeof(Profile)));
            foreach (var type in allProfileTypes)
            {
                services.AddTransient(typeof(Profile), type);
            }

            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(mc => { mc.AddProfiles(provider.GetServices<Profile>()); });
                return config.CreateMapper();
            });
            return services;
        }

        /// <summary>
        /// 注册对象映射工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperAdapter(this IServiceCollection services)
        {
            services.AddSingleton<IMapProvider>(provider =>
            {
                var autoMapper = provider.GetService<IMapper>();
                return new MapperAdapter(autoMapper);
            });
            return services;
        }

        public static void UseMapProvider(this IMapProvider provider)
        {
            autoMapProvider = provider;
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            if (source == null)
            {
                return default(TDestination);
            }
            return autoMapProvider.MapTo<TDestination>(source);
        }
    }
}