using System.ComponentModel;

namespace CT.TcyAppAdmLog.Config.Enums
{
    /// <summary>
    /// API结果代号
    /// </summary>
    public enum ApiResultCode
    {
        [Description("成功")]
        Success = 0,

        [Description("未登录")]
        NotLogin = 10001,

        [Description("参数错误")]
        ParamError = 10002,

        [Description("访问过于频繁")]
        RequestFrequently = 10003,

        [Description("拒绝访问")]
        AccessDenied = 10004,

        [Description("数据空")]
        EmptyData = 10005,

        [Description("未知错误")]
        UnknownError = 10006,

        [Description("用户不存在")]
        UserNotExist = 10007,

        [Description("没有权限,请向管理员申请")]
        UnAuthized = 11000,

        [Description("出现异常")]
        Exception = 99999
    }
}