using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 动态复杂的Where条件逻辑表达式生成的扩展
    /// Where条件扩展
    /// 用于分步实现不确定条件个数的逻辑运算组织
    /// <remarks>作者：陈国伟 日期：2013/05/07</remarks>
    /// </summary>
    public static class WhereExtension
    {
        /// <summary>
        /// 逻辑枚举
        /// </summary>
        public enum LogicEnum
        {
            /// <summary>
            /// 相等
            /// </summary>
            Equal,
            /// <summary>
            /// 不等
            /// </summary>
            NotEqual,
            /// <summary>
            /// 大于
            /// </summary>
            GreaterThan,
            /// <summary>
            /// 大于等于
            /// </summary>
            GreaterThanOrEqual,
            /// <summary>
            /// 小于
            /// </summary>
            LessThan,
            /// <summary>
            /// 小于等于
            /// </summary>
            LessThanOrEqual,
            /// <summary>
            /// 字条串的Contains
            /// </summary>
            Contains
        }

        /// <summary>
        /// 参数替换对象
        /// </summary>
        private class ParameterReplacer : ExpressionVisitor
        {
            /// <summary>
            /// 表达式的参数
            /// </summary>
            public ParameterExpression ParameterExpression { get; private set; }

            /// <summary>
            /// 参数替换对象
            /// </summary>
            /// <param name="paramExp">表达式的参数</param>
            public ParameterReplacer(ParameterExpression paramExp)
            {
                this.ParameterExpression = paramExp;
            }

            /// <summary>
            /// 将表达式调度到此类中更专用的访问方法之一
            /// </summary>
            /// <param name="exp">表达式</param>
            /// <returns></returns>
            public Expression Replace(Expression exp)
            {
                return this.Visit(exp);
            }

            /// <summary>
            /// 获取表达式的参数
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression p)
            {
                return this.ParameterExpression;
            }
        }
        //http://www.cnblogs.com/ldp615/archive/2011/02/17/WhereIf-ExtensionMethod.html
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// 返回默认为True的条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return item => true;
        }

        /// <summary>
        /// 返回默认为False的条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return item => false;
        }

        /// <summary>
        /// 与逻辑运算
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expLeft">表达式1</param>
        /// <param name="expRight">表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "item");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(expLeft.Body);
            var right = parameterReplacer.Replace(expRight.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 或逻辑运算
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expLeft">表达式1</param>
        /// <param name="expRight">表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "item");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(expLeft.Body);
            var right = parameterReplacer.Replace(expRight.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 动态生成Where逻辑表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="member">实体的成员(不区分大小写)</param>
        /// <param name="logic">逻辑关系</param>
        /// <param name="matchValue">要匹配的值</param>     
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Parse<T>(string member, LogicEnum logic, string matchValue)
        {
            if (string.IsNullOrEmpty(member))
            {
                throw new ArgumentNullException("member");
            }

            var keyProperty = typeof(T).GetProperties().FirstOrDefault(item => item.Name.ToLower().Equals(member.Trim().ToLower()));
            if (keyProperty == null)
            {
                throw new ArgumentException("member不存在");
            }

            // item
            var pExp = Expression.Parameter(typeof(T), "item");
            // item.CreateTime
            Expression memberExp = Expression.MakeMemberAccess(pExp, keyProperty);

            if (logic != LogicEnum.Contains)
            {
                // 是否是可空类型
                bool memberIsNullableType = keyProperty.PropertyType.IsGenericType && keyProperty.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                // 是可空类型则取类型的Value属性
                if (memberIsNullableType == true)
                {
                    // item.CreateTime.Value
                    memberExp = Expression.MakeMemberAccess(memberExp, keyProperty.PropertyType.GetProperty("Value"));
                }

                // 目标值类型
                Type valueType = keyProperty.PropertyType;
                if (memberIsNullableType == true)
                {
                    valueType = valueType.GetGenericArguments().FirstOrDefault();
                }

                object value = matchValue;
                if (valueType.Equals(typeof(string)) == false)
                {
                    // value = DateTime.Parse(matchValue)
                    value = valueType.GetMethod("Parse", new Type[] { typeof(string) }).Invoke(null, new object[] { value });
                }

                var valueExp = Expression.Constant(value, valueType);
                // Expression.Equal
                var expMethod = typeof(Expression).GetMethod(logic.ToString(), new Type[] { typeof(Expression), typeof(Expression) });

                // item.CreateTime.Value == value
                var body = expMethod.Invoke(null, new object[] { memberExp, valueExp }) as Expression;
                return Expression.Lambda(body, pExp) as Expression<Func<T, bool>>;
            }
            else
            {
                // item.Member.Contains("something")
                var body = Expression.Call(memberExp, typeof(string).GetMethod(logic.ToString()), Expression.Constant(matchValue, typeof(string)));
                return Expression.Lambda(body, pExp) as Expression<Func<T, bool>>;
            }
        }

        /// <summary>
        /// 动态生成和匹配参数数目相同的逻辑表达式并用Or关联起来
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="member">实体成员</param>
        /// <param name="logic">逻辑关系</param>
        /// <param name="matchValues">匹配值</param>       
        /// <returns></returns>
        public static Expression<Func<T, bool>> ParseOr<T>(string member, LogicEnum logic, params string[] matchValues)
        {
            var where = WhereExtension.Parse<T>(member, logic, matchValues.FirstOrDefault());
            matchValues.Skip(1).ToList().ForEach(value => where = where.Or(WhereExtension.Parse<T>(member, logic, value)));
            return where;
        }

        /// <summary>
        /// 动态生成和成员目相同的逻辑表达式并用Or关联起来
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="members">实体成员</param>
        /// <param name="logic">逻辑关系</param>
        /// <param name="matchValue">匹配值</param>       
        /// <returns></returns>
        public static Expression<Func<T, bool>> ParseOr<T>(string[] members, LogicEnum logic, string matchValue)
        {
            var where = WhereExtension.Parse<T>(members.FirstOrDefault(), logic, matchValue);
            members.Skip(1).ToList().ForEach(member => where = where.Or(WhereExtension.Parse<T>(member, logic, matchValue)));
            return where;
        }

        /// <summary>
        /// 动态生成和匹配参数数目相同的逻辑表达式并用And关联起来
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="member">实体成员</param>
        /// <param name="logic">逻辑关系</param>
        /// <param name="matchValues">匹配值</param>       
        /// <returns></returns>
        public static Expression<Func<T, bool>> ParseAnd<T>(string member, LogicEnum logic, params string[] matchValues)
        {
            var where = WhereExtension.Parse<T>(member, logic, matchValues.FirstOrDefault());
            matchValues.Skip(1).ToList().ForEach(value => where = where.And(WhereExtension.Parse<T>(member, logic, value)));
            return where;
        }

        /// <summary>
        /// 动态生成和成员目相同的逻辑表达式并用And关联起来
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="members">实体成员</param>
        /// <param name="logic">逻辑关系</param>
        /// <param name="matchValue">匹配值</param>       
        /// <returns></returns>
        public static Expression<Func<T, bool>> ParseAnd<T>(string[] members, LogicEnum logic, string matchValue)
        {
            var where = WhereExtension.Parse<T>(members.FirstOrDefault(), logic, matchValue);
            members.Skip(1).ToList().ForEach(member => where = where.And(WhereExtension.Parse<T>(member, logic, matchValue)));
            return where;
        }
    }
}
