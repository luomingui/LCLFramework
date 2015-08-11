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
        public int SuccessCount { get; private set; }

        internal string Generate()
        {
            this.SuccessCount = 0;
            _error = null;
            try
            {
                var dba = DALFactory.Factory(this.DbSetting);
                var dbMeta = dba.GetDatabaseModel();
                foreach (var table in dbMeta.Tables)
                {
                    var res = this.GenerateClassFile(table);
                    if (res) this.SuccessCount++;
                }
                return _error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private bool GenerateClassFile(TableModel table)
        {
            if (table.TableName == "zzzDbMigrationVersion") return false;
            var item = _directory.ProjectItems.FindByName(table.TableName);
            if (item != null) return false;
            if (string.IsNullOrWhiteSpace(table.TablePK))
            {
                AddError(string.Format("{0} 表中没有自增、命名以 ID 结尾的Guid列。", table.TableName));
                return false;
            }

            BuildEntity(table);

            return true;
        }

        private void BuildEntity(TableModel table)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.Collections.Generic; ");
            builder.AppendLine("using System.Linq; ");
            builder.AppendLine("using System.Text; ");
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
            var file = Path.Combine(Path.GetDirectoryName(_directory.get_FileNames(1)), table.TableName);
            File.WriteAllText(file, builder.ToString());
            _directory.ProjectItems.AddFromFile(file);
        }


        private string _error;
        private void AddError(string error)
        {
            _error += error;
            _error += Environment.NewLine;
        }
    }
}
