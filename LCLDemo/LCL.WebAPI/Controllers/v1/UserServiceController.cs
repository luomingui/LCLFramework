using LCL.Domain.Model;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Domain.Specifications;
using System;
using System.Web.Http;
using LCL.Domain.ValueObject;
using System.Collections.Generic;
using LCL.WebAPI.Utility;

namespace LCL.WebAPI.Controllers
{
    /// <summary>
    /// 表示与“用户”相关的应用层服务契约。
    /// </summary>
    [VersionedRoute("api/version", 1)]
    [RoutePrefix("api/v1/UserService")]
    public class UserServiceController : ApiController
    {
        private readonly IUserService server;
        /// <summary>
        /// 表示与“用户”相关的应用层服务契约。
        /// </summary>
        public UserServiceController()
        {
            this.server = RF.Service<IUserService>();
        }
        /// <summary>
        /// 将指定的用户赋予指定的角色。
        /// </summary>
        /// <param name="userID">需要赋予角色的用户ID值。</param>
        /// <param name="roleID">需要向用户赋予的角色ID值。</param>
        public void AssignRole(Guid userID, Guid roleID)
        {
            server.AssignRole(userID, roleID);
        }
        /// <summary>
        /// 创建角色。
        /// </summary>
        /// <param name="roleDataObjects">需要创建的角色。</param>
        /// <returns>已创建的角色。</returns>
        public List<Role> CreateRoles(List<Role> roleDataObjects)
        {
            return server.CreateRoles(roleDataObjects);
        }
        /// <summary>
        /// 根据指定的用户信息，创建用户对象。
        /// </summary>
        /// <param name="userDataObjects">包含了用户信息的数据传输对象。</param>
        /// <returns>已创建用户对象的全局唯一标识。</returns>
        public List<User> CreateUsers(List<User> userDataObjects)
        {
            return server.CreateUsers(userDataObjects);
        }
        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="roleIDs">需要删除的角色ID值列表。</param>
        public void DeleteRoles(IDList roleIDs)
        {
            server.DeleteRoles(roleIDs);
        }
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="userDataObjects">需要删除的用户对象。</param>
        public void DeleteUsers(List<User> userDataObjects)
        {
            server.DeleteUsers(userDataObjects);
        }
        /// <summary>
        /// 禁用指定用户。
        /// </summary>
        /// <param name="userDataObject">需要禁用的用户。</param>
        /// <returns>如果成功，则返回true，否则返回false。</returns>
        public bool DisableUser(User userDataObject)
        {
            return server.DisableUser(userDataObject);
        }
        /// <summary>
        /// 启用指定用户。
        /// </summary>
        /// <param name="userDataObject">需要启用的用户。</param>
        /// <returns>如果成功，则返回true，否则返回false。</returns>
        public bool EnableUser(User userDataObject)
        {
            return server.EnableUser(userDataObject);
        }
        /// <summary>
        /// 根据指定的ID值，获取角色。
        /// </summary>
        /// <param name="id">指定的角色ID值。</param>
        /// <returns>角色。</returns>
        public Role GetRoleByKey(Guid id)
        {
            return server.GetRoleByKey(id);
        }
        /// <summary>
        /// 获取所有角色。
        /// </summary>
        /// <returns>所有角色。</returns>
        public List<Role> GetRoles()
        {
            return server.GetRoles();
        }
        /// <summary>
        /// 根据指定的用户名，获取该用户所属的角色。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns>角色。</returns>
        public List<SalesOrder> GetSalesOrders(string userName)
        {
            return server.GetSalesOrders(userName);
        }
        /// <summary>
        /// 根据用户的电子邮件地址获取用户信息。
        /// </summary>
        /// <param name="email">用户的电子邮件地址。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByEmail(string email)
        {
            return server.GetUserByEmail(email);
        }
        /// <summary>
        /// 根据用户的全局唯一标识获取用户信息。
        /// </summary>
        /// <param name="ID">用户的全局唯一标识。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByKey(Guid ID)
        {
            return server.GetUserByKey(ID);
        }
        /// <summary>
        /// 根据用户的用户名获取用户信息。
        /// </summary>
        /// <param name="userName">用户的用户名。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByName(string userName)
        {
            return server.GetUserByName(userName);
        }
        /// <summary>
        /// 根据指定的用户名，获取该用户所属的角色。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns>角色。</returns>
        public Role GetUserRoleByUserName(string userName)
        {
            return server.GetUserRoleByUserName(userName);
        }
        /// <summary>
        /// 获取所有用户的信息。
        /// </summary>
        /// <returns>包含了所有用户信息的数据传输对象列表。</returns>
        public List<User> GetUsers()
        {
            return server.GetUsers();
        }
        /// <summary>
        /// 将指定的用户从角色中移除。
        /// </summary>
        /// <param name="userID">用户ID值。</param>
        public void UnassignRole(Guid userID)
        {
            server.UnassignRole(userID);
        }
        /// <summary>
        /// 更新角色。
        /// </summary>
        /// <param name="roleDataObjects">需要更新的角色。</param>
        /// <returns>已更新的角色。</returns>
        public List<Role> UpdateRoles(List<Role> roleDataObjects)
        {
            return server.UpdateRoles(roleDataObjects);
        }
        /// <summary>
        /// 根据指定的用户信息，更新用户对象。
        /// </summary>
        /// <param name="userDataObjects">需要更新的用户对象。</param>
        /// <returns>已更新的用户对象。</returns>
        public List<User> UpdateUsers(List<User> userDataObjects)
        {
            return server.UpdateUsers(userDataObjects);
        }
        /// <summary>
        /// 校验指定的用户用户名与密码是否一致。
        /// </summary>
        /// <param name="userName">用户用户名。</param>
        /// <param name="password">用户密码。</param>
        /// <returns>如果校验成功，则返回true，否则返回false。</returns>
        public bool ValidateUser(string userName, string password)
        {
            return server.ValidateUser(userName, password);
        }
    }
}
