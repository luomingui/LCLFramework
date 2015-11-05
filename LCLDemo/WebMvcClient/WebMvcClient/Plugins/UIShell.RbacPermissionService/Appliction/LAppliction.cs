using LCL.Repositories;
using System;
/*******************************************************
 * 
 * 作者：罗敏贵
 * 说明：
 * 运行环境：.NET 4.0.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 罗敏贵 20150403
 * 
*******************************************************/
using System.Linq;
using System.Web;

namespace UIShell.RbacPermissionService
{
    public class LAppliction
    {
        public static GeneralConfigInfo Config
        {
            get
            {
                GeneralConfigInfo config = GeneralConfigs.GetConfig();
                return config;
            }
        }

    }
}