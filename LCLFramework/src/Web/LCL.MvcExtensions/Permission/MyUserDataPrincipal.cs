using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 存放数据的用户实体    
    /// 自定义用户对象的基本功能(A custom principal with support for CG.Security features.) 
    /// </summary>
    public class MyUserDataPrincipal : IPrincipal
    {
        public MyUserDataPrincipal()
        {
            this._Identity = new LCLIdentity();
        }
        public MyUserDataPrincipal(LCLIdentity realIdentity)
        {
            this._Identity = realIdentity;
        }
        //这里可以定义其他一些属性
        public Guid UserId { get; set; }
        public Guid OrgId { get; set; }
        public string UnitName { get; set; }

        #region IPrincipal
        [NonSerialized]
        private IIdentity _Identity;
        public IIdentity Identity
        {
            get
            {
                return _Identity;
            }
            set
            {
                _Identity = value;
            }
        }
        public virtual bool IsInRole(string role)
        {
            return true;
        }
        public virtual bool IsInUser(string user)
        {
            return true;
        }
        #endregion
    }
}
