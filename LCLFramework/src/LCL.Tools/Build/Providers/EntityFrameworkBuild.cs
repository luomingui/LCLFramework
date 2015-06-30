using System.Collections.Generic;
using System.Text;

namespace LCL.Tools
{
    //ClassLib
    public class EntityFrameworkBuild : BuildBase, IBuild
    {
        public string Library(string path, TableModel tableModel, System.Windows.Forms.ProgressBar progressBar)
        {
            //Entity
            BuildEntity(path, tableModel, progressBar);
            //Repository
            new RepositoryBuild().BuildRepository(path);
            //ValidationModel
            new ValidationModelBuild().GenerateValidationModel(path);
            //ViewModels

            //EFContexts
            
            //BundleActivator


            return "";

        }
        public void BuildDbContext(string path, List<TableModel> tableNames)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("using System.Data.Entity; ");
            builder.AppendLine("using System.Data.Entity.ModelConfiguration.Conventions; ");
            builder.AppendLine("using System.Linq; ");

            builder.AppendLine("namespace " + Utils.NameSpaceEntities);
            builder.AppendLine("{ ");
            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// 数据上下文 Db" + Utils.dbName + "Context ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public class Db" + Utils.dbName + "Context : DbContext ");
            builder.AppendLine("    { ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 构造函数 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        public Db" + Utils.dbName + "Context() ");
            builder.AppendLine("            : base(\"" + Utils.dbName + "\") ");
            builder.AppendLine("        { ");
            builder.AppendLine("        } ");
            builder.AppendLine("#region 属性 ");
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                builder.AppendLine("        /// <summary> ");
                builder.AppendLine("        /// " + tableInfo + " ");
                builder.AppendLine("        /// </summary> ");
                builder.AppendLine("        public DbSet<" + tablename + "> " + Utils.GetCapFirstName(tablename) + " { get; set; } ");
            }
            builder.AppendLine("#endregion ");
            builder.AppendLine("    } ");

            builder.AppendLine(" /// <summary> ");
            builder.AppendLine("    /// 数据库初始化操作类 ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public static class DatabaseInitializer ");
            builder.AppendLine("    { ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 数据库初始化 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        public static void Initialize() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            Database.SetInitializer(new SampleData()); ");
            builder.AppendLine("            using (var db = new Db" + Utils.dbName + "Context()) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                db.Database.Initialize(false); ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");

            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// 数据库初始化策略 ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public class SampleData : CreateDatabaseIfNotExists<AreaManageDbContext> ");
            builder.AppendLine("    { ");
            builder.AppendLine("        protected override void Seed(AreaManageDbContext context) ");
            builder.AppendLine("        { ");
            builder.AppendLine(" ");
            builder.AppendLine("            //MonitoringArea org = context.Set<MonitoringArea>().Add(new MonitoringArea { PID = 0, Name = \"江西\", X = 0, Y = 0, ImagePath = \"\" }); ");
            builder.AppendLine("            //context.SaveChanges(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");

            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path + @"\LCL\EFContexts\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\EFContext.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
