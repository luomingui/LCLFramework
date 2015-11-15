using System;
using System.Collections.Generic;
using System.Text;

namespace LCL.Data
{
    /// <summary>
    /// string sql = @"select * from cb_cond_subject cmt where seq_id=@seq_id";
    ///        sql = SqlUtil.setString(sql, "@seq_id", CbcondsubjectId);
    /// </summary>
    public class SqlUtil
    {
        /// <summary>
        /// 判断Value是否为空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool isValueEmpty(object val)
        {
            bool result = false;
            if (val == null) return true;
            if (DBNull.Value.Equals(val)) return true;
            if (val.ToString().Trim() == "") return true;

            return result;
        }

        /// <summary>
        /// 判断Value是否为空或指定的值
        /// </summary>
        /// <param name="val"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static bool isValueEmpty(object val, object nullValue)
        {
            bool result = false;
            if (val == null) return true;
            if (DBNull.Value.Equals(val)) return true;
            if (val.ToString().Trim() == "") return true;
            if (val.Equals(nullValue)) return true;
            return result;
        }

        /// <summary>
        /// 字符串SQL文化
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string stringSql(string val)
        {
            string sqlStr = "";
            if (val != null) sqlStr = val.Replace("'", "''");
            sqlStr = "'" + val + "'";
            return sqlStr;
        }

        /// <summary>
        /// SQL中String参数替换
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string setString(string sql, string pName, string val)
        {
            string sqlVal = SqlUtil.stringSql(val);
            string sqlResult = sql.Replace(pName, sqlVal);

            return sqlResult;
        }

        /// <summary>
        /// SQL文中Number参数替换
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string setNumber(string sql, string pName, string val)
        {
            string sqlVal = val.Replace("'", "");
            if (sqlVal == "") sqlVal = " NULL ";
            string sqlResult = sql.Replace(pName, sqlVal);

            return sqlResult;
        }

        /// <summary>
        /// SQL文中一般参数替换
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string setValue(string sql, string pName, string val)
        {
            string sqlVal = val.Replace("'", "''");
            if (sqlVal == "") sqlVal = "";
            string sqlResult = sql.Replace(pName, sqlVal);

            return sqlResult;
        }

        /// <summary>
        /// SQL文中Oracle日期格式替换
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string setOracleYYYYMMDD(string sql, string pName, string val)
        {
            string sqlVal = val.Replace("'", "");
            if (sqlVal == "")
            {
                sqlVal = " NULL ";
            }
            else
            {
                sqlVal = "TO_DATE('" + sqlVal.Replace("'", "") + "', 'YYYYMMDD')";
            }
            string sqlResult = sql.Replace(pName, sqlVal);

            return sqlResult;
        }

        /// <summary>
        /// SQL文中SQLSERVER日期格式替换
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string setSQLSERVERDate(string sql, string pName, string val)
        {
            string sqlVal = val.Replace("'", "");
            if (sqlVal == "")
            {
                sqlVal = " NULL ";
            }
            else
            {
                sqlVal = "'" + sqlVal.Replace("'", "") + "'";
            }
            string sqlResult = sql.Replace(pName, sqlVal);

            return sqlResult;
        }
    }
}
