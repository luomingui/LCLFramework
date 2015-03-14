using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SF.Tools
{
    public class BuildBase
    {
        public INIFile datatype = null;
        public BuildBase()
        {
            string path = Application.StartupPath + @"\datatype.ini";
            if (!File.Exists(path))
            {
                Utils.CreateFiles(path, Utils.ini);
            }
            datatype = new INIFile(path);
        }
        // Entity or WCF
        public virtual string BuildEntity(string path, TableModel tableModel, System.Windows.Forms.ProgressBar progressBar)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.ComponentModel; ");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("using System.Runtime.Serialization;");
            builder.AppendLine("namespace " + Utils.NameSpace);
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
        // App.config
        public virtual void BuildConfig(string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\"?> ");
            builder.AppendLine("<configuration> ");
            builder.AppendLine("  <connectionStrings> ");
            builder.AppendLine("    <add name=\"" + Utils.dbName + "\" connectionString=\"Data Source=" + Utils.Sqlserver + ";Initial Catalog=" + Utils.dbName + ";User ID=sa;Password=" + Utils.Pwd + ";Persist Security Info=True\" providerName=\"System.Data.SqlClient\" /> ");
            builder.AppendLine("  </connectionStrings> ");
            builder.AppendLine("  <runtime> ");
            builder.AppendLine("    <assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"> ");
            builder.AppendLine("      <probing privatePath=\"Library;Module;plugins\"/> ");
            builder.AppendLine("    </assemblyBinding> ");
            builder.AppendLine("  </runtime> ");
            builder.AppendLine("</configuration> ");
            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\App.config";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
        //Plugin MyMenus
        public virtual void BuildMyMenusClass(string path, List<TableModel> tableNames)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using MinGuiLuo; ");
            builder.AppendLine("using MinGuiLuo.MetaModel; ");
            builder.AppendLine("using MinGuiLuo.PluginSystem; ");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("namespace " + Utils.NameSpace);
            builder.AppendLine("{ ");
            builder.AppendLine("    public class MyMenus : ModulePlugin ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public override void Initialize(IClientApp app) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            app.ModuleOperations += (o, e) => ");
            builder.AppendLine("            { ");
            builder.AppendLine("                var moduleBookImport = CommonModel.Modules.AddRoot(new ModuleMeta ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    Label = \"" + Utils.dbName + "\", ");
            builder.AppendLine("                    Children = ");
            builder.AppendLine("                    { ");
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                builder.AppendLine("                        new ModuleMeta{ Label = \"" + tableInfo + "\",UIFromType=typeof(" + tablename + "Frm)}, ");
            }
            builder.AppendLine("                    } ");
            builder.AppendLine("                }); ");
            builder.AppendLine("            }; ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path;
                Utils.FolderCheck(folder);
                string filename = folder + @"\MyMenus.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
