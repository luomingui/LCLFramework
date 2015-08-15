using EnvDTE;
using LCL.Data;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LCL.Tools;

namespace LCL.VSPackage.Commands.MigrateOldDatabase
{
    class CodeFileGenerator
    {
        private ProjectItem _directory;
        private ProjectItem _repoDirectory;

        public string DomainName { get; set; }
        public DbSetting DbSetting { get; set; }
        //entity
        public ProjectItem Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        public ProjectItem RepoDirectory
        {
            get { return _repoDirectory; }
            set { _repoDirectory = value; }
        }
        public ProjectItem EFContexts { get; set; }

        public ProjectItem ViewModels { get; set; }
        public ProjectItem ValidationModel { get; set; }
        public ProjectItem Controllers { get; set; }

        public bool IsMVC { get; set; }
        public int SuccessCount { get; private set; }

        private string DbName { get; set; }
        internal string Generate()
        {
            this.SuccessCount = 0;
            _error = null;
            try
            {
                var dba = DALFactory.Factory(this.DbSetting);
                var dbMeta = dba.GetDatabaseModel();
                DbName = this.DbSetting.Database;

                foreach (var table in dbMeta.Tables)
                {
                    var res = this.GenerateClassFile(table);
                    if (res) this.SuccessCount++;
                }
                BuildEFContexts(dbMeta.Tables);
                return _error;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private bool GenerateClassFile(TableModel table)
        {
            if (table == null) return false;
            if (table.TableName == "__MigrationHistory") return false;
            var item = _directory.ProjectItems.FindByName(table.TableName);
            if (item != null) return false;
            if (string.IsNullOrWhiteSpace(table.TablePK))
            {
                AddError(string.Format("{0} 表中没有自增、命名以 ID 结尾的Guid列。", table.TableName));
                return false;
            }
            if (table.Columns == null || table.Columns.Count == 0)
            {
                AddError(string.Format("{0} 表中没有列。", table.TableName));
                return false;
            }

            BuildEntity(table);
            BuildRepository(table);
            if (IsMVC)
            {
                BuildValidationModel(table);
                BuildViewModels(table);
                BuildControllers(table);
            }

            return true;
        }
        private void BuildEntity(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("/*******************************************************   ");
            builder.AppendLine("*    ");
            builder.AppendLine("* 作者：罗敏贵   ");
            builder.AppendLine("* 说明：   ");
            builder.AppendLine("* 运行环境：.NET 4.5.0   ");
            builder.AppendLine("* 版本号：1.0.0   ");
            builder.AppendLine("*    ");
            builder.AppendLine("* 历史记录：   ");
            builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now + "  ");
            builder.AppendLine("*    ");
            builder.AppendLine("*******************************************************/   ");
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.Collections.Generic; ");
            builder.AppendLine("using System.Linq; ");
            builder.AppendLine("using System.Text; ");
            builder.AppendLine("using LCL; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace " + this.DomainName + " ");
            builder.AppendLine("{ ");
            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// " + table.TableNameRemark + " ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public partial class " + table.TableName + " : Entity ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public " + table.TableName + "() ");
            builder.AppendLine("        { ");
            builder.AppendLine("             ");
            builder.AppendLine("        } ");
            foreach (TableColumn column in table.Columns)
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

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(_directory.get_FileNames(1)), table.TableName) + ".cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                _directory.ProjectItems.AddFromFile(file);
            }
        }
        private void BuildRepository(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("/*******************************************************   ");
            builder.AppendLine("*    ");
            builder.AppendLine("* 作者：罗敏贵   ");
            builder.AppendLine("* 说明：   ");
            builder.AppendLine("* 运行环境：.NET 4.5.0   ");
            builder.AppendLine("* 版本号：1.0.0   ");
            builder.AppendLine("*    ");
            builder.AppendLine("* 历史记录：   ");
            builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now + "  ");
            builder.AppendLine("*    ");
            builder.AppendLine("*******************************************************/   ");
            builder.AppendLine("using LCL;   ");
            builder.AppendLine("using LCL.Repositories;   ");
            builder.AppendLine("using LCL.Repositories.EntityFramework; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace " + this.DomainName + " ");
            builder.AppendLine("{   ");
            builder.AppendLine("    public interface I" + table.TableName + "Repository : IRepository<" + table.TableName + ">   ");
            builder.AppendLine("    {   ");
            builder.AppendLine("   ");
            builder.AppendLine("    }   ");
            builder.AppendLine("    public class " + table.TableName + "Repository : EntityFrameworkRepository<" + table.TableName + ">, I" + table.TableName + "Repository   ");
            builder.AppendLine("    {   ");
            builder.AppendLine("        public " + table.TableName + "Repository(IRepositoryContext context) : base(context) ");
            builder.AppendLine("        {    ");
            builder.AppendLine("           ");
            builder.AppendLine("        }   ");
            builder.AppendLine("    }   ");
            builder.AppendLine("}   ");

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(RepoDirectory.get_FileNames(1)), table.TableName) + ".cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                RepoDirectory.ProjectItems.AddFromFile(file);
            }
        }
        private void BuildValidationModel(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.ComponentModel.DataAnnotations; ");
            builder.AppendLine("using System.Linq; ");
            builder.AppendLine("using System.Text; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace " + this.DomainName + " ");
            builder.AppendLine("{ ");
            builder.AppendLine("    [Serializable] ");
            builder.AppendLine("    [MetadataType(typeof(" + table.TableName + "MD))]  ");
            builder.AppendLine("    public partial class " + table.TableName + "  ");
            builder.AppendLine("    {  ");
            builder.AppendLine("        public class " + table.TableName + "MD  ");
            builder.AppendLine("        {  ");
            foreach (var item in table.Columns)
            {
                if (item.ColumnName != "ID"
                    && item.ColumnName != "AddDate"
                    && item.ColumnName != "UpdateDate"
                    && item.ColumnName != "IsDelete"
                    && !item.ColumnName.Contains("_"))
                {
                    builder.AppendLine("            [Display(Name = \"" + item.ColumnRemark + "\")]  ");
                    builder.AppendLine("            public " + item.ColumnType + " " + item.ColumnName + " { get; set; }  ");
                }
            }
            builder.AppendLine("        }  ");
            builder.AppendLine("    }  ");
            builder.AppendLine(" ");
            builder.AppendLine("} ");

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(ValidationModel.get_FileNames(1)), table.TableName) + ".cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                ValidationModel.ProjectItems.AddFromFile(file);
            }
        }
        private void BuildViewModels(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System.Collections.Generic; ");
            builder.AppendLine("using " + this.DomainName + "; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace LCL.MvcExtensions ");
            builder.AppendLine("{ ");
            builder.AppendLine("   public class " + table.TableName + "AddOrEditViewModel : AddOrEditViewModel<" + table.TableName + "> ");
            builder.AppendLine("    { ");
            builder.AppendLine("        ");
            builder.AppendLine("    } ");

            builder.AppendLine("   public class " + table.TableName + "PagedListViewModel : PagedListViewModel<" + table.TableName + "> ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public " + table.TableName + "PagedListViewModel(int currentPageNum, int pageSize, List<" + table.TableName + "> allModels) ");
            builder.AppendLine("            : base(currentPageNum, pageSize, allModels) ");
            builder.AppendLine("        { ");
            builder.AppendLine(" ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine(" ");
            builder.AppendLine("} ");

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(ViewModels.get_FileNames(1)), table.TableName) + ".cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                ViewModels.ProjectItems.AddFromFile(file);
            }
        }
        private void BuildEFContexts(List<TableModel> tables)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("using System.Data.Entity; ");
            builder.AppendLine("using System.Data.Entity.ModelConfiguration.Conventions; ");
            builder.AppendLine("using System.Linq; ");

            builder.AppendLine("namespace " + this.DomainName + " ");
            builder.AppendLine("{ ");
            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// 数据上下文 Db" + DbName + "Context ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public class Db" + DbName + "Context : DbContext ");
            builder.AppendLine("    { ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 构造函数 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        public Db" + DbName + "Context() ");
            builder.AppendLine("            : base(\"" + DbName + "\") ");
            builder.AppendLine("        { ");
            builder.AppendLine("        } ");
            builder.AppendLine("#region 属性 ");
            for (int i = 0; i < tables.Count; i++)
            {
                TableModel tm = tables[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                builder.AppendLine("        /// <summary> ");
                builder.AppendLine("        /// " + tableInfo + " ");
                builder.AppendLine("        /// </summary> ");
                builder.AppendLine("        public DbSet<" + tablename + "> " + tablename + " { get; set; } ");
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
            builder.AppendLine("            using (var db = new Db" + DbName + "Context()) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                db.Database.Initialize(false); ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");

            builder.AppendLine("    /// <summary> ");
            builder.AppendLine("    /// 数据库初始化策略 ");
            builder.AppendLine("    /// </summary> ");
            builder.AppendLine("    public class SampleData : CreateDatabaseIfNotExists<Db" + DbName + "> ");
            builder.AppendLine("    { ");
            builder.AppendLine("        protected override void Seed(Db" + DbName + " context) ");
            builder.AppendLine("        { ");
            builder.AppendLine(" ");
            builder.AppendLine("            //MonitoringArea org = context.Set<MonitoringArea>().Add(new MonitoringArea { PID = 0, Name = \"江西\", X = 0, Y = 0, ImagePath = \"\" }); ");
            builder.AppendLine("            //context.SaveChanges(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");

            builder.AppendLine("} ");

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(EFContexts.get_FileNames(1)), DbName) + ".cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                EFContexts.ProjectItems.AddFromFile(file);
            }
        }
        private void BuildControllers(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("/******************************************************* ");
            builder.AppendLine("*  ");
            builder.AppendLine("* 作者：罗敏贵 ");
            builder.AppendLine("* 说明： " + table.TableNameRemark);
            builder.AppendLine("* 运行环境：.NET 4.5.0 ");
            builder.AppendLine("* 版本号：1.0.0 ");
            builder.AppendLine("*  ");
            builder.AppendLine("* 历史记录： ");
            builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString());
            builder.AppendLine("*  ");
            builder.AppendLine("*******************************************************/ ");
            builder.AppendLine("using LCL.MvcExtensions; ");
            builder.AppendLine("using LCL.Repositories;");
            builder.AppendLine("using LCL;");
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.Collections.Generic; ");
            builder.AppendLine("using System.Linq; ");
            builder.AppendLine("using System.Web; ");
            builder.AppendLine("using System.Web.Mvc; ");
            builder.AppendLine("using UIShell.RbacPermissionService; ");
            builder.AppendLine(" ");
            builder.AppendLine("namespace " + this.DomainName + ".Controllers ");
            builder.AppendLine("{ ");
            builder.AppendLine("    public class " + table.TableName + "Controller : RbacController<" + table.TableName + "> ");
            builder.AppendLine("    { ");
            var list = table.Columns.Where(e => e.ColumnName.Contains("_"));
            StringBuilder ddl = new StringBuilder();
            StringBuilder crto = new StringBuilder();
            foreach (var item in list)
            {
                crto.AppendLine("       ddl" + item.ColumnName.Remove(item.ColumnName.Length - 3) + "(Guid.Empty); ");

                ddl.AppendLine("        public void ddl" + item.ColumnName.Remove(item.ColumnName.Length - 3) + "(Guid dtId) ");
                ddl.AppendLine("        { ");
                ddl.AppendLine("            var repo = RF.FindRepo<" + item.ColumnName.Remove(item.ColumnName.Length - 3) + ">(); ");
                ddl.AppendLine("            var list = repo.FindAll(); ");
                ddl.AppendLine(" ");
                ddl.AppendLine("            List<SelectListItem> selitem = new List<SelectListItem>(); ");
                ddl.AppendLine("            if (list.Count() > 0) ");
                ddl.AppendLine("            { ");
                if (table.IsTree)
                {
                    ddl.AppendLine("                var roots = list.Where(e => e.ParentId == Guid.Empty); ");
                    ddl.AppendLine("                foreach (var item in roots) ");
                    ddl.AppendLine("                { ");
                    ddl.AppendLine("                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() }); ");
                    ddl.AppendLine("                    var child = list.Where(p => p.ParentId == item.ID); ");
                    ddl.AppendLine("                    foreach (var item1 in child) ");
                    ddl.AppendLine("                    { ");
                    ddl.AppendLine("                        if (dtId == item1.ID) ");
                    ddl.AppendLine("                        { ");
                    ddl.AppendLine("                            selitem.Add(new SelectListItem { Text = \"----\" + item1.Name, Value = item1.ID.ToString(), Selected = true }); ");
                    ddl.AppendLine("                            this.ViewData[\"selected\"] = item1.ID.ToString(); ");
                    ddl.AppendLine("                        } ");
                    ddl.AppendLine("                        else ");
                    ddl.AppendLine("                        { ");
                    ddl.AppendLine("                            selitem.Add(new SelectListItem { Text = \"----\" + item1.Name, Value = item1.ID.ToString() }); ");
                    ddl.AppendLine("                        } ");
                    ddl.AppendLine("                    } ");
                    ddl.AppendLine("                } ");
                    ddl.AppendLine("            } ");
                }
                else
                {
                    ddl.AppendLine("                var roots = list; ");
                    ddl.AppendLine("                foreach (var item in roots) ");
                    ddl.AppendLine("                { ");
                    ddl.AppendLine("                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() }); ");
                    ddl.AppendLine("                } ");
                    ddl.AppendLine("            } ");
                }
                ddl.AppendLine("            selitem.Insert(0, new SelectListItem { Text = \"==" + item.ColumnRemark + "==\", Value = \"-1\" }); ");
                ddl.AppendLine("            ViewData[\"ddl" + table.TableName + "\"] = selitem; ");
                ddl.AppendLine("        } ");
            }
            builder.AppendLine("        public " + table.TableName + "Controller() ");
            builder.AppendLine("        { ");
            builder.AppendLine(crto.ToString());
            builder.AppendLine("        } ");
            builder.AppendLine(ddl.ToString());
            builder.AppendLine(" ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");

            //写到文件，并加入到项目中。
            var file = Path.Combine(Path.GetDirectoryName(Controllers.get_FileNames(1)), table.TableName) + "Controller.cs";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, builder.ToString());
                Controllers.ProjectItems.AddFromFile(file);
            }
        }

        private string _error;
        private void AddError(string error)
        {
            _error += error;
            _error += Environment.NewLine;
        }
    }
}
