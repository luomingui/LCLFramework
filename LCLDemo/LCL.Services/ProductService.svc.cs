using LCL.Domain.Model;
using LCL.Domain.Services;
using LCL.Domain.ValueObject;
using LCL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LCL.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProductService : IProductService
    {
        private readonly IProductService productServiceImpl = RF.Service<IProductService>();
        public List<Product> CreateProducts(List<Product> productDataObjects)
        {
            try
            {
                return productServiceImpl.CreateProducts(productDataObjects);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Category> CreateCategories(List<Category> categoryDataObjects)
        {
            try
            {
                return productServiceImpl.CreateCategories(categoryDataObjects);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Product> UpdateProducts(List<Product> productDataObjects)
        {
            try
            {
                return productServiceImpl.UpdateProducts(productDataObjects);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Category> UpdateCategories(List<Category> categoryDataObjects)
        {
            try
            {
                return productServiceImpl.UpdateCategories(categoryDataObjects);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void DeleteProducts(IDList productIDs)
        {
            try
            {
                productServiceImpl.DeleteProducts(productIDs);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void DeleteCategories(IDList categoryIDs)
        {
            try
            {
                productServiceImpl.DeleteCategories(categoryIDs);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Categorization CategorizeProduct(Guid productID, Guid categoryID)
        {
            try
            {
                return productServiceImpl.CategorizeProduct(productID, categoryID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void UncategorizeProduct(Guid productID)
        {
            try
            {
                productServiceImpl.UncategorizeProduct(productID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Category GetCategoryByID(Guid id)
        {
            try
            {
                return productServiceImpl.GetCategoryByID(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Category> GetCategories()
        {
            try
            {
                return productServiceImpl.GetCategories();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Product GetProductByID(Guid id)
        {
            try
            {
                return productServiceImpl.GetProductByID(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Product> GetProducts()
        {
            try
            {
                return productServiceImpl.GetProducts();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
      
        public List<Product> GetProductsForCategory(Guid categoryID)
        {
            try
            {
                return productServiceImpl.GetProductsForCategory(categoryID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Product> GetFeaturedProducts(Int32 count)
        {
            try
            {
                return productServiceImpl.GetFeaturedProducts(count);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void Dispose() { productServiceImpl.Dispose(); }

        public PagedResult<Product> GetProductsWithPagination(int pageNumber, int pageSize)
        {
            try
            {
                return productServiceImpl.GetProductsWithPagination(pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }

        public PagedResult<Product> GetProductsForCategoryWithPagination(Guid categoryID, int pageNumber, int pageSize)
        {
            try
            {
                return productServiceImpl.GetProductsForCategoryWithPagination(categoryID, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
    }
}