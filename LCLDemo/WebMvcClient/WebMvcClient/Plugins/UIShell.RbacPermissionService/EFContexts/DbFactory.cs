using LCL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    public class DbFactory
    {
        public static IDbAccesser DBA {
            get {
                return new DbAccesser(DbSetting.FindOrCreate("RBAC2015"));
            }
        }
    }
}
