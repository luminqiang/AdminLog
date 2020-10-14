namespace CT.TcyAppAdmLog.Framework.Mapper
{
    /// <summary>
    /// 自动映射提供者接口
    /// </summary>
    public interface IMapProvider
    {
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination MapTo<TDestination>(object source);
    }
}