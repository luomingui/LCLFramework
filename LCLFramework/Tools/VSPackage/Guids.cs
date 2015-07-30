/*******************************************************
 * 
 * Copyright(c)2012-2018 Luomg.All rights reserved.
 * CLR版本：.NET 4.0.0
 * 组织：Luomg@中国
 * 网站：http://luomingui.cnblogs.com
 * 说明：
 *
 * 历史记录：
 * 创建文件 Luomg 20150730
 * 
*******************************************************/
using System;

namespace LCL.VSPackage
{
    static class GuidList
    {
        public const string guidVSPackagePkgString = "cdb6f500-d128-47fc-9455-4ab43a33a426";
        public const string guidVSPackageCmdSetString = "8bd97cf8-4c13-4dd9-ba7f-1e066c72b1eb";
        public const string guidVSPackageEditorFactoryString = "67e54b37-14c7-46fe-a5ce-e23e5660a1d5";

        public static readonly Guid guidVSPackageCmdSet = new Guid(guidVSPackageCmdSetString);
        public static readonly Guid guidVSPackageEditorFactory = new Guid(guidVSPackageEditorFactoryString);
    };

}