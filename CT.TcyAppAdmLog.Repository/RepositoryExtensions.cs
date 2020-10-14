using CT.TcyAppAdmLog.Constant;
using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.IUnitOfWork;
using CT.TcyAppAdmLog.Framework.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace CT.TcyAppAdmLog.Repository
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAdminLogRepository, AdminLogRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<ISqlSugarClient>(o =>
                    {
                        return new SqlSugarClient(new ConnectionConfig()
                        {
                            ConnectionString = configuration.GetConnectionString("TcyAppAdmLogDBConnStr") ?? configuration.GetValue<string>("TcyAppAdmLogDBConnStr"),
                            DbType = DbType.MySql,
                            IsAutoCloseConnection = true,
                            InitKeyType = InitKeyType.SystemTable//默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
                        });
                    });

            var currentAssembly = typeof(RepositoryExtensions).Assembly;
            return services.AddDependency(currentAssembly, InjectLifeTime.Scoped);
        }
    }
}