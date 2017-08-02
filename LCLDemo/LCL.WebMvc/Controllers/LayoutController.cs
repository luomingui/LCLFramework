using LCL.Domain.Model;
using LCL.Domain.Services;
using System;
using System.Web.Mvc;

namespace LCL.WebMvc.Controllers
{
    [HandleError]
    public class LayoutController : CommonController
    {
        public ActionResult CategoriesPartial(string categoryID = null)
        {
            using (var proxy = this.Service<IProductService>())
            {
                var categories = proxy.GetCategories();
                if (string.IsNullOrEmpty(categoryID))
                    ViewBag.CategoryName = "";
                else
                {
                    var category = proxy.GetCategoryByID(new Guid(categoryID));
                    ViewBag.CategoryName = category.Name;
                }
                return PartialView(categories);
            }
        }
        public ActionResult _LoginPartial()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var proxy = this.Service<IOrderService>())
                {
                    ViewBag.ShoppingCartItems = proxy.GetShoppingCartItemCount(UserID);
                }
            }
            return PartialView();
        }
        public ActionResult FeaturedProductsPartial()
        {
            using (var proxy = this.Service<IProductService>())
            {
                var featuredProducts = proxy.GetFeaturedProducts(4);
                return PartialView(featuredProducts);
            }
        }
        public ActionResult ProductsPartial(string categoryID = null, bool? fromIndexPage = null, int pageNumber = 1)
        {
            using (var proxy = this.Service<IProductService>())
            {

                PagedResult<Product> productsWithPagination = null;
                if (string.IsNullOrEmpty(categoryID))
                    productsWithPagination = proxy.GetProductsWithPagination(pageNumber, 10);
                else
                    productsWithPagination = proxy.GetProductsForCategoryWithPagination(new Guid(categoryID), pageNumber, 10);

                if (fromIndexPage != null && !fromIndexPage.Value)
                {
                    if (string.IsNullOrEmpty(categoryID))
                    {
                        ViewBag.CategoryName = "所有商品";
                    }
                    else
                    {
                        var category = proxy.GetCategoryByID(new Guid(categoryID));
                        ViewBag.CategoryName = category.Name;
                    }
                }
                else
                {
                    ViewBag.CategoryName = null;
                }
                ViewBag.CategoryID = categoryID;
                ViewBag.FromIndexPage = fromIndexPage;
                if (fromIndexPage == null || fromIndexPage.Value)
                    ViewBag.Action = "Index";
                else
                    ViewBag.Action = "Category";
                if (productsWithPagination != null)
                {
                    ViewBag.IsFirstPage = productsWithPagination.PageNumber == 1;
                    ViewBag.IsLastPage = productsWithPagination.PageNumber == productsWithPagination.TotalPages;
                }
                return PartialView(productsWithPagination);
            }
        }
    }
}