using CT.TcyAppAdmLog.Common.Security;
using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.Core.Notifications;
using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Domain.Validations;
using CT.TcyAppAdmLog.Framework.Mapper;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.ServiceContract;
using CtCommon.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Service
{
    public class AdminLogService : BaseServices, IAdminLogService
    {
        private readonly IAdminLogRepository _adminLogRepository;
        private readonly IOperationIdConfigRepository _operationIdConfigRepository;
        private readonly IMediatorHandler _bus;

        public AdminLogService(IAdminLogRepository adminLogRepository, IOperationIdConfigRepository operationIdConfigRepository, IMediatorHandler mediatorHandler)
        {
            _adminLogRepository = adminLogRepository;
            _operationIdConfigRepository = operationIdConfigRepository;
            _bus = mediatorHandler;
        }

        public async Task<AdminLogViewModel> QueryAsync(QueryAdminLog query)
        {
            var sugarQueryable = _adminLogRepository.QueryAsQueryable(a => a.AppId == query.AppId && a.LinkId == query.LinkId && a.OperationId == query.OperationId)
                .WhereIF(query.BeginUnixTime > 0 && query.EndUnixTime > 0, a => query.BeginUnixTime <= a.CreateUnixTime && a.CreateUnixTime <= query.EndUnixTime);
            var adminLog = await sugarQueryable.OrderBy(a => a.CreateUnixTime, SqlSugar.OrderByType.Desc).FirstAsync();
            return adminLog.MapTo<AdminLogViewModel>();
        }

        public async Task<PageViewModel<AdminLogViewModel>> QueryAdminLogListAsync(QueryAdminLogList query)
        {
            var sugarQueryable = _adminLogRepository.QueryAsQueryable(a => true);

            sugarQueryable.WhereIF(query.LinkId > 0, a => a.LinkId == query.LinkId);
            sugarQueryable.WhereIF(query.AppId > 0, a => a.AppId == query.AppId);
            sugarQueryable.WhereIF(query.OperationId > 0, a => a.OperationId == query.OperationId);

            //如果不使用时间范围，默认为前三个月
            if (query.BeginUnixTime == 0 && query.EndUnixTime == 0)
            {
                var dateTime = DateTime.Now;
                var nowUnixTime = dateTime.ToUnixTime(true);
                var beginUnixTime = dateTime.AddMonths(-3).ToUnixTime(true);

                sugarQueryable.WhereIF(true, a => beginUnixTime <= a.CreateUnixTime && a.CreateUnixTime <= nowUnixTime);
            }
            else
            {
                sugarQueryable.WhereIF(query.BeginUnixTime > 0, a => query.BeginUnixTime <= a.CreateUnixTime);
                sugarQueryable.WhereIF(query.EndUnixTime > 0, a => a.CreateUnixTime <= query.EndUnixTime);
            }

            var pageModel = await _adminLogRepository.QueryPageModel(sugarQueryable);
            var adminLogViewModels = pageModel.Data.MapTo<List<AdminLogViewModel>>();
            return new PageViewModel<AdminLogViewModel>()
            {
                Data = adminLogViewModels,
                DataCount = pageModel.DataCount,
                Page = pageModel.Page,
                PageCount = pageModel.PageCount,
                PageSize = pageModel.PageSize
            };
        }

        public async Task<OperationContentViewModel> QueryOperationContentAsync(QueryOperationContent query)
        {
            var adminLog = await _adminLogRepository.QueryById(query.Id);
            if (adminLog == null)
            {
                return null;
            }

            var aesHelper = new AesHelper();
            var decryptAfterContent = aesHelper.Decrypt(adminLog.AfterContent);
            var decryptBeforeContent = aesHelper.Decrypt(adminLog.BeforeContent);
            return new OperationContentViewModel()
            {
                AfterContent = decryptAfterContent,
                BeforeContent = decryptBeforeContent
            };
        }

        public async Task<ServiceInvokeResult<bool>> ReportAsync(AdminLogDto dto)
        {
            var operationIdCorrect = await _operationIdConfigRepository.QueryAsQueryable(a => a.AppId == dto.AppId && a.OperationId == dto.OperationId).AnyAsync();
            if (!operationIdCorrect)
            {
                return PrintInvokeResult(false, "应用ID与操作ID不匹配");
            }

            var aesHelper = new AesHelper();
            var encryptBeforeContent = aesHelper.Encrypt(dto.BeforeContent);
            var encryptAfterContent = aesHelper.Encrypt(dto.AfterContent);

            //实例化领域模型
            var administrator = new Administrator(dto.AdminId, dto.AdminName);
            var adminLog = new AdminLog(dto.AppId, encryptBeforeContent, encryptAfterContent, dto.LinkId,
                dto.OperationId, dto.OperationIP, dto.OperationRemark, dto.CreateUnixTime, administrator);

            //验证模型
            var validationResult = new AdminLogReportValidation().Validate(adminLog);
            if (!validationResult.IsValid)
            {
                await _bus.RaiseEvent(new DomainNotification(nameof(AdminLogService), JsonConvert.SerializeObject(validationResult.Errors)));
            }

            //提交至仓储持久化
            var addResult = await _adminLogRepository.Add(adminLog);
            if (addResult <= 0)
            {
                return PrintInvokeResult(false, "日志入库失败");
            }

            return PrintInvokeResult(true, "上报完成");
        }
    }
}