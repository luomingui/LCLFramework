using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LCL.Plugins.WorkflowManage
{
    public class SQLiteDBHelper
    {
        private static string connStr = "data source=" + System.Environment.CurrentDirectory + "//sqllitedb.db3" + ";password=admin";
        private static string dbFile = "";
        public SQLiteDBHelper(string connectionString)
        {
            connStr = connectionString;

            if (string.IsNullOrWhiteSpace(dbFile))
            {
                //Oracle 中，把用户名（Schema）认为数据库名。
                var match = Regex.Match(connStr, @"data source=\s*(?<dbName>\w+)\s*");
                if (!match.Success)
                {
                    throw new NotSupportedException("无法解析出此数据库连接字符串中的数据库名：" + connStr);
                }
                dbFile = match.Groups["dbName"].Value;
            }
        }
        private static object lockobj = new object();
        public static bool IsDbExists()
        {
            if (!File.Exists(dbFile))
            {//数据库文件不存在
                return false;
            }
            return true;
        }
        public static int ExecuteText(string commandSql)
        {
            if (commandSql.Length == 0) return 0;
            int retval = 0;
            lock (lockobj)
            {
                if (IsDbExists())
                {
                    SQLiteConnection noteConn = Conn();
                    SQLiteCommand command = new SQLiteCommand();
                    try
                    {
                        command = noteConn.CreateCommand();
                        command.CommandTimeout = 5000;
                        command.CommandText = commandSql;
                        retval = command.ExecuteNonQuery();
                    }
                    finally
                    {
                        command.Dispose();
                        noteConn.Close();
                        noteConn.Dispose();
                    }
                }
            }
            return retval;
        }
        public static DataSet QueryDataSet(string commandSql)
        {
            if (commandSql.Length == 0) return null;
            DataSet ds = new DataSet();
            if (IsDbExists())
            {
                SQLiteConnection noteConn = Conn();
                SQLiteCommand command = noteConn.CreateCommand();
                command.CommandTimeout = 5000;
                command.CommandText = commandSql;
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
                command.Dispose();
                noteConn.Close();
            }
            return ds;
        }
        public static DataTable QueryDataTable(string commandSql)
        {
            DataSet ds = QueryDataSet(commandSql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public static object ExecuteScalar(string commandSql)
        {
            if (commandSql.Length == 0) return 0;
            object retval = 0;
            if (IsDbExists())
            {
                SQLiteConnection noteConn = Conn();
                SQLiteCommand command = noteConn.CreateCommand();
                command.CommandTimeout = 5000;
                command.CommandText = commandSql;
                retval = command.ExecuteScalar();
                command.Dispose();
                noteConn.Close();
            }
            return retval;
        }
        public static SQLiteDataReader QueryDataReader(string commandSql)
        {
            if (commandSql.Length == 0) return null;
            SQLiteDataReader retval = null;
            if (IsDbExists())
            {
                SQLiteConnection noteConn = Conn();
                SQLiteCommand command = noteConn.CreateCommand();
                command.CommandTimeout = 5000;
                command.CommandText = commandSql;
                retval = command.ExecuteReader();
            }
            return retval;
        }
        private static SQLiteConnection Conn()
        {
            //data source=sqllitedb.db3;password=admin
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteConnectionStringBuilder ConnStr = new SQLiteConnectionStringBuilder(connStr);
            conn.Open();
            return conn;
        }
        public static bool CreateSQLiteDb()
        {
            try
            {
                if (!IsDbExists())
                {
                    SQLiteConnection.CreateFile(dbFile);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
