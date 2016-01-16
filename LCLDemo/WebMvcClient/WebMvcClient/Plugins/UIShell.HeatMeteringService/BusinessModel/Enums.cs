
namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public enum DictionaryType
    {
        减免原因 = 1,
        退费原因 = 2,
        报停原因 = 3,
        强停原因 = 4,
        调整面积原因 = 5,
        退网原因 = 6,
        黑名单性质 = 7,
        附加费用类别 = 8,
        收费方式 = 9,
        交款方式 = 10,
        欠费性质 = 11
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        未指定类型 = 1,
        执行命令 = 2,
        打开模块 = 3,
        登录 = 4,
    }
    /// <summary>
    /// 热量计量单位
    /// </summary>
    public enum HeatUnitType
    { 
        KWH,
        GJ
    }
    /// <summary>
    /// 取暖类型
    /// </summary>
    public enum HeatType
    {
        面积收费=1,
        计量收费=2,
    }
    public enum BillStatus
    {
        票据报损,
        票据作废,
        票据核销,
    }
}
