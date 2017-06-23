using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Entities
{
    public interface IAggregateRoot : IAggregateRoot<Guid>, IEntity
    {

    }
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {

    }
  
}
