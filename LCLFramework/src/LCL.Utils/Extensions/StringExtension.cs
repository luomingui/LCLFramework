using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LCL
{
    public static class StringExtension
    {
        /// <summary>
        /// if this string's length is more than size,
        /// cut the excessive part and append another string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size">whether longer than this size</param>
        /// <param name="appendMe">if longer, append this string</param>
        /// <returns></returns>
        public static string Cut(this string str, int size, string appendMe)
        {
            if (str.IsAllWhite())
            {
                return string.Empty;
            }
            if (str.Length > size)
            {
                return str.Substring(0, size) + appendMe;
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// if this string's length is more than size,
        /// cut the excessive part and append another string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size">whether longer than this size</param>
        /// <returns></returns>
        public static string Cut(this string str, int size)
        {
            return Cut(str, size, string.Empty);
        }
        /// <summary>
        /// Cut a string by a english word size.
        /// CutChinese("abcdefg",3,"...") => "abc..."
        /// CutChinese("a胡庆访",3,"...") => "a胡..."
        /// CutChinese("胡庆访",3,"...")  => "胡..."
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxSize">the max size of English chars.</param>
        /// <param name="appendMe">if longer, append this string. This value could be null.</param>
        /// <returns></returns>
        public static string CutChinese(this string str, int maxSize, string appendMe)
        {
            //input check
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (maxSize < 0)
            {
                throw new ArgumentOutOfRangeException("size");
            }
            int strLen = str.Length;

            //Both chinese and english words, if this is ture, it means "no cut".
            if (maxSize > strLen)
            {
                return str;
            }
            //convert to the size of english words.
            int maxEnglishSize = maxSize * 2;

            //find the position to cut.
            int englishLength = 0;
            int index = 0;
            for (; englishLength < maxEnglishSize && index < strLen; index++)
            {
                //if current char is a chinese char, cut size should be substracted.
                if (((int)(str[index]) & 0xFF00) != 0)
                {
                    englishLength += 2;
                }
                else
                {
                    englishLength++;
                }
            }
            //at the end of circle, the index indicate where to remove.

            //If last char is a chinese word, this judgement is true.
            if (englishLength > maxEnglishSize)
            {
                index--;
            }

            //no cut.
            if (index == strLen)
            {
                return str;
            }

            return str.Remove(index) + appendMe;
        }
        /// <summary>
        /// Cut a string by a english word size.
        /// CutChinese("abcdefg",3) => "abc"
        /// CutChinese("a胡庆访",3) => "a胡"
        /// CutChinese("胡庆访",3)  => "胡"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxSize">the max size of English chars.</param>
        /// <returns></returns>
        public static string CutChinese(this string str, int maxSize)
        {
            return str.CutChinese(maxSize, string.Empty);
        }
        /// <summary>
        /// 比较两个字符串是否相等。忽略大小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str, string target)
        {
            return string.Compare(str, target, true) == 0;
        }
        /// <summary>
        /// judge this string is :
        /// null/String.Empty/all white spaces.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAllWhite(this string str)
        {
            return string.IsNullOrEmpty(str) || str.Trim() == string.Empty;
        }
        public static bool IsNullOrLengthBetween(this string str, int minLength, int maxLength)
        {
            if (str == null)
            {
                return true;
            }
            return str.IsLengthBetween(minLength, maxLength);
        }
        public static bool IsLengthBetween(this string str, int minLength, int maxLength)
        {
            if (str == null)
            {
                return false;
            }
            int length = str.Length;
            return length <= maxLength && length >= minLength;
        }
        /// <summary>
        /// Removes all leading and trailing white-space characters from the current System.String object.
        /// if it is null, return the string.Empty.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimNull(this string s)
        {
            return string.IsNullOrEmpty(s) ? string.Empty : s.Trim();
        }
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }
        public static void Open(this string s)
        {
            Process.Start(s);
        }
        public static string ExecuteDOS(this string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            return process.StandardOutput.ReadToEnd();
        }
        public static string ExecuteDOS(this string cmd, out string error)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            error = process.StandardError.ReadToEnd();
            return process.StandardOutput.ReadToEnd();
        }
        public static void CreateDirectory(this string path)
        {
            Directory.CreateDirectory(path);
        }
        public static void WriteText(this string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
        public static void DeleteFile(this string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        // 倒置字符串，输入"abcd123"，返回"321dcba"
        public static string Reverse(this string value)
        {
            char[] input = value.ToCharArray();
            char[] output = new char[value.Length];
            for (int i = 0; i < input.Length; i++)
                output[input.Length - 1 - i] = input[i];
            return new string(output);
        }

        public static string If(this string s, Predicate<string> predicate, Func<string, string> func)
        {
            return predicate(s) ? func(s) : s;
        }
    }
}
