using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using System;
using System.Diagnostics;
using System.Threading;
using UIShell.RbacPermissionService;

namespace UIShell.BusinessEvent
{
    public class BundleActivator : LCLPlugin
    {
        System.Threading.Timer eventTimer;
        public override void Initialize(IApp app)
        {
            Logger.LogInfo("UIShell.BusinessEvent Initialize....");
            if (eventTimer == null && ScheduleConfigs.GetConfig().Enabled)
            {
                eventTimer = new System.Threading.Timer(new TimerCallback(ScheduledEventWorkCallback), 
                    null, 60000, EventManager.TimerMinutesInterval * 60000);
            }
        }
        private void ScheduledEventWorkCallback(object state)
        {
            try
            {
                if (ScheduleConfigs.GetConfig().Enabled)
                {
                    EventManager.Execute();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed ScheduledEventCallBack: " , ex);
            }
        }
    }
}
