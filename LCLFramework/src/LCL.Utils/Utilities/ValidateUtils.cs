/**********************************************
 * 类作用：   验证实用类
 * 建立人：   
 * 建立时间： 2010-09-02 
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace LCL
{
    public class ValidateUtils
    {
        #region SQL注入的安全验证
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 替换sql的特殊字符
        /// </summary>
        /// <param name="str">要被处理的字符串</param>
        /// <returns></returns>
        public static string ReplaceSql(string str)
        {
            string badstr = "and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|set|;|from";

            string[] arraybad = badstr.Split('|');
            for (int i = 0; i < arraybad.Length; i++)
            {
                str = str.Replace(arraybad[i], "");
            }
            return filterSQL(str);
        }

        /// <summary> 
        /// 过滤SQL,所有涉及到输入的用户直接输入的地方都要使用。 
        /// </summary> 
        /// <param name="text">输入内容</param> 
        /// <returns>过滤后的文本</returns> 
        public static string filterSQL(string text)
        {
            text = text.Replace("'", "''");
            text = text.Replace("{", "{");
            text = text.Replace("}", "}");

            return text;
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
                return defValue;

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(strValue, out intValue);
            }
            return intValue;
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }
        #endregion
    }
}
