using LCL;
using System;
using UIShell.RbacPermissionService;

namespace UIShell.BusinessEvent
{
    public class CallonWebSiteEvent : IEvent
    {
        public void Execute(object state)
        {
            Logger.LogInfo("" + DateTime.Now + "正在执行事件：定时访问站点，使其永远保持活力");
            try
            {
                
                //string url = "http://www.baidu.com";
                //WebClient client = new WebClient();
                //client.Headers.Add("user-agent", "Mozilla/4.0");
                //Stream data = client.OpenRead(url);
                //StreamReader reader = new StreamReader(data);
                //string s = reader.ReadToEnd();
                //Console.WriteLine(s);
                //data.Close();
                //reader.Close();  

            }
            catch (Exception ex)
            {
                Logger.LogError("CallonWebSiteEvent", ex);
            }
        }
    }
}
