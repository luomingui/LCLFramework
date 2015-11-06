using LCL.Repositories;
using LCL;
using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 业务日志
    /// [BizActivityLog("新增激活码", "activateCodeType,filePath")]
    /// </summary>
    public class BizActivityLogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 参数名称列表,可以用, | 分隔
        /// </summary>
        private readonly string _parameterNameList;
        //类型名称
        private string _activityLogTypeName = "";
        /// <summary>
        /// 活动日志
        /// </summary>
        /// <param name="activityLogTypeName">类别名称</param>
        /// <param name="parm">参数名称列表,可以用, | 分隔</param>
        public BizActivityLogAttribute(string activityLogTypeName, string parm)
        {
            _activityLogTypeName = activityLogTypeName;
            _parameterNameList = parm;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                string moduleId = filterContext.GetPluginSymbolicName();
                Dictionary<string, string> parmsObj = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(_parameterNameList))
                {
                    foreach (var item in _parameterNameList.Split(',', '|'))
                    {
                        var valueProviderResult = filterContext.Controller.ValueProvider.GetValue(item);
                        if (valueProviderResult != null && !parmsObj.ContainsKey(item))
                        {
                            parmsObj.Add(item, valueProviderResult.AttemptedValue);
                        }
                    }
                }

                //日志内容
                StringBuilder logContent = new StringBuilder();
                foreach (KeyValuePair<string, string> kvp in parmsObj)
                {
                    logContent.AppendFormat("{0}:{1} ", kvp.Key, kvp.Value);
                }

                HttpBrowserCapabilitiesBase bc = filterContext.HttpContext.Request.Browser;
                string Browser = "您好，您正在使用 " + bc.Browser + " v." + bc.Version + ",你的运行平台是 " + bc.Platform;

                //******************************************************************************
                //这里是插入数据表操作
                //步骤：
                //1、根据日志类型表的SystemKeyword得到日志类型Id
                //2、往日志表里插入数据，logContent.ToString()是内容，内容可以自己拼接字符串，
                //   比如：string.Format("删除记录，删除操作者{0}","xxxx");
                
                var repo = RF.Concrete<ITLogRepository>();
                repo.Create(new TLog
                {
                    ID = Guid.NewGuid(),
                    UpdateDate = DateTime.Now,
                    AddDate = DateTime.Now,
                    UserName = LCL.LEnvironment.Principal.Identity.Name,
                    LogType = EnumLogType.打开模块,
                    ModuleName = moduleId,
                    MachineName = Environment.MachineName,
                    Title = _activityLogTypeName,
                    IP =LRequest.GetIP(),
                    url = filterContext.HttpContext.Request.Path,
                    IsActiveX = bc.ActiveXControls,
                    Browser = Browser,
                    Content = _activityLogTypeName + logContent.ToString()

                });
                repo.Context.Commit();
                //******************************************************************************
            }
            catch (Exception ex)
            {
                Logger.LogError("BizActivityLog", ex);
            }
        }
    }
}
