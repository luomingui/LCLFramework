window.onload = function () {
    $('#loading-mask').fadeOut();
}
var onlyOpenTitle = "欢迎使用";//不允许关闭的标签的标题

function addTab(subtitle,url,icon){
	if(!$('#tabs').tabs('exists',subtitle)){
		$('#tabs').tabs('add',{
			title:subtitle,
			content:createFrame(url),
			closable:true,
			icon:icon
		});
	}else{
		$('#tabs').tabs('select',subtitle);
		$('#mm-tabupdate').click();
	}
	tabClose();
}
function createFrame(url)
{
	var s = '<iframe scrolling="auto" frameborder="0"  src="'+url+'" style="width:100%;height:100%;"></iframe>';
	return s;
}
function tabClose()
{
	/*双击关闭TAB选项卡*/
	$(".tabs-inner").dblclick(function(){
		var subtitle = $(this).children(".tabs-closable").text();
		$('#tabs').tabs('close',subtitle);
	})
	/*为选项卡绑定右键*/
	$(".tabs-inner").bind('contextmenu',function(e){
		$('#mm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle =$(this).children(".tabs-closable").text();

		$('#mm').data("currtab",subtitle);
		$('#tabs').tabs('select',subtitle);
		return false;
	});
}
//绑定右键菜单事件
function tabCloseEven()
{
	//刷新
	$('#mm-tabupdate').click(function(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('rel');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createFrame(url)
			}
		})
	})
	//关闭当前
	$('#mm-tabclose').click(function(){
		var t = $('#mm').data("currtab");
		if (t !== "首页") {
		    $('#tabs').tabs('close', t);//currtab_title
		}
	})
	//全部关闭
	$('#mm-tabcloseall').click(function(){
		$('.tabs-inner span').each(function(i,n){
			var t = $(n).text();
			if(t !== "首页")
				$('#tabs').tabs('close',t);
		});
	});
	//关闭除当前之外的TAB
	$('#mm-tabcloseother').click(function(){
		$('#mm-tabcloseright').click();
		$('#mm-tabcloseleft').click();
	});
	//关闭当前右侧的TAB
	$('#mm-tabcloseright').click(function(){
		var nextall = $('.tabs-selected').nextAll();
		if(nextall.length==0){
			//alert('后边没有啦~~');
			return false;
		}
		nextall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			if(t !== "首页")
			   $('#tabs').tabs('close',t);
		});
		return false;
	});
	//关闭当前左侧的TAB
	$('#mm-tabcloseleft').click(function(){
		var prevall = $('.tabs-selected').prevAll();
		if(prevall.length==0){
			//alert('到头了，前边没有啦~~');
			return false;
		}
		prevall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			if(t !== "首页")
			    $('#tabs').tabs('close',t);
		});
		return false;
	});
	//退出
	$("#mm-exit").click(function(){
		$('#mm').menu('hide');
	})
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
