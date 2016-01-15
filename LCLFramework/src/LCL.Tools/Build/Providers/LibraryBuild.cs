using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tools
{
    class LibraryBuild
    {
        internal void GenerateLibBuild(string path)
        {
            BuildRepository(path);
            BuildDbContext(path);
            BuildEntity(path);
            BuildLCLPlugin(path);
            BuildSpecification(path);
        }
        private void BuildRepository(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/*******************************************************  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 作者：罗敏贵  ");
                builder.AppendLine("* 说明： " + tableInfo + " ");
                builder.AppendLine("* 运行环境：.NET 4.5.0  ");
                builder.AppendLine("* 版本号：1.0.0  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 历史记录：  ");
                builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString() + " ");
                builder.AppendLine("*   ");
                builder.AppendLine("*******************************************************/  ");
                builder.AppendLine("using LCL.Repositories;  ");
                builder.AppendLine("using LCL.Repositories.EntityFramework;  ");
                builder.AppendLine("using System;  ");
                builder.AppendLine("using System.Collections.Generic;  ");
                builder.AppendLine("using System.Linq;  ");
                builder.AppendLine("using System.Text;  ");
                builder.AppendLine("using System.Threading.Tasks;  ");
                builder.AppendLine("  ");
                builder.AppendLine("namespace " + Utils.NameSpace + "  ");
                builder.AppendLine("{  ");
                builder.AppendLine("    /// <summary> ");
                builder.AppendLine("    /// " + tableInfo + " ");
                builder.AppendLine("    /// </summary> ");
                builder.AppendLine("    public interface I" + tablename + "Repository : IRepository<" + tablename + ">  ");
                builder.AppendLine("    {  ");
                builder.AppendLine("  ");
                builder.AppendLine("    }  ");
                builder.AppendLine("    /// <summary> ");
                builder.AppendLine("    /// " + tableInfo + " ");
                builder.AppendLine("    /// </summary> ");
                builder.AppendLine("    public class " + tablename + "Repository : EntityFrameworkRepository<" + tablename + ">, I" + tablename + "Repository  ");
                builder.AppendLine("    {  ");
                builder.AppendLine("        public " + tablename + "Repository(IRepositoryContext context)  ");
                builder.AppendLine("            : base(context)  ");
                builder.AppendLine("        {   ");
                builder.AppendLine("          ");
                builder.AppendLine("        }  ");
                builder.AppendLine("    }  ");
                builder.AppendLine("}  ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Library\Repository\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "Repository.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }

        }
        private void BuildDbContext(string path)
        {
            StringBuilder builder = new StringBuilder();
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);

            builder.AppendLine("using System.Data.Entity; ");
            builder.AppendLine("using System.Data.Entity.ModelConfiguration.Conventions; ");
            builder.AppendLine("using System.Linq; ");

            builder.AppendLine("namespace " + Utils.NameSpace);
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
            builder.AppendLine("    public class SampleData : CreateDatabaseIfNotExists<Db" + Utils.dbName + "> ");
            builder.AppendLine("    { ");
            builder.AppendLine("        protected override void Seed(Db" + Utils.dbName + " context) ");
            builder.AppendLine("        { ");
            builder.AppendLine(" ");
            builder.AppendLine("            //MonitoringArea org = context.Set<MonitoringArea>().Add(new MonitoringArea { PID = 0, Name = \"江西\", X = 0, Y = 0, ImagePath = \"\" }); ");
            builder.AppendLine("            //context.SaveChanges(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");

            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path + @"\LCL\Library\EFContexts\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\EFContext.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
        private void BuildEntity(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);

            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/*******************************************************  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 作者：罗敏贵  ");
                builder.AppendLine("* 说明： " + tableInfo + " ");
                builder.AppendLine("* 运行环境：.NET 4.5.0  ");
                builder.AppendLine("* 版本号：1.0.0  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 历史记录：  ");
                builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString() + " ");
                builder.AppendLine("*   ");
                builder.AppendLine("*******************************************************/  ");
                builder.AppendLine("using System; ");
                builder.AppendLine("using System.Collections.Generic; ");
                builder.AppendLine("using System.Linq; ");
                builder.AppendLine("using System.Text; ");
                builder.AppendLine("using System.Threading.Tasks; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace " + Utils.NameSpace + " ");
                builder.AppendLine("{ ");
                builder.AppendLine("    /// <summary> ");
                builder.AppendLine("    /// " + tableInfo + " ");
                builder.AppendLine("    /// </summary> ");
                builder.AppendLine("    public partial class " + tablename + " : BaseModel ");
                builder.AppendLine("    { ");
                builder.AppendLine("        public " + tablename + "() ");
                builder.AppendLine("        { ");
                builder.AppendLine("             ");
                builder.AppendLine("        } ");
                foreach (TableColumn column in tm.Columns)
                {
                    builder.AppendLine("        /// <summary> ");
                    builder.AppendLine("        /// " + column.ColumnRemark + " ");
                    builder.AppendLine("        /// </summary> ");
                    builder.AppendLine("        public " + column.ColumnType + " " + column.ColumnName + " { get; set; } ");
                }
                builder.AppendLine("        ");
                builder.AppendLine(" ");
                builder.AppendLine(" ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Library\Entity\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + ".cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
        private void BuildLCLPlugin(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using LCL; ");
            builder.AppendLine("using LCL.ComponentModel; ");
            builder.AppendLine("using LCL.Repositories; ");
            builder.AppendLine("using LCL.Repositories.EntityFramework; ");
            builder.AppendLine("using System.Data.Entity; ");
            builder.AppendLine("using System.Diagnostics; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace " + Utils.NameSpace + " ");
            builder.AppendLine("{ ");
            builder.AppendLine("    public class PluginActivator : LCLPlugin ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public override void Initialize(IApp app) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            app.ModuleOperations += app_ModuleOperations; ");
            builder.AppendLine("            app.AllPluginsIntialized += app_AllPluginsIntialized; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        void app_ModuleOperations(object sender, AppInitEventArgs e) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            ");

            builder.AppendLine("               CommonModel.Modules.AddRoot(new MvcModuleMeta ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    Label = \"" + Utils.dbName + "\", ");
            builder.AppendLine("                    Image = \".icon-application\", ");
            builder.AppendLine("                    Bundle = this, ");
            builder.AppendLine("                    Children = ");
            builder.AppendLine("                    { ");
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                builder.AppendLine("                        new MvcModuleMeta{ Image = \".icon-application\",  Label = \"" + tableInfo + "\", CustomUI=\"/"+Utils.NameSpace+"/"+tablename+"/Index\",UIFromType=typeof(" + tablename + "Controller)}, ");
            }
            builder.AppendLine("                    } ");
            builder.AppendLine("                }); ");

            builder.AppendLine("        } ");
            builder.AppendLine("        void app_AllPluginsIntialized(object sender, System.EventArgs e) ");
            builder.AppendLine("        { ");
            builder.AppendLine(" ");
            StringBuilder ben = new StringBuilder();
            StringBuilder end = new StringBuilder();
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename != "__MigrationHistory" && tablename != "sysdiagrams")
                {
                    //ben.AppendLine("ServiceLocator.Instance.Register<IRepository<" + tablename + ">, EntityFrameworkRepository<" + tablename + ">>();");

                    end.AppendLine("ServiceLocator.Instance.Register<I" + tablename + "Repository, " + tablename + "Repository>();");
                }
            }

            builder.AppendLine(" #region 默认仓库 ");
            //builder.AppendLine(ben.ToString());
            builder.AppendLine(" #endregion");
            builder.AppendLine(" #region 扩展仓库 ");
            builder.AppendLine(end.ToString());
            builder.AppendLine(" #endregion");

            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");

            if (path.Length > 0)
            {
                string folder = path + @"\LCL\Library\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\BundleActivator.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
        private void BuildSpecification(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/*******************************************************  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 作者：罗敏贵  ");
                builder.AppendLine("* 说明： " + tableInfo + " ");
                builder.AppendLine("* 运行环境：.NET 4.5.0  ");
                builder.AppendLine("* 版本号：1.0.0  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 历史记录：  ");
                builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString() + " ");
                builder.AppendLine("*   ");
                builder.AppendLine("*******************************************************/  ");
                builder.AppendLine("using System; ");
                builder.AppendLine("using LCL.Specifications; ");
                builder.AppendLine("using System.Linq.Expressions; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace "+Utils.NameSpace+" ");
                builder.AppendLine("{ ");
                builder.AppendLine("     /// <summary> ");
                builder.AppendLine("    ///  根据关键字进行查询 ");
                builder.AppendLine("    /// </summary> ");
                builder.AppendLine("    public class Key" + tablename + "Specification : Specification<" + tablename + "> ");
                builder.AppendLine("    { ");
                builder.AppendLine("        string _keyword = \"\"; ");
                builder.AppendLine("        string _fieldName = \"\"; ");
                builder.AppendLine("        public Key" + tablename + "Specification(string keyword, string fieldName) ");
                builder.AppendLine("        { ");
                builder.AppendLine("            _keyword = keyword; ");
                builder.AppendLine("            _fieldName = fieldName; ");
                builder.AppendLine("        } ");
                builder.AppendLine("        public override Expression<Func<" + tablename + ", bool>> GetExpression() ");
                builder.AppendLine("        { ");
                builder.AppendLine("            Expression<Func<" + tablename + ", bool>> spec = null; ");
                builder.AppendLine("            switch (_fieldName) ");
                builder.AppendLine("            { ");
                foreach (TableColumn column in tm.Columns)
                {
                    builder.AppendLine("                case \"" + column.ColumnName + "\": // " + column.ColumnRemark + "");
                    switch (column.ColumnType.ToLower())
                    {
                        case "string":
                            builder.AppendLine("                    spec = c => c." + column.ColumnName + ".IndexOf(_keyword) != 0; ");
                            break;
                        case "int":
                            builder.AppendLine("                    spec = c => c." + column.ColumnName + "==Convert.ToInt32( _keyword); ");
                            break;
                        case "bool":
                            builder.AppendLine("                    spec = c => c." + column.ColumnName + "==Convert.ToBoolean( _keyword); ");
                            break;
                        case "guid":
                            builder.AppendLine("                    spec = c => c." + column.ColumnName + "== Guid.Parse(_keyword); ");
                            break;
                        case "datetime":
                            builder.AppendLine("                    spec = c => c." + column.ColumnName + "==Convert.ToDateTime( _keyword); ");
                            break;
                    }
                    builder.AppendLine("                    break; ");
                }
                builder.AppendLine("            } ");
                builder.AppendLine("            return spec; ");
                builder.AppendLine("        } ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Library\Specifications\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\Key" + tablename + "Specification.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
    }
}
