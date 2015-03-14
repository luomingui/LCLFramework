using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests.Repositories.EntityFrameworkRepository
{
    public interface IUserRepository : IRepository<User>
    {

    }
    [Serializable]
    [RepositoryFor(typeof(User))]
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public User GetBy(string username, string password)
        {
            var users = base.Get(e => e.Code == username && e.Password == password);
            if (users.Count() > 0)
            {
                return users.ToList()[0];
            }
            return null;
        }
        public User GetBy(string username)
        {
            var li = EFContext.Context.Set<User>().Include("Position")
                .Where(e => e.Code == username);

            var lis = EFContext.Context.Set<User>().Where(e => e.Code == username);

            var users = base.Get(e => e.Code == username);
            if (users.Count() > 0)
            {
                return users.ToList()[0];
            }
            return null;
        }
    }
}
