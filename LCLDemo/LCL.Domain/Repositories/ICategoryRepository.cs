
using LCL.Domain.Repositories;
using LCL.Domain.Model;

namespace LCL.Domain.Repositories
{
    /// <summary>
    /// 表示用于“商品分类”聚合根的仓储接口。
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
