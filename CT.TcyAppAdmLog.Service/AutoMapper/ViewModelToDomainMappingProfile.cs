using AutoMapper;
using CT.TcyAppAdmLog.Domain.Commands;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;

namespace CT.TcyAppAdmLog.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AdminLogViewModel, AdminLog>();
        }
    }
}