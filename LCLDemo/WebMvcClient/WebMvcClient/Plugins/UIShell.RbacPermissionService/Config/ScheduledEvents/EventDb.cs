using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    public class EventDb
    {
        public EventDb()
        {

        }
        public static EventDb Instance = new EventDb();
        internal DateTime GetLastExecuteScheduledEventDateTime(string key, string servername)
        {
            object obj = DbFactory.DBA.QueryValue(string.Format(@"SELECT MAX([lastexecuted]) FROM [{0}ScheduledEvents] WHERE [key]='{1}' AND [servername] ='{2}'"
                , AppConstant.Tableprefix, key, servername));
            if (obj == null || obj.ToString().Length == 0)
                obj = DateTime.Now.AddYears(-1);
            return (DateTime)obj;
        }
        internal void SetLastExecuteScheduledEventDateTime(string key, string servername, DateTime lastexecuted)
        {
            string sql = string.Format(@"DELETE FROM [{0}ScheduledEvents]
WHERE ([key]='{1}') AND ([lastexecuted] < DATEADD([day], - 7, GETDATE()))", AppConstant.Tableprefix, key);
            DbFactory.DBA.ExecuteText(sql);

            sql = string.Format(@"INSERT [{0}ScheduledEvents] ([ID],[key],[servername],[lastexecuted],[AddDate],[UpdateDate]) 
Values ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", AppConstant.Tableprefix, Guid.NewGuid(), key, servername,
              lastexecuted.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            DbFactory.DBA.ExecuteText(sql);

        }
    }
}
