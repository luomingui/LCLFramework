using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LCL.Data.Providers
{
    public class SqlServer2008Backuper : SqlServerBackuper
    {
        public SqlServer2008Backuper(IDbAccesser masterDBAccesser)
            : base(masterDBAccesser)
        {
        }

        protected override string DatabaseIdColumnName
        {
            get
            {
                return "dbid";
            }
        }
    }
}
