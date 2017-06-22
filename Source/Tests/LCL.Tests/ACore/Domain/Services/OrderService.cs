using LCL.Application;
using LCL.Bus;
using LCL.Domain.Repositories;
using LCL.ServiceProxys;
using LCL.Tests.Domain.Repositories;
using LCL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests.ACore.Domain.Services
{
    /// <summary>
    /// 表示与“销售订单”相关的应用层服务契约。
    /// </summary>
    [ServiceContract(Namespace = "http://www.ByteartRetail.com")]
    public interface IOrderService : IApplicationServiceContract
    {
        #region Methods
       
        /// <summary>
        /// 销售订单确认。
        /// </summary>
        /// <param name="orderID">需要被确认的销售订单的全局唯一标识。</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void Confirm(Guid orderID);
     
        #endregion
    }
    public class OrderService : ApplicationService
    {
        
        #region Private Fields
        private readonly ISalesOrderRepository salesOrderRepository;
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
        public OrderService(IRepositoryContext context,
            ISalesOrderRepository salesOrderRepository,
            IEventBus bus)
            :base(context,bus)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.bus = bus;
        }
        #endregion

        #region IOrderService Members
        /// <summary>
        /// 销售订单确认。
        /// </summary>
        /// <param name="orderID">需要被确认的销售订单的全局唯一标识。</param>
        public void Confirm(Guid orderID)
        {
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(Context,EventBus))
            {
                var salesOrder = salesOrderRepository.GetByKey(orderID);
                salesOrder.Confirm();
                salesOrderRepository.Update(salesOrder);
                coordinator.Commit();
            }
        }
        #endregion
    }
}
