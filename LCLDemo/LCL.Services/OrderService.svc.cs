using LCL.Domain.Model;
using LCL.Domain.Model.ViewModels;
using LCL.Domain.Services;
using LCL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LCL.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class OrderService : IOrderService
    {
        private readonly IOrderService orderServiceImpl = RF.Service<IOrderService>();
        public Int32 GetShoppingCartItemCount(Guid userID)
        {
            try
            {
                return orderServiceImpl.GetShoppingCartItemCount(userID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void AddProductToCart(Guid customerID, Guid productID, Int32 quantity)
        {
            try
            {
                orderServiceImpl.AddProductToCart(customerID, productID, quantity);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public ShoppingCartModels GetShoppingCart(Guid customerID)
        {
            try
            {
                return orderServiceImpl.GetShoppingCart(customerID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void UpdateShoppingCartItem(Guid shoppingCartItemID, Int32 quantity)
        {
            try
            {
                orderServiceImpl.UpdateShoppingCartItem(shoppingCartItemID, quantity);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void DeleteShoppingCartItem(Guid shoppingCartItemID)
        {
            try
            {
                orderServiceImpl.DeleteShoppingCartItem(shoppingCartItemID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public SalesOrder Checkout(Guid customerID)
        {
            try
            {
                return orderServiceImpl.Checkout(customerID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void Confirm(Guid orderID)
        {
            try
            {
                orderServiceImpl.Confirm(orderID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void Dispatch(Guid orderID)
        {
            try
            {
                orderServiceImpl.Dispatch(orderID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<SalesOrder> GetSalesOrdersForUser(Guid userID)
        {
            try
            {
                return orderServiceImpl.GetSalesOrdersForUser(userID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<SalesOrder> GetAllSalesOrders()
        {
            try
            {
                return orderServiceImpl.GetAllSalesOrders();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public SalesOrder GetSalesOrder(Guid orderID)
        {
            try
            {
                return orderServiceImpl.GetSalesOrder(orderID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void Dispose() { orderServiceImpl.Dispose(); }
    }
}