using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Objects;

namespace LCL.Repositories.EntityFramework
{
    /// <summary>
    /// Helper extensions for add IDbSet methods defined only
    /// for DbSet and ObjectQuery
    /// </summary>
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Include<T>
        (this IQueryable<T> source, string path)
        {
            var objectQuery = source as ObjectQuery<T>;
            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }
            return source;
        }
        /// <summary>
        /// Include extension method for IDbSet
        /// <example>
        /// var query = ReturnTheQuery();
        /// query = query.Include(customer=>customer.Orders);//"Orders"
        /// //or
        /// query = query.Include(customer=>customer.Orders.Select(o=>o.OrderDetails) //"Orders.OrderDetails"
        /// </example>
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in IQueryable</typeparam>
        /// <typeparam name="TEntity">Type of navigated element</typeparam>
        /// <param name="queryable">Queryable object</param>
        /// <param name="path">Expression with path to include</param>
        /// <returns>Queryable object with include path information</returns>
        public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> queryable, 
            Expression<Func<TEntity, TProperty>> path)
            where TEntity : class
        {
            if (queryable is ObjectQuery<TEntity>) //delegate execution in DbSet
                return ((ObjectQuery<TEntity>)queryable).Include(path);
            else // probably a IDbSet mock
                return queryable;
            
        }
        /// <summary>
        /// OfType extension method for IQueryable
        /// </summary>
        /// <typeparam name="KEntity">The type to filter the elements of the sequence on. </typeparam>
        /// <param name="queryable">The queryable object</param>
        /// <returns>
        /// A new IQueryable hat contains elements from
        /// the input sequence of type TResult
        /// </returns>
        public static IQueryable<KEntity> OfType<TEntity, KEntity>(this IQueryable<TEntity> queryable)
            where TEntity : class
            where KEntity : class,TEntity
        {
            if (queryable is ObjectQuery<TEntity>) //delegate execution in DbSet
                return ((ObjectQuery<TEntity>)queryable).OfType<KEntity>();
            else // probably a IDbSet mock
                return queryable.OfType<KEntity>();
        }
    }
}
