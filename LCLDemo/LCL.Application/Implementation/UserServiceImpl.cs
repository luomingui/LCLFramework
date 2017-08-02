using LCL.Application;
using LCL.Bus;
using LCL.Domain.Model;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Domain.Specifications;
using LCL.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LCL.Application.Implementation
{
    /// <summary>
    /// 表示与“客户”相关的应用层服务的一种实现。
    /// </summary>
    public class UserServiceImpl : ApplicationService, IUserService
    {
        #region Private Fields
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly ISalesOrderRepository salesOrderRepository;
        private readonly ILDomainService domainService;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>UserServiceImpl</c>实例。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="salesOrderRepository"></param>
        /// <param name="domainService"></param>
        public UserServiceImpl(IRepositoryContext context,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IShoppingCartRepository shoppingCartRepository,
            ISalesOrderRepository salesOrderRepository,
            ILDomainService domainService,
           IEventBus bus)
            : base(context, bus)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.domainService = domainService;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        #region IUserService Members
        /// <summary>
        /// 根据指定的客户信息，创建客户对象。
        /// </summary>
        /// <param name="userDataObjects">包含了客户信息的数据传输对象。</param>
        /// <returns>已创建客户对象的全局唯一标识。</returns>
        public List<User> CreateUsers(List<User> userDataObjects)
        {
            userRepository.Insert(userDataObjects);
            return userDataObjects; 
        }

        /// <summary>
        /// 校验指定的客户用户名与密码是否一致。
        /// </summary>
        /// <param name="userName">客户用户名。</param>
        /// <param name="password">客户密码。</param>
        /// <returns>如果校验成功，则返回true，否则返回false。</returns>
        public bool ValidateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            return userRepository.CheckPassword(userName, password);
        }

        /// <summary>
        /// 禁用指定用户。
        /// </summary>
        /// <param name="userDataObject">需要禁用的用户。</param>
        /// <returns>如果成功，则返回true，否则返回false。</returns>
        public bool DisableUser(User userDataObject)
        {
            if (userDataObject == null)
                throw new ArgumentNullException("userDataObject");
            User user = null;
            if (!IsEmptyGuidString(userDataObject.ID.ToString()))
                user = userRepository.Get(userDataObject.ID);
            else if (!string.IsNullOrEmpty(userDataObject.UserName))
                user = userRepository.GetUserByName(userDataObject.UserName);
            else if (!string.IsNullOrEmpty(userDataObject.Email))
                user = userRepository.GetUserByEmail(userDataObject.Email);
            else
                throw new ArgumentNullException("userDataObject", "Either ID, UserName or Email should be specified.");
            user.Disable();
            userRepository.Update(user);
            Context.Commit();
            return user.IsDisabled;
        }

        /// <summary>
        /// 启用指定用户。
        /// </summary>
        /// <param name="userDataObject">需要启用的用户。</param>
        /// <returns>如果成功，则返回true，否则返回false。</returns>
        public bool EnableUser(User userDataObject)
        {
            if (userDataObject == null)
                throw new ArgumentNullException("userDataObject");
            User user = null;
            if (!IsEmptyGuidString(userDataObject.ID.ToString()))
                user = userRepository.Get(userDataObject.ID);
            else if (!string.IsNullOrEmpty(userDataObject.UserName))
                user = userRepository.GetUserByName(userDataObject.UserName);
            else if (!string.IsNullOrEmpty(userDataObject.Email))
                user = userRepository.GetUserByEmail(userDataObject.Email);
            else
                throw new ArgumentNullException("userDataObject", "Either ID, UserName or Email should be specified.");
            user.Enable();
            userRepository.Update(user);
            Context.Commit();
            return user.IsDisabled;
        }

        /// <summary>
        /// 根据指定的用户信息，更新用户对象。
        /// </summary>
        /// <param name="userDataObjects">需要更新的用户对象。</param>
        /// <returns>已更新的用户对象。</returns>
        public List<User> UpdateUsers(List<User> userDataObjects)
        {
            userRepository.Update(userDataObjects);

            return userDataObjects;
        }

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="userDataObjects">需要删除的用户对象。</param>
        public void DeleteUsers(List<User> userDataObjects)
        {
            if (userDataObjects == null)
                throw new ArgumentNullException("userDataObjects");
            foreach (var userDataObject in userDataObjects)
            {
                User user = null;
                if (!IsEmptyGuidString(userDataObject.ID))
                    user = userRepository.Get(userDataObject.ID);
                else if (!string.IsNullOrEmpty(userDataObject.UserName))
                    user = userRepository.GetUserByName(userDataObject.UserName);
                else if (!string.IsNullOrEmpty(userDataObject.Email))
                    user = userRepository.GetUserByEmail(userDataObject.Email);
                else
                    throw new ArgumentNullException("userDataObject", "Either ID, UserName or Email should be specified.");
                var userRole = userRoleRepository.Find(Specification<UserRole>.Eval(ur => ur.UserID == user.ID));
                if (userRole != null)
                    userRoleRepository.Delete(userRole);
                userRepository.Delete(user);
            }
            Context.Commit();
        }

        /// <summary>
        /// 根据用户的全局唯一标识获取用户信息。
        /// </summary>
        /// <param name="ID">用户的全局唯一标识。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByKey(Guid ID)
        {
            var user = userRepository.Get(ID);
            return user;
        }

        /// <summary>
        /// 根据用户的电子邮件地址获取用户信息。
        /// </summary>
        /// <param name="email">用户的电子邮件地址。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("email");
            var user = userRepository.GetUserByEmail(email);

            return user;
        }

        /// <summary>
        /// 根据用户的用户名获取用户信息。
        /// </summary>
        /// <param name="userName">用户的用户名。</param>
        /// <param name="spec">查询方式。</param>
        /// <returns>包含了用户信息的数据传输对象。</returns>
        public User GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("userName");
            var user = userRepository.GetUserByName(userName);
            return user;
        }

        /// <summary>
        /// 获取所有用户的信息。
        /// </summary>
        /// <param name="spec">查询方式。</param>
        /// <returns>包含了所有用户信息的数据传输对象列表。</returns>
        public List<User> GetUsers()
        {
            var users = userRepository.FindAll();

            return users.ToList();
        }

        /// <summary>
        /// 根据指定的ID值，获取角色。
        /// </summary>
        /// <param name="id">指定的角色ID值。</param>
        /// <returns>角色。</returns>
        public Role GetRoleByKey(Guid id)
        {
            return roleRepository.Get(id);
        }

        /// <summary>
        /// 获取所有角色。
        /// </summary>
        /// <returns>所有角色。</returns>
        public List<Role> GetRoles()
        {
            var roles = roleRepository.FindAll();

            return roles.ToList();
        }

        /// <summary>
        /// 创建角色。
        /// </summary>
        /// <param name="roleDataObjects">需要创建的角色。</param>
        /// <returns>已创建的角色。</returns>
        public List<Role> CreateRoles(List<Role> roleDataObjects)
        {
            roleRepository.Insert(roleDataObjects);
            return roleDataObjects.ToList();
        }

        /// <summary>
        /// 更新角色。
        /// </summary>
        /// <param name="roleDataObjects">需要更新的角色。</param>
        /// <returns>已更新的角色。</returns>
        public List<Role> UpdateRoles(List<Role> roleDataObjects)
        {
             roleRepository.Update(roleDataObjects);

            return roleDataObjects.ToList();
        }

        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="roleIDs">需要删除的角色ID值列表。</param>
        public void DeleteRoles(IDList roleIDs)
        {
            userRoleRepository.Delete(roleIDs);
        }

        /// <summary>
        /// 将指定的用户赋予指定的角色。
        /// </summary>
        /// <param name="userID">需要赋予角色的用户ID值。</param>
        /// <param name="roleID">需要向用户赋予的角色ID值。</param>
        public void AssignRole(Guid userID, Guid roleID)
        {
            var user = userRepository.Get(userID);
            var role = roleRepository.Get(roleID);
            domainService.AssignRole(user, role);
        }

        /// <summary>
        /// 将指定的用户从角色中移除。
        /// </summary>
        /// <param name="userID">用户ID值。</param>
        public void UnassignRole(Guid userID)
        {
            var user = userRepository.Get(userID);
            domainService.UnassignRole(user);
        }

        /// <summary>
        /// 根据指定的用户名，获取该用户所属的角色。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns>角色。</returns>
        public Role GetUserRoleByUserName(string userName)
        {
            User user = userRepository.GetUserByName(userName);
            var role = userRoleRepository.GetRoleForUser(user);
            return role;
        }

        public List<SalesOrder> GetSalesOrders(string userName)
        {
            User user = userRepository.GetUserByName(userName);
            var salesOrders = user.SalesOrders;
            var result = new List<SalesOrder>();
            if (salesOrders != null)
            {
                foreach (var so in salesOrders)
                {
                    result.Add(so);
                }
            }
            return result;
        }

        #endregion
    }
}
