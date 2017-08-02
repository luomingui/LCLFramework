using LCL.Domain.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using LCL.Domain.Model;
using LCL.Domain.ValueObject;
using LCL.WebAPI.Utility;

namespace LCL.WebAPI.Controllers
{
    /// <summary>
    /// 表示与“商品”相关的应用层服务契约。
    /// </summary>
    [RoutePrefix("api/v1/ProductService")]
    [VersionedRoute("api/version", 1)]
    public class ProductServiceController : ApiController
    {
        private readonly IProductService server;
        /// <summary>
        /// 表示与“商品”相关的应用层服务契约。
        /// </summary>
        public ProductServiceController()
        {
            this.server = RF.Service<IProductService>();
        }
        /// <summary>
        /// 表示与“商品”相关的应用层服务契约。
        /// </summary>
        /// <param name="server"></param>
        public ProductServiceController(IProductService server)
        {
            this.server = server;
        }
        /// <summary>
        /// 设置商品分类。
        /// </summary>
        /// <param name="productID">需要进行分类的商品ID值。</param>
        /// <param name="categoryID">商品分类ID值。</param>
        /// <returns>带有商品分类信息的对象。</returns>
        [HttpPost, Route("CategorizeProduct")]
        public Categorization CategorizeProduct(Guid productID, Guid categoryID)
        {
            return this.server.CategorizeProduct(productID, categoryID);
        }
        /// <summary>
        /// 创建商品分类。
        /// </summary>
        /// <param name="categoryDataObjects">需要创建的商品分类。</param>
        /// <returns>已创建的商品分类。</returns>
        [HttpPost, Route("CreateCategories")]
        public List<Category> CreateCategories(List<Category> categoryDataObjects)
        {
            return this.server.CreateCategories(categoryDataObjects);
        }
        /// <summary>
        /// 创建商品信息。
        /// </summary>
        /// <param name="productDataObjects">需要创建的商品信息。</param>
        /// <returns>已创建的商品信息。</returns>
        [HttpPost, Route("CreateProducts")]
        public List<Product> CreateProducts(List<Product> productDataObjects)
        {
            return this.server.CreateProducts(productDataObjects);
        }
        /// <summary>
        /// 删除商品分类。
        /// </summary>
        /// <param name="categoryIDs">需要删除的商品分类的ID值。</param>
        [HttpDelete, Route("DeleteCategories")]
        public void DeleteCategories(IDList categoryIDs)
        {
            this.server.DeleteCategories(categoryIDs);
        }
        /// <summary>
        /// 删除商品信息。
        /// </summary>
        /// <param name="productIDs">需要删除的商品信息的ID值。</param>
        [HttpDelete, Route("DeleteProducts")]
        public void DeleteProducts(IDList productIDs)
        {
            this.server.DeleteProducts(productIDs);
        }
        /// <summary>
        /// 获取所有的商品分类。
        /// </summary>
        /// <returns>所有的商品分类。</returns>
        [HttpGet, Route("GetCategories")]
        public List<Category> GetCategories()
        {
            return this.server.GetCategories();
        }
        /// <summary>
        /// 根据指定的ID值获取商品分类。
        /// </summary>
        /// <param name="id">商品分类ID值。</param>
        /// <returns>商品分类。</returns>
        [HttpGet, Route("GetCategories/{id}")]
        public Category GetCategoryByID(Guid id)
        {
            return this.server.GetCategoryByID(id);
        }
        /// <summary>
        /// 获取所有的特色商品信息。
        /// </summary>
        /// <param name="count">需要获取的特色商品信息的个数。</param>
        /// <returns>特色商品信息。</returns>
        [HttpGet, Route("GetFeaturedProducts/{count}")]
        public List<Product> GetFeaturedProducts(int count)
        {
            return this.server.GetFeaturedProducts(count);
        }
        /// <summary>
        /// 根据指定的ID值获取商品信息。
        /// </summary>
        /// <param name="id">商品信息ID值。</param>
        /// <returns>商品信息。</returns>
        [HttpGet, Route("GetProducts/{id}")]
        public Product GetProductByID(Guid id)
        {
            return this.server.GetProductByID(id);
        }
        /// <summary>
        /// 获取所有的商品信息。
        /// </summary>
        /// <returns>商品信息。</returns>
        [HttpGet, Route("GetProducts")]
        public List<Product> GetProducts()
        {
            return this.server.GetProducts();
        }
        /// <summary>
        /// 根据指定的商品分类ID值，获取该分类下所有的商品信息。
        /// </summary>
        /// <param name="categoryID">商品分类ID值。</param>
        /// <returns>所有的商品信息。</returns>
        [HttpGet, Route("GetProductsForCategory/{categoryID}")]
        public List<Product> GetProductsForCategory(Guid categoryID)
        {
            return this.server.GetProductsForCategory(categoryID);
        }
        /// <summary>
        /// 以分页的方式获取所有商品信息
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet, Route("GetProductsForCategoryWithPagination")]
        public PagedResult<Product> GetProductsForCategoryWithPagination(Guid categoryID, int pageNumber, int pageSize)
        {
            return this.server.GetProductsForCategoryWithPagination(categoryID,pageNumber,pageSize);
        }
        /// <summary>
        /// 以分页的方式获取所有商品信息
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet, Route("GetProductsWithPagination")]
        public PagedResult<Product> GetProductsWithPagination(int pageNumber, int pageSize)
        {
            return this.server.GetProductsWithPagination(pageNumber, pageSize);
        }
        /// <summary>
        /// 取消商品分类。
        /// </summary>
        /// <param name="productID">需要取消分类的商品ID值。</param>
        [HttpGet, Route("UncategorizeProduct")]
        public void UncategorizeProduct(Guid productID)
        {
             this.server.UncategorizeProduct(productID);
        }
        /// <summary>
        /// 更新商品分类。
        /// </summary>
        /// <param name="categoryDataObjects">需要更新的商品分类。</param>
        /// <returns>已更新的商品分类。</returns>
        [HttpPost, Route("UpdateCategories")]
        public List<Category> UpdateCategories(List<Category> categoryDataObjects)
        {
            return this.server.UpdateCategories(categoryDataObjects);
        }
        /// <summary>
        /// 更新商品信息。
        /// </summary>
        /// <param name="productDataObjects">需要更新的商品信息。</param>
        /// <returns>已更新的商品信息。</returns>
        [HttpPost, Route("UpdateProducts")]
        public List<Product> UpdateProducts(List<Product> productDataObjects)
        {
            return this.server.UpdateProducts(productDataObjects);
        }
    }
}
