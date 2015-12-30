<%@ page language="C#" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="WebMvcClient.Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            $('#loading-mask').fadeOut();
        }
    </script>
</head>
<body style="overflow: hidden; overflow-y: hidden; overflow-x: hidden;" scrolling="no" class="easyui-layout" fit="true">
    <noscript>
        <div style=" position:absolute; z-index:100000; height:2046px;top:0px;left:0px; width:100%; background:white; text-align:center;">
            <img src="/Plugins/UIShell.EasyUIAdminShellPlugin/Content/images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2; z-index: 20000">
        <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px;">
            <img src="/Plugins/UIShell.EasyUIAdminShellPlugin/Content/images/1.gif" alt='正在加载中,请稍候.' />
        </div>
    </div>
</body>
</html>
