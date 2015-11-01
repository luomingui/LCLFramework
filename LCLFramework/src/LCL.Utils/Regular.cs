using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LCL
{
    public class Regular
    {
        #region 验证的字符串
        /// <summary>
        /// 判断输入的字符类型　
        /// </summary>
        /// <param name="_value">输入的字串</param>
        /// <param name="_kind">要验证的类型</param>
        /// 1: 由26个英文字母组成的字串 
        /// 2: 正整数 
        /// 3: 非负整数（正整数 + 0)
        /// 4: 非正整数（负整数 + 0）
        /// 5: 负整数 
        /// 6: 整数
        /// 7: 非负浮点数（正浮点数 + 0）
        /// 8: 正浮点数
        /// 9: 非正浮点数（负浮点数 + 0)
        /// 10: 负浮点数 
        /// 11: 浮点数
        /// 12: 由26个英文字母的大写组成的字串
        /// 13: 由26个英文字母的小写组成的字串
        /// 14: 由数字和26个英文字母组成的字串
        /// 15: 由数字、26个英文字母或者下划线组成的字串
        /// 16: Email
        /// 17: URL
        /// 18: 只能输入入中文
        /// 19: 只能输入0和非0打头的数字
        /// 20: 只能输入数字
        /// 21: 只能输入数字加2位小数
        /// 22: 只能输入0和非0打头的数字加2位小数
        /// 23: 只能输入0和非0打头的数字加2位小数,但不匹配0.00
        /// 24：验证固话：区号2-5位，电话号码7-8位，分机号一位以上，例如：010-23658974(12);
        ///     150、151、152、153、155、156、157、158、159                  九个
        ///     130、131、132、133、134、135、136、137、138、139             十个
        ///     180、182、185、186、187、188、189                            七个
        /// 25: IP地址
        /// 26: MAC地址:8位0-9，A-F的数字或者字母；
        /// </param>
        /// <returns>true是验证通过,false是验证错误</returns>
        /// <returns></returns>
        public static bool ValidateUserInput(String _value, int _kind)
        {
            string RegularExpressions = null;

            switch (_kind)
            {
                case 1:
                    //由26个英文字母组成的字串
                    RegularExpressions = "^[A-Za-z]+$";
                    break;
                case 2:
                    //正整数 
                    RegularExpressions = "^[0-9]*[1-9][0-9]*$";
                    break;
                case 3:
                    //非负整数（正整数 + 0)
                    RegularExpressions = "^\\d+$";
                    break;
                case 4:
                    //非正整数（负整数 + 0）
                    RegularExpressions = "^((-\\d+)|(0+))$";
                    break;
                case 5:
                    //负整数 
                    RegularExpressions = "^-[0-9]*[1-9][0-9]*$";
                    break;
                case 6:
                    //整数
                    RegularExpressions = "^-?\\d+$";
                    break;
                case 7:
                    //非负浮点数（正浮点数 + 0）
                    RegularExpressions = "^\\d+(\\.\\d+)?$";
                    break;
                case 8:
                    //正浮点数
                    RegularExpressions = "^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
                    break;
                case 9:
                    //非正浮点数（负浮点数 + 0）
                    RegularExpressions = "^((-\\d+(\\.\\d+)?)|(0+(\\.0+)?))$";
                    break;
                case 10:
                    //负浮点数
                    RegularExpressions = "^(-(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
                    break;
                case 11:
                    //浮点数
                    RegularExpressions = "^(-?\\d+)(\\.\\d+)?$";
                    break;
                case 12:
                    //由26个英文字母的大写组成的字串
                    RegularExpressions = "^[A-Z]+$";
                    break;
                case 13:
                    //由26个英文字母的小写组成的字串
                    RegularExpressions = "^[a-z]+$";
                    break;
                case 14:
                    //由数字和26个英文字母组成的字串
                    RegularExpressions = "^[A-Za-z0-9]+$";
                    break;
                case 15:
                    //由数字、26个英文字母或者下划线组成的字串 
                    RegularExpressions = "^[0-9a-zA-Z_]+$";
                    break;
                case 16:
                    //email地址
                    RegularExpressions = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                    break;
                case 17:
                    //url
                    RegularExpressions = "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";
                    break;
                case 18:
                    //只能输入中文
                    RegularExpressions = "^[\u4E00-\u9FA5]";
                    break;
                case 19:
                    //只能输入0和非0打头的数字
                    RegularExpressions = "^(0|[1-9][0-9]*)$";
                    break;
                case 20:
                    //只能输入数字
                    RegularExpressions = "^[0-9]*$";
                    break;
                case 21:
                    //只能输入数字加2位小数
                    RegularExpressions = "^[0-9]+(.[0-9]{1,2})?$";
                    break;
                case 22:
                    //只能输入0和非0打头的数字加2位小数
                    RegularExpressions = "^(0|[1-9]+)(.[0-9]{1,2})?$";
                    break;
                case 23:
                    //只能输入0和非0打头的数字加2位小数  但不匹配0.00
                    RegularExpressions = "^(0(.(0[1-9]|[1-9][0-9]))?|[1-9]+(.[0-9]{1,2})?)$";
                    break;
                case 24:
                    //验证固话：区号2-5位，电话号码7-8位，分机号一位以上，例如：010-23658974(12);
                    //150、151、152、153、155、156、157、158、159                  九个
                    //130、131、132、133、134、135、136、137、138、139             十个
                    //180、182、185、186、187、188、189                            七个
                    RegularExpressions = "^(\\d{2,5}-\\d{7,8}(-\\d{1,})?)|(13\\d{9})|(15[0-3,5-9]\\d{8})|(18[0,2,5-9]\\d{8})";
                    break;
                case 25:
                    //IP地址
                    //RegularExpressions = "^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$";
                    RegularExpressions = "^(((\\d{1})|(\\d{2})|(1\\d{2})|(2[0-4]\\d{1})|(25[0-5]))\\.){3}((\\d{1})|(\\d{2})|(1\\d{2})|(2[0-4]\\d{1})|(25[0-5]))$";
                    break;
                case 26:
                    //MAC地址:8位0-9，A-F的数字或者字母；
                    RegularExpressions = "^([0-9,A-F]{8})$";
                    break;
                default:
                    break;
            }

            Match m = Regex.Match(_value, RegularExpressions);

            if (m.Success)
                return true;
            else
                return false;

        }
        #endregion 
    }
}
