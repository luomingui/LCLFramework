using LCL.Infrastructure;
using System;
using System.IO;

namespace LCL.Services
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetDataDir();
            EngineContext.Initialize(false);
        }

        private static void SetDataDir()
        {
            DirectoryInfo baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string data_dir = baseDir.FullName;
            if ((baseDir.Name.ToLower() == "debug" || baseDir.Name.ToLower() == "release")
                && (baseDir.Parent.Name.ToLower() == "bin"))
            {
                data_dir = Path.Combine(baseDir.Parent.Parent.FullName, "App_Data");
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", data_dir);
        }
    }
}