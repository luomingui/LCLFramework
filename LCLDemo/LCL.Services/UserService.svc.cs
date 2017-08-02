using LCL.Domain.Model;
using LCL.Domain.Services;
using LCL.Domain.ValueObject;
using LCL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LCL.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UserService : IUserService
    {
        private readonly IUserService userServiceImpl = RF.Service<IUserService>();
        public List<User> CreateUsers(List<User> users)
        {
            try
            {
                return userServiceImpl.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Boolean ValidateUser(String userName, String password)
        {
            try
            {
                return userServiceImpl.ValidateUser(userName, password);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Boolean DisableUser(User user)
        {
            try
            {
                return userServiceImpl.DisableUser(user);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Boolean EnableUser(User user)
        {
            try
            {
                return userServiceImpl.EnableUser(user);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<User> UpdateUsers(List<User> users)
        {
            try
            {
                return userServiceImpl.UpdateUsers(users);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void DeleteUsers(List<User> users)
        {
            try
            {
                userServiceImpl.DeleteUsers(users);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public User GetUserByKey(Guid ID)
        {
            try
            {
                return userServiceImpl.GetUserByKey(ID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public User GetUserByEmail(String email)
        {
            try
            {
                return userServiceImpl.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public User GetUserByName(String userName)
        {
            try
            {
                return userServiceImpl.GetUserByName(userName);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<User> GetUsers()
        {
            try
            {
                return userServiceImpl.GetUsers();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Role> GetRoles()
        {
            try
            {
                return userServiceImpl.GetRoles();
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Role GetRoleByKey(Guid id)
        {
            try
            {
                return userServiceImpl.GetRoleByKey(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Role> CreateRoles(List<Role> roles)
        {
            try
            {
                return userServiceImpl.CreateRoles(roles);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<Role> UpdateRoles(List<Role> roles)
        {
            try
            {
                return userServiceImpl.UpdateRoles(roles);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void DeleteRoles(IDList roleIDs)
        {
            try
            {
                userServiceImpl.DeleteRoles(roleIDs);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void AssignRole(Guid userID, Guid roleID)
        {
            try
            {
                userServiceImpl.AssignRole(userID, roleID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void UnassignRole(Guid userID)
        {
            try
            {
                userServiceImpl.UnassignRole(userID);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public Role GetUserRoleByUserName(String userName)
        {
            try
            {
                return userServiceImpl.GetUserRoleByUserName(userName);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public List<SalesOrder> GetSalesOrders(String userName)
        {
            try
            {
                return userServiceImpl.GetSalesOrders(userName);
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
            }
        }
        public void Dispose() { userServiceImpl.Dispose(); }
    }
}


