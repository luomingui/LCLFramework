
using LCL;
using LCL.Domain.Events;
using LCL.Domain.Services;
using System;

namespace LCL.Domain.Events.Handlers
{
    /// <summary>
    /// 表示向外发送邮件的事件处理器。
    /// </summary>
    [HandlesAsynchronously]
    public class SendEmailHandler : IEventHandler<OrderDispatchedEvent>, IEventHandler<OrderConfirmedEvent>
    {
        public SendEmailHandler()
        { }

        #region IEventHandler<OrderDispatchedEvent> Members
        /// <summary>
        /// 处理给定的事件。
        /// </summary>
        /// <param name="evnt">需要处理的事件。</param>
        public void Handle(OrderDispatchedEvent evnt)
        {
            try
            {
                Utils.SendEmail(evnt.UserEmailAddress,
                    "您的订单已经发货",
                    string.Format("您的订单 {0} 已于 {1} 发货。有关订单的更多信息，请与系统管理员联系。",
                    evnt.OrderID.ToString().ToUpper(), evnt.DispatchedDate));
            }
            catch (Exception ex)
            {
                Logger.LogError("SendEmailHandler", ex);
            }
        }

        #endregion

        #region IEventHandler<OrderConfirmedEvent> Members
        /// <summary>
        /// 处理给定的事件。
        /// </summary>
        /// <param name="evnt">需要处理的事件。</param>
        public void Handle(OrderConfirmedEvent evnt)
        {
            try
            {
                Utils.SendEmail(evnt.UserEmailAddress,
                    "您的订单已经确认收货",
                    string.Format("您的订单 {0} 已于 {1} 确认收货。有关订单的更多信息，请与系统管理员联系。",
                    evnt.OrderID.ToString().ToUpper(), evnt.ConfirmedDate));
            }
            catch (Exception ex)
            {
                Logger.LogError("SendEmailHandler", ex);
            }
        }

        #endregion
    }
}
