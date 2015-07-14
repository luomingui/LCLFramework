using LCL.Events;
using LCL.Events.Bus;
using System;
using System.Diagnostics;

namespace LCL.Tests.Common
{
    public class UserChangeEmailDomainEventHandler : IDomainEventHandler<UserChangeEmailDomainEvent>
    {
        private readonly IEventBus bus;
        public UserChangeEmailDomainEventHandler(IEventBus bus)
        {
            this.bus = bus;
        }
        public void Handle(UserChangeEmailDomainEvent evnt)
        {
            
            User model = evnt.Source as User;
            model.Email = evnt.Email;

            bus.Publish<UserChangeEmailDomainEvent>(evnt);

        }
    }
    [Serializable]
    public class UserChangeEmailDomainEvent : DomainEvent
    {
        public string Email { get; set; }
        #region Ctor
        public UserChangeEmailDomainEvent() { }
        public UserChangeEmailDomainEvent(IEntity source) : base(source) { }
        #endregion
    }


    /// <summary>
    /// 表示向外发送邮件的事件处理器。
    /// </summary>
    [HandlesAsynchronously]
    public class SendEmailHandler : IEventHandler<UserChangeEmailDomainEvent>
    {
        public SendEmailHandler()
        { }

        #region IEventHandler<UserChangeEmailDomainEvent> Members
        /// <summary>
        /// 处理给定的事件。
        /// </summary>
        /// <param name="evnt">需要处理的事件。</param>
        public void Handle(UserChangeEmailDomainEvent evnt)
        {
            try
            {
                // 此处仅为演示，所以邮件内容很简单。可以根据自己的实际情况做一些复杂的邮件功能，比如
                // 使用邮件模板或者邮件风格等。
                Debug.Write("SendEmailHandler");
            }
            catch (Exception ex)
            {
                // 如遇异常，直接记Log
            }
        }

        #endregion
    }
}
