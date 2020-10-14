using System;

namespace CT.TcyAppAdmLog.Framework.Dependency
{
    /// <summary>
    /// 自动依赖注入标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class AutoInjectAttribute : Attribute
    {
        public AutoInjectAttribute(InjectLifeTime injectLifeTime)
        {
            LifeTime = injectLifeTime;
        }

        /// <summary>
        /// 默认单例模式
        /// </summary>
        public InjectLifeTime LifeTime { get; set; }
    }
}
