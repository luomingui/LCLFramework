using System;

namespace LCL.VSPackage
{
    static class PkgCmdIDList
    {
        public const uint 老罗 =    0x101;
        public const int cmdidMigrateOldDatabaseCommand = 0x101;
        public const int cmdidAddGenericInterfaceFileCommand = 0x102;
        public const int cmdidRefreshCodeSnippetsCommand = 0x103;

        public const int cmdidCreateDependencyCommand = 0x104;
        public const int cmdidDropDependencyCommand = 0x105;
        public const int cmdidFileHeaderCommandsCommand = 0x106;
        public const int cmdidDeleteNullCommandCommand = 0x107;
        public const int cmdidInstallVSTemplatesCommandCommand = 0x108;
    };
}