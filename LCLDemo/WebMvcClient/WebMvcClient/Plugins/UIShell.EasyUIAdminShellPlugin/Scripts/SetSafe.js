//添加信任站点及设置IE安全配置

try
{
	var internetSet = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\";
	var zoneMapDomains = internetSet + "ZoneMap\\Domains\\";
	var zoneMapRanges = internetSet + "ZoneMap\\Ranges\\";
	var zonesActivexPolicy = internetSet + "Zones\\2\\1201";

	var WshShell=new ActiveXObject("WScript.Shell");

	//添加信任站点. 如果为http://www.123.com, 则设置userSite="123.com"
	var userSiteDomain = "cfca.com.cn";
	WshShell.RegWrite(zoneMapDomains + userSiteDomain + "\\" , "");
	WshShell.RegWrite(zoneMapDomains + userSiteDomain + "\\www\\", "");
	WshShell.RegWrite(zoneMapDomains + userSiteDomain + "\\www\\http", "2", "REG_DWORD");
	//var userSiteIP = "";
	//WshShell.RegWrite(zoneMapRanges + "Range1\\","");
	//WshShell.RegWrite(zoneMapRanges + "Range1\\:Range", userSiteIP, "REG_SZ");
	//WshShell.RegWrite(zoneMapRanges + "Range1\\http", "2", "REG_DWORD");

	//将“对未标记为可安全执行脚本的ActiveX控件初始化并执行脚本”启用（0启用，1提示，3禁用）
	WshShell.RegWrite(zonesActivexPolicy, "0", "REG_DWORD");

	//WScript.Echo("设置成功！");
}
catch(e)
{
	//if(e.description.length != 0)
	//	WScript.Echo("设置失败！错误信息："+e.description);
	//else
	//	WScript.Echo("设置失败！");
}
