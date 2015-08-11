/*******************************************************
 * 
 * Copyright(c)2012-2018 Luomg.All rights reserved.
 * CLR版本：.NET 4.0.0
 * 组织：Luomg@中国
 * 网站：http://luomingui.cnblogs.com
 * 说明：
 *
 * 历史记录：
 * 创建文件 Luomg 20150731
 * 
*******************************************************/
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
            app.ModuleOperations += app_ModuleOperations;
        }
        void app_ModuleOperations(object sender, AppInitEventArgs e)
        {
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
