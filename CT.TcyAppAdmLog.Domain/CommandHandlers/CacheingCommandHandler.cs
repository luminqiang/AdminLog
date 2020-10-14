using CT.TcyAppAdmLog.Cache;
using CT.TcyAppAdmLog.Domain.Commands.Cacheing;
using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.Core.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Domain.CommandHandlers
{
    public class CacheingCommandHandler : CommandHandler, IRequestHandler<CacheingCommand, Unit>
    {
        public readonly ICaching _caching;
        private readonly IMediatorHandler _bus;

        public CacheingCommandHandler(ICaching caching, IMediatorHandler bus) : base(bus)
        {
            _caching = caching;
            _bus = bus;
        }

        public Task<Unit> Handle(CacheingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    NotifyValidationErrors(request);
                    return Task.FromResult(new Unit());
                }

                _caching.SetValue(request.CacheKey, request.CacheValue, request.CacheMinuteTime);
            }
            catch (Exception e)
            {
                _bus.RaiseEvent(new DomainNotification(request.MessageType, $"缓存写入失败: {e.Message}"));
                return Task.FromResult(new Unit());
            }

            return Task.FromResult(new Unit());
        }
    }
}