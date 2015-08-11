using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Data.Entity;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Reflection;
using LCL.ComponentModel;
using System.Diagnostics;

namespace LCL.Repositories.EntityFramework
{
    public class EntityFrameworkPlugin : LCLPlugin
    {
        protected override int SetupLevel
        {
            get { return PluginSetupLevel.System + 1; }
        }
        public override void Initialize(IApp app)
        {
            ServiceLocator.Instance.Register<IRepositoryContext, EntityFrameworkRepositoryContext>();
            ServiceLocator.Instance.RegisterType(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
        }
    }
}
