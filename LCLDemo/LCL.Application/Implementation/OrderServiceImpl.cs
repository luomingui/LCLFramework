
using LCL.Application;
using LCL.Bus;
using LCL.Domain.Model;
using LCL.Domain.Model.ViewModels;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LCL.Application.Implementation
{
    /// <summary>
    /// 表示与“销售订单”相关的应用层服务的一种实现。
    /// </summary>
    public class OrderServiceImpl : ApplicationService, IOrderService
    {
        #region Private Fields
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IShoppingCartItemRepository shoppingCartItemRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly ISalesOrderRepository salesOrderRepository;
        private readonly ILDomainService domainService;
        private readonly IEventBus bus;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>OrderServiceImpl</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>OrderServiceImpl</c>类型的仓储上下文实例。</param>
        /// <param name="shoppingCartRepository">“购物篮”仓储实例。</param>
        /// <param name="shoppingCartItemRepository">“购物篮项目”仓储实例。</param>
        /// <param name="productRepository">“笔记本电脑”仓储实例。</param>
        /// <param name="customerRepository">“客户”仓储实例。</param>
        /// <param name="salesOrderRepository">“销售订单”仓储实例。</param>
        public OrderServiceImpl(IRepositoryContext context,
            IShoppingCartRepository shoppingCartRepository,
            IShoppingCartItemRepository shoppingCartItemRepository,
            IProductRepository productRepository,
            IUserRepository customerRepository,
            ISalesOrderRepository salesOrderRepository,
            ILDomainService domainService,
            IEventBus bus)
            :base(context,bus)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.shoppingCartItemRepository = shoppingCartItemRepository;
            this.productRepository = productRepository;
            this.userRepository = customerRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.domainService = domainService;
            this.bus = bus;
        }
        #endregion

        #region IOrderService Members
        public int GetShoppingCartItemCount(Guid userID)
        {
            var user = userRepository.Get(userID);
            var shoppingCart = shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new InvalidOperationException("没有可用的购物车实例。");
            var shoppingCartItems = shoppingCartItemRepository.FindItemsByCart(shoppingCart);
            return shoppingCartItems.Sum(s => s.Quantity);
        }
        /// <summary>
        /// 将指定的商品添加到指定客户的购物篮里。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <param name="productID">用于指代特定商品对象的全局唯一标识。</param>
        public void AddProductToCart(Guid customerID, Guid productID, int quantity)
        {
            var user = userRepository.Get(customerID);

            var shoppingCart = shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new LException("Shopping cart doesn't exist for customer {0}.", customerID);
            var product = productRepository.Get(productID);
            var shoppingCartItem = shoppingCartItemRepository.FindItem(shoppingCart, product);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem(product, shoppingCart, quantity);
                shoppingCartItemRepository.Insert(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.UpdateQuantity(shoppingCartItem.Quantity + quantity);
                shoppingCartItemRepository.Update(shoppingCartItem);
            }
            Context.Commit();
        }

        /// <summary>
        /// 根据指定客户对象的全局唯一标识，获取该客户的购物篮信息。
        /// </summary>
        /// <param name="customerID">用于指代特定客户对象的全局唯一标识。</param>
        /// <returns>包含了购物篮信息的数据传输对象。</returns>
        public ShoppingCartModels GetShoppingCart(Guid customerID)
        {
            ShoppingCartModels model = new ShoppingCartModels();
            var user = userRepository.Get(customerID);

            var shoppingCart = shoppingCartRepository.FindShoppingCartByUser(user);
            if (shoppingCart == null)
                throw new LException("Customer ID='{0}' doesn't have shopping cart defined.", customerID);

            var shoppingCartItems = shoppingCartItemRepository.FindItemsByCart(shoppingCart);

            if (shoppingCartItems != null && shoppingCartItems.Count() > 0)
            {
                foreach (var shoppingCartItem in shoppingCartItems)
                    model.Items.Add(shoppingCartItem);
                model.Subtotal = model.Items.Sum(p => p.LineAmount);
            }

            return model;
        }

        /// <summary>
        /// 使用指定的商品数量，更新购物篮中的项目。
        /// </summary>
        /// <param name="shoppingCartItemID">需要更新的购物篮项目的全局唯一标识。</param>
        /// <param name="quantity">商品数量。</param>
        public void UpdateShoppingCartItem(Guid shoppingCartItemID, int quantity)
        {
            var shoppingCartItem = shoppingCartItemRepository.Get(shoppingCartItemID);
            shoppingCartItem.UpdateQuantity(quantity);
            shoppingCartItemRepository.Update(shoppingCartItem);
            Context.Commit();
        }

        /// <summary>
        /// 将具有指定的全局唯一标识的购物篮项目从购物篮中删除。
        /// </summary>
        /// <param name="shoppingCartItemID">需要删除的购物篮项目的全局唯一标识。</param>
        public void DeleteShoppingCartItem(Guid shoppingCartItemID)
        {
            var shoppingCartItem = shoppingCartItemRepository.Get(shoppingCartItemID);
            shoppingCartItemRepository.Delete(shoppingCartItem);
            Context.Commit();
        }

        /// <summary>
        /// 购物结账并产生销售订单。
        /// </summary>
        /// <param name="customerID">需要结账并生成订单的客户的全局唯一标识。</param>
        public SalesOrder Checkout(Guid customerID)
        {
            var user = userRepository.Get(customerID);
            var shoppingCart = shoppingCartRepository.FindShoppingCartByUser(user);


            var shoppingCartItems = shoppingCartItemRepository.FindItemsByCart(shoppingCart);
            if (shoppingCartItems == null ||
                shoppingCartItems.Count() == 0)
                throw new InvalidOperationException("购物篮中没有任何物品。");

            var salesOrder = new SalesOrder();
            salesOrder.SalesLines = new List<SalesLine>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var salesLine = shoppingCartItem.ConvertToSalesLine();
                salesLine.SalesOrder = salesOrder;
                salesOrder.SalesLines.Add(salesLine);
                shoppingCartItemRepository.Delete(shoppingCartItem);
            }
            salesOrder.User = user;
            salesOrder.Status = SalesOrderStatus.Paid;
            salesOrderRepository.Insert(salesOrder);
            Context.Commit();
            return salesOrder;
        }

        /// <summary>
        /// 销售订单确认。
        /// </summary>
        /// <param name="orderID">需要被确认的销售订单的全局唯一标识。</param>
        public void Confirm(Guid orderID)
        {
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(Context, bus))
            {
                var salesOrder = salesOrderRepository.Get(orderID);
                salesOrder.Confirm();
                salesOrderRepository.Update(salesOrder);
                coordinator.Commit();
            }
        }

        public void Dispatch(Guid orderID)
        {
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(Context, bus))
            {
                var salesOrder = salesOrderRepository.Get(orderID);
                salesOrder.Dispatch();
                salesOrderRepository.Update(salesOrder);
                coordinator.Commit();
            }
        }

        public List<SalesOrder> GetSalesOrdersForUser(Guid userID)
        {
            var user = userRepository.Get(userID);
            var salesOrders = salesOrderRepository.FindSalesOrdersByUser(user);
            var salesOrderDataObjectList = new List<SalesOrder>();
            foreach (var salesOrder in salesOrders)
                salesOrderDataObjectList.Add(salesOrder);
            return salesOrderDataObjectList;
        }

        public List<SalesOrder> GetAllSalesOrders()
        {
            var salesOrders = salesOrderRepository.FindAll(sort => sort.DateCreated, SortOrder.Descending);
            var result = new List<SalesOrder>();
            salesOrders
                .ToList()
                .ForEach(p => result.Add(p));
            return result;
        }

        /// <summary>
        /// 根据指定的全局唯一标识获取销售订单信息。
        /// </summary>
        /// <param name="orderID">销售订单全局唯一标识。</param>
        /// <returns>包含销售订单信息的数据传输对象。</returns>
        public SalesOrder GetSalesOrder(Guid orderID)
        {
            var salesOrder = salesOrderRepository.GetSalesOrderByID(orderID);
            return salesOrder;
        }

        #endregion

        
    }
}
