/// <reference path="../../Doc/jquery-1.6.2-vsdoc.js" />
/// <reference path="LCL.JQuery.Core.js" />
/// <reference path="../../Doc/VsDoc/Core/LCL.JQuery.Page.js" />

var LCLPageConfig = {
    SystemName: "",
    SystemURL: "2052",
    SystemNameSpace: "",
    CurrLanguageID: "",
    SiteRoot: "",
    CurrUserID: ""
};

$.LCLBase = {
    SiteConfig: {
        IsRelease: function () {
            /// <summary>
            /// 获取当前系统版本，true:Release模式,false:为Debug模式
            /// </summary>
            return false;
        },
        GetSystemName: function () {
            /// <summary>
            /// 获取系统名称
            /// </summary>
            var result = "";
            try {
                result = decodeURI(LCLPageConfig.SystemName);
            } catch (e) {

            }
            return result;
        },
        GetSystemLogo: function () {
            /// <summary>
            /// 获取系统名称
            /// </summary>
            var result = "";
            try {
                result = decodeURI(LCLPageConfig.SystemName);
            } catch (e) {

            }
            return result;
        },
        GetSystemURL: function () {
            /// <summary>
            /// 获取系统地址
            /// </summary>
            var result = "";
            try {
                result = decodeURI(LCLPageConfig.SystemURL);
                if (result == "") {
                    //获取当前网址，如：http://localhost:2005/Repair/AlarmCounty.aspx
                    var curWwwPath = window.document.location.href;
                    //获取主机地址，如： http://localhost:2005
                    var localhostPaht = curWwwPath.substring(0, pos);
                    result = localhostPaht;
                }
            } catch (e) {

            }
            return result;
        },
        GetSystemFlag: function () {
            /// <summary>
            /// 获取系统标识
            /// </summary>
            var result = "";
            try {
                result = decodeURI(LCLPageConfig.SystemNameSpace);
            } catch (e) {

            }
            return result;
        },
        GetCurrUserID: function () {
            /// <summary>
            /// 获取当前站点的语言，先重界面隐藏控件获取，再从客户端缓存获取
            /// </summary>
            var result = "";
            try {
                result = decodeURI(LCLPageConfig.CurrUserID);
            } catch (e) {

            }
            return result;
        },
        GetCurrLanguageID: function () {
            /// <summary>
            /// 获取当前站点的语言，先重界面隐藏控件获取，再从客户端缓存获取
            /// </summary>
            try {
                if (LCLPageConfig.CurrLanguageID) return decodeURI(LCLPageConfig.CurrLanguageID);
            } catch (e) {

            }
            var t = $("#hidLanguageID").val();
            if (t) return t;
            return "2052";
        },
        GetSiteRoot: function () {
            /// <summary>
            /// 获取当前站点的相对地址
            /// </summary>
            try {
                if (LCLPageConfig.SiteRoot) return decodeURI(LCLPageConfig.SiteRoot);
            } catch (e) {

            }
            var t = $("body").attr("rel");
            if (t) return t;
            return "/";
        },
        GetFrameworkRoot: function () {
            /// <summary>
            /// 获取RIAFramework部署地址，如果是本地部署可直接调用SiteConfig.GetSiteRoot()方法
            /// </summary>
            return "/";
        },
        GetCopyRight: function (languageId) {
            /// <summary>
            /// 获取版权信息
            /// </summary>
            /// <param name="languageId" type="String">语言标识：2052中文 1033英文</param>
            /// <returns type="string">版权信息</returns>
            return SystemResource[languageId].CopyRight;
        },
        GetSystemTitle: function (languageId) {
            /// <summary>
            /// 获取统一系统头信息
            /// </summary>
            /// <param name="languageId" type="String">语言标识：2052中文 1033英文</param>
            /// <returns type="string">统一系统头信息</returns>
            return SystemResource[languageId].SystemTitle;
        },
        GetNavigatePage: function () {
            /// <summary>
            /// 获取前台登录首页
            /// </summary>
            return $.LCLBase.SiteConfig.GetSiteRoot() + "login.aspx";
        },
        GetSystemAdmin: function () {
            /// <summary>
            /// 获取后台系统管理员
            /// </summary>
            return $("#hidSystemAdmin").val();
        },
        GetMaxTabPage: function () {
            return 5;
        },
        GetOpenDesignPageHotKey: function () {
            return "Ctrl + Alt + O";
        },
        SetCurrAppContext: function (languageID, employeeNo, cnName, enName) {
            /// <summary>
            /// 设置全局登录标识
            /// </summary>
            /// <param name="languageId" type="String">语言标识：2052中文 1033英文</param>
            /// <param name="employeeNo" type="String">人员工号</param>
            /// <param name="cnName" type="String">中文姓名</param>
            /// <param name="enName" type="String">英文姓名</param>
            var loginInfoKey = BaseAttr.AppContextKey;
            var loginInfoValue = $.ZTECore.StringUtil.StringFormat("{0}|{1}|{2}|{3}", languageID, employeeNo, cnName, enName);
            $.LCLCore.SiteCache.SetData(loginInfoKey, loginInfoValue);
        },
        ClearCurrAppContext: function () {
            var loginInfoKey = BaseAttr.AppContextKey;
            $.ZTECore.SiteCache.SetData(loginInfoKey, "");
        }
    }
}

var BaseAttr = {
    AppContextKey: "DevClient-LoginInfo",
    IWebFlag: true
};

var SystemResource = new Array("2052", "1033");

SystemResource["2052"] = {
    CopyRight: "©2008-2018 永新科技股份有限公司 版权所有",
    SystemTitle: "iWeb"
}

SystemResource["1033"] = {
    CopyRight: "©Copyright by LCL Corp. 2008-2018 ",
    SystemTitle: "SOC Security Operation Center"
}