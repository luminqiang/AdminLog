using System;
using System.Collections.Generic;
using System.Text;

namespace CT.TcyAppAdmLog.Framework.Dependency
{
    /// <summary>
    /// 注入生命周期类型
    /// </summary>
    public enum InjectLifeTime
    {
        /// <summary>
        /// 默认单例
        /// </summary>
        Singleton,

        Transient,

        Scoped
    }
}
