using LCL.Domain.Model.ViewModels;
using LCL.Domain.Services;
using LCL.WebMvc.Models;
using System;
using System.Web.Mvc;

namespace LCL.WebMvc.Controllers
{
    public class HomeController : CommonController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        #region 订单相关
        [Authorize]
        public ActionResult AddToCart(string productID, string items)
        {
            int quantity = 0;
            if (!int.TryParse(items, out quantity))
                quantity = 1;
            var server = this.Service<IOrderService>();
            server.AddProductToCart(UserID, new Guid(productID), quantity);
            return RedirectToAction("ShoppingCart");

        }

        [Authorize]
        public ActionResult ShoppingCart()
        {
            ShoppingCartModels vModel = new ShoppingCartModels();
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.GetShoppingCart(UserID);
                return View(model);
            }
        }
        [Authorize]
        public ActionResult UpdateShoppingCartItem(string shoppingCartItemID, int quantity)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                proxy.UpdateShoppingCartItem(new Guid(shoppingCartItemID), quantity);
                return Json(null);
            }
        }

        [Authorize]
        public ActionResult DeleteShoppingCartItem(string shoppingCartItemID)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                proxy.DeleteShoppingCartItem(new Guid(shoppingCartItemID));
                return Json(null);
            }
        }
        [Authorize]
        public ActionResult Checkout()
        {
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.Checkout(UserID);
                return View(model);
            }
        }
        [Authorize]
        public ActionResult SalesOrders()
        {
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.GetSalesOrdersForUser(this.UserID);
                return View(model);
            }
        }

        [Authorize]
        public ActionResult SalesOrder(string id)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.GetSalesOrder(new Guid(id));
                return View(model);
            }
        }
        [Authorize]
        public ActionResult Confirm(string id)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                proxy.Confirm(new Guid(id));
                return RedirectToSuccess("确认收货成功！", "SalesOrders", "Home");
            }
        }
        #endregion

    }
}