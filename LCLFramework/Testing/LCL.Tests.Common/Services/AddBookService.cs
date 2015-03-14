using LCL.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tests.Common
{
    [Serializable]
    [Contract, ContractImpl]
    public class AddBookService : DomainService
    {
        [ServiceOutput]
        public int Result { get; set; }

        protected override void Execute()
        {
            this.Result = 1;
        }
    }

    [Serializable]
    [ContractImpl(typeof(AddBookService), Version = "1.0.0.2")]
    public class AddBookService_V1002 : AddBookService
    {
        protected override void Execute()
        {
            this.Result = 2;
        }
    }

    [Serializable]
    [ContractImpl(typeof(AddBookService), Version = "1.0.0.3")]
    public class AddBookService_V1003 : AddBookService
    {
        protected override void Execute()
        {
            this.Result = 3;
        }
    }
}