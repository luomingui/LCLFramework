
using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;

namespace UIShell.OlsonAdminShellPlugin
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
                Value = @"~\Plugins\UIShell.OlsonAdminShellPlugin\Views\Shared\_Layout.cshtml",
                Priority = 0
            });
            PageFlowService.PageNodes.AddPageNode(new PageNode
            {
                Name = "LayoutHome",
                Bundle = this,
                Value = @"\UIShell.OlsonAdminShellPlugin\Home\Index",
                Priority = 2
            });
        }
    }
}
