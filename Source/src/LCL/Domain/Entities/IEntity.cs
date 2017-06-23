using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Entities
{
    public interface IEntity : IEntity<Guid>
    {

    }
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
        bool IsTransient();
    }
}
