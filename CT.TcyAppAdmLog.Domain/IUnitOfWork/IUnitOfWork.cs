using SqlSugar;

namespace CT.TcyAppAdmLog.Domain.IUnitOfWork
{
    public interface IUnitOfWork
    {
        ISqlSugarClient GetDbClient();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}