using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace LCL
{
    /// <summary>
    ///     通用类型扩展方法类
    ///     http://www.cnblogs.com/ldp615/archive/2009/09/02/1559020.html
    /// </summary>
    public static class ObjectExtensions
    {
        public static string ToJson(this object instance)
        {
            return ToJson(instance, null);
        }
        public static string ToJson(this object instance, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            jsonSerializer.RegisterConverters(jsonConverters ?? new JavaScriptConverter[0]);
            return jsonSerializer.Serialize(instance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString4(this object obj, string format)
        {
            MatchEvaluator evaluator = match =>
            {
                string[] propertyNames = match.Groups["Name"].Value.Split('.');
                string propertyFormat = match.Groups["Format"].Value;

                object propertyValue = obj;
                try
                {
                    foreach (string propertyName in propertyNames)
                        propertyValue = propertyValue.GetPropertyValue(propertyName);
                }
                catch
                {
                    return match.Value;
                }

                if (string.IsNullOrEmpty(format) == false)
                    return string.Format("{0:" + propertyFormat + "}", propertyValue);
                else return propertyValue.ToString();
            };
            string pattern = @"\[(?<Name>[^\[\]:]+)(\s*[:]\s*(?<Format>[^\[\]:]+))?\]";
            return Regex.Replace(format, pattern, evaluator, RegexOptions.Compiled);
        }
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            return info.GetValue(obj, null);
        }
        //TODO:类型转换
        public static T GetObjTranNull<T>(this object obj)
        {
            try
            {
                if (obj == null)
                {
                    return (T)System.Convert.ChangeType("", typeof(T));
                }
                else
                {
                    if (obj.GetType() == typeof(T))
                        return (T)obj;
                }

                return (T)System.Convert.ChangeType(obj, typeof(T));
            }
            catch
            {

            }
            return default(T);
        }
        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }
        /// <summary>
        /// 强制转换当前对象为指定类型。
        /// 
        /// 传入的对象为空，或者转换失败，则会抛出异常。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj)
            where T : class
        {
            var res = obj as T;

            if (res == null) throw new InvalidCastException("传入的对象为空，或者不能转换为 " + typeof(T).Name + " 类型。");

            return res;
        }

        #region c# 扩展方法奇思妙用变态篇二：封装 if/else、swith/case 及 while
        //http://www.cnblogs.com/ldp615/archive/2009/08/13/1545312.html
        public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func) where T : struct
        {
            return predicate(t) ? func(t) : t;
        }
        public static T If<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
            return t;
        }
        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }
        public static void While<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            while (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
        }
        #endregion


        public static IEnumerable<T> GetDescendants<TRoot, T>(this TRoot root,
        Func<TRoot, IEnumerable<T>> rootChildSelector,
        Func<T, IEnumerable<T>> childSelector, Predicate<T> filter)
        {
            foreach (T t in rootChildSelector(root))
            {
                if (filter == null || filter(t))
                    yield return t;
                foreach (T child in GetDescendants(t, childSelector, filter))
                    yield return child;
            }
        }
        public static IEnumerable<T> GetDescendants<T>(this T root,
        Func<T, IEnumerable<T>> childSelector, Predicate<T> filter)
        {
            foreach (T t in childSelector(root))
            {
                if (filter == null || filter(t))
                    yield return t;
                foreach (T child in GetDescendants((T)t, childSelector, filter))
                    yield return child;
            }
        }



    }
}