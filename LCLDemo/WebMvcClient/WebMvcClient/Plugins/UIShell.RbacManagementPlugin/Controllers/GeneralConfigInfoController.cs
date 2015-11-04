using LCL.MvcExtensions;
using System.Collections.Generic;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
     [ValidateInput(false)]   
    public class GeneralConfigInfoController : RbacController
    {
        [Permission("首页", "Index")]
        public ActionResult Index()
        {
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            InitFrom(config);
            return View(config);
        }
        [ValidateInput(false)]
        [Permission("保存", "Save")]
        [BizActivityLog("修改系统配置", "AppName")]
        public virtual ActionResult Save(GeneralConfigInfo config, FormCollection collection)
        {
            if (config != null)
            {
                GeneralConfigs.SaveConfig(config);
                InitFrom(config);
            }
            return View("Index", config);
        }
        public void InitFrom(GeneralConfigInfo config)
        {
            //SetSelectListItem("_SchoolResponseTimeout", "SchoolResponseTimeout", config.SchoolResponseTimeout);
            //SetSelectListItem("_SchoolMaintainTimeout", "SchoolMaintainTimeout", config.SchoolMaintainTimeout);
            //SetSelectListItem("_DefaultAuditTimeout", "DefaultAuditTimeout", config.DefaultAuditTimeout);
            //SetSelectListItem("_CompanyResponseTimeout", "CompanyResponseTimeout", config.CompanyResponseTimeout);
            //SetSelectListItem("_CompanyMaintainTimeout", "CompanyMaintainTimeout", config.CompanyMaintainTimeout);
        }
        public void SetSelectListItem(string slName, string checkName, int selected)
        {
            List<SelectListItem> selitem = new List<SelectListItem>();
            for (int i = 1; i < 7; i++)
            {
                if (i == selected)
                {
                    selitem.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = true });
                    ViewData[checkName] = i.ToString();
                }
                else
                {
                    selitem.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = false });
                }
            }
            ViewData[slName] = selitem;
        }
    }
}
