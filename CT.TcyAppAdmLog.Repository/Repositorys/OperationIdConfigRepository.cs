using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.IUnitOfWork;
using CT.TcyAppAdmLog.Domain.Models;

namespace CT.TcyAppAdmLog.Repository
{
    public class OperationIdConfigRepository : BaseRepository<OperationIdConfig>, IOperationIdConfigRepository
    {
        public OperationIdConfigRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}