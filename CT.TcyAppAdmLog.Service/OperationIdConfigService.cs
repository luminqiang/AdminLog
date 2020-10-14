using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Framework.Dependency;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.ServiceContract;
using CtCommon.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Service
{
    [AutoInject(InjectLifeTime.Scoped)]
    public class OperationIdConfigService : BaseServices, IOperationIdConfigService
    {
        private readonly IAppConfigRepository _appConfigRepository;
        private readonly IOperationIdConfigRepository _operationIdConfigRepository;
        private readonly IMediatorHandler _bus;

        public OperationIdConfigService(IOperationIdConfigRepository operationIdConfigRepository, IAppConfigRepository appConfigRepository, IMediatorHandler mediatorHandler)
        {
            _appConfigRepository = appConfigRepository;
            _operationIdConfigRepository = operationIdConfigRepository;
            _bus = mediatorHandler;
        }

        public async Task<ServiceInvokeResult<bool>> CreateOperationIdAsync(AddOperationIdConfig add)
        {
            var appIdExist = await _appConfigRepository.QueryAsQueryable(a => a.AppId == add.AppId).AnyAsync();
            if (!appIdExist)
            {
                return PrintInvokeResult(false, "不存在的AppId");
            }

            var operationIdConfigs = await _operationIdConfigRepository.Query(a => a.AppId == add.AppId);
            var operationIds = operationIdConfigs.Select(a => a.OperationId).ToList();
            var newOperationIds = add.Opretations.Select(a => a.OperationId).ToList();
            var isRepetition = operationIds.Intersect(newOperationIds).Any();
            if (isRepetition)
            {
                return PrintInvokeResult(false, "重复的OpretationId, 请重新添加");
            }

            var nowUnixTime = DateTime.Now.ToUnixTime(true);
            var newOperationIdConfigs = add.Opretations.ConvertAll(a =>
            {
                return new OperationIdConfig(add.AppId, a.OperationId, a.OperationName, nowUnixTime);
            });

            await _operationIdConfigRepository.Add(newOperationIdConfigs);

            return PrintInvokeResult(true, "添加成功");
        }
    }
}