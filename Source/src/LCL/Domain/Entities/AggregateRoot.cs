using System;
using System.Collections.Generic;
using System.Reflection;
using LCL;

namespace LCL.Domain.Entities
{
    /// <summary>
    /// 聚合根 唯一类型是Guid
    /// </summary>
    [Serializable]
    public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
    {

    }
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TPrimaryKey">自定义唯一类型</typeparam>
    [Serializable]
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
      
    }



}
