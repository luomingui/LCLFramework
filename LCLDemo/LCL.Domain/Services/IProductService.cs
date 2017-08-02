using LCL.Caching;
using LCL.Domain.Model;
using LCL.Domain.ValueObject;
using LCL.Infrastructure;
using LCL.ServiceModel;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LCL.Domain.Services
{
    /// <summary>
    /// 表示与“商品”相关的应用层服务契约。
    /// </summary>
    [ServiceContract(Namespace = "http://www.ByteartRetail.com")]
    public interface IProductService : IApplicationServiceContract
    {
        #region Methods
        /// <summary>
        /// 创建商品信息。
        /// </summary>
        /// <param name="productDataObjects">需要创建的商品信息。</param>
        /// <returns>已创建的商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetProductsForCategory",
            "GetProductsWithPagination",
            "GetFeaturedProducts",
            "GetProductsForCategoryWithPagination",
            "GetProducts", "GetProductByID")]
        List<Product> CreateProducts(List<Product> productDataObjects);
        /// <summary>
        /// 创建商品分类。
        /// </summary>
        /// <param name="categoryDataObjects">需要创建的商品分类。</param>
        /// <returns>已创建的商品分类。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetCategories")]
        List<Category> CreateCategories(List<Category> categoryDataObjects);
        /// <summary>
        /// 更新商品信息。
        /// </summary>
        /// <param name="productDataObjects">需要更新的商品信息。</param>
        /// <returns>已更新的商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetProductsForCategory",
            "GetProductsWithPagination",
            "GetFeaturedProducts",
            "GetProductsForCategoryWithPagination",
            "GetProducts", "GetProductByID")]
        List<Product> UpdateProducts(List<Product> productDataObjects);


        /// <summary>
        /// 更新商品分类。
        /// </summary>
        /// <param name="categoryDataObjects">需要更新的商品分类。</param>
        /// <returns>已更新的商品分类。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetCategories", "GetCategoryByID")]
        List<Category> UpdateCategories(List<Category> categoryDataObjects);
        /// <summary>
        /// 删除商品信息。
        /// </summary>
        /// <param name="productIDs">需要删除的商品信息的ID值。</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetProductsForCategory",
            "GetProductsWithPagination",
            "GetFeaturedProducts",
            "GetProductsForCategoryWithPagination",
            "GetProducts", "GetProductByID")]
        void DeleteProducts(IDList productIDs);
        /// <summary>
        /// 删除商品分类。
        /// </summary>
        /// <param name="categoryIDs">需要删除的商品分类的ID值。</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetCategories", "GetCategoryByID")]
        void DeleteCategories(IDList categoryIDs);
        /// <summary>
        /// 设置商品分类。
        /// </summary>
        /// <param name="productID">需要进行分类的商品ID值。</param>
        /// <param name="categoryID">商品分类ID值。</param>
        /// <returns>带有商品分类信息的对象。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetProductsForCategory",
            "GetProductsForCategoryWithPagination")]
        Categorization CategorizeProduct(Guid productID, Guid categoryID);
        /// <summary>
        /// 取消商品分类。
        /// </summary>
        /// <param name="productID">需要取消分类的商品ID值。</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Remove, "GetProductsForCategory",
            "GetProductsForCategoryWithPagination")]
        void UncategorizeProduct(Guid productID);
        /// <summary>
        /// 根据指定的ID值获取商品分类。
        /// </summary>
        /// <param name="id">商品分类ID值。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品分类。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        Category GetCategoryByID(Guid id);
        /// <summary>
        /// 获取所有的商品分类。
        /// </summary>
        /// <param name="spec">查询方式。</param>
        /// <returns>所有的商品分类。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        List<Category> GetCategories();
        /// <summary>
        /// 根据指定的ID值获取商品信息。
        /// </summary>
        /// <param name="id">商品信息ID值。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        Product GetProductByID(Guid id);
        /// <summary>
        /// 获取所有的商品信息。
        /// </summary>
        /// <param name="spec">查询方式。</param>
        /// <returns>商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        List<Product> GetProducts();
        /// <summary>
        /// 以分页的方式获取所有商品信息。
        /// </summary>
        /// <param name="pagination">带有分页参数信息的对象。</param>
        /// <returns>经过分页的商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        PagedResult<Product> GetProductsWithPagination(int pageNumber, int pageSize);

        PagedResult<Product> GetProductsForCategoryWithPagination(Guid categoryID, int pageNumber, int pageSize);
        /// <summary>
        /// 根据指定的商品分类ID值，获取该分类下所有的商品信息。
        /// </summary>
        /// <param name="categoryID">商品分类ID值。</param>
        /// <returns>所有的商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        List<Product> GetProductsForCategory(Guid categoryID);

        /// <summary>
        /// 获取所有的特色商品信息。
        /// </summary>
        /// <param name="count">需要获取的特色商品信息的个数。</param>
        /// <returns>特色商品信息。</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        [Caching(CachingMethod.Get)]
        List<Product> GetFeaturedProducts(int count);
        #endregion
    }
}
