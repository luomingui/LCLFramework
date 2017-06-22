

using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class CategoryRepository : MongoDBRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IRepositoryContext context) : base(context) { }
    }
}
