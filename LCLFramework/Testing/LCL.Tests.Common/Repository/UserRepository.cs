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
        User GetBy(string username);
        User GetBy(string username, string password);
        IEnumerable<User> GetFeaturedUsers(int count = 0);
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
            var users = base.Get(e => e.Name == username && e.Password == password);
            if (users.Count() > 0)
            {
                return users.ToList()[0];
            }
            return null;
        }
        public User GetBy(string username)
        {
            var li = EFContext.Context.Set<User>().Include("Position")
                .Where(e => e.Name == username);

            var lis = EFContext.Context.Set<User>().Where(e => e.Name == username);

            var users = base.Get(e => e.Name == username);
            if (users.Count() > 0)
            {
                return users.ToList()[0];
            }
            return null;
        }
        public IEnumerable<User> GetFeaturedUsers(int count = 0)
        {
            var ctx = this.EFContext.Context as EFTestDbContext;
            if (ctx != null)
            {
                var query = from p in ctx.User
                            where p.IsLockedOut
                            select p;
                if (count == 0)
                    return query.ToList();
                else
                    return query.Take(count).ToList();
            }
            return null;
        }
    }
}
