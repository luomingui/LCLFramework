/// <reference path="LCL.JQuery.Base.js" />

$.LCLCore = {
    /// <summary>
    /// 脚本开发框架-Clipboard
    /// </summary>
    Clipboard: {
        CopyToClipboard: function (text, type) {
            /// <summary>
            /// 将内容复制到剪贴板
            /// </summary>
            /// <param name="text" type="String">需要复制的内容</param>
            /// <param name="type" type="String">数据类型：比如:Text</param>
            var t = type;
            if ($.LCLCore.StringUtil.IsNullOrEmpty(t)) {
                t = "Text";
            }
            if (window.clipboardData) // IE
            {
                window.clipboardData.setData(t, text);
            }
            else {
                unsafeWindow.netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                clipboardHelper = Components.classes["@mozilla.org/widget/clipboardhelper;1"].getService(Components.interfaces.nsIClipboardHelper);
                clipboardHelper.copyString(text);
            }
        },
        GetClipboardData: function (type) {
            /// <summary>
            /// 获取剪贴板的内容
            /// </summary>
            /// <param name="type" type="String">数据类型：比如:Text</param>
            var t = type;
            if ($.LCLCore.StringUtil.IsNullOrEmpty(t)) {
                t = "Text";
            }
            return window.clipboardData.getData(t);
        }
    },
    /// <summary>
    /// 脚本开发框架-Ajax对象方法
    /// </summary>
    Ajax: {
        CallAsyncAjax: function (url, successFn, faileFn, maskID) {
            /// <summary>
            /// 调用JQuery Ajax
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            /// <param name="maskID" type="String">如果有此ID，则自动根据此ID加载Mask界面，如果没有则不加载Mask</param>
            try {
                url = FormatAjaxURL(url);
                $.ajax({
                    type: "GET",
                    url: url,
                    async: false,
                    cache: false,
                    success: successFn,
                    error: faileFn,
                    timeout: 50000
                })
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjax", e.message);
            }
        },
        CallAjax: function (url, successFn, faileFn, maskID) {
            /// <summary>
            /// 调用JQuery Ajax
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            /// <param name="maskID" type="String">如果有此ID，则自动根据此ID加载Mask界面，如果没有则不加载Mask</param>
            try {
                url = FormatAjaxURL(url);
                $.ajax({
                    type: "GET",
                    url: url,
                    async: true,
                    cache: false,
                    success: successFn,
                    error: faileFn,
                    timeout: 50000
                })
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjax", e.message);
            }
        },
        CallAjaxResultJson: function (url, successFn, faileFn, showErrorMsg, maskID) {
            try {
                /// <summary>
                /// 调用JQuery Ajax
                /// </summary>
                /// <param name="url" type="String">Ajax请求地址</param>
                /// <param name="successFn" type="String">成功后的回调函数</param>
                /// <param name="faileFn" type="String">失败后的回调函数</param>
                /// <param name="showErrorMsg" type="bool">是否显示异常消息</param>
                /// <param name="maskID" type="String">如果有此ID，则自动根据此ID加载Mask界面，如果没有则不加载Mask</param>
                url = FormatAjaxURL(url);
                $.ajax({
                    type: "GET",
                    url: url,
                    async: true,
                    cache: false,
                    dataType: "json",
                    success: successFn,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.LCLCore.Logger.AjaxErrorLog(url, XMLHttpRequest, textStatus, errorThrown, showErrorMsg);
                        if (faileFn) {
                            faileFn.call(this, XMLHttpRequest, textStatus, errorThrown);
                        }
                    },
                    timeout: 50000
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjaxResultJson", e.message);
            }
        },
        CallDomainAjaxResultJson: function (url, successFn, faileFn, maskID) {
            /// <summary>
            /// 跨域调用，返回Json对象
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            /// <param name="maskID" type="String">如果有此ID，则自动根据此ID加载Mask界面，如果没有则不加载Mask</param>
            try {
                if (maskID) {
                    maskID = "#" + maskID;
                }
                if (url.indexOf("?") < 0) {
                    url = url + "?CallBack=?";
                }
                else {
                    url = url + "&CallBack=?";
                }
                url = FormatAjaxURL(url);
                $.ajax({
                    type: "GET",
                    url: url,
                    async: true,
                    cache: false,
                    dataType: "jsonp",
                    jsonp: 'jsonCallBack', //默认callback   
                    success: successFn,
                    error: faileFn,
                    timeout: 50000
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallDomainAjaxResultJson", e.message);
            }
        },
        CallAjaxPostData: function (url, data, type, successFn, faileFn, maskID, ajaxOptions) {
            /// <summary>
            /// post方法调用远程地址
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="data" type="String">参数</param>
            /// <param name="type" type="String">返回数据类型</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            /// <param name="maskID" type="String">如果有此ID，则自动根据此ID加载Mask界面，如果没有则不加载Mask</param>
            try {
                url = FormatAjaxURL(url);
                if (type == undefined || type == "") {
                    type = "json";
                }
                var sync = true;
                try {
                    if (ajaxOptions == "syn") {
                        sync = false;
                    }
                } catch (e) {
                    //$.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjaxPostData", e.message);
                }
                $.ajax({
                    type: "POST",
                    url: url,
                    data: data,
                    async: sync,
                    cache: false,
                    dataType: type,
                    success: successFn,
                    error: faileFn,
                    timeout: 500000
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjaxPostData", e.message);
            }
        },
        CallAjaxPostJsonData: function (url, jsondata, successFn, faileFn) {
            /// <summary>
            /// post方法调用远程地址
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="jsondata" type="JsonObject">json对象</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            try {
                var data = $.LCLCore.JSONHelp.ConvertToString(jsondata);
                $.LCLCore.Ajax.CallAjaxPostData(url, data, "json", successFn, faileFn);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjaxPostData", e.message);
            }
        },
        CallDomainPostData: function (url, data, successFn, faileFn) {
            /// <summary>
            /// Ajax跨域提交数据
            /// </summary>
            /// <param name="url" type="String">Ajax请求地址</param>
            /// <param name="type" type="String">返回数据类型</param>
            /// <param name="successFn" type="String">成功后的回调函数</param>
            /// <param name="faileFn" type="String">失败后的回调函数</param>
            try {
                if (url.indexOf("?") < 0) {
                    url = url + "?CallBack=?";
                }
                else {
                    url = url + "&CallBack=?";
                }
                url = FormatAjaxURL(url);
                $.getJSON(url, data, successFn, faileFn);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Ajax", "CallAjaxPostData", e.message);
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-核心类库-界面验证
    /// </summary>
    ValidUI: {
        ValidFalse: function (controlID) {
            /// <summary>
            /// ZTE脚本开发框架-核心类库-界面验证-验证失败
            /// </summary>
            /// <param name="controlID" type="String">控件ID</param>
            var jqContorlId = "#" + controlID;
            $(jqContorlId).focus().seek({
                css: {
                    borderStyle: "dotted",
                    borderWidth: "2px",
                    borderColor: "Red",
                    borderStyle: "solid"
                },
                animation: { speed: 600 }
            });
        },
        ValidFalseMessage: function (controlID, message) {
            if (message) {
                alert(message);
            }
            $.LCLCore.ValidUI.ValidFalse(controlID);
        },
        JustNum: function (domId) {
            /// <summary>
            /// 数字校验
            /// </summary>
            var control = $("#" + domId);
            control.keyup(function (event) {
                var t = control.get(0);
                if (event.keyCode != 37 && event.keyCode != 39) {
                    t.value = t.value.replace(/[^\d\.]/g, "").replace(/\.*/g, "");
                }
            });

        },
        NumAndDot: function (domId) {
            /// <summary>
            /// 金额校验
            /// </summary>
            var control = $("#" + domId);
            control.keypress(function () {
                var txtval = control.val();
                var key = event.keyCode;
                if ((key < 48 || key > 57) && key != 46) {
                    event.keyCode = 0;
                }
                else {
                    if (key == 46) {
                        if (txtval.indexOf(".") != -1 || txtval.length == 0)
                            event.keyCode = 0;
                    }
                }
            });
        },
        Trim: function (str) {
            //FUNCTION:去掉字符串的两边空格,中间的空格保留
            try {
                v = str.replace(/(^\s*)|(\s*$)/g, "");
                return v;
            } catch (e) {
                return str;
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-核心类库
    /// </summary>
    Request: {
        QueryString: function (item) {
            /// <summary>
            /// 获取URL中的QueryString对象信息
            /// </summary>
            /// <param name="item" type="String"/>
            /// <returns type="string" >QueryString参数</returns>
            try {
                var resultVale = "";
                if (document.location.search == "") return resultVale;
                var qs = document.location.search.substring(1, document.location.search.length);
                if (qs.length == 0) return params;
                qs = qs.replace(/\+/g, ' ');
                var args = qs.split('&');
                for (var i = 0; i < args.length; i++) {
                    var pair = args[i].split('=');
                    var name = decodeURIComponent(pair[0]);
                    if (name.toLowerCase() == item.toLowerCase()) {
                        resultVale = (pair.length == 2) ? decodeURIComponent(pair[1]) : name;
                        break;
                    }
                }
                if (resultVale == "undefined") return "";
                return resultVale;
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Request", "QueryString", e.message);
            }
        },
        GetAllQueryStringParameter: function (item) {
            /// <summary>
            /// 获取URL中的QueryString对象信息
            /// </summary>
            /// <param name="item" type="String"/>
            /// <returns type="string" >QueryString参数</returns>
            try {
                var currentLocal = document.location.search;
                if (currentLocal === "") {
                    return "";
                }
                var pos = currentLocal.indexOf('?');
                return currentLocal.substring(pos + 1);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Request", "GetAllQueryStringParameter", e.message);
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-URL对象方法
    /// </summary>
    URL: {
        AddParame: function (url, parame) {
            /// <summary>
            /// 在URL后面附加一个参数项
            /// </summary>
            /// <param name="url" type="String">URL</param>
            /// <returns type="string" >要附加的参数项name=123或者&name=1234</returns>
            if (!$.LCLCore.StringUtil.IsNullOrEmpty(url)) {
                if ($.LCLCore.StringUtil.IsNullOrEmpty(parame) || url.indexOf(parame) >= 0) {
                    return url;
                }
                if (url.indexOf("?") < 0) {
                    url += "?1=1";
                }
                if (parame.indexOf("&") != 0 && url.indexOf("?") != url.length - 1) {
                    url += "&";
                }
                url += parame;
            }
            return url;
        },
        UrlEncode: function (url) {
            /// <summary>
            /// 对URL进行编码,不编码字符有82个：!，#，$，&，''，(，)，*，+，,，-，.，/，:，;，=，?，@，_，~，0-9，a-z，A-Z 
            /// </summary>
            /// <param name="url" type="String">需要编码的URL</param>
            /// <returns type="string" >URL 编码后的URL</returns>
            try {
                return encodeURI(url);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "UrlEncode", e.message);
            }
        },
        UrlEscape: function (url) {
            /// <summary>
            /// 对URL进行编码,不编码字符有69个：*，+，-，.，/，@，_，0-9，a-z，A-Z 
            /// </summary>
            /// <param name="url" type="String">需要编码的URL</param>
            /// <returns type="string" >URL 编码后的URL</returns>
            try {
                return escape(url);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "UrlEscape", e.message);
            }
        },
        UrlEncodeURIComponent: function (url) {
            /// <summary>
            /// 对URL进行编码，不编码字符有71个：!， ''，(，)，*，-，.，_，~，0-9，a-z，A-Z
            /// </summary>
            /// <param name="url" type="String">需要编码的URL</param>
            /// <returns type="string" >URL 编码后的URL 传递参数时需要使用 encodeURIComponent，这样组合的 url 才不会被 # 等特殊字符截断</returns>
            try {
                return encodeURIComponent(url);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "UrlEncodeURIComponent", e.message);
            }
        },
        UrlDecode: function (url) {
            /// <summary>
            /// 对URL进行解码
            /// </summary>
            /// <param name="url" type="String">需要解码的URL</param>
            /// <returns type="string" >解码后的URL</returns>
            try {
                return decodeURI(url);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "UrlDecode", e.message);
            }
        },
        HtmlEncode: function (text) {
            /// <summary>
            /// 对特殊字符进行编码
            /// </summary>
            /// <param name="url" type="String">需要编码的字符串</param>
            /// <returns type="string" >编码后的字符串</returns>
            try {
                if (text == null || text == "") {
                    return "";
                }
                else {
                    return text.replace(new RegExp('&', "gm"), '&amp;').replace(new RegExp('"', "gm"), '&quot;').replace(new RegExp('<', "gm"), '&lt;').replace(new RegExp('>', "gm"), '&gt;').replace(new RegExp("'", "gm"), "&#39;");
                }
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "HtmlEncode", e.message);
            }
        },
        HtmlDecode: function (text) {
            /// <summary>
            /// 对特殊字符进行解码
            /// </summary>
            /// <param name="url" type="String">需要解码的字符串</param>
            /// <returns type="string" >解码后的字符串</returns>
            try {
                if (text == null || text == "") {
                    return "";
                }
                else {
                    return text.replace(new RegExp('&amp;', "gm"), '&').replace(new RegExp('&quot;', "gm"), '"').replace(new RegExp('&lt;', "gm"), '<').replace(new RegExp('&gt;', "gm"), '>').replace(new RegExp("&#39;", "gm"), "'");
                }
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "HtmlDecode", e.message);
            }
        },
        TransferSQLSpecialChar: function (text) {
            /// <summary>
            /// 对sql字符串进行处理
            /// </summary>
            /// <param name="url" type="String">需要处理的字符串</param>
            /// <returns type="string" >处理后的字符串</returns>
            try {
                if (text == null || text == "")
                    return "";
                else {
                    return text.replace(new RegExp(String.fromCharCode(6), "gm"), '?').replace(new RegExp(String.fromCharCode(5), "gm"), '_').replace(new RegExp(String.fromCharCode(1), "gm"), '%');
                }
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.URL", "TransferSQLSpecialChar", e.message);
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-Windows窗体对象方法
    /// </summary>
    WinDialog: {
        OpenWindow: function (url, name, winWidth, winHeight) {
            /// <summary>
            /// 打开Window窗体
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="name" type="String">窗口名称</param>
            /// <param name="winWidth" type="String">窗口宽度，只需输入数量，不需要带px</param>
            /// <param name="winHeight" type="String">窗口高度，只需输入数量，不需要带px</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                if ($.LCLCore.StringUtil.IsNullOrEmpty(winHeight)) {
                    winHeight = "500";
                }
                if ($.LCLCore.StringUtil.IsNullOrEmpty(winWidth)) {
                    winWidth = "800";
                }
                var xPosition = (screen.width - winWidth) / 2;
                var yPosition = (screen.height - winHeight) / 2;
                windowFeatures = "resizable=yes,width=" + winWidth + ",height=" + winHeight + ",left=" + xPosition + ",top=" + yPosition;
                return window.open(url, name, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenWindow", e.message);
            }
        },
        OpenWindowArguments: function (url, width, height, arguments) {
            /// <summary>
            /// 打开Window窗体，传入arguments
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="arguments" type="String">arguments参数</param>
            /// <param name="winWidth" type="String">窗口宽度，只需输入数量，不需要带px</param>
            /// <param name="winHeight" type="String">窗口高度，只需输入数量，不需要带px</param>
            try {
                if (arguments == null || arguments == "" || arguments == undefined) {
                    arguments = "resizable=yes,scrollbars=yes,status=yes,toolbar=yes,menubar=yes,location=yes,left:0px,top:0px";
                }
                if (arguments.indexOf('width') == -1) {
                    arguments += ",width=" + width + "px,";
                }
                if (arguments.indexOf('height') == -1) {
                    arguments += ",height=" + height + "px";
                }
                return window.open(url, "", arguments);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenFullArgumentsWin", e.message);
            }
        },
        OpenWindowWithScroll: function (url, name, winWidth, winHeight) {
            /// <summary>
            /// 打开Window窗体
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="name" type="String">窗口名称</param>
            /// <param name="winWidth" type="String">窗口宽度，只需输入数量，不需要带px</param>
            /// <param name="winHeight" type="String">窗口高度，只需输入数量，不需要带px</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                if ($.LCLCore.StringUtil.IsNullOrEmpty(winHeight)) {
                    winHeight = "500";
                }
                if ($.LCLCore.StringUtil.IsNullOrEmpty(winWidth)) {
                    winWidth = "800";
                }
                var xPosition = (screen.width - winWidth) / 2;
                var yPosition = (screen.height - winHeight) / 2;
                windowFeatures = "resizable=yes,scrollbars=yes,width=" + winWidth + ",height=" + winHeight + ",left=" + xPosition + ",top=" + yPosition;
                return window.open(url, name, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenWindowWithScroll", e.message);
            }
        },
        EscapeUrl: function (url) {
            /// <summary>
            /// 对URL进行编码
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <returns type="string" >返回编码后的地址</returns>
            try {
                var urlArray = url.split("?");
                if (urlArray.length == 2) {
                    _path = urlArray[0];
                    _query = urlArray[1];
                    return _path + mergeUrlQuery("", _query);
                }
                return url;
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "EscapeUrl", e.message);
            }
        },
        OpenInnerModalDialog: function (url, arguments, windowFeatures) {
            /// <summary>
            /// 对模式窗体
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="arguments" type="String">Arguments 对象</param>
            /// <param name="windowFeatures" type="String">WindowFeatures 对象</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                return window.showModalDialog(this.EscapeUrl(url), arguments, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenInnerModalDialog", e.message);
            }
        },
        OpenModalDialog: function (url, arguments, winWidth, winHeight, useScroll) {
            /// <summary>
            /// 打开模式窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="arguments" type="String">Arguments 对象</param>
            /// <param name="winWidth" type="String">宽度</param>
            /// <param name="winHeight" type="String">高度</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var needScroll = "";
                if (useScroll) { } else { needScroll = "scroll:no;"; }
                windowFeatures = "dialogwidth:" + winWidth + "px;dialogheight:" + winHeight + "px;help:no;" + needScroll + "status:no;";
                if (!arguments) {
                    arguments = {};
                }
                arguments.isModalDialog = true;
                return window.showModalDialog(url, arguments, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenModalDialog", e.message);
            }
        },
        OpenModuleWin: function (url, winWidth, winHeight, useScroll) {
            /// <summary>
            /// 打开模式窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="winWidth" type="String">宽度</param>
            /// <param name="winHeight" type="String">高度</param>
            /// <param name="useScroll" type="bool">是否需要滚屏</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var needScroll = "";
                if (useScroll) { } else { needScroll = "scroll:no;"; }
                windowFeatures = "dialogwidth:" + winWidth + "px;dialogheight:" + winHeight + "px;help:no;" + needScroll + "status:no;";
                return window.showModalDialog(url, window, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenModuleWin", e.message);
            }
        },
        OpenModuleFullWin: function (url) {
            /// <summary>
            /// 打开全屏窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var width = screen.width;
                var height = screen.height;
                windowFeatures = "dialogwidth:" + width + "px;dialogheight:" + height + "px;help:no;scroll:no;status:no;";
                return window.showModalDialog(url, window, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenModuleFullWin", e.message);
            }
        },
        OpenModuleFullWinArguments: function (url, arguments) {
            /// <summary>
            /// 打开全屏窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var width = screen.width;
                var height = screen.height;
                windowFeatures = "dialogwidth:" + width + "px;dialogheight:" + height + "px;help:no;scroll:no;status:no;";
                return window.showModalDialog(url, arguments, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenModuleFullWin", e.message);
            }
        },
        OpenModuleFullWinForScroll: function (url, useScroll) {
            /// <summary>
            /// 打开全屏窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="useScroll" type="String">是否需要滚屏</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var width = screen.width;
                var height = screen.height;
                var needScroll = "";
                if (useScroll) { } else { needScroll = "scroll:no;"; }
                windowFeatures = "dialogwidth:" + width + "px;dialogheight:" + height + "px;help:no;" + needScroll + "status:no;";
                return window.showModalDialog(url, window, windowFeatures);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenModuleFullWinForScroll", e.message);
            }
        },
        OpenFullArgumentsWin: function (url, arguments) {
            /// <summary>
            /// 打开全屏窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <param name="arguments" type="String">参数:resizable=yes,scrollbars=yes,status=yes,toolbar=yes,menubar=yes,location=yes</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                if (arguments == null || arguments == "" || arguments == undefined) {
                    arguments = "resizable=yes,scrollbars=yes,status=yes,toolbar=yes,menubar=yes,location=yes,left:0px,top:0px";
                }
                var width = screen.width - 10;
                var height = screen.height - 10;
                if (arguments.indexOf('width') == -1) {
                    arguments += ",width=" + width + "px,";
                }
                if (arguments.indexOf('height') == -1) {
                    arguments += ",height=" + height + "px";
                }
                return window.open(url, "", arguments);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenFullArgumentsWin", e.message);
            }
        },
        OpenFullWin: function (url) {
            /// <summary>
            /// 打开全屏窗口
            /// </summary>
            /// <param name="url" type="String">连接地址</param>
            /// <returns type="WindowsDialog" >打开的Windows窗体</returns>
            try {
                var width = screen.width - 10;
                var height = screen.height - 10;
                return window.open(url, "", "resizable=yes,scrollbars=yes,status=yes,toolbar=no,menubar=no,location=yes,dependent,channelmode");
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "OpenFullWin", e.message);
            }
        },
        CloseWin: function () {
            /// <summary>
            /// 关闭窗体
            /// </summary>
            try {
                window.close();
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "CloseWin", e.message);
            }
        },
        AlertMsg: function (msg) {
            /// <summary>
            /// 显示消息
            /// </summary>
            /// <param name="msg" type="String">消息内容</param>
            try {
                alert(msg);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "AlertMsg", e.message);
            }
        },
        ConfirmMsg: function (msg) {
            /// <summary>
            /// 显示消息
            /// </summary>
            /// <param name="msg" type="String">消息内容</param>
            try {
                return confirm(msg);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.WinDialog", "ConfirmMsg", e.message);
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-Div 对象方法
    /// </summary>
    DivDialog: {
        SystemMsg: function (languageId, msg, callBackFn) {
            /// <summary>
            /// 弹出系统消息
            /// </summary>
            /// <param name="languageId" type="String">语言ID:2052 1033</param>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="callBackFn" type="Function">点击确认后回调方法</param>
            try {
                var btnTxt = "确定";
                var title = "系统提示信息";
                if (languageId == "1033") {
                    btnTxt = "OK";
                    title = "System Message";
                }
                $.LCLCore.DivDialog.AlertShow({
                    btn: [btnTxt, callBackFn],
                    message: msg,
                    title: title
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.DivDialog", "AlertSuccess", e.message);
            }
        },
        AlertSuccess: function (msg, titletext, callBackFn) {
            /// <summary>
            /// 弹出成功消息窗体 
            /// </summary>
            /// <param name="titletext" type="String">标题</param>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="callBackFn" type="Function">点击确认后回调方法</param>
            try {
                var t = titletext;
                if ($.LCLCore.StringUtil.IsNullOrEmpty(t)) {
                    t = "成功";
                }
                $.LCLCore.DivDialog.AlertShow({
                    icoCls: 'ymPrompt_succeed',
                    btn: ['OK', callBackFn],
                    message: msg,
                    title: t
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.DivDialog", "AlertSuccess", e.message);
            }
        },
        AlertError: function (msg, titletext, callBackFn) {
            /// <summary>
            /// 弹出错误消息窗体 
            /// </summary>
            /// <param name="titletext" type="String">标题</param>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="callBackFn" type="Function">点击确认后回调方法</param>
            try {
                var t = titletext;
                if ($.LCLCore.StringUtil.IsNullOrEmpty(t)) {
                    t = "错误";
                }
                $.LCLCore.DivDialog.AlertShow({
                    icoCls: 'ymPrompt_error',
                    btn: ['OK', callBackFn],
                    message: msg,
                    title: t
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.DivDialog", "AlertError", e.message);
            }
        },
        AlertWarn: function (msg, titletext, callBackFn) {
            /// <summary>
            /// 弹出警告消息窗体
            /// </summary>
            /// <param name="titletext" type="String">标题</param>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="callBackFn" type="Function">点击确认后回调方法</param>
            try {
                var t = titletext;
                if ($.LCLCore.StringUtil.IsNullOrEmpty(t)) {
                    t = "警告";
                }
                $.LCLCore.DivDialog.AlertShow({
                    icoCls: 'ymPrompt_alert',
                    btn: ['OK', callBackFn],
                    message: msg,
                    title: t
                });
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.DivDialog", "AlertWarn", e.message);
            }
        },
        AlertShow: function (json) {
            try {
                if ($.LCLCore.StringUtil.IsNullOrEmpty($.ZTEEasyDialog)) {
                    $.LCLCore.LazyLoader.CustomLoadNottry("DivDialog/EasyDialog/EasyDialog.js", { js: [$.LCLCore.SiteHelp.GetSiteRoot() + "Scripts/Plugins/DivDialog/EasyDialog/EasyDialog.js"], css: [$.LCLCore.SiteHelp.GetSiteRoot() + "Scripts/Plugins/DivDialog/EasyDialog/skin/qq/ymPrompt.css"] }, function () {
                        $.ZTEEasyDialog.win(json);
                    });
                }
                else {

                    $.ZTEEasyDialog.win(json);
                }
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.DivDialog", "AlertShow", e.message);
            }
        }
    },
    ModelURLDialog: {
        InitDialog: function (dialogId, dialogFrameId) {
            /// <summary>
            /// 初始化EasyUI风格的窗体
            /// </summary>
            /// <param name="dialogId" type="String">窗体ID</param>
            /// <param name="dialogFrameId" type="String">窗体Frame的ID</param>
            var dialogArea = $('<div style="padding: 10px;dispaly:none; height:0px;position:absolute;"  />').attr("id", dialogId).appendTo('body');
            $('<iframe scrolling="no" frameborder="0" src="" style="width: 100%;height: 100%;" />').attr("id", dialogFrameId).appendTo(dialogArea);
        },
        InitTopDialog: function (dialogId, dialogFrameId) {
            /// <summary>
            /// 初始化EasyUI风格的窗体
            /// </summary>
            /// <param name="dialogId" type="String">窗体ID</param>
            /// <param name="dialogFrameId" type="String">窗体Frame的ID</param>
            var dialogArea = $('<div style="padding: 10px;"  />').attr("id", dialogId).appendTo(top.document.body);
            $('<iframe scrolling="no" frameborder="0" src="" style="width: 100%;height: 100%;" />').attr("id", dialogFrameId).appendTo(dialogArea);
        },
        OpenDialog: function (DialogId, DialogFrameId, Title, Width, Height, Url, Icon) {
            /// <summary>
            /// 打开窗体
            /// </summary>
            /// <param name="DialogId" type="String">窗体ID</param>
            /// <param name="DialogFrameId" type="String">窗体Frame的ID</param>
            /// <param name="Title" type="String">窗体的抬头信息</param>
            /// <param name="Width" type="String">窗体的宽度</param>
            /// <param name="Height" type="String">窗体的高度</param>
            /// <param name="Url" type="String">窗体Frame的URL</param>
            /// <param name="Icon" type="String">窗体抬头显示的图标</param>
            var jqDialogId = "#" + DialogId;
            var jqDialogFrameId = "#" + DialogFrameId;
            $(jqDialogId).dialog({
                title: Title,
                width: Width,
                height: Height,
                iconCls: Icon,
                modal: true,
                minimizable: false,
                maximized: false,
                shadow: true,
                closed: true
            });
            var r = $.LCLCore.RandomUtil.GetRandom(10000);
            if (Url.indexOf("?") > -1) {
                Url += "&r=" + r
            } else {
                Url += "?r=" + r;
            }
            $(jqDialogFrameId).attr("src", Url);
            $(jqDialogId).dialog("open");
        },
        OpenSubDialog: function (winObj, DialogId, DialogFrameId, Title, Width, Height, Url, Icon) {
            /// <summary>
            /// 打开窗体中的窗体
            /// </summary>
            /// <param name="winObj" type="String">打开的目标</param>
            /// <param name="DialogId" type="String">窗体ID</param>
            /// <param name="DialogFrameId" type="String">窗体Frame的ID</param>
            /// <param name="Title" type="String">窗体的抬头信息</param>
            /// <param name="Width" type="String">窗体的宽度</param>
            /// <param name="Height" type="String">窗体的高度</param>
            /// <param name="Url" type="String">窗体Frame的URL</param>
            /// <param name="Icon" type="String">窗体抬头显示的图标</param>
            var jqDialogId = "#" + DialogId;
            var jqDialogFrameId = "#" + DialogFrameId;
            winObj.$(jqDialogId, winObj.document.body).dialog({
                title: Title,
                width: Width,
                height: Height,
                iconCls: Icon,
                modal: true,
                minimizable: false,
                maximized: false,
                shadow: true,
                closed: true
            });
            winObj.$(jqDialogFrameId, winObj.document.body).attr("src", Url);
            winObj.$(jqDialogId, winObj.document.body).dialog("open");
        }
    },
    /// <summary>
    /// 脚本开发框架-Logger对象方法
    /// </summary>
    Logger: {
        ExceptionLog: function (msg, showMsg) {
            /// <summary>
            /// 记录系统异常日志
            /// </summary>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="showMsg" type="bool">是否显示提示消息</param>
            /// <returns type="" >无返回值</returns>
            try {
                if (showMsg) {
                    $.LCLCore.WinDialog.AlertMsg(msg);
                }
                $.LCLCore.Logger.WriteLog("E", "", "", msg);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Logger", "ExceptionLog", e.message);
            }
        },
        FunctionExceptionLog: function (url, functionname, msg) {
            /// <summary>
            /// 跨域调用，返回Json对象
            /// </summary>
            /// <param name="url" type="String">出错地址</param>
            /// <param name="functionname" type="String">方法名</param>
            /// <param name="msg" type="String">出错信息</param>
            /// <returns type="" >无返回值</returns>
            try {
                $.LCLCore.Logger.WriteLog("FE", url, functionname, msg);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Logger", "FunctionExceptionLog", e.message);
            }
        },
        AjaxErrorLog: function (ajaxURL, XMLHttpRequest, textStatus, errorThrown, showErrorMsg) {
            /// <summary>
            /// 记录Ajax错误日志
            /// </summary>
            /// <param name="XMLHttpRequest" type="object">请求对象</param>
            /// <param name="textStatus" type="String">状态</param>
            /// <param name="errorThrown" type="String">异常信息</param>
            /// <param name="showErrorMsg" type="bool">是否显示异常消息</param>
            /// <returns type="" >无返回值</returns>
            try {
                var tReadyState = XMLHttpRequest.readyState;
                var tStatus = XMLHttpRequest.status;
                var tStatusText = XMLHttpRequest.statusText;
                var msg = $.LCLCore.StringUtil.StringFormat("Ajax调用：{0}异常，异常状态为：{1}，异常信息为：{2}", ajaxURL, tStatus, textStatus);
                if (showErrorMsg) {
                    $.LCLCore.WinDialog.AlertMsg(msg);
                }
                $.LCLCore.Logger.WriteLog("AJAX", "", "", msg);
            } catch (e) {
                $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Logger", "AjaxErrorLog", e.message);
            }
        },
        WriteLog: function (type, url, functionname, msg) {
            /// <summary>
            /// 记录错误日志
            /// </summary>
            /// <param name="type" type="object">消息类型</param>
            /// <param name="url" type="String">出错地址</param>
            /// <param name="functionname" type="String">方法名称</param>
            /// <param name="msg" type="bool">消息内容</param>
            /// <returns type="" >无返回值</returns>
            try {
                var sysname = ""; // 获取系统名称
                $.LCLCore.LazyLoader.CustomLoadNottry("ZTE.JQuery.Logger.js", { js: [$.LCLCore.SiteHelp.GetSiteRoot() + "Scripts/Plugins/Core/ZTE.JQuery.Logger.js"] }, function () {
                    $.ZTELogger.WriteLog(type, sysname, url, functionname, msg);
                    $.LCLCore.WinDialog.AlertMsg(msg);
                });
            } catch (e) {
                // 这个方法不catch，防止循环
                // $.LCLCore.Logger.FunctionExceptionLog("$.LCLCore.Logger", "WriteLog", e.message);
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-字符串对象方法
    /// </summary>
    StringUtil: {
        ReplaceAll: function (str, s1, s2) {
            if (str) {
                return str.replace(new RegExp(s1, "gm"), s2);
            }
            return str;
        },
        TextAreaReplaceEnter: function (v) {
            var s = v.replace(/[(^*\n*)|(^*\r*)]/g, '<br />');
            return s;
        },
        TextAreaReplaceEnterSet: function (v) {
            var s = $.LCLCore.StringUtil.ReplaceAll(v, "<br />", "\r\n");
            return s;
        },
        FormatWidth: function (width) {
            var w = width;
            if (w == null || w == undefined) {
                return "";
            }
            if (w != "auto" && w.toString().indexOf("%") < 0) {
                if (w.toString().indexOf("px") < 0) {
                    w += "px";
                }
            }
            return w;
        },
        ReplaceJQueryAttr: function (controlHtml) {
            return controlHtml.replace(/jQuery([0-9]+)=\"([0-9]+)\"/, "");
        },
        AddToStringSplit: function (str, sp, s) {
            if (str != "") {
                str += sp;
            }
            str += s;
            return str;
        },
        IndexOfStringSplit: function (str, sp, s) {
            var strs = str.split(sp);
            for (var i = 0; i < strs.length; i++) {
                if (strs[i] == s) {
                    return true;
                }
            }
            return false;
        },
        NewGuid: function () {
            /// <summary>
            /// 清除" "
            /// </summary>
            /// <param name="str" type="string">要清除空格的字符串</param>
            /// <returns type="string">清除空格后的字符串</returns>
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        },
        NewGuidForId: function () {
            /// <summary>
            /// 清除" "
            /// </summary>
            /// <param name="str" type="string">要清除空格的字符串</param>
            /// <returns type="string">清除空格后的字符串</returns>
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
            }
            return guid;
        },
        TrimValue: function (str) {
            /// <summary>
            /// 清除" "
            /// </summary>
            /// <param name="str" type="string">要清除空格的字符串</param>
            /// <returns type="string">清除空格后的字符串</returns>
            if (str == null || str == "") return;
            if (str.length == 0) return;
            return str.replace(/(^\s*)|(\s*$)/g, "");
        },
        TrimAll: function (str) {
            /// <summary>
            /// 清除所有空格" "
            /// </summary>
            /// <param name="str" type="string">要清除空格的字符串</param>
            /// <returns type="string">清除所有空格字符串</returns>
            return str.replace(/( )/g, "");
        },
        IsNullOrEmpty: function (values) {
            /// <summary>
            /// 是否为空
            /// </summary>
            /// <param name="values" type="string">要判断的字符串</param>
            /// <returns type="bool">是否空字符:空返回true,否则返回false</returns>
            if (typeof (values) === "string") {
                return (values == undefined || values == null || $.LCLCore.StringUtil.TrimValue(values) == null || $.LCLCore.StringUtil.TrimValue(values) == "" || $.LCLCore.StringUtil.TrimValue(values).length == 0);
            }
            else if (typeof (values) === "number") {
                return isNaN(values);
            }
            else {
                return values === undefined || values === null;
            }
        },
        ToUpper: function (str) {
            /// <summary>
            /// 将字符串转换为大写
            /// </summary>
            /// <param name="str" type="string">需要转换的字符串</param>
            /// <returns type="string">如果有值则返回大写形式，否则返回空</returns>
            if (str) {
                return str.toUpperCase();
            }
            return "";
        },
        ToLower: function (str) {
            /// <summary>
            /// 将字符串转换为小写
            /// </summary>
            /// <param name="str" type="string">需要转换的字符串</param>
            /// <returns type="string">如果有值则返回小写形式，否则返回空</returns>
            if (str) {
                return str.toLowerCase();
            }
            return "";
        },
        StringBoolToBool: function (str) {
            /// <summary>
            /// 将逻辑字符串"true"or"false"随便大小写，转换为布尔类型
            /// </summary>
            /// <param name="str" type="string">需要转换的字符串</param>
            /// <returns type="bool" >如果成功返回布尔类型：true or false 否则返回布尔类型：false</returns>
            if (str) {
                if (typeof (str) === "string") {
                    return (str.toLowerCase() == "true");
                }
                return str;
            }
            return false;
        },
        ReplaceDoubleQuotMark: function (text) {
            /// <summary>
            /// 把双引号替换成单引号
            /// </summary>
            if (text == null || text == "")
                return "";

            else
                return text.replace(new RegExp('"', "gm"), '\'');
        },
        SubString: function (str, len) {
            /// <summary>
            /// 截取字符长度
            /// </summary>
            /// <param name="str" type="String">需要截取的字符串</param>
            /// <param name="len" type="String">需要截取字符串的长度</param>
            /// <returns type="返回替换后的文字,最后会加上..." />
            if (str == null) return "";
            if (str == "") return "";
            if (str.length == 0) return "";
            if (len == null) return "";
            if (len.length == 0) return "";
            if (str.length > len) {
                str = str.substring(0, len) + "...";
            }
            return str;
        },
        GetTxtNewRow: function () {
            /// <summary>
            /// 获取文本换行(\r\n)
            /// </summary>
            return "\r\n";
        },
        GetTxtTab: function () {
            /// <summary>
            /// 获取文本Tab(\t)
            /// </summary>
            return "\t";
        },
        AppendNewRow: function (array, str) {
            /// <summary>
            /// 将一个字符串尾部加入换行
            /// </summary>
            /// <param name="array" type="arrayObj">需要加入的数组对象</param>
            /// <param name="str" type="string">需要修改的文字</param>
            /// <returns type="string">格式化后的对象</returns>
            if (array) {
                array.push(str + $.LCLCore.StringUtil.GetTxtNewRow());
            }
            return array;
        },
        StringFormat: function (format, args) {
            /// <summary>
            /// 将format字符串中的内容，替换为args指定数组中等效的内容,形式如：$.LCLCore.StringUtil.StringFormat("ABBB{0}sffdsf{1}","AAA","BBB");
            /// </summary>
            /// <param name="format" type="string">需要格式化的字符串信息</param>
            /// <param name="args" type="string">需要格式化的数组信息</param>
            /// <returns type="string">格式化后的字符串</returns>
            if (format) {
                if (arguments.length > 0) {
                    var result = format;
                    if (arguments.length == 1 && typeof (args) == "object") {
                        for (var key in args) {
                            var reg = new RegExp("({" + key + "})", "g");
                            result = result.replace(reg, args[key]);
                        }
                    }
                    else {
                        for (var i = 1; i <= arguments.length; i++) {
                            if (arguments[i] == undefined) {
                                return result;
                            }
                            else {
                                var t = i - 1;
                                var reg = new RegExp("({[" + t + "]})", "g");
                                result = result.replace(reg, arguments[i]);
                            }
                        }
                    }
                    return result;
                }
                else {
                    return this;
                }
            }
            return this;
        }
    },
    /// <summary>
    /// 脚本开发框架-鼠标对象方法
    /// </summary>
    MouseUtil: {
        /// <summary>
        /// 获取鼠标X,Y轴坐标
        /// </summary>
        /// <param name="Mouse X, Mouse Y" type="String">鼠标的X 和鼠标 Y轴</param>
        /// <returns type="JsonObj" >具备X和Y两个属性的JSON对象，例如：var mouseObj = $.LCLCore.MouseUtil.GetMouseXY(); mouseObj.x即表示鼠标的X轴坐标  mouseObj.y即表示鼠标的Y轴坐标</returns>
        GetMouseXY: function () {
            var ev = window.event;
            if (ev.pageX || ev.pageY) {
                return { x: ev.pageX, y: ev.pageY };
            }
            else {
                return {
                    x: ev.clientX + document.body.scrollLeft + document.documentElement.scrollLeft,
                    y: ev.clientY + document.body.scrollTop + document.documentElement.scrollTop
                }
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-随机数对象方法
    /// </summary>
    RandomUtil: {
        GetCurrentTime: function () {
            /// <summary>
            /// 返回一个随机数，可用于打开模态窗体后跟一个随机参数用
            /// </summary>
            var datenow = new Date();
            var sdate = new String();
            var sminute = new String();
            var sMillisecond = new String();
            sminute = datenow.getMinutes();
            sMillisecond = sdate + datenow.getMilliseconds();
            return sminute + sMillisecond;
        },
        GetRandom: function (num) {
            /// <summary>
            /// 返回一个最小为1最大为num的随机数
            /// </summary>
            //声明一个随机数变量，默认为1
            var GetRandomn1 = 1;
            //获取随机范围内数值的函数
            function GetRandom1(n) { GetRandomn1 = Math.floor(Math.random() * n + 1) }
            //开始调用，获得一个1-100的随机数
            GetRandom1(num);
            return GetRandomn1;
        }
    },
    /// <summary>
    /// 脚本开发框架-日期、时间对象方法
    /// </summary>
    DateTimeUtil: {
        GetFullNowTime: function () {
            /// <summary>
            /// 返回当前的日期时间 格式：YYYY-MM-DD HH:mm:ss
            /// </summary>
            var myDate = new Date();
            var monthStr = $.LCLCore.DateTimeUtil.FormatTime(myDate.getMonth() + 1);
            var dayStr = $.LCLCore.DateTimeUtil.FormatTime(myDate.getDate());
            var hourStr = $.LCLCore.DateTimeUtil.FormatTime(myDate.getHours());
            var minStr = $.LCLCore.DateTimeUtil.FormatTime(myDate.getMinutes());
            var secStr = $.LCLCore.DateTimeUtil.FormatTime(myDate.getSeconds());
            return myDate.getFullYear() + "-" + monthStr + "-" + dayStr + " " + hourStr + ":" + minStr + ":" + secStr;
        },
        CompareDate: function (sourceDate, objDate) {
            /// <summary>
            /// 比较两个日期类型的大小，如果SourceDate大于ObjData，则返回：true，否则返回：false
            /// </summary>
            /// <param name="sourceDate" type="datetime">源比较对象</param>
            /// <param name="objDate" type="datetime">目标比较对象</param>
            /// <returns type="bool" >如果SourceDate大于ObjData，则返回：true，否则返回：false</returns>
            try {
                var s = Date.parse(sourceDate.replace(/-/g, "/"));
                var o = Date.parse(objDate.replace(/-/g, "/"));
                return s > o;
            } catch (e) {
                var msg = $.LCLCore.StringUtil.StringFormat("在比较{0}和{1}日期类型时出错，具体错误原因：{2}", sourceDate, objDate, e);
                $.LCLCore.WinDialog.AlertMsg(msg);
                return false;
            }
        },
        GetEnMonth: function (monthNumber) {
            /// <summary>
            /// 获取英文下的月份
            /// </summary>
            /// <param name="monthNumber" type="String">月份标识</param>
            /// <returns type="string" >英文标识</returns>
            var result = "Jan.";
            try {
                var monthFlag = parseInt(monthNumber);
                if (monthFlag == 1) {
                    result = "Jan.";
                } else if (monthFlag == 2) {
                    result = "Feb.";
                } else if (monthFlag == 3) {
                    result = "Mar.";
                } else if (monthFlag == 4) {
                    result = "Apr.";
                } else if (monthFlag == 5) {
                    result = "May";
                } else if (monthFlag == 6) {
                    result = "Jun";
                } else if (monthFlag == 7) {
                    result = "Jul.";
                } else if (monthFlag == 8) {
                    result = "Aug.";
                } else if (monthFlag == 9) {
                    result = "Sept.";
                } else if (monthFlag == 10) {
                    result = "Oct.";
                } else if (monthFlag == 11) {
                    result = "Nov.";
                } else if (monthFlag == 12) {
                    result = "Dec.";
                }
            } catch (e) {
            }
            return result;
        },
        FormatTime: function (date) {
            /// <summary>
            /// 格式化日期单位变成双位
            /// </summary>
            /// <param name="date" type="String">日期</param>
            /// <returns type="string" >格式化后的日期</returns>
            if (date < 10) {
                return "0" + date;
            }
            return date;
        },
        Sleep: function (miliSecond) {
            /// <summary>
            /// 休眠指定时间
            /// </summary>
            /// <param name="miliSecond" type="String">毫秒</param>
            var currentDate, beginDate = new Date();

            var beginHour, beginMinute, beginSecond, beginMs;

            var hourGaps, minuteGaps, secondGaps, msGaps, gaps;

            beginHour = beginDate.getHours();

            beginMinute = beginDate.getMinutes();

            beginSecond = beginDate.getSeconds();

            beginMs = beginDate.getMilliseconds();

            do {

                currentDate = new Date();

                hourGaps = currentDate.getHours() - beginHour;

                minuteGaps = currentDate.getMinutes() - beginMinute;

                secondGaps = currentDate.getSeconds() - beginSecond;

                msGaps = currentDate.getMilliseconds() - beginMs;

                if (hourGaps < 0) hourGaps += 24; //考虑进时进分进秒的特殊情况 

                gaps = hourGaps * 3600 + minuteGaps * 60 + secondGaps;

                gaps = gaps * 1000 + msGaps;

            } while (gaps < miliSecond);
        },
        IsLeapYear: function (year) {
            /// <summary>
            /// 判断是否闰年
            /// </summary>
            /// <param name="year" type="String">年</param>
            /// <returns type="bool" >如果是闰年，则返回：true，否则返回：false</returns>
            var y = year;
            if (y === undefined || isNaN(y)) {
                y = this.getYear();
            }
            return (0 == y % 4 && ((y % 100 != 0) || (y % 400 == 0)));
        },
        DateAdd: function (date, strInterval, Number) {
            /// <summary>
            /// 在现有日期上增加指定的时间
            /// </summary>
            /// <param name="date" type="String">日期</param>
            /// <param name="strInterval" type="String">增加的类型</param>
            /// <param name="Number" type="Numner">增加量</param>
            /// <returns type="string" >返回增加后的新日期</returns>
            var dtTmp = Date.parse(date);
            if (dtTmp === undefined || isNaN(dtTmp)) {
                dtTmp = new Date();
            }
            var d = "";
            switch (strInterval) {
                case 's': d = new Date(dtTmp.getTime() + (1000 * Number)); break;
                case 'n': d = new Date(dtTmp.getTime() + (60000 * Number)); break;
                case 'h': d = new Date(dtTmp.getTime() + (3600000 * Number)); break;
                case 'd': d = new Date(dtTmp.getTime() + (86400000 * Number)); break;
                case 'w': d = new Date(dtTmp.getTime() + ((86400000 * 7) * Number)); break;
                case 'q': d = new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); break;
                case 'm': d = new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); break;
                case 'y': d = new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); break;
            }
            return d.toString();
        },
        /**   
        * 时间对象的格式化;   
        */
        Dateformat: function (format, date) {
            /*   
            * eg:format="YYYY-MM-dd hh:mm:ss";   
            */
            if (typeof date == "string") {
                date = new Date(Date.parse(date.replace(/-/g, "/")));
            }
            var o = {
                "M+": date.getMonth() + 1, // month    
                "d+": date.getDate(), // day    
                "h+": date.getHours(), // hour  
                "H+": date.getHours(), // hour                    
                "m+": date.getMinutes(), // minute    
                "s+": date.getSeconds(), // second    
                "q+": Math.floor((date.getMonth() + 3) / 3), // quarter    
                "S": date.getMilliseconds()
                // millisecond    
            }

            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (date.getFullYear() + "")
                .substr(4 - RegExp.$1.length));
            }

            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k]
                    : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        }


    },
    /// <summary>
    /// ZTE脚本开发框架-站点缓存对象
    /// </summary>
    SiteCache: {
        SetData: function (key, value) {
            /// <summary>
            /// 设置站点数据缓存
            /// </summary>
            /// <param name="key" type="string">键</param>
            /// <param name="value" type="string">值</param>
            var sysName = $.ZTEBase.SiteConfig.GetSystemName();
            var newKey = sysName + "--" + key;
            var coOptions = { expires: 1, domain: "" };
            $.LCLCore.Cookies.WriteCookies(newKey, value, coOptions);
        },
        GetData: function (key) {
            /// <summary>
            /// 获取站点数据缓存
            /// </summary>
            /// <param name="key" type="string">键</param>
            /// <returns type="string" >通过Key获取缓存的值</returns>
            var sysName = $.ZTEBase.SiteConfig.GetSystemName();
            var newKey = sysName + "--" + key;
            return $.LCLCore.Cookies.GetCookies(newKey);
        }
    },
    /// <summary>
    /// 脚本开发框架-Cookies对象方法
    /// </summary>
    Cookies: {
        GetCookies: function (key) {
            /// <summary>
            /// 获取Cookies
            /// </summary>
            /// <param name="key" type="string">Cookies键值</param>
            /// <returns type="string" >获取Key以及Opetions内容对应的cookie-value信息</returns>
            var cookieValue = null;
            var docCookie = document.cookie;
            if (docCookie && docCookie != '' && docCookie != undefined) {
                if (docCookie.indexOf(key) > -1) {
                    var cookies = docCookie.split(';');
                    for (var i = 0; i < cookies.length; i++) {
                        var cookie = jQuery.trim(cookies[i]);
                        if (cookie.substring(0, key.length + 1) == (key + '=')) {
                            cookieValue = decodeURIComponent(cookie.substring(key.length + 1));
                            break;
                        }
                    }
                }
            }
            return cookieValue;
        },
        WriteCookies: function (key, value, options) {
            /// <summary>
            /// 写Cookies
            /// </summary>
            /// <param name="key" type="string">Cookies键值</param>
            /// <param name="value" type="string">Cookies值</param>
            /// <param name="options" type="JSON">Cookies选项目：expires有效期，path路径，domain域，secure安全验证</param>
            options = options || {};
            if (value === null) {
                value = '';
                options = $.extend({}, options);
                options.expires = -1;
            }
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                } else {
                    date = options.expires;
                }
                expires = '; expires=' + date.toUTCString();
            }
            var path = options.path ? '; path=' + (options.path) : '';
            var domain = options.domain ? '; domain=' + (options.domain) : '; domain=zte.com.cn';
            var secure = options.secure ? '; secure' : '';
            document.cookie = [key, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
        },
        DelCookie: function (name)//删除cookie
            /// <summary>
            /// 在现有日期上增加指定的时间
            /// </summary>
            /// <param name="name" type="String">cookie名称</param>
        {
            var path = "";
            var domain = "";
            if ($.LCLCore.Cookies.GetCookies(name, "")) {
                document.cookie = name + "=" +
                ((path) ? "; path=" + path : "") +
                ((domain) ? "; domain=" + domain : "") +
                "; expires=Thu, 01-Jan-70 00:00:01 GMT";
            }
        }
    },
    /// <summary>
    /// 脚本开发框架-缓加载对象方法
    /// </summary>
    LazyLoader: {
        LoadSynHeighlighterScript: function (siteRoot, successFn) {
            var root = siteRoot + "Scripts/syntaxhighlighter/";
            var jsRoot = root + "scripts/";
            var cssRoot = "../Scripts/syntaxhighlighter/" + "styles/";
            var resource = {
                js: [jsRoot + "shCore.js", jsRoot + "shBrushBash.js", jsRoot + "shBrushJScript.js", jsRoot + "shBrushXml.js"]
                //css: ["/Style/Main.css", cssRoot + "shCore.css", cssRoot + "shThemeDefault.css"]
            };
            $.LCLCore.LazyLoader.CustomLoad("SynHeight", resource, successFn);
        },
        LoadJSONHelp: function (successFn) {
            var siteRoot = $.LCLCore.SiteHelp.GetSiteRoot();
            var jsRoot = siteRoot + "Scripts/Plugins/Core/";
            var resource = { js: [jsRoot + "LCL.JQuery.JSON.js"] };
            $.LCLCore.LazyLoader.CustomLoad("JSONHelp", resource, successFn);
        },
        CustomLoad: function (resourceName, resource, successFn) {
            /// <summary>
            /// 自定义缓加载脚本和样式
            /// </summary>
            /// <param name="resourceName" type="string">缓加载资源名称，标识这些资源是否被加载过</param>
            /// <param name="resource" type="资源集合">格式如：js:[path/XXX.js,path/XXX.js],css:[path/xxx.css,path/xxx.css],image:[path/xxx.gif,path/xxx.png]</param>
            /// <param name="successFn" type="Function">成功加载后的方法</param>
            try {
                var js = css = img = new Array();
                js = resource["js"];
                css = resource["css"];
                img = resource["image"];
                $.xLazyLoader({ name: resourceName, image: img, js: js, css: css, load: successFn });
            } catch (e) {
                var msg = $.LCLCore.StringUtil.StringFormat("使用LazyLoader的CustomLoad方法出错，具体原因:{0}", e.message);
                $.LCLCore.Logger.ExceptionLog(msg, false);
            }
        },
        CustomLoadNottry: function (resourceName, resource, successFn) {
            /// <summary>
            /// 自定义缓加载脚本和样式,不try-catch写日志防止和写日志中的延迟加载形成循环调用
            /// </summary>
            /// <param name="resourceName" type="string">缓加载资源名称，标识这些资源是否被加载过</param>
            /// <param name="resource" type="资源集合">格式如：js:[path/XXX.js,path/XXX.js],css:[path/xxx.css,path/xxx.css],image:[path/xxx.gif,path/xxx.png]</param>
            /// <param name="successFn" type="Function">成功加载后的方法</param>
            var js = css = img = new Array();
            js = resource["js"];
            css = resource["css"];
            img = resource["image"];
            $.xLazyLoader({ name: resourceName, image: img, js: js, css: css, load: successFn });
        }
    },
    JSONHelp: {
        DelAttr: function (jsonObj, attr) {
            delete jsonObj[attr];
        },
        GetJSONItem: function (key, value) {
            /// <summary>
            /// 构建一个JSON项，返回如："key":"value"形式
            /// </summary>
            /// <param name="key" type="string">JSON对象的键值</param>
            /// <param name="value" type="string">JSON对象的值</param>
            /// <returns type="string" >返回如："key":"value"形式</returns>
            return $.LCLCore.StringUtil.StringFormat('"{0}":"{1}"', key, value);
        },
        GetJSONForArray: function (arrayList) {
            /// <summary>
            /// 通过一个数组转换为JSON对象，数组如：{"XXX":"XXX","XXX":"XX"}
            /// </summary>
            /// <param name="arrayList" type="arrayList">数组对象，数组如：{"XXX":"XXX","XXX":"XX"}</param>
            /// <returns type="JSON Obj" >返回JSON对象</returns>
            var newJsonStr = "[" + arrayList.join(",") + "]";
            return $.parseJSON(newJsonStr);
        },
        ParseToString: function (jsonObj, successFn) {
            /// <summary>
            /// 将JSON对象转换为JSON字符串
            /// </summary>
            /// <param name="jsonObj" type="JSON Obj">需要转换的JSON对象</param>
            /// <param name="successFn" type="function">转换成功后的回调方法</param>
            /// <returns type="string" >返回JSON字符串</returns>
            $.LCLCore.LazyLoader.LoadJSONHelp(function () {
                successFn(JSON.stringify(jsonObj));
                //                var aa = JSON.stringify(jsonObj);
                //                alert(aa);
                //                return aa;
            });
        },
        ConvertStringToJson: function (jsonString) {
            /// <summary>
            /// 将JSON对象转换为JSON字符串
            /// </summary>
            /// <param name="jsonObj" type="JSON Obj">需要转换的JSON对象</param>
            /// <returns type="string" >返回JSON字符串</returns>
            return $.parseJSON(jsonString);
            //return eval(jsonString);
        },
        ConvertToString: function (jsonObj) {
            /// <summary>
            /// 将JSON对象转换为JSON字符串
            /// </summary>
            /// <param name="jsonObj" type="JSON Obj">需要转换的JSON对象</param>
            /// <returns type="string" >返回JSON字符串</returns>
            return JSON.stringify(jsonObj);
        },
        CloneObj: function (jsonObj) {
            /// <summary>
            /// 将JSON对象转换为JSON字符串
            /// </summary>
            /// <param name="jsonObj" type="JSON Obj">需要转换的JSON对象</param>
            /// <returns type="string" >返回JSON字符串</returns>
            var s = JSON.stringify(jsonObj);
            return $.LCLCore.JSONHelp.ConvertStringToJson(s);
        }
    },
    SiteHelp: {
        GetSiteRoot: function () {
            /// <summary>
            /// 获取当前站点的相对地址
            /// </summary>
            //  var customRoot = $(document.body).attr("rel");
            //   if (customRoot) return customRoot;
            return "/";
        },
        getPluginPath: function () {
            /// <summary>
            /// 获取plugins的根目录
            var path = "";
            $.each($("script"), function (index, item) {
                if (item.src.toLowerCase().indexOf("lcl.jquery.core.js") != -1) {
                    var arr = item.src.split('/');
                    path = item.src.replace(arr[arr.length - 2] + '/' + arr[arr.length - 1], "");
                    return path;
                }
            });
            return path;
        }
    },
    ConHelp: {
        NumberInputOnly: function (con) {
            /// <summary>
            /// 设置控件只能输入数字
            /// </summary>
            con.bind("propertychange", function () {
                if ("" != this.value) {
                    var str = this.value.replace(/(^\s*)|(\s*$)/g, "");
                    if (this.value != str)
                        this.value = str;
                }
                if (this.value.indexOf('.') != -1) {
                    this.value = this.value.replace(/[\.]/, '');
                    try {
                        // 不可见的时候会出错
                        this.focus();
                    } catch (e)
                    { }
                }
                if (isNaN(Number(this.value))) {
                    this.value = ($.trim(this.value)).replace(/[\D]/, '');
                    try {
                        // 不可见的时候会出错
                        this.focus();
                    } catch (e)
                    { }
                }
            });
            con.css("ime-mode", "disabled");
            con.css("ondragenter", "expression(ondragenter=function(){return false;})");
            con.css("onpaste", "expression(onpaste=function(){return false;})");
        }
    },
    PageMask: {
        InitPageMask: function (msg) {
            /// <summary>
            /// 创建页面页面
            /// </summary>
            var lan = $.LCLBase.SiteConfig.GetCurrLanguageID();
            if (lan == null || lan == undefined) {
                lan = "2052";
            }
            var loadMsg = "正在加载，请稍后...";
            if (msg) {
                loadMsg = msg;
            } else {
                if (lan != "2052") {
                    loadMsg = "Loading...";
                }
            }
            var html = [];
            html.push('<div id="loading-mask" style=""></div><div id="loading"><div class="loading-indicator"><img alt="Loading..." src="/SiteRoot/Index/Styles/Images/1.gif" class="loading-Img" />');
            if ($.LCLBase.SiteConfig.GetSystemFlag() != "") {
                html.push($.LCLBase.SiteConfig.GetSystemFlag());
                html.push(' - <a href="' + $.LCLBase.SiteConfig.GetSystemURL() + '">');
                html.push($.LCLBase.SiteConfig.GetSystemLogo());
                html.push('</a><br />');
            }
            html.push('<span id="loading-msg">');
            html.push(loadMsg);
            html.push('</span></div></div>');
            $(html.join('')).appendTo("body");
        },
        ChangePageMaskTxt: function (maskTxt) {
            $("#loading-msg").text(maskTxt);
        },
        ClosePageMask: function () {
            // 在框架中并且框架被隐藏的时候会出现没有消失的情况，因此加入callback函数实现直接隐藏
            $("#loading-mask").fadeOut(500, function () { $("#loading-mask").hide(); });
            $("#loading").fadeOut(500, function () { $("#loading").hide(); });
        },
        OpenPageMask: function () {
            $("#loading-mask").show();
            $("#loading").show();
        }
    },
    ControlPath: {
        GetControlScriptPath: function (scriptName) {
            /// <summary>
            /// 获取控件脚本所在路径
            /// </summary>
            scriptName = scriptName.toLowerCase();
            var path = "";
            $.each($("script"), function (index, item) {
                if (item.src.toLowerCase().indexOf("/" + scriptName + ".js") != -1) {
                    var arr = item.src.split('/');
                    path = item.src.replace(arr[arr.length - 1], "");
                    return path;
                }
            });
            return path;
        }
    }
};

$.extend({}, $.LCL, $.LCLCore, $.LCLBase, $.LCLLogger);

function FormatAjaxURL(ajaxURL) {
    /// <summary>
    /// 格式化所有的Ajax请求的URL，统一添加人员工号和语言标识
    /// </summary>
    /// <param name="ajaxURL" type="string">Ajax请求的URL</param>
    /// <returns type="string">添加统一标识后的信息</returns>
    try {
        if ($.LCLBase.SiteConfig.GetCurrUserID()) {
            var f = "&";
            if (ajaxURL.indexOf('?') == -1) {
                f = "?";
            }
            ajaxURL += $.LCLCore.StringUtil.StringFormat("{0}userId={1}&LanguageID={2}", f,
             $.LCLBase.SiteConfig.GetCurrUserID(), $.LCLBase.SiteConfig.GetCurrLanguageID());
        }
        ajaxURL = $.LCLCore.URL.UrlEncode(ajaxURL);
    } catch (e) {

    }
    return ajaxURL;
}

// 跨域处理函数
function GetEasyDialog(fn) {
    $.LCLCore.LazyLoader.CustomLoadNottry("DivDialog/EasyDialog/EasyDialog.js", { js: [$.LCLCore.SiteHelp.GetSiteRoot() + "Scripts/Plugins/DivDialog/EasyDialog/EasyDialog.js"], css: [$.LCLCore.SiteHelp.GetSiteRoot() + "Scripts/Plugins/DivDialog/EasyDialog/skin/qq/ymPrompt.css"] }, function () {
        var p = jQuery.LCL.EasyDialog;
        fn(p);
    });
}

function Getbody(div) {
    if (div !== null && div !== undefined) {
        document.body.appendChild(div);
    }
    return document.body;
}

(function ($) {
    $.xLazyLoader = function (method, options) {
        if (typeof method == 'object') {
            options = method;
            method = 'load';
        };
        xLazyLoader[method](options);
    };
    var xLazyLoader = new function () {
        var head = document.getElementsByTagName("head")[0];
        this.load = function (options) {
            var d = {
                js: [],
                css: [],
                image: [],
                name: null,
                load: function () { }
            };
            $.extend(d, options);
            var self = this,
				ready = false,
				loaded = {
				    js: [],
				    css: [],
				    image: []
				};
            each('js', d.js);
            each('css', d.css);
            each('image', d.image);
            function each(type, urls) {
                if ($.isArray(urls) && urls.length > 0) {
                    $.each(urls, function (i, url) {
                        load(type, url);
                    });
                }
                else if (typeof urls == 'string') {
                    load(type, urls);
                }
            };
            function load(type, url) {
                self[type](url, function () {
                    $.isArray(d[type]) ? loaded[type].push(url) : loaded[type] = url;
                    d.js.length == loaded.js.length
					&& d.css.length == loaded.css.length
					&& d.image.length == loaded.image.length
					&& d.load.apply(loaded, []);
                    return;
                }, d.name ? 'lazy-loaded-' + d.name : 'lazy-loaded-' + new Date().getTime());
            };
        };
        this.js = function (src, callback, name) {
            if ($('script[src*="' + src + '"]').length > 0) {
                callback();
                return;
            };
            var script = document.createElement('script');
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", src);
            script.setAttribute('id', name);
            head.appendChild(script);
            if ($.browser.msie) {
                script.onreadystatechange = function () {
                    /loaded|complete/.test(script.readyState) && callback();
                }
            }
            else {
                script.onload = callback;
            }
        };
        this.css = function (href, callback, name) {
            if ($('link[href*="' + href + '"]').length > 0) {
                if (callback) {
                    callback.call(link);
                }
                return;
            };
            var link = document.createElement('link');
            link.rel = 'stylesheet';
            link.type = 'text/css';
            link.media = 'screen';
            link.href = href;
            link.id = name + "-css";
            document.getElementsByTagName('head')[0].appendChild(link);
            if (callback) {
                callback.call(link);
            }
        };
        this.image = function (src, callback) {
            var img = new Image();
            img.onload = callback;
            img.src = src;
        };
        this.disable = function (name) {
            $('#lazy-loaded-' + name, head).attr('disabled', 'disabled');
        };
        this.enable = function (name) {
            $('#lazy-loaded-' + name, head).removeAttr('disabled');
        };
        this.destroy = function (name) {
            $('#lazy-loaded-' + name, head).remove();
        };
    };
})(jQuery);