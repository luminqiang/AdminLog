using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.IUnitOfWork;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Framework.Dependency;

namespace CT.TcyAppAdmLog.Repository
{
    [AutoInject(InjectLifeTime.Scoped)]
    public class AdminLogRepository : BaseRepository<AdminLog>, IAdminLogRepository
    {
        public AdminLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}