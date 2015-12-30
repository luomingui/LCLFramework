using LCL;
using System;
using UIShell.RbacPermissionService;

namespace UIShell.BusinessEvent
{
    /// <summary>
    /// 执行脚本的任务
    /// 约束：
    ///    两个数据库必须放在一起并且使用的是同一个帐号登录
    /// </summary>
    public class ScriptEvent : IEvent
    {
        private static ScriptEventConfigInfo scriptevents = ScriptEventConfigs.GetConfig();
        public void Execute(object state)
        {
            Logger.LogInfo("" + DateTime.Now + "正在执行事件：定时执行脚本的任务");
            try
            {
                foreach (ScriptEventInfo sei in scriptevents.ScriptEvents)
                {
                    if (sei.Enabled && sei.ShouldExecute)
                    {
                        if (!string.IsNullOrWhiteSpace(sei.Script))
                            DbFactory.DBA.ExecuteText(sei.Script);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ScriptEvent", ex);
            }
        }
    }
}
