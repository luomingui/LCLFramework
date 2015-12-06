/// <reference path="/Content/Code/LCL.JQuery.Base.js" />
/// <reference path="/Content/Core/LCL.JQuery.Core.js" />
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" />
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" />
window.onload = function () {
    $('#loading-mask').fadeOut();
}

//页面属性PageAttr （该行不允许删除）
var pageAttr = {
    SiteRoot: "",
    LanguageId: "2052",
};
//页面入口
$(document).ready(function () {
    InitAttribute();
    InitLanguage();
    InitControls();
    InitEvent();
});
//初始化页面属性
function InitAttribute() {
    pageAttr.SiteRoot = $.LCLBase.SiteConfig.GetSiteRoot();
    pageAttr.LanguageId = $.LCLBase.SiteConfig.GetCurrLanguageID();
}
//初始化多语言
function InitLanguage() {
    $.LCLPageModel.Resource.InitLanguage();
}
//初始化控件
function InitControls() {
    //绑定菜单单击事件
    BindMenuClickHrefEvent();
    //读取动态时间的变化
    ReadDateTimeShow();
    //这里实现对时间动态的变化
    var setTimeInterval = setInterval(ReadDateTimeShow, 1000);
    $("#tabs").contextMenus();
    $("#tabs").tabs({
        width: $("#tabs").parent().width(),
        height: "auto"
    });
}
//初始化事件
function InitEvent() {
    $('#editpass').click(function () {
        openPwd();
        $('#w').window('open');
    });
    $('#btnEp').click(function () {
        serverLogin();
    });
    $('#btnCancel').click(function () { closePwd(); })
    $('#loginOut').click(function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {
            if (r) {
                location.href = 'Logout';
            }
        });
    });
}
//实现用户单击导航栏跳转页面的方法
function BindMenuClickHrefEvent() {
    $(".menulink").bind("click", function () {
        var title = $.LCLCore.ValidUI.Trim($(this).text());
        var url = $(this).attr('rel');
        var iconCls = $(this).attr('iconcss');
        var iframe = $(this).attr('iframe') == 1 ? true : false;
        addTab(title, url, iconCls, iframe);
    });
}
//设置登录窗口
function openPwd() {
    $('#w').window({
        title: '修改密码',
        width: 400,
        height: 180,
        modal: true,
        shadow: false,
        closed: true,
        resizable: false,
        minimizable: false,
        maximizable: false,
        collapsible: false
    });
}
//关闭登录窗口
function closePwd() {
    $('#w').window('close');
}
//修改密码
function serverLogin() {
    var $newpass = $('#txtNewPass');
    var $rePass = $('#txtRePass');

    if ($newpass.val() == '') {
        msgShow('系统提示', '请输入密码！', 'warning');
        return false;
    }
    if ($rePass.val() == '') {
        msgShow('系统提示', '请在一次输入密码！', 'warning');
        return false;
    }
    if ($newpass.val() != $rePass.val()) {
        msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
        return false;
    }
    $.post('/ajax/editpassword.ashx?newpass=' + $newpass.val(), function (msg) {
        msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
        $newpass.val('');
        $rePass.val('');
        close();
    })
}
/**
* Name 添加菜单选项
* Param title 名称
* Param href 链接
* Param iconCls 图标样式
* Param iframe 链接跳转方式（true为iframe，false为href）
*/
function addTab(title, href, iconCls, iframe) {
    var tabPanel = $('#tabs');
    if (!tabPanel.tabs('exists', title)) {
        var content = '<iframe scrolling="auto" frameborder="0" src="' + href + '" style="width:100%;height:100%;"></iframe>';
        if (iframe) {
            tabPanel.tabs('add', {
                title: title,
                content: content,
                iconCls: iconCls,
                fit: true,
                cls: 'pd3',
                closable: true,
                closed: true
            });
        }
        else {
            tabPanel.tabs('add', {
                title: title,
                href: href,
                iconCls: iconCls,
                fit: true,
                cls: 'pd3',
                closable: true,
                closed: true
            });
        }
    }
    else {
        tabPanel.tabs('select', title);
    }
}
//读取动态时间的变化
function ReadDateTimeShow() {
    var year = new Date().getFullYear();
    var Month = new Date().getMonth() + 1;
    var Day = new Date().getDate();
    var Time = new Date().toLocaleTimeString();
    var AddDate = year + "年" + Month + "月" + Day + "日,时间:" + Time;
    $("#date").text(AddDate);
}
$.fn.contextMenus = function () {
    var $tabs = $(this);

    var temphtml = '<div id="tabs-contextmenuparent"><div id="tabs-contextmenu" class="easyui-menu" style="width:150px">' +
    '<div id="mm-tabclose">关闭</div>' +
    '<div id="mm-tabcloseall">关闭全部</div>' +
    '<div id="mm-tabcloseother">关闭其他</div>' +
    '<div class="menu-sep"></div>' +
    '<div id="mm-tabcloseright">关闭右侧标签</div>' +
    '<div id="mm-tabcloseleft">关闭左侧标签</div>' +
    '</div></div>';
    $("body").append(temphtml);
    $.parser.parse($('#tabs-contextmenuparent'));
    var $menus = $("#tabs-contextmenu");
    $(document).on("dblclick", ".tabs-inner", function () {
        var currtab_title = $(this).children("span").text();
        var $link = $(".tabs-title:contains(" + currtab_title + ")", $tabs);
        if ($link.is('.tabs-closable')) {
            $tabs.tabs('close', currtab_title);
        }
    });
    $(document).on("contextmenu", ".tabs-inner", function (e) {

        $menus.menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        var subtitle = $(this).children("span").text();
        $menus.data("currtab", subtitle);
        return false;

    });
    //关闭当前
    $('#mm-tabclose', $menus).click(function () {
        var currtab_title = $menus.data("currtab");
        var $link = $(".tabs-title:contains(" + currtab_title + ")", $tabs);

        if ($link.is('.tabs-closable')) {
            $tabs.tabs('close', currtab_title);
        }
    });
    //全部关闭
    $('#mm-tabcloseall', $menus).click(function () {
        $('.tabs-inner span', $tabs).each(function (i, n) {
            if ($(this).is('.tabs-closable')) {
                var t = $(n).text();
                $tabs.tabs('close', t);
            }
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother', $menus).click(function () {
        var currtab_title = $('tabs-contextmenu').data("currtab");
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).is('.tabs-closable')) {
                var t = $(n).text();
                if (t != currtab_title)
                    $tabs.tabs('close', t);
            }
        });
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright', $menus).click(function () {
        var currtab_title = $('tabs-contextmenu').data("currtab");
        var $li = $(".tabs-title:contains(" + currtab_title + ")", $tabs).parent().parent();
        var nextall = $li.nextAll();

        if (nextall.length == 0) {
            jAlert('已经是最后一个了');
            return false;
        }
        nextall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $tabs.tabs('close', t);
            }
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft', $menus).click(function () {
        var currtab_title = $menus.data("currtab");
        var $li = $(".tabs-title:contains(" + currtab_title + ")", $tabs).parent().parent();

        var prevall = $li.prevAll();
        if (prevall.length == 1) {
            jAlert('已经是第一个了');
            return false;
        }
        prevall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $tabs.tabs('close', t);
            }
        });
        return false;
    });
};
/// <reference path="/Content/Core/LCL.PageModel.js" />
$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#ff_lab_wfrout_id').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_ID);
    $('#ff_lab_wfrout_name').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Name);
    $('#ff_lab_wfrout_deptid').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_DeptId);
    $('#ff_lab_wfrout_version').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Version);
    $('#ff_lab_wfrout_state').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_State);
    $('#ff_lab_wfrout_isenable').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_IsEnable);
    $('#ff_lab_wfrout_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_IsDelete);
    $('#ff_lab_wfrout_adddate').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_AddDate);
    $('#ff_lab_wfrout_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_UpdateDate);

    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '流程'
};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'WFRout'
};