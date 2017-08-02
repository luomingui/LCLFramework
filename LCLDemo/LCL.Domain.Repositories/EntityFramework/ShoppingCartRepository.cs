using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Domain.Model;
using LCL.Domain.Repositories.Specifications;
using LCL;
using System;
using System.Collections.Generic;

namespace LCL.Domain.Repositories.EntityFramework
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
