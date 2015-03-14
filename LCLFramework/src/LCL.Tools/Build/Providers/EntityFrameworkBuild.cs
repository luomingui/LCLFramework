using System.Collections.Generic;
using System.Text;

namespace SF.Tools
{
    //ClassLib
    public class EntityFrameworkBuild : BuildBase, IBuild
    {
        public string Library(string path, TableModel tableModel, System.Windows.Forms.ProgressBar progressBar)
        {
            //Entity
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.ComponentModel; ");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("using System.Runtime.Serialization;");
            builder.AppendLine("namespace " + Utils.NameSpaceEntities);
            builder.AppendLine("{ ");
            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// 实体类" + tableModel.TableNameRemark + " 。(属性说明自动提取数据库字段的描述信息) ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    [Serializable]");
            builder.AppendLine("    public class " + tableModel.TableName + ":Entity<" + tableModel.TableName + ">");
            builder.AppendLine("    { ");
            int num = 0;
            if (progressBar != null)
                progressBar.Maximum = tableModel.Columns.Count;
            foreach (TableColumn column in tableModel.Columns)
            {
                num++;
                if (progressBar != null)
                    progressBar.Value = num;
                builder.AppendLine("        public " + column.ColumnType + " " + column.ColumnName + " { get; set; } ");
            }
            builder.AppendLine("        public static string ConnectionString = \"" + Utils.dbName + "\"; ");
            builder.AppendLine("        protected override string ConnectionStringSettingName{ get { return ConnectionString; }} ");

            builder.AppendLine("    } ");
            builder.AppendLine("} ");
            if (path != null && path.Length > 0)
            {
                string folder = path + @"\Entities";
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + tableModel.TableName + ".cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
            return builder.ToString();
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
                string folder = path + @"\Entities\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + Utils.dbName + "Context.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
