using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CT.TcyAppAdmLog.Framework.DbLoadBalance
{
    /// <summary>
    /// 数据库分表路由
    /// </summary>
    public static class DbLoadBalance
    {
        private static List<DbLoadBalanceInfo.DatabaseConfigInfo> _configInfo;


        public static void SetConfigInfo(List<DbLoadBalanceInfo.DatabaseConfigInfo> configInfo)
        {
            _configInfo = configInfo;
        }


        /// <summary>
        /// get database by unique db alias
        /// </summary>
        /// <param name="uniqueDbAlias"></param>
        /// <returns></returns>
        public static DbLoadBalanceInfo.DatabaseConfigInfo GetDatabaseConfigByUniqueDbAlias(string uniqueDbAlias)
        {
            if (_configInfo == null || _configInfo.Count == 0)
                return null;
            return _configInfo.FirstOrDefault(db => db.UniqueDbAlias.ToLower(CultureInfo.InvariantCulture) == uniqueDbAlias.ToLower(CultureInfo.InvariantCulture));
        }


        public static string GetTableName<T>(string uniqueDbAlias, string tablePrefix, T objValue, out long splitIndex)
        {
            var dbConfigInfo = GetDatabaseConfigByUniqueDbAlias(uniqueDbAlias);
            if (dbConfigInfo == null)
            {
                splitIndex = 0;
                return tablePrefix;
            }
            return GetTableName(dbConfigInfo, tablePrefix, objValue, out splitIndex);
        }

        private static string GetTableName<T>(DbLoadBalanceInfo.DatabaseConfigInfo dci, string tablePrefix, T objValue, out long splitIndex)
        {
            splitIndex = 0;
            string tableName = tablePrefix;
            if (dci == null || dci.TableNameRules == null || dci.TableNameRules.Count == 0)
                return tableName;
            DbLoadBalanceInfo.TableNameRule tnr = dci.TableNameRules.Find(t => t.Prefix.ToLower() == tablePrefix.ToLowerInvariant());
            if (tnr == null)
                return tableName;

            if (!tnr.AllowSplitTable || tnr.SplitType == DbLoadBalanceInfo.TableSplitType.Null)
                return tableName;

            switch (tnr.SplitType)
            {
                case DbLoadBalanceInfo.TableSplitType.Int:
                    tableName = SplitInt(objValue, out splitIndex, tnr);
                    break;
                case DbLoadBalanceInfo.TableSplitType.Mod:
                    tableName = SplitMod(objValue, out splitIndex, tnr);
                    break;
                case DbLoadBalanceInfo.TableSplitType.Hash:
                    tableName = SplitHash(objValue, out splitIndex, tnr);
                    break;
                case DbLoadBalanceInfo.TableSplitType.Date:
                    tableName = SplitDate(objValue, out splitIndex, tnr);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected value tnr.SplitType = " + tnr.SplitType);
            }
            return tableName;
        }

        private static string SplitDate<T>(T objValue, out long splitIndex, DbLoadBalanceInfo.TableNameRule tnr)
        {
            splitIndex = 0;
            string tableName = tnr.Prefix;
            DateTime dateValue = Convert.ToDateTime(objValue);
            if (tnr.Format >= 1 && tnr.Format <= 4)
            {
                string suffix = string.Empty;
                switch (tnr.Format)
                {
                    case 1:
                        suffix = dateValue.ToString("yyyy");
                        break;
                    case 2:
                        suffix = dateValue.ToString("yyyyMM");
                        break;
                    case 3:
                        suffix = dateValue.ToString("yyyyMMdd");
                        break;
                    case 4://当年第几周
                        int week = new System.Globalization.GregorianCalendar().GetWeekOfYear(dateValue, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        suffix = dateValue.ToString("yyyy") + week.ToString();
                        break;
                    default:
                        break;
                }
                if (suffix != string.Empty)
                {
                    tableName = string.Format("{0}_{1}", tnr.Prefix, suffix);
                    splitIndex = int.Parse(dateValue.ToString("yyyyMMdd"));
                }
            }
            return tableName;
        }

        private static string SplitHash<T>(T objValue, out long splitIndex, DbLoadBalanceInfo.TableNameRule tnr)
        {
            splitIndex = 0;
            string tableName = tnr.Prefix;
            string strValue = objValue.ToString();
            if (tnr.Format >= 1 && tnr.Format <= 4) //取前4位
            {
                string uidHex = GetMD5(strValue).Substring(0, tnr.Format).ToUpper();
                Int64 uidInt = Convert.ToInt64(uidHex, 16);
                tableName = string.Format("{0}{1}", tnr.Prefix, uidHex);
                splitIndex = uidInt;
            }
            return tableName;
        }

        private static string SplitMod<T>(T objValue, out long splitIndex, DbLoadBalanceInfo.TableNameRule tnr)
        {
            Int64 intValue = Convert.ToInt64(objValue);
            string tableName = tnr.Format == -1 ? string.Format("{0}{1}", tnr.Prefix, intValue) : string.Format("{0}{1}", tnr.Prefix, (intValue % tnr.Format));
            splitIndex = intValue;
            return tableName;
        }
        private static string SplitInt<T>(T objValue, out long splitIndex, DbLoadBalanceInfo.TableNameRule tnr)
        {
            Int64 intValue = Convert.ToInt64(objValue);
            string tableName = tnr.Format == -1 ? string.Format("{0}{1}", tnr.Prefix, intValue) : string.Format("{0}{1}", tnr.Prefix, (intValue / tnr.Format));
            splitIndex = intValue;
            return tableName;
        }

        private static string GetMD5(string strSource)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取密文字节数组
            var bytResult = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strSource));
            var strResult = BitConverter.ToString(bytResult).ToLowerInvariant();

            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult;
        }
    }
}