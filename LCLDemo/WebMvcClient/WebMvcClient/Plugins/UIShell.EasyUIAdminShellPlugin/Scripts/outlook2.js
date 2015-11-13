window.onload = function () {
    $('#loading-mask').fadeOut();
}
var onlyOpenTitle = "欢迎使用";//不允许关闭的标签的标题

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
                closable: true
            });
        }
        else {
            tabPanel.tabs('add', {
                title: title,
                href: href,
                iconCls: iconCls,
                fit: true,
                cls: 'pd3',
                closable: true
            });
        }
    }
    else {
        tabPanel.tabs('select', title);
    }
}
//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}
function trim(str) {
    //FUNCTION:去掉字符串的两边空格,中间的空格保留
    v = str.replace(/(^\s*)|(\s*$)/g, "");
    return v;
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
//http://www.cnblogs.com/huanlei/archive/2012/04/13/2445662.html
function $class(domclass) {
    var odiv = document.getElementsByTagName("*");
    var a;
    for (var i = 0; i < odiv.length; i++) {
        if (odiv[i].className == domclass) {
            a = odiv
        }
        return a;
    }
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
//datagrid宽度高度自动调整的函数
$.fn.extend({
    resizeDataGrid: function (heightMargin, widthMargin, minHeight, minWidth) {
        var height = $(document.body).height() - heightMargin;
        var width = $(document.body).width() - widthMargin;
        height = height < minHeight ? minHeight : height;
        width = width < minWidth ? minWidth : width;
        $(this).datagrid('resize', {
            height: height,
            width: width
        });
    }
});