using CT.TcyAppAdmLog.Domain.Core.Commands;

namespace CT.TcyAppAdmLog.Domain.Commands.Cacheing
{
    /// <summary>
    /// 缓存命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CacheingCommand : Command
    {
        public CacheingCommand(string key, object value, int time)
        {
            CacheKey = key;
            CacheValue = value;
            CacheMinuteTime = time;
        }

        public string CacheKey { get; private set; }

        public object CacheValue { get; private set; }

        public int CacheMinuteTime { get; private set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(CacheKey) || CacheValue == null)
            {
                return false;
            }

            return true;
        }
    }
}