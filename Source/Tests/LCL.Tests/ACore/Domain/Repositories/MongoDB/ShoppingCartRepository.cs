
using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories.Specifications;
namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class ShoppingCartRepository : MongoDBRepository<ShoppingCart>, IShoppingCartRepository
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
