using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Domain.Core.Model;
using CT.TcyAppAdmLog.Domain.IRepository;
using CT.TcyAppAdmLog.Domain.Models;
using CT.TcyAppAdmLog.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace CT.TcyAppAdmLog.Test
{
    public class AdminLogRepositoryTest
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationRoot _configuration;
        private ServiceProvider ServiceProvider => _services.BuildServiceProvider();

        public AdminLogRepositoryTest()
        {
            _services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            var keyValuePairs = new Dictionary<string, string>
            {
                { "TcyAppAdmLogDBConnStr", "Database=tcyappadminlog;Data Source=localhost;User Id=luminqiang;Password=123456;pooling=false;CharSet=utf8;port=3306"}
            };
            configurationBuilder.AddInMemoryCollection(keyValuePairs);
            _configuration = configurationBuilder.Build();

            _services.AddRepository(_configuration);
        }

        [Fact]
        public void AddTest()
        {
            var adminLogRepository = ServiceProvider.GetService<IAdminLogRepository>();
            var adminLog = new AdminLog();

            var id = adminLogRepository.Add(adminLog).GetAwaiter().GetResult();

            Assert.True(id > 0);
        }

        [Fact]
        public void QueryTest()
        {
            var adminLogRepository = ServiceProvider.GetService<IAdminLogRepository>();
            var adminLogs = adminLogRepository.Query().Result;

            Assert.NotNull(adminLogs);
            Assert.True(adminLogs.Count > 0);
        }

        [Fact]
        public void UpdateTest()
        {
            var adminLogRepository = ServiceProvider.GetService<IAdminLogRepository>();
            var adminLog = adminLogRepository.QueryById(1).Result;
            var result = adminLogRepository.Update(adminLog).Result;

            Assert.True(result);
        }
    }
}
