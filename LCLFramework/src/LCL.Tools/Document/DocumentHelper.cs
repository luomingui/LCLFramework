using LCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Document
{
    public class DocumentHelper
    {
        public static void GenerateDbTableFieldDescribe(Type[] types, string codeDir)
        {
            GetAllFilesInDirectory(codeDir);
            if (listFiles.Count > 0)
            {
                IList<PropertyDoc> list = new List<PropertyDoc>();
                foreach (var fileInfo in listFiles)
                {
                    string FileName = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    string filePath = fileInfo.FullName;

                    Type temp = null;
                    foreach (Type type in types)
                    {
                        //类名与文件名相等
                        if (type.Name == FileName)
                        {
                            temp = type;
                            break;
                        }
                    }
                    if (temp != null)
                    {
                        var parser = new PropertyDocParser();
                        parser.Types = types;
                        parser.TypeContext = temp;
                        parser.ClassContent = System.IO.File.ReadAllText(filePath, System.Text.Encoding.Default);
                        IList<PropertyDoc> item = parser.Parse();
                        Console.WriteLine(FileName);
                        if (!string.IsNullOrWhiteSpace(parser.Comment))
                        {
                            if (DbHelper.IsTableDec(FileName))
                            {
                                DbHelper.ExtendedProperty(true, parser.Comment, FileName, "");
                            }
                            else
                            {
                                DbHelper.ExtendedProperty(false, parser.Comment, FileName, "");
                            }
                        }
                        foreach (var pd in item)
                        {
                            if (pd.Comment.Length > 0)
                            {
                                if (DbHelper.IsProperty(FileName, pd.PropertyName))
                                {
                                    DbHelper.ExtendedProperty(true, pd.Comment, FileName, pd.PropertyName);
                                }
                                else
                                {
                                    DbHelper.ExtendedProperty(false, pd.Comment, FileName, pd.PropertyName);
                                }
                            }
                            Console.WriteLine(pd.ToString());
                        }
                        Console.WriteLine("============================================");
                    }
                }
            }
        }
        /// <summary>
        /// 返回指定目录下的所有文件信息
        /// </summary>
        /// <param name="strDirectory"></param>
        /// <returns></returns>
        private static List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息
        private static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles("*.cs");
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);

            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历
            }
            return listFiles;
        }
    }
    internal class DbHelper
    {
        public static string ConnectionString = "";
        public static void ExtendedProperty(bool addbyupdate, string describe, string tableName, string columnName)
        {
            string sql_propertyInfo = @"EXEC sys.{0} N'MS_Description',N'{1}',N'SCHEMA',N'dbo',N'TABLE',N'{2}',N'COLUMN',N'{3}'";
            string sql_propertyInfo1 = @"EXEC sys.{0} N'MS_Description',N'{1}',N'SCHEMA',N'dbo',N'TABLE',N'{2}'";
            string sql = "";
            if (addbyupdate)
            {
                if (columnName == null || columnName.Length == 0)
                {
                    sql = string.Format(sql_propertyInfo1, "sp_updateextendedproperty", describe, tableName);
                }
                else
                {
                    sql = string.Format(sql_propertyInfo, "sp_updateextendedproperty", describe, tableName, columnName);
                }
            }
            else
            {
                if (columnName == null || columnName.Length == 0)
                {
                    sql = string.Format(sql_propertyInfo1, "sp_addextendedproperty", describe, tableName);
                }
                else
                {

                    sql = string.Format(sql_propertyInfo, "sp_addextendedproperty", describe, tableName, columnName);
                }
            }
            if (sql.Length > 1)
                DbExecute(sql);
        }
        public static void DbExecute(string sql)
        {
            if (sql.Length == 0) return;
            try
            {
                //执行SQL
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConnectionString;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StringBuilder list = new StringBuilder();
                list.AppendLine("ConnectionString:" + ConnectionString);
                list.AppendLine("sql:" + sql);
                list.AppendLine("error:" + ex.Message);
            }
        }
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {

                }
                return ds;
            }
        }
        public static bool IsProperty(string TableName, string ColumnName)
        {
            DataTable dt = GetTablefiedDec();
            DataRow[] rows = dt.Select("TableName='" + TableName + "' and ColumnName='" + ColumnName + "'");

            if (rows.Length > 0)
                return true;
            else
                return false;
        }
        public static DataTable GetTablefiedDec()
        {
            string sql = @"SELECT  
    [TableName] = OBJECT_NAME(c.object_id), 
    [ColumnName] = c.name, 
    [Description] = ex.value  
FROM  
    sys.columns c  
LEFT OUTER JOIN  
    sys.extended_properties ex  
ON  
    ex.major_id = c.object_id 
    AND ex.minor_id = c.column_id  
    AND ex.name = 'MS_Description'  
WHERE  
    OBJECTPROPERTY(c.object_id, 'IsMsShipped')=0  
    AND ex.value IS NOT NULL
ORDER  
    BY OBJECT_NAME(c.object_id), c.column_id";

            return Query(sql).Tables[0];
        }

        public static bool IsTableDec(string TableName)
        {
            string sql = @"SELECT tbs.name 表名,ds.value 描述   
FROM sys.extended_properties ds  
LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
WHERE  ds.minor_id=0 and tbs.name='"+TableName+"' ";
           var dt= Query(sql).Tables[0];
           if (dt != null && dt.Rows.Count > 0)
               return true;
           else
               return false;
        }
    }
}
