
using System;
using System.Linq;
using MongoDB.Driver.Linq;
using LCL.Repositories.MongoDB;
using LCL.Domain.Repositories;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class UserRoleRepository : MongoDBRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IRepositoryContext context)
            : base(context) { }

        public Role GetRoleForUser(User user)
        {
            var con = this.Context as MongoDBRepositoryContext;
            var userRoleCollection = con.GetCollectionForType(typeof(UserRole));
            var userRole = userRoleCollection.AsQueryable<UserRole>().Where(ur => ur.UserID == user.ID).FirstOrDefault();
            if (userRole == null)
                return null;
            var roleCollection = con.GetCollectionForType(typeof(Role));
            return roleCollection.AsQueryable<Role>().Where(r => r.ID == userRole.RoleID).FirstOrDefault();
        }
    }
}
