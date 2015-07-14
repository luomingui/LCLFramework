using LCL.Events;
using System;
using System.Diagnostics;

namespace LCL.Tests.Common
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User : MyEntity
    {
        public string Name { set; get; }
        public string Password { set; get; }
        public bool IsLockedOut { get; set; }
        public string Email { set; get; }
        public virtual Position Position { get; set; }
        /// <summary>
        /// 当客户完成收货后，对销售订单进行确认。
        /// </summary>
        public void ChangeEmail(string email)
        {
            Debug.WriteLine("ChangeEmail=>Name:" + Name + " Email:" + Email);
            DomainEvent.Publish<UserChangeEmailDomainEvent>(new UserChangeEmailDomainEvent(this) { Email = email });
        }
    }
}
