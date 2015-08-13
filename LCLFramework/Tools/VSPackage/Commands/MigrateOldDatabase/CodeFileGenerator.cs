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
        public ProjectItem ViewModels { get; set; }
        public ProjectItem ValidationModel { get; set; }
        public ProjectItem EFContexts { get; set; }
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
            BuildValidationModel(table);
            BuildViewModels(table);
            BuildEFContexts(table);

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
        public void BuildValidationModel(TableModel table)
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
        public void BuildViewModels(TableModel table)
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
        public void BuildEFContexts(TableModel table)
        {
            
        }
        private string _error;
        private void AddError(string error)
        {
            _error += error;
            _error += Environment.NewLine;
        }
    }
}
