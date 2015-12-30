
using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;
using UIShell.EducationDeviceMaintenancePlugin.Controllers;

namespace UIShell.EducationDeviceMaintenancePlugin
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            Debug.WriteLine("UIShell.EducationDeviceMaintenancePlugin Initialize....");
            app.ModuleOperations += app_ModuleOperations;
        }
        void app_ModuleOperations(object sender, System.EventArgs e)
        {
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "报修管理",
                Image = "icon-advancedsettings2",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image="icon-layout-add", Label = "填写报修单", CustomUI="/UIShell.EducationDeviceMaintenancePlugin/EDMSRepairsBill/Index",EntityType=typeof(EDMSRepairsBillController)},
                }
            });
        }
    }
}
