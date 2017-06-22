﻿
using LCL.Domain.Repositories;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories
{
    /// <summary>
    /// 表示用于“角色”聚合根的仓储接口。
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        
    }
}
