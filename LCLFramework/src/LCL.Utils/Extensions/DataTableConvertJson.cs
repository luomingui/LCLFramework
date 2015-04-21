using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace LCL
{
    public class DataTableConvertJson
    {

        #region BUIJson
        public static string BUIGridResultsJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append("rows");
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("],");
            jsonBuilder.Append("\"results\": " + dt.Rows.Count + "");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        public static string BUIGridJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
        #endregion

        #region dataTable转换成Json格式
        /// <summary>  
        /// dataTable转换成Json格式  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        #endregion dataTable转换成Json格式

        #region DataSet转换成Json格式
        /// <summary>  
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="ds">DataSet</param> 
        /// <returns></returns>  
        public static string Dataset2Json(DataSet ds)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(DataTable2Json(dt));
                json.Append("}");
            } return json.ToString();
        }
        #endregion

        /// <summary>
        /// Msdn
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
    }
    /// < summary> 
     ////// DataTable扩展 
     /// </summary> 
    public static class DataTableExtension 
    { 
        /// <summary>
        /// 将DataTable转换为对应的实体数组 
        /// 实体属性与DataTable列对应
        /// </summary> 
        public static T[] ToArray<T>(this DataTable dataTable) where T : new() 
        { 
            if (dataTable == null || dataTable.Rows.Count == 0) 
                return new T[] { }; 
 
            // 定义集合 
            T[] array = new T[dataTable.Rows.Count]; 
 
            // 获得此模型的类型 
            Type type = typeof(T); 
 
            T t = new T(); 
            // 获得此模型的公共属性 
            PropertyInfo[] properties = t.GetType().GetProperties(); 
 
            for (int i = 0; i < dataTable.Rows.Count; i++) 
            { 
                array[i] = new T(); 
                foreach (PropertyInfo pi in properties) 
                { 
                    // 检查DataTable是否包含此列 
                    if (!dataTable.Columns.Contains(pi.Name)) 
                        continue; 
 
                    // 判断此属性是否有Setter 
                     if (!pi.CanWrite) 
                        continue; 
 
                    object value = dataTable.Rows[i][pi.Name];
                    if (value == DBNull.Value)
                        continue;
 
                    pi.SetValue(array[i], value, null);
               }
            }
            return array;
        }
    }
}