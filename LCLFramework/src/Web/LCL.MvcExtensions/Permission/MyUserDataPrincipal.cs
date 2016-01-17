using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LCL.MvcExtensions
{
    //存放数据的用户实体
    public class MyUserDataPrincipal : IPrincipal
    {
        public MyUserDataPrincipal()
        {
            UserModel = new Hashtable();
            RoleId = new List<Guid>();
            GroupId = new List<Guid>();
        }
        public Guid UserId { get; set; }
        //这里可以定义其他一些属性
        public List<Guid> RoleId { get; set; }
        public List<Guid> GroupId { get; set; }
        public Hashtable UserModel { get; set; }
        //当使用Authorize特性时，会调用改方法验证角色 
        public virtual bool IsInRole(string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(role)) return true;
                return RoleId.Contains(Guid.Parse(role));
            }
            catch (Exception)
            {
                return true;  
            }
        }
        //验证用户信息
        public bool IsInUser(Guid userId)
        {
            return true;
        }
        [ScriptIgnore]    //在序列化的时候忽略该属性
        public IIdentity Identity { get { return LEnvironment.Principal.Identity; } }
    }
}
