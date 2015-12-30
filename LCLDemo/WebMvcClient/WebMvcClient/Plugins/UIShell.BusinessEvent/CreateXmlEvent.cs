using LCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UIShell.RbacPermissionService;

namespace UIShell.BusinessEvent
{
    public class CreateXmlEvent : IEvent
    {
        public void Execute(object state)
        {
            Logger.LogInfo("" + DateTime.Now + "正在执行事件：定时创建XML文件");
            try
            {
                //DataTable dt = PatrolDBClass.GetMonthQualified();
                //string xml = XmlUtil.Serializer(typeof(DataTable), dt);
                //XmlUtil.CreateXML(Utils.GetMapPath("Config\\MonthQualified.xml"), xml);
            }
            catch (Exception ex)
            {
                Logger.LogError("CreateXmlEvent", ex);
            }
        }
    }
}
