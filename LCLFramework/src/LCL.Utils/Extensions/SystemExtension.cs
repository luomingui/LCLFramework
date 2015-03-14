using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace LCL
{
    public static class SystemExtension
    {
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
        public static T ConvertType<T>(this object value)
        {
            if (value == null)
            {
                return default(T);
            }
            TypeConverter typeConverter1 = TypeDescriptor.GetConverter(typeof(T));
            TypeConverter typeConverter2 = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter1.CanConvertFrom(value.GetType()))
            {
                return (T)typeConverter1.ConvertFrom(value);
            }
            else if (typeConverter2.CanConvertTo(typeof(T)))
            {
                return (T)typeConverter2.ConvertTo(value, typeof(T));
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}