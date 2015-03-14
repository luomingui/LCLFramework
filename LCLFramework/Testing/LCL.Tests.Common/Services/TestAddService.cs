using LCL.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tests.Common
{
    [Contract]
    public interface ITestAddService : IDomainService
    {
        int A { get; set; }
        int B { get; set; }

        [ServiceOutput]
        int Result { get; set; }
    }

    [Serializable]
    [ContractImpl(typeof(ITestAddService))]
    public class TestAddService : DomainService, ITestAddService
    {
        public int A { get; set; }
        public int B { get; set; }
        public int Result { get; set; }

        protected override void Execute()
        {
            Result = A + B;
        }
    }
}
