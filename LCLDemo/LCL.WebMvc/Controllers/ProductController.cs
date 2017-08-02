using LCL.Domain.Model;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using System;
using System.Web.Mvc;

namespace LCL.WebMvc.Controllers
{
    public class ProductController : CommonController<Product>
    {
        public override ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            return base.Index(currentPageNum, pageSize, collection);
        }
        public ActionResult Category(string categoryID = null, int pageNumber = 1)
        {
            ViewData["CategoryID"] = categoryID;
            ViewData["FromIndexPage"] = false;
            return View();
        }
        public ActionResult ProductInfo(Guid id)
        {
            using (var proxy = this.Service<IProductService>())
            {
                var product = proxy.GetProductByID(id);
                return View(product);
            }
        }

    }
}