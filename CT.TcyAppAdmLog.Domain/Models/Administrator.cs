using CT.TcyAppAdmLog.Domain.Core.Models;
using System;

namespace CT.TcyAppAdmLog.Domain.Models
{
    /// <summary>
    /// 管理员 值对象
    /// </summary>
    public class Administrator : ValueObject<Administrator>
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }

        public Administrator()
        {

        }

        public Administrator(int adminId, string adminName)
        {
            AdminId = adminId;
            AdminName = adminName;
        }

        protected override bool EqualsCore(Administrator other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
