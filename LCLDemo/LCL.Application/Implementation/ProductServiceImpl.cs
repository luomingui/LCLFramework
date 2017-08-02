
using LCL.Application;
using LCL.Bus;
using LCL.Domain.Model;
using LCL.Domain.Repositories;
using LCL.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using LCL.Infrastructure;
using LCL.Domain.Services;

namespace LCL.Application.Implementation
{
    /// <summary>
    /// 表示与“商品”相关的应用层服务的一种实现。
    /// </summary>
    public class ProductServiceImpl : ApplicationService, IProductService
    {
        #region Private Fields
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly ICategorizationRepository categorizationRepository;
        private readonly ILDomainService domainService;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>ProductServiceImpl</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>ProductServiceImpl</c>类型的仓储上下文实例。</param>
        /// <param name="laptopRepository">“笔记本电脑”仓储实例。</param>
        public ProductServiceImpl(IRepositoryContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ICategorizationRepository categorizationRepository,
            ILDomainService domainService,
             IEventBus bus)
            : base(context, bus)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.categorizationRepository = categorizationRepository;
            this.domainService = domainService;
        }
        #endregion

        #region IProductService Members
        /// <summary>
        /// 创建商品信息。
        /// </summary>
        /// <param name="products">需要创建的商品信息。</param>
        /// <returns>已创建的商品信息。</returns>
        public List<Product> CreateProducts(List<Product> products)
        {
            productRepository.Insert(products);
            return products;
        }
        /// <summary>
        /// 创建商品分类。
        /// </summary>
        /// <param name="categorys">需要创建的商品分类。</param>
        /// <returns>已创建的商品分类。</returns>
        public List<Category> CreateCategories(List<Category> categorys)
        {
            categoryRepository.Insert(categorys);
            return categorys;
        }
        /// <summary>
        /// 更新商品信息。
        /// </summary>
        /// <param name="products">需要更新的商品信息。</param>
        /// <returns>已更新的商品信息。</returns>
        public List<Product> UpdateProducts(List<Product> products)
        {
            productRepository.Update(products);
            return products;
        }
        /// <summary>
        /// 更新商品分类。
        /// </summary>
        /// <param name="categorys">需要更新的商品分类。</param>
        /// <returns>已更新的商品分类。</returns>
        public List<Category> UpdateCategories(List<Category> categorys)
        {
            categoryRepository.Update(categorys);

            return categorys;
        }
        /// <summary>
        /// 删除商品信息。
        /// </summary>
        /// <param name="productIDs">需要删除的商品信息的ID值。</param>
        public void DeleteProducts(IDList productIDs)
        {
            productRepository.Delete(productIDs);
        }
        /// <summary>
        /// 删除商品分类。
        /// </summary>
        /// <param name="categoryIDs">需要删除的商品分类的ID值。</param>
        public void DeleteCategories(IDList categoryIDs)
        {
            categoryRepository.Delete(categoryIDs);
        }
        /// <summary>
        /// 设置商品分类。
        /// </summary>
        /// <param name="productID">需要进行分类的商品ID值。</param>
        /// <param name="categoryID">商品分类ID值。</param>
        /// <returns>带有商品分类信息的对象。</returns>
        public Categorization CategorizeProduct(Guid productID, Guid categoryID)
        {
            if (productID == Guid.Empty)
                throw new ArgumentNullException("productID");
            if (categoryID == Guid.Empty)
                throw new ArgumentNullException("categoryID");
            var product = productRepository.Get(productID);
            var category = categoryRepository.Get(categoryID);
            return domainService.Categorize(product, category);
        }
        /// <summary>
        /// 取消商品分类。
        /// </summary>
        /// <param name="productID">需要取消分类的商品ID值。</param>
        public void UncategorizeProduct(Guid productID)
        {
            if (productID == Guid.Empty)
                throw new ArgumentNullException("productID");
            var product = productRepository.Get(productID);
            domainService.Uncategorize(product);
        }
        /// <summary>
        /// 根据指定的ID值获取商品分类。
        /// </summary>
        /// <param name="id">商品分类ID值。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品分类。</returns>
        public Category GetCategoryByID(Guid id)
        {
            var category = categoryRepository.Get(id);

            return category;
        }
        /// <summary>
        /// 获取所有的商品分类。
        /// </summary>
        /// <param name="spec">查询方式。</param>
        /// <returns>所有的商品分类。</returns>
        public List<Category> GetCategories()
        {
            var categories = categoryRepository.FindAll().ToList();
            return categories;
        }
        /// <summary>
        /// 根据指定的ID值获取商品信息。
        /// </summary>
        /// <param name="id">商品信息ID值。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品信息。</returns>
        public Product GetProductByID(Guid id)
        {
            var product = productRepository.Get(id);
            return product;
        }
        /// <summary>
        /// 获取所有的商品信息。
        /// </summary>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品信息。</returns>
        public List<Product> GetProducts()
        {
            var result = productRepository
                .FindAll()
                .ToList();

            return result;
        }
        /// <summary>
        /// 获取所有的特色商品信息。
        /// </summary>
        /// <param name="count">需要获取的特色商品信息的个数。</param>
        /// <returns>特色商品信息。</returns>
        public List<Product> GetFeaturedProducts(int count)
        {
            var featuredProducts = productRepository.GetFeaturedProducts(count).ToList();

            return featuredProducts;
        }

        public PagedResult<Product> GetProductsWithPagination(int pageNumber, int pageSize)
        {
            return productRepository.FindAll(p => p.ID, SortOrder.Ascending, pageNumber, pageSize);
        }

        public List<Product> GetProductsForCategory(Guid categoryID)
        {
            var category = categoryRepository.Get(categoryID);
            var products = categorizationRepository.GetProductsForCategory(category);

            return products.ToList();
        }

        public PagedResult<Product> GetProductsForCategoryWithPagination(Guid categoryID, int pageNumber, int pageSize)
        {
            var category = categoryRepository.Get(categoryID);
            var pagedProducts = categorizationRepository.GetProductsForCategoryWithPagination(category, pageNumber, pageSize);

            return pagedProducts;
        }

        #endregion
    }
}