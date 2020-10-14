using Microsoft.Extensions.Configuration;

namespace CT.TcyAppAdmLog.Framework.DbLoadBalance
{
    public class DbLoadBalanceConfigurationSource : FileConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="XmlConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="XmlConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new DbLoadBalanceConfigurationProvider(this);
        }
    }
}
