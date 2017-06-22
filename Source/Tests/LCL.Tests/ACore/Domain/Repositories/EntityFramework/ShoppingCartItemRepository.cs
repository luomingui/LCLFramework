
using System.Collections.Generic;
using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;
using LCL.Domain.Specifications;
using LCL.Tests.Domain.Repositories.Specifications;
namespace LCL.Tests.Domain.Repositories.EntityFramework
{
    public class ShoppingCartItemRepository : EntityFrameworkRepository<ShoppingCartItem>, IShoppingCartItemRepository
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
