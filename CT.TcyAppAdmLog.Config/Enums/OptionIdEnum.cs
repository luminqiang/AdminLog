using System.ComponentModel;

namespace CT.TcyAppAdmLog.Config.Enums
{
    public enum OperationIdEnum
    {
        [Description("查询操作")]
        Query = 10,

        [Description("新增操作")]
        Add = 20,

        [Description("更新操作")]
        Update = 30,

        [Description("删除操作")]
        Delete = 40
    }
}