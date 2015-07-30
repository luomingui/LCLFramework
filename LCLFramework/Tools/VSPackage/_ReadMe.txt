如何扩展：
1：创建一个命令
class XXXXXCommand: Command
{
  public XXXXXCommand()
  {
     this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidDeleteNullCommandCommand);
  }
｝
2：配置IDE VSPackage.vsct