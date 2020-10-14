using System.IO;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

namespace CT.TcyAppAdmLog.Framework.DbLoadBalance
{

    /// <summary>
    /// Represents an XML file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class DbLoadBalanceConfigurationProvider : FileConfigurationProvider
    {
        private const string NameAttributeKey = "Name";

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public DbLoadBalanceConfigurationProvider(DbLoadBalanceConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads the XML data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                var dbload = Deserialize<DbLoadBalanceInfo.DbLoadBalanceConfig>(text);
                if (dbload != null)
                {
                    DbLoadBalance.SetConfigInfo(dbload.DbLoadBalanceConfigs);
                }
            }
        }

        private T Deserialize<T>(string xml)
        {
            T result;

            if (!string.IsNullOrEmpty(xml))
            {
                using (StringReader textReader = new StringReader(xml))
                {
                    result = (T)new XmlSerializer(typeof(T)).Deserialize(textReader);
                }
            }
            else
            {
                result = default(T);
            }

            return result;
        }
    }
}
