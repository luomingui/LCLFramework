using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;
using LCL.Tests.Domain.Repositories.Specifications;

namespace LCL.Tests.Domain.Repositories.EntityFramework
{
    public class ShoppingCartRepository : EntityFrameworkRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(IRepositoryContext context)
            : base(context)
        {

        }

        #region IShoppingCartRepository Members

        public ShoppingCart FindShoppingCartByUser(User user)
        {
            return Find(new ShoppingCartBelongsToCustomerSpecification(user));
        }

        #endregion
    }
}
