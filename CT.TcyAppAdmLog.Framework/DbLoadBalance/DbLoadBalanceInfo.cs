using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CT.TcyAppAdmLog.Framework.DbLoadBalance
{
    public class DbLoadBalanceInfo
    {
        /// <summary>
        ///分表类型
        /// </summary>
        public enum TableSplitType
        {
            /// <summary>
            /// 不分表
            /// </summary>
            Null,

            /// <summary>
            /// 取整
            /// </summary>
            Int,

            /// <summary>
            /// 取模
            /// </summary>
            Mod,

            /// <summary>
            /// Md5
            /// </summary>
            Hash,

            /// <summary>
            /// 日期
            /// </summary>
            Date,
        }

        /// <summary>
        /// 分表对象
        /// </summary>
        [Serializable]
        [DataContract]
        public class TableNameRule
        {
            [DataMember]
            [XmlAttribute]
            public string Prefix { get; set; }

            [DataMember]
            [XmlAttribute]
            public bool AllowSplitTable { get; set; }

            [DataMember]
            [XmlAttribute]
            public TableSplitType SplitType { get; set; }

            [DataMember]
            [XmlAttribute]
            public int Format { get; set; }
        }

        [Serializable]
        [DataContract]
        public class DbLoadBalanceConfig
        {
            [DataMember]
            [XmlArray]
            public List<DatabaseConfigInfo> DbLoadBalanceConfigs { get; set; }
        }

        [Serializable]
        [DataContract]
        public class DatabaseConfigInfo
        {
            [DataMember]
            [XmlAttribute]
            public string UniqueDbAlias { get; set; }

            [DataMember]
            [XmlArray]
            public List<TableNameRule> TableNameRules { get; set; }
        }
    }
}