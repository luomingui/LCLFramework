/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCL.MvcExtensions
{
    public interface IMethodInvoker
    {
        // Methods
        object Invoke(object instance, params object[] parameters);
    }
    public class MethodInvoker : IMethodInvoker
    {
        // Fields
        private Func<object, object[], object> m_invoker;

        // Methods
        public MethodInvoker(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
            this.m_invoker = CreateInvokeDelegate(methodInfo);
        }

        private static Func<object, object[], object> CreateInvokeDelegate(MethodInfo methodInfo)
        {
            ParameterExpression expression = null;
            ParameterExpression expression2 = null;
            List<Expression> arguments = new List<Expression>();
            ParameterInfo[] parameters = methodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                UnaryExpression item = Expression.Convert(Expression.ArrayIndex(expression2 = Expression.Parameter(typeof(object[]), "parameters"),
                    Expression.Constant(i)), parameters[i].ParameterType);
                arguments.Add(item);
            }
            UnaryExpression instance = methodInfo.IsStatic ? null : Expression.Convert(expression = Expression.Parameter(typeof(object), "instance"),
                methodInfo.ReflectedType);
            MethodCallExpression body = Expression.Call(instance, methodInfo, arguments);
            if (body.Type == typeof(void))
            {
                Action<object, object[]> execute = Expression.Lambda<Action<object, object[]>>(body, new ParameterExpression[] { expression, expression2 }).Compile();
                return delegate(object instanceExt, object[] parametersExt)
                {
                    execute(instanceExt, parametersExt);
                    return null;
                };
            }
            return Expression.Lambda<Func<object, object[], object>>(Expression.Convert(body, typeof(object)), new ParameterExpression[] { expression, expression2 }).Compile();
        }

        object IMethodInvoker.Invoke(object instance, params object[] parameters)
        {
            return this.Invoke(instance, parameters);
        }

        public object Invoke(object instance, params object[] parameters)
        {
            return this.m_invoker(instance, parameters);
        }

        // Properties
        public MethodInfo MethodInfo { get; private set; }
    }


}
