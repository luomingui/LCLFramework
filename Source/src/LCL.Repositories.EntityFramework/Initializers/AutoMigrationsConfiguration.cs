using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace LCL.Repositories.EntityFramework
{
    /// <summary>
    /// 自动迁移设置
    /// </summary>
    public class AutoMigrationsConfiguration : DbMigrationsConfiguration<LclDbContext>
    {
        public AutoMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;//自动迁移
            AutomaticMigrationDataLossAllowed = true;//允许数据丢失
        }
    }
}
