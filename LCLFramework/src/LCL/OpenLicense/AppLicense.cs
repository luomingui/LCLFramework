using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LCL.OpenLicense
{
    /// <summary>
    /// LCL组件授权规则
    /// </summary>
    [LicenseProvider(typeof(FileLicenseProvider))]
    public class AppLicense
    {
        #region License
        private License license = null;
        public AppLicense()
        {
            try
            {
                license = LicenseManager.Validate(typeof(AppLicense), this);
                if (license == null)
                {
                    throw new Exception("LCL组件授权失败,请检查应用程序是否有LCL.lic文件或者联系程序开发商,邮箱是：minguiluo@163.com .");
                }
                if (license != null && license.LicenseKey.Length > 5)
                    throw new Exception("LCL组件授权失败," + license.LicenseKey + ",请联系程序开发商,邮箱是：minguiluo@163.com .");
            }
            catch
            {
                throw;
            }
        }
        public void Dispose(bool disposing)
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
        #endregion
    }
}
