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

namespace LCL.MvcExtensions
{
	/// <summary>
	/// Extension methods for sorting.
	/// </summary>
	public static class SortExtensions
	{
		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> datasource, string propertyName, SortDirection direction)
		{
			return datasource.AsQueryable().OrderBy(propertyName, direction);
		}
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> datasource, string propertyName, SortDirection direction)
		{
			//http://msdn.microsoft.com/en-us/library/bb882637.aspx

			if(string.IsNullOrEmpty(propertyName))
			{
				return datasource;
			}

			var type = typeof(T);
			var property = type.GetProperty(propertyName);

			if(property == null)
			{
				throw new InvalidOperationException(string.Format("Could not find a property called '{0}' on type {1}", propertyName, type));
			}

			var parameter = Expression.Parameter(type, "p");
			var propertyAccess = Expression.MakeMemberAccess(parameter, property);
			var orderByExp = Expression.Lambda(propertyAccess, parameter);

			const string orderBy = "OrderBy";
			const string orderByDesc = "OrderByDescending";

			string methodToInvoke = direction == SortDirection.Ascending ? orderBy : orderByDesc;

			var orderByCall = Expression.Call(typeof(Queryable), 
				methodToInvoke, 
				new[] { type, property.PropertyType }, 
				datasource.Expression, 
				Expression.Quote(orderByExp));

			return datasource.Provider.CreateQuery<T>(orderByCall);
		}
	}
}