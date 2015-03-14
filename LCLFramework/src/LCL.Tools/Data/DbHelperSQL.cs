
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SF.Tools.Data
{
    public class DbHelperSQL
    {
        #region DBHelper
        public INIFile datatype = null;
        private SqlConnection connect;
        private string _dbconnectStr;
        public DbHelperSQL( )
        {
            this.connect = new SqlConnection();
            this._dbconnectStr = Utils.ConnStr;
            this.connect.ConnectionString = this._dbconnectStr;
            Init();
        }
        private void Init( )
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\datatype.ini";
            if (!File.Exists(path))
            {
                Utils.CreateFiles(path, Utils.ini);
            }
            datatype = new INIFile(path);
        }
        public SqlDataReader ExecuteReader(string DbName, string strSQL)
        {
            SqlDataReader reader2;
            try
            {
                this.OpenDB(DbName);
                reader2 = new SqlCommand(strSQL, this.connect).ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return reader2;
        }
        public DataSet Query(string DbName, string SQLString)
        {
            if (SQLString.Length == 0) return null;
            DataSet dataSet = new DataSet();
            try
            {
                this.OpenDB(DbName);
                new SqlDataAdapter(SQLString, this.connect).Fill(dataSet, "ds");
            }
            catch (SqlException exception)
            {
                throw new Exception(exception.Message);
            }
            return dataSet;
        }
        private SqlCommand OpenDB(string DbName)
        {
            try
            {
                if (this.connect.ConnectionString == "")
                {
                    this.connect.ConnectionString = this._dbconnectStr;
                }
                if (this.connect.ConnectionString != this._dbconnectStr)
                {
                    this.connect.Close();
                    this.connect.ConnectionString = this._dbconnectStr;
                }
                SqlCommand command = new SqlCommand();
                command.Connection = this.connect;
                if (this.connect.State == ConnectionState.Closed)
                {
                    this.connect.Open();
                }
                command.CommandText = "use [" + DbName + "]";
                command.ExecuteNonQuery();
                return command;
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                return null;
            }
        }
        public void ExceSql(string sql)
        {
            try
            {
                if (this.connect.State == ConnectionState.Closed)
                {
                    this.connect.Open();
                }
                SqlCommand command = OpenDB(Utils.dbName);
                command.Connection = this.connect;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        //C#中，如何将DataRow[]转换DataTable?
        public DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = new DataTable();// rows[0].Table.Clone();  // 复制DataRow的表结构
            foreach (DataRow row in rows)
                tmp.Rows.Add(row);  // 将DataRow添加到DataTable中
            return tmp;
        }
    }
}
