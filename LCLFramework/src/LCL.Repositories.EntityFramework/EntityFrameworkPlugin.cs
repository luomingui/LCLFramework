
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
            get { return PluginSetupLevel.System; }
        }
        public override void Initialize(IApp app)
        {
            Debug.WriteLine("LCL.Repositories.EntityFramework Plugin Initialize......");
            ServiceLocator.Instance.Register<IRepositoryContext,EntityFrameworkRepositoryContext>();
        }
    }
}
