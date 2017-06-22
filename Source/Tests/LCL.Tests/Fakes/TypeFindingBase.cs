using LCL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests
{
    public abstract class TypeFindingBase : TestsBase
    {
        protected ITypeFinder typeFinder;

        protected abstract Type[] GetTypes();

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            typeFinder = new FakeTypeFinder(typeof(TypeFindingBase).Assembly, GetTypes());
        }
    }
}
