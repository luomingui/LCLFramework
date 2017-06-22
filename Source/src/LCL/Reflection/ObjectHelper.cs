using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LCL.Reflection
{
    /// <summary>
    /// object 类型的帮助方法。
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 获取指定属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object obj, string propertyName)
        {
            return (T)GetPropertyValue(obj, propertyName);
        }

        /// <summary>
        /// 获取指定属性的值
        /// 
        /// 使用方法：
        /// var value = obj.GetStepPropertyValue("Property1.Property2.Property3");
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName">
        /// 级联属性过滤串,格式如:属性.子属性.子子属性...
        /// </param>
        /// <returns></returns>
        public static object GetPropertyValue(object obj, string propertyName)
        {
            //if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");

            //如果是层级属性
            if (propertyName.Contains('.'))
            {
                return GetStepPropertyValue(obj, propertyName);
            }

            var property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new InvalidProgramException("类型" + obj.GetType().ToString() + "不存在属性" + propertyName);
            return property.GetValue(obj, null);
        }

        /// <summary>
        /// 设置指定属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new InvalidProgramException("类型" + obj.GetType().ToString() + "不存在属性" + propertyName);
            property.SetValue(obj, value, null);
        }

        private static object GetStepPropertyValue(object obj, string propertyName)
        {
            var propertiesArray = propertyName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            object value = obj;
            for (int i = 0, c = propertiesArray.Length; i < c; i++)
            {
                if (value == null) throw new ArgumentNullException("value");

                var property = propertiesArray[i];
                value = GetPropertyValue(value, property);
            }

            return value;
        }
    }
}
