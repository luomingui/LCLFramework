using LCL;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户
    /// </summary>
    public class LCLIdentity : ILCLIdentity
    {
        public User User { get; internal set; }
        public List<Role> Roles { get; internal set; }
        public Guid UserId { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public int DepUserType { get; set; }

        #region ILCLIdentity
        public string UserCode { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        #endregion

        internal void LoadChildrenProperties()
        {
            if (this.User != null)
            {
                this.UserId = this.User.ID;
                this.UserCode = this.User.Name;
                this.Name = this.User.Name;
                this.IsAuthenticated = true;
                if (this.User.Department != null)
                    this.DepUserType = (int)this.User.Department.DepartmentType;
                else
                {
                    this.DepUserType = Convert.ToInt32(DbFactory.DBA.QueryValue(@"SELECT d.DepartmentType FROM Department d INNER JOIN [User] u ON u.Department_ID = d.ID WHERE u.ID='" + this.UserId + "'"));
                }
                this.Roles = RF.Concrete<IRoleRepository>().GetRoleByUserId(this.User.ID);
            }
            else
            {
                this.UserId = Guid.Empty;
                this.IsAuthenticated = false;
                this.Roles = null;
                this.DepUserType = -100;
            }
        }
    }
    public interface ILCLIdentityRepository
    {

    }
    public partial class LCLIdentityRepository : ILCLIdentityRepository
    {
        public LCLIdentityRepository() { }
        internal LCLIdentity GetBy(string username, string password)
        {
            var res = new LCLIdentity
            {
                User = RF.Concrete<IUserRepository>().GetBy(username, password)
            };

            res.LoadChildrenProperties();

            return res;
        }
        internal LCLIdentity GetById(Guid ID)
        {
            var res = new LCLIdentity
            {
                User = RF.Concrete<UserRepository>().GetByKey(ID) as User
            };
            res.LoadChildrenProperties();
            return res;
        }
    }
}
