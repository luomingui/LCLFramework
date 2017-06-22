
using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories.EntityFramework
{
    public class CategoryRepository : EntityFrameworkRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IRepositoryContext context) : base(context) { }
    }
}
