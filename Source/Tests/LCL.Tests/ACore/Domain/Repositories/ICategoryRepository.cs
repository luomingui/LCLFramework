
using LCL.Domain.Repositories;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories
{
    /// <summary>
    /// 表示用于“商品分类”聚合根的仓储接口。
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
