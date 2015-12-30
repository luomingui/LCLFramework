using LCL;
using LCL.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using UIShell.RbacPermissionService;

namespace UIShell.BusinessEvent
{
    public class ClearCatchEvent : IEvent
    {
        public void Execute(object state)
        {
            Logger.LogInfo("" + DateTime.Now + "正在执行事件：定时清理缓存");
            try
            {
                MemoryCacheHelper.RemoveCacheAll();
                DbFactory.DBA.ExecuteText("DELETE TLog WHERE AddDate < '" + DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd") + "'");
            }
            catch (Exception ex)
            {
                Logger.LogError("ClearCatchEvent", ex);
            }
        }
    }
}
