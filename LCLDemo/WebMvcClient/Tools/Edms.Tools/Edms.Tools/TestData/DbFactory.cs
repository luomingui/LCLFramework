using LCL.Data;

namespace Edms.Tools
{
    public class DbFactory
    {
        public static IDbAccesser DBA {
            get {
                return new DbAccesser(DbSetting.FindOrCreate("LCL_Rbac"));
            }
        }
    }
}
