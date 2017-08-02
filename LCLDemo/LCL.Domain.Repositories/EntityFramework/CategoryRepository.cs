
using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Domain.Model;

namespace LCL.Domain.Repositories.EntityFramework
{
    public class CategoryRepository : EntityFrameworkRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IRepositoryContext context) : base(context) { }
    }
}
