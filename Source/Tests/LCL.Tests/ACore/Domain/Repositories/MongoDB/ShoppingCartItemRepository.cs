
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories.Specifications;
using System.Collections.Generic;


namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class ShoppingCartItemRepository : MongoDBRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(IRepositoryContext context)
            : base(context)
        { }

        #region IShoppingCartItemRepository Members

        public ShoppingCartItem FindItem(ShoppingCart shoppingCart, Product product)
        {
            return Find(Specification<ShoppingCartItem>.Eval(sci => sci.ShoppingCart.ID == shoppingCart.ID &&
                sci.Product.ID == product.ID), elp => elp.Product);
        }

        public IEnumerable<ShoppingCartItem> FindItemsByCart(ShoppingCart cart)
        {
            return FindAll(new ShoppingCartItemBelongsToShoppingCartSpecification(cart), elp => elp.Product);
        }

        #endregion
    }
}
