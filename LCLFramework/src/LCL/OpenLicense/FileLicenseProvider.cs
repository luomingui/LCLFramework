using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LCL.Serialization;
//http://www.codeguru.com/csharp/.net/net_framework/licensing/article.php/c5469/Licensed-Applications-using-the-NET-Framework.htm
namespace LCL.OpenLicense
{
    /*
    [LicenseProvider(typeof(FileLicenseProvider))]
    public abstract class App
    {
        private License license = null;
        public App()
        {
            try
            {
                license = LicenseManager.Validate(typeof(App), this);
            }
            catch
            {
                throw new Exception("LCL组件未授权,请联系程序开发商.");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (license != null)
                {
                    license.Dispose();
                    license = null;
                }
            }
        }
      }
     */
    /// <summary>
    /// 许可证的实例类
    /// FileLicense会应用到控件上，如果返回给控件的FileLicense为空，则控件会抛出异常
    /// </summary>
    public class FileLicense : License
    {
        //许可验证提供程序 
        private FileLicenseProvider owner;
        private string key;
        public FileLicense(FileLicenseProvider owner, string key)
        {
            this.owner = owner;
            this.key = key;
        }
        //许可Key,通俗的理解是序列号。 
        public override string LicenseKey
        {
            get
            {
                return key;
            }
        }
        public override void Dispose()
        {

        }
    }
    //- 下面是许可证提供者，主要用于判断是否发出许可证
    //  许可证有两种方式，设计时和运行时，我们主要考虑运行时许可
    public class FileLicenseProvider : LicenseProvider
    {
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            FileLicense license = null;
            StringBuilder listMsg =new StringBuilder();
            if (context != null)
            {
                if (context.UsageMode == LicenseUsageMode.Designtime)
                {
                    license = new FileLicense(this, "");
                }
                if (license != null)
                {
                    return license;
                }
                //licenses
                string licenseFile = GetlicFilePath();
                if (!string.IsNullOrWhiteSpace(licenseFile) && File.Exists(licenseFile))
                {
                    try
                    {

                        StreamReader sr = new StreamReader(licenseFile, Encoding.Default);
                        String xmlStr = DESEncrypt.Decrypt(sr.ReadToEnd());
                        var entity = XmlFormatterSerializer.DeserializeFromXml<LicenseEntity>(xmlStr, typeof(LicenseEntity));
                        if (entity != null)
                        {
                            var pastDate = DateTime.Parse(entity.PastDate);
                            if (DateTime.Now > pastDate)
                            {
                                listMsg.AppendLine(" 许可证已过期");
                            }
                        }

                        if (listMsg.Length > 5)
                        {
                            listMsg.Insert(0, " 许可证编号:" + entity.ID);
                            listMsg.Insert(1, " 许可证名称:"+entity.Name);
                            listMsg.Insert(2, " 程序集版本号:" + entity.AssemblyName);
                            license = new FileLicense(this, listMsg.ToString());
                        }
                        else
                            license = new FileLicense(this, "");
                    }
                    catch (IOException e)
                    {
                        Logger.LogError("检查许可证失败", e);
                    }
                }
                else
                {
                    Logger.LogWarn("lic文件不存在，路径是：" + licenseFile);
                }
            }
            return license;
        }
        public string GetlicFilePath()
        {
            /*
             * 1：优先查找程序的基目录
             * 
             */
            string licenseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LCL.lic");
            if (!File.Exists(licenseFile))
            {
                licenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "LCL.lic");
            }
            if (!File.Exists(licenseFile))
            {
                licenseFile = LEnvironment.Provider.ToAbsolute("LCL.lic");
            }
            if (!File.Exists(licenseFile))
            {
                licenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "LCL.lic");
            }
            if (!File.Exists(licenseFile))
            {
                licenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "LCL.lic");
            }
            if (!File.Exists(licenseFile))
            {
                licenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LCL.lic");
            }
            if (File.Exists(licenseFile))
            {
                return licenseFile;
            }
            return string.Empty;
        }
    }
}
