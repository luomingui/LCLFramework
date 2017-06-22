
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LCL.Reflection
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetRuntimeProperty<T>(Expression<Func<T, object>> expProperty)
        {
            var member = expProperty.Body as MemberExpression;
            if (member == null)
            {
                member = (expProperty.Body as UnaryExpression).Operand as MemberExpression;
                if (member == null) throw new ArgumentNullException("property");
            }

            var property = member.Member as PropertyInfo;
            if (property == null) throw new ArgumentNullException("property");
            return property;
        }

        public static string GetProperty<T>(Expression<Func<T, object>> expProperty)
        {
            return GetRuntimeProperty(expProperty).Name;
        }
    }
}
