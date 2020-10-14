using System;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Cache
{
    public interface ICaching
    {
        /// <summary>
        /// 不存在则设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheItemName"></param>
        /// <param name="cacheTimeInMinutes"></param>
        /// <param name="objectSettingFunction"></param>
        /// <returns></returns>
        T GetOrSetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction);

        /// <summary>
        /// 不存在则设置 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheItemName"></param>
        /// <param name="cacheTimeInMinutes"></param>
        /// <param name="objectSettingFunction"></param>
        /// <returns></returns>
        Task<T> GetOrSetObjectFromCacheAsync<T>(string cacheItemName, int cacheTimeInMinutes, Func<Task<T>> objectSettingFunction);

        /// <summary>
        /// 移除Key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 移除所有Key
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 设置Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTimeInSeconds">默认两分钟</param>
        void SetValue(string key, object value, int cacheTimeInMinutes = 120);

        /// <summary>
        /// 设置Key 永不过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, object value);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetValue(string key);
    }
}