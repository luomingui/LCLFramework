using LCL.Domain.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using LCL.Domain.Model;
using LCL.WebAPI.Utility;
using LCL.Domain.Model.ViewModels;

namespace LCL.WebAPI.Controllers
{
    /// <summary>
    /// 表示与“销售订单”相关的应用层服务契约。
    /// </summary>
    [RoutePrefix("api/v1/OrderService")]
    [VersionedRoute("api/version", 1)]
    public class OrderServiceController : ApiController
    {
        private readonly IOrderService server;
        /// <summary>
        /// 表示与“销售订单”相关的应用层服务契约。
        /// </summary>
        public OrderServiceController()
        {
            this.server = RF.Service<IOrderService>();
        }
        /// <summary>
        /// 将指定的商品添加到指定客户的购物篮里。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <param name="productID">用于指代特定商品对象的全局唯一标识。</param>
        public void AddProductToCart(Guid customerID, Guid productID, int quantity)
        {
            server.AddProductToCart(customerID, productID, quantity);
        }
        /// <summary>
        /// 购物结账并产生销售订单。
        /// </summary>
        /// <param name="customerID">需要结账并生成订单的客户的全局唯一标识。</param>
        public SalesOrder Checkout(Guid customerID)
        {
            return server.Checkout(customerID);
        }
        /// <summary>
        /// 销售订单确认。
        /// </summary>
        /// <param name="orderID">需要被确认的销售订单的全局唯一标识。</param>
        public void Confirm(Guid orderID)
        {
            server.Confirm(orderID);
        }
        /// <summary>
        /// 将具有指定的全局唯一标识的购物篮项目从购物篮中删除。
        /// </summary>
        /// <param name="shoppingCartItemID">需要删除的购物篮项目的全局唯一标识。</param>
        public void DeleteShoppingCartItem(Guid shoppingCartItemID)
        {
            server.DeleteShoppingCartItem(shoppingCartItemID);
        }
        /// <summary>
        /// 处理发货业务。
        /// </summary>
        /// <param name="orderID">需要进行发货的订单ID值。</param>
        public void Dispatch(Guid orderID)
        {
            server.Dispatch(orderID);
        }
        /// <summary>
        /// 获取系统中的所有订单。
        /// </summary>
        /// <returns>所有订单。</returns>
        public List<SalesOrder> GetAllSalesOrders()
        {
            return server.GetAllSalesOrders();
        }
        /// <summary>
        /// 根据指定的全局唯一标识获取销售订单信息。
        /// </summary>
        /// <param name="orderID">销售订单全局唯一标识。</param>
        /// <returns>包含销售订单信息的数据传输对象。</returns>
        public SalesOrder GetSalesOrder(Guid orderID)
        {
            return server.GetSalesOrder(orderID);
        }
        /// <summary>
        /// 根据指定的用户ID值，获取该用户的所有订单。
        /// </summary>
        /// <param name="userID">用户ID值。</param>
        /// <returns>指定用户的所有订单。</returns>
        public List<SalesOrder> GetSalesOrdersForUser(Guid userID)
        {
            return server.GetSalesOrdersForUser(userID);
        }
        /// <summary>
        /// 根据指定客户对象的全局唯一标识，获取该客户的购物篮信息。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <returns>包含了购物篮信息的数据传输对象。</returns>
        public ShoppingCartModels GetShoppingCart(Guid customerID)
        {
            return server.GetShoppingCart(customerID);
        }
        /// <summary>
        /// 将指定的商品添加到指定客户的购物篮里。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <param name="productID">用于指代特定商品对象的全局唯一标识。</param>
        public int GetShoppingCartItemCount(Guid userID)
        {
            return server.GetShoppingCartItemCount(userID);
        }
        /// <summary>
        /// 使用指定的商品数量，更新购物篮中的项目。
        /// </summary>
        /// <param name="shoppingCartItemID">需要更新的购物篮项目的全局唯一标识。</param>
        /// <param name="quantity">商品数量。</param>
        public void UpdateShoppingCartItem(Guid shoppingCartItemID, int quantity)
        {
            server.UpdateShoppingCartItem(shoppingCartItemID, quantity);
        }
    }
}
