using LCL.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LCL
{
    public static class SortByExtension
    {
        #region Internal Methods
        public static IOrderedQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : IEntity
        {
            return InvokeSortBy(query, sortPredicate, SortOrder.Ascending);
        }
        public static IOrderedQueryable<TEntity> SortBy<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, dynamic>> sortPredicate)
        where TEntity : IEntity<TPrimaryKey>
        {
            return InvokeSortBy<TEntity, TPrimaryKey>(query, sortPredicate, SortOrder.Ascending);
        }

        public static IOrderedQueryable<TEntity> SortByDescending<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : IEntity
        {
            return InvokeSortBy(query, sortPredicate, SortOrder.Descending);
        }
        public static IOrderedQueryable<TEntity> SortByDescending<TEntity, TPrimaryKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : IEntity<TPrimaryKey>
        {
            return InvokeSortBy<TEntity, TPrimaryKey>(query, sortPredicate, SortOrder.Descending);
        }
        #endregion

        #region Private Methods
        private static IOrderedQueryable<TEntity> InvokeSortBy<TEntity>(IQueryable<TEntity> query,
            Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
            where TEntity : IEntity
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else
                throw new ArgumentException(@"The body of the sort predicate expression should be 
                either UnaryExpression or MemberExpression.", "sortPredicate");
            MemberExpression memberExpression = (MemberExpression)bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
                propertyType = propertyInfo.PropertyType;
            }
            else
                throw new InvalidOperationException(@"Cannot evaluate the type of property since the member expression 
                represented by the sort predicate expression does not contain a PropertyInfo object.");

            Type funcType = typeof(Func<,>).MakeGenericType(typeof(TEntity), propertyType);
            LambdaExpression convertedExpression = Expression.Lambda(funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType), param);

            var sortingMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod = sortingMethods.Where(sm => sm.Name == sortingMethodName &&
                sm.GetParameters() != null &&
                sm.GetParameters().Length == 2).First();
            return (IOrderedQueryable<TEntity>)sortingMethod
                .MakeGenericMethod(typeof(TEntity), propertyType)
                .Invoke(null, new object[] { query, convertedExpression });
        }
        private static IOrderedQueryable<TEntity> InvokeSortBy<TEntity, TPrimaryKey>(IQueryable<TEntity> query,
           Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
           where TEntity : IEntity<TPrimaryKey>
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else
                throw new ArgumentException(@"The body of the sort predicate expression should be 
                either UnaryExpression or MemberExpression.", "sortPredicate");
            MemberExpression memberExpression = (MemberExpression)bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
                propertyType = propertyInfo.PropertyType;
            }
            else
                throw new InvalidOperationException(@"Cannot evaluate the type of property since the member expression 
                represented by the sort predicate expression does not contain a PropertyInfo object.");

            Type funcType = typeof(Func<,>).MakeGenericType(typeof(TEntity), propertyType);
            LambdaExpression convertedExpression = Expression.Lambda(funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType), param);

            var sortingMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod = sortingMethods.Where(sm => sm.Name == sortingMethodName &&
                sm.GetParameters() != null &&
                sm.GetParameters().Length == 2).First();
            return (IOrderedQueryable<TEntity>)sortingMethod
                .MakeGenericMethod(typeof(TEntity), propertyType)
                .Invoke(null, new object[] { query, convertedExpression });
        }

        private static string GetSortingMethodName(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "OrderBy";
                case SortOrder.Descending:
                    return "OrderByDescending";
                default:
                    throw new ArgumentException("Sort Order must be specified as either Ascending or Descending.",
            "sortOrder");
            }
        }
        #endregion
    }
}
