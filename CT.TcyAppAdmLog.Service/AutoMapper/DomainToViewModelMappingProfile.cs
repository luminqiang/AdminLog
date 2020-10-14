using AutoMapper;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Model.ViewModels;

namespace CT.TcyAppAdmLog.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AdminLog, AdminLogViewModel>();
        }
    }
}