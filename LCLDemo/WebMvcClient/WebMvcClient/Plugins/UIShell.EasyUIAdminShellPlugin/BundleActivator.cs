
using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;

namespace UIShell.EasyUIAdminShellPlugin
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            Debug.WriteLine("UIShell.OlsonAdminShellPlugin Initialize....");
            Bundle = this;

            PageFlowService.PageNodes.AddPageNode(new PageNode
            {
                Name = "LayoutPage",
                Bundle = this,
                Value = @"~/Plugins/UIShell.EasyUIAdminShellPlugin\Views\Shared\_Layout.cshtml",
                Priority = 1
            });
            PageFlowService.PageNodes.AddPageNode(new PageNode
            {
                Name = "LayoutHome",
                Bundle = this,
                Value = @"\UIShell.EasyUIAdminShellPlugin\Home\Index",
                Priority = 2
            });
        }
    }
}
