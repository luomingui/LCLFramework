using LCL.Data;

namespace UIShell.RbacPermissionService
{
    public class DbFactory
    {
        public static IDbAccesser DBA {
            get {
                return new DbAccesser(DbSetting.FindOrCreate("LCL"));
            }
        }
    }
}
