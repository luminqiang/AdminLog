using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CtCommon.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace CT.TcyAppAdmLog.Framework.Dependency
{
    /// <summary>
    /// 自动注入工具
    /// </summary>
    public static class AutoInjectUtils
    {
        /// <summary>
        /// 注册单例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddSingleton(this IServiceCollection services, Assembly currentAssembly)
        {
            List<Type> types = currentAssembly.GetTypes().Where(o => !o.IsInterface && !o.IsGenericType).Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
            if (types.IsEmpty())
            {
                return services;
            }

            foreach (var currentType in types)
            {
                List<Type> interfaces = currentType.GetInterfaces().Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
                if (interfaces.IsEmpty())
                {
                    continue;
                }

                foreach (var currentInterface in interfaces)
                {
                    services.AddSingleton(currentInterface, currentType);
                }
            }

            return services;
        }

        /// <summary>
        /// Scoped方式注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddScoped(this IServiceCollection services, Assembly currentAssembly)
        {
            List<Type> types = currentAssembly.GetTypes().Where(o => !o.IsInterface && !o.IsGenericType).Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
            if (types.IsEmpty())
            {
                return services;
            }

            foreach (var currentType in types)
            {
                List<Type> interfaces = currentType.GetInterfaces().Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
                if (interfaces.IsEmpty())
                {
                    continue;
                }

                foreach (var currentInterface in interfaces)
                {
                    services.AddScoped(currentInterface, currentType);
                }
            }

            return services;
        }

        /// <summary>
        /// transient方式注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddTransient(this IServiceCollection services, Assembly currentAssembly)
        {
            List<Type> types = currentAssembly.GetTypes().Where(o => !o.IsInterface && !o.IsGenericType).Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
            if (types.IsEmpty())
            {
                return services;
            }

            foreach (var currentType in types)
            {
                List<Type> interfaces = currentType.GetInterfaces().Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
                if (interfaces.IsEmpty())
                {
                    continue;
                }

                foreach (var currentInterface in interfaces)
                {
                    services.AddTransient(currentInterface, currentType);
                }
            }

            return services;
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependency(this IServiceCollection services, Assembly currentAssembly)
        {
            List<Type> types = currentAssembly.GetTypes().Where(o => !o.IsInterface && !o.IsGenericType).Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
            if (types.IsEmpty())
            {
                return services;
            }

            foreach (var currentType in types)
            {
                List<Type> interfaces = currentType.GetInterfaces().Where(o => o.IsDefined(typeof(AutoInjectAttribute), true)).ToList();
                if (interfaces.IsEmpty())
                {
                    continue;
                }

                foreach (var currentInterface in interfaces)
                {
                    AutoInjectAttribute attribute = (AutoInjectAttribute)currentType.GetCustomAttributes(typeof(AutoInjectAttribute)).FirstOrDefault();
                    if (attribute == null)
                    {
                        continue;
                    }
                    if (attribute.LifeTime == InjectLifeTime.Singleton)
                    {
                        services.AddSingleton(currentInterface, currentType);
                    }
                    if (attribute.LifeTime == InjectLifeTime.Scoped)
                    {
                        services.AddScoped(currentInterface, currentType);
                    }
                    if (attribute.LifeTime == InjectLifeTime.Transient)
                    {
                        services.AddTransient(currentInterface, currentType);
                    }
                }
            }

            return services;
        }

        /// <summary>
        /// 注册单例 该方法不使用特性 程序集内包含指定名称的类进行自动注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependency(this IServiceCollection services, Assembly currentAssembly, InjectLifeTime injectLifeTime)
        {
            List<Type> types = currentAssembly.GetTypes().Where(o => !o.IsInterface && !o.IsGenericType).ToList();
            if (types.IsEmpty())
            {
                return services;
            }

            foreach (var currentType in types)
            {
                List<Type> interfaces = currentType.GetInterfaces().ToList();
                if (interfaces.IsEmpty())
                {
                    continue;
                }

                foreach (var currentInterface in interfaces)
                {
                    if (injectLifeTime == InjectLifeTime.Singleton)
                    {
                        services.AddSingleton(currentInterface, currentType);
                    }
                    if (injectLifeTime == InjectLifeTime.Scoped)
                    {
                        services.AddScoped(currentInterface, currentType);
                    }
                    if (injectLifeTime == InjectLifeTime.Transient)
                    {
                        services.AddTransient(currentInterface, currentType);
                    }
                }
            }

            return services;
        }
    }
}
