using CT.TcyAppAdmLog.Cache;
using CT.TcyAppAdmLog.Domain.Bus;
using CT.TcyAppAdmLog.Domain.CommandHandlers;
using CT.TcyAppAdmLog.Domain.Commands;
using CT.TcyAppAdmLog.Domain.Commands.Cacheing;
using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.Core.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.TcyAppAdmLog.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            // 命令总线
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // 领域事件
            //services.AddScoped<INotificationHandler<StudentRegisteredEvent>, StudentEventHandler>();

            // 领域命令
            services.AddScoped<IRequestHandler<CacheingCommand, Unit>, CacheingCommandHandler>();

            // 领域层 - Memory
            services.AddSingleton<ICaching, Caching>();

            return services;
        }
    }
}
