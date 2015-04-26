using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class AdoNetBuild
    {
        public void GenerateDAL(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams")
                {
                    continue;
                }

                var ls = tm.Columns.Select<TableColumn, string>(e => e.ColumnName);
                string[] arr = ls.ToArray();
                string insert = string.Join(",", arr);

                List<string> _str = new List<string>();
                List<string> _str1 = new List<string>();
                for (int j = 0; j < arr.Length; j++)
                {
                    _str.Add("@" + arr[j]);
                    if (arr[j] != "ID")
                        _str1.Add(arr[j] + "=" + "@" + arr[j]);
                }
                arr = _str.ToArray();
                string _insert = string.Join(",", arr);
                arr = _str1.ToArray();
                string _update = string.Join(",", arr);

                StringBuilder builder = new StringBuilder();

                builder.AppendLine("using LCL.Data; ");
                builder.AppendLine("using System; ");
                builder.AppendLine("using System.Data; ");
                builder.AppendLine("using System.Text; ");

                builder.AppendLine("namespace " + Utils.NameSpace + " ");
                builder.AppendLine("{ ");
                builder.AppendLine("    public class " + tablename + "DAL ");
                builder.AppendLine("    { ");

                #region ===Add===
                builder.AppendLine("       public int Add(" + tablename + " entity) ");
                builder.AppendLine("       { ");
                builder.AppendLine("            string sql = @\"INSERT INTO " + tablename + " (" + insert + ")VALUES(" + _insert + ")\"; ");
                builder.AppendLine(" ");

                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName.Contains("_"))
                    {
                        string[] arro = item.ColumnName.Split('_');
                        builder.AppendLine("            sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + arro[0] + ".ID.ToString()); ");
                    }
                    else
                    {
                        if (item.ColumnType == "DateTime")
                        {
                            builder.AppendLine("            sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".Value.ToString(\"yyyy-MM-dd HH:mm:ss\")); ");
                        }
                        else if (item.ColumnType == "int")
                        {
                            builder.AppendLine("            sql = SqlUtil.setNumber(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".ToString()); ");
                        }
                        else
                        {
                            builder.AppendLine("            sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".ToString()); ");
                        }
                    }
                }

                builder.AppendLine("       ");
                builder.AppendLine("            int ret = DbFactory.DBA.ExecuteText(sql); ");
                builder.AppendLine("            return ret; ");
                builder.AppendLine("       } ");
                #endregion

                #region ==Del==
                builder.AppendLine("    public int Delete(Guid Id) ");
                builder.AppendLine("    { ");
                builder.AppendLine("       string sql = @\"delete from " + tablename + "  where id = @ID\"; ");
                builder.AppendLine("       sql = SqlUtil.setString(sql, \"@ID\", Id.ToString()); ");
                builder.AppendLine("       int ret = DbFactory.DBA.ExecuteText(sql); ");
                builder.AppendLine("       return ret; ");
                builder.AppendLine("    } ");
                #endregion

                #region ==Update==
                builder.AppendLine("    public int Update(" + tablename + " entity) ");
                builder.AppendLine("    { ");
                builder.AppendLine("            string sql = @\"UPDATE " + tablename + " SET " + _update + " WHERE ID=@ID\"; ");
                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName.Contains("_"))
                    {
                        string[] arro = item.ColumnName.Split('_');
                        builder.AppendLine("            sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + arro[0] + ".ID.ToString()); ");
                    }
                    else
                    {
                        if (item.ColumnType == "DateTime")
                        {
                            builder.AppendLine("         sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".Value.ToString(\"yyyy-MM-dd HH:mm:ss\")); ");
                        }
                        else if (item.ColumnType == "int")
                        {
                            builder.AppendLine("         sql = SqlUtil.setNumber(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".ToString()); ");
                        }
                        else
                        {
                            builder.AppendLine("         sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + ".ToString()); ");
                        }
                    }
                }
                builder.AppendLine("         sql = SqlUtil.setString(sql, \"@ID\", entity.ID.ToString()); ");
                builder.AppendLine("         int ret = DbFactory.DBA.ExecuteText(sql); ");
                builder.AppendLine("         return ret; ");
                builder.AppendLine("    } ");

                #endregion

                #region ==GetList==
                builder.AppendLine("		public DataTable GetList(string strWhere) ");
                builder.AppendLine("		{ ");
                builder.AppendLine("			StringBuilder strSql=new StringBuilder(); ");
                builder.AppendLine("			strSql.Append(\"select * FROM [" + tablename + "]\"); ");
                builder.AppendLine("			if(strWhere.Trim()!=\"\") ");
                builder.AppendLine("			{ ");
                builder.AppendLine("				strSql.Append(\" where \"+strWhere); ");
                builder.AppendLine("			} ");
                builder.AppendLine("			return DbFactory.DBA.QueryDataTable(strSql.ToString()); ");
                builder.AppendLine("		} ");

                #endregion

                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\DAL\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "DAL.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
            GenerateDba(path);
        }
        private void GenerateDba(string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using LCL.Data; ");
            builder.AppendLine("namespace " + Utils.NameSpace + " ");
            builder.AppendLine("{ ");
            builder.AppendLine("    public class DbFactory ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public static IDbAccesser DBA { ");
            builder.AppendLine("            get { ");
            builder.AppendLine("                return new DbAccesser(DbSetting.FindOrCreate(\"EDMS2015\")); ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");

            if (path.Length > 0)
            {
                string folder = path + @"\LCL\DAL\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\DbFactory.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
