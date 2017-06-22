
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;

using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;
using LCL;

namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class CategorizationRepository : MongoDBRepository<Categorization>, ICategorizationRepository
    {
        MongoDBRepositoryContext con;
        public CategorizationRepository(IRepositoryContext context)
            : base(context)
        {
            con = this.Context as MongoDBRepositoryContext;
        }

        #region ICategorizationRepository Members

        public IEnumerable<Product> GetProductsForCategory(Category category)
        {

            var categorizationCollection = con.GetCollectionForType(typeof(Categorization));
            var categorizations = categorizationCollection.AsQueryable<Categorization>().Where(c => c.CategoryID == category.ID).ToList();
            var productCollection = con.GetCollectionForType(typeof(Product));
            var productsQuery = productCollection.AsQueryable<Product>();
            List<Product> totalList = new List<Product>();
            foreach (var categorization in categorizations)
            {
                ;
                totalList.AddRange(productsQuery.Where(p => p.ID == categorization.ProductID).ToList());
            }
            return totalList;
        }

        public PagedResult<Product> GetProductsForCategoryWithPagination(Category category, int pageNumber, int pageSize)
        {

            var categorizationCollection = con.GetCollectionForType(typeof(Categorization));
            var categorizations = categorizationCollection.AsQueryable<Categorization>().Where(c => c.CategoryID == category.ID).ToList();
            var productCollection = con.GetCollectionForType(typeof(Product));
            var productsQuery = productCollection.AsQueryable<Product>();
            List<Product> totalList = new List<Product>();
            foreach (var categorization in categorizations)
            {
                totalList.AddRange(productsQuery.Where(p => p.ID == categorization.ProductID).ToList());
            }
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            int count = totalList.Count();
            return new PagedResult<Product>(count, (count + pageSize - 1) / pageSize, pageSize, pageNumber, totalList.Skip(skip).Take(take).ToList());
        }

        public Category GetCategoryForProduct(Product product)
        {
            var categorizationCollection = con.GetCollectionForType(typeof(Categorization));
            var categorizations = categorizationCollection.AsQueryable<Categorization>().Where(c => c.ProductID == product.ID).FirstOrDefault();
            if (categorizations == null)
                return null;
            var categoryCollection = con.GetCollectionForType(typeof(Category));
            return categoryCollection.AsQueryable<Category>().Where(c => c.ID == categorizations.CategoryID).FirstOrDefault();
        }
        #endregion
    }
}
