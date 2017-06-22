using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LCL
{
    public static class StringExtensions
    {
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(str);
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

     

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }
        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();
            }

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }
        public static string ToSentenceCase(this string str, bool invariantCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(
                str,
                "[a-z][A-Z]",
                m => m.Value[0] + " " + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]))
            );
        }
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(value);
            }

            return (T)Enum.Parse(typeof(T), value);
        }
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(value);
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        public static string ToPascalCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToUpperInvariant(): str.ToUpper();
            }

            return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])) + str.Substring(1);
        }
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }
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
        /// CutChinese("aºúÇì·Ã",3,"...") => "aºú..."
        /// CutChinese("ºúÇì·Ã",3,"...")  => "ºú..."
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
        /// CutChinese("aºúÇì·Ã",3) => "aºú"
        /// CutChinese("ºúÇì·Ã",3)  => "ºú"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxSize">the max size of English chars.</param>
        /// <returns></returns>
        public static string CutChinese(this string str, int maxSize)
        {
            return str.CutChinese(maxSize, string.Empty);
        }
        /// <summary>
        /// ±È½ÏÁ½¸ö×Ö·û´®ÊÇ·ñÏàµÈ¡£ºöÂÔ´óÐ¡Ð´
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
        public static void OpenProcess(this string s)
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
        /// ×ªÈ«½Ç(SBC case)
        /// </summary>
        /// <param name="input">ÈÎÒâ×Ö·û´®</param>
        /// <returns>È«½Ç×Ö·û´®</returns>
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
        /// ×ª°ë½Ç(DBC case)
        /// </summary>
        /// <param name="input">ÈÎÒâ×Ö·û´®</param>
        /// <returns>°ë½Ç×Ö·û´®</returns>
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

        // µ¹ÖÃ×Ö·û´®£¬ÊäÈë"abcd123"£¬·µ»Ø"321dcba"
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