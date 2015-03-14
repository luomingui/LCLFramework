/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace LCL.MvcExtensions
{
    public static class Utility
    {
        public static string RedirectToBundlePath(string locationFormat, string bundleRelativePath)
        {
            return locationFormat.Replace("~", bundleRelativePath);
        }

        public static IEnumerable<string> RedirectToBundlePath(IEnumerable<string> locationFormats, string bundleRelativePath)
        {
            return locationFormats.Select(item => RedirectToBundlePath(item, bundleRelativePath));
        }

        public static string MapPathReverse(string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HostingEnvironment.ApplicationPhysicalPath, String.Empty);
        }
    }
}
