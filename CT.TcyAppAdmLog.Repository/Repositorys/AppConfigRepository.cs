using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.IUnitOfWork;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.TcyAppAdmLog.Repository.Repositorys
{
    [AutoInject(InjectLifeTime.Scoped)]
    public class AppConfigRepository : BaseRepository<AppConfig>, IAppConfigRepository
    {
        public AppConfigRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
