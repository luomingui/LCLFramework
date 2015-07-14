using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Transactions
{
    public interface ITransactionCoordinator : IUnitOfWork, IDisposable
    {
    }
}
