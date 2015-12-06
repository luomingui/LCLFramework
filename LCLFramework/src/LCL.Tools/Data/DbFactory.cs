using LCL.Data;
namespace LCL.Tools.Data
{
    public class DbFactory
    {
        public static IDbAccesser DBA
        {
            get
            {
                return new DbAccesser(Utils.ConnStr);
            }
        }
    }
}

