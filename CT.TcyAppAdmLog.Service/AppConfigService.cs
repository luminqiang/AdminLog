using CT.TcyAppAdmLog.Cache;
using CT.TcyAppAdmLog.Common.Security;
using CT.TcyAppAdmLog.Constant;
using CT.TcyAppAdmLog.Domain.Commands.Cacheing;
using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Domain.Validations;
using CT.TcyAppAdmLog.Framework.Dependency;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.ServiceContract;
using CtCommon.Utility;
using System;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Service
{
    [AutoInject(InjectLifeTime.Scoped)]
    public class AppConfigService : BaseServices, IAppConfigService
    {
        private readonly IAppConfigRepository _appConfigRepository;
        private readonly IMediatorHandler _bus;
        private readonly ICaching _caching;

        public AppConfigService(IAppConfigRepository appConfigRepository, IMediatorHandler mediatorHandler, ICaching caching)
        {
            _appConfigRepository = appConfigRepository;
            _bus = mediatorHandler;
            _caching = caching;
        }

        public async Task<ServiceInvokeResult<AppConfigViewModel>> CreateAppAsync(AddAppConfig appConfig)
        {
            var existAppInfo = await _appConfigRepository.QueryAsQueryable(a => a.AppCode == appConfig.AppCode).FirstAsync();
            if (existAppInfo != null)
            {
                return PrintInvokeResult(new AppConfigViewModel() { AppId = existAppInfo.AppId, AppKey = existAppInfo.AppKey}, "此应用已申请");
            }

            var maxAppId = await _appConfigRepository.QueryAsQueryable(a => true).OrderBy(a => a.AppId, SqlSugar.OrderByType.Desc).Select(a => a.AppId).FirstAsync();
            var newAppId = maxAppId == 0 ? 1000 : maxAppId + 1;
            var appKey = SecretKeyHelper.GetRandomKey();
            var unixTime = DateTime.Now.ToUnixTime(true);
            var app = new AppConfig(newAppId, appConfig.AppCode, appKey, appConfig.AppName, unixTime);

            //领域模型验证
            var validationResult = new AppConfigValidation().Validate(app);
            if (!validationResult.IsValid)
            {
                return PrintInvokeResult<AppConfigViewModel>(null, "模型检验失败");
            }

            await _appConfigRepository.Add(app);

            //写入缓存
            var key = string.Format(CacheKeyConstant.AppConfig, newAppId);
            var cacheingCommand = new CacheingCommand(key, app, 120);
            await _bus.SendCommand(cacheingCommand);

            var viewModel = new AppConfigViewModel() { AppId = newAppId, AppKey = appKey };
            return PrintInvokeResult(viewModel, "应用创建成功");
        }

        public async Task AddAppCacheAsync()
        {
            var appConfigs = await _appConfigRepository.Query();
            appConfigs.ForEach(a =>
            {
                var key = string.Format(CacheKeyConstant.AppConfig, a.AppId);
                var appConfig = new AppConfig(a.AppId, a.AppCode, a.AppKey, a.AppName, a.CreateUnixTime);
                _caching.SetValue(key, appConfig);
            });
        }

        public async Task<ServiceInvokeResult<bool>> AuthAppInfoAsync(int appId, long unixTime, string sign)
        {
            var key = string.Format(CacheKeyConstant.AppConfig, appId);
            var appConfig = _caching.GetValue(key) as AppConfig;
            if (appConfig == null)
            {
               appConfig = await _appConfigRepository.QueryAsQueryable(a => a.AppId == appId).FirstAsync();
            }

            if (appConfig == null)
            {
                return PrintInvokeResult(false, "不存在的应用ID");
            }

            var signSource = $"{appId}{appConfig.AppKey}{unixTime}";
            var correctSign = HashHelper.GetMd5(signSource);
            if (!correctSign.Equals(sign))
            {
                return PrintInvokeResult(false, "签名错误");
            }

            return PrintInvokeResult(true, "验证通过");
        }
    }
}