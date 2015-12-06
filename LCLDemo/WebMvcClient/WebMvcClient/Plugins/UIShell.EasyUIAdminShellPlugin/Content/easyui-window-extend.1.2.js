(function ($) {
    $.fn.hWindow = function (options) {
        var defaults = {
            width: 500, 			//宽度
            height: 400, 		//高度
            iconCls: '', 		//图标class
            collapsible: false, 	//折叠
            minimizable: false, 	//最小化
            maximizable: false, 	//最大化
            resizable: false, 	//改变窗口大小
            title: '窗口标题', 	//窗口标题
            modal: true, 		//模态	
            submit: function () {
                alert('写入执行的代码。');
            },
            html: '',
            center: true,         //每次弹出窗口居中
            url:'',          //要加载文件的地址
            onload:function () {
            }  //加载文件完成后，执行的函数
        }
        var options = $.extend(defaults, options);
        var win_width = $(window).width();
        var win_height = $(window).height();
        var _top = (win_height - options.height) / 2;
        var _left = (win_width - options.width) / 2;

        var self = this;

        var html = options.html;
        $(self).window({ title: options.title, width: options.width, height: options.height, content: buildWindowContent(html, options.submit,options.url),
            collapsible: options.collapsible, minimizable: options.minimizable, maximizable: options.maximizable,
            modal: options.modal, iconCls: options.iconCls, top: _top, left: _left
        }).window('open');

        $(self).keyup(function (e) {
            if (e.keyCode == 27) {
                $(self).window('close'); return false;
            }
        }).focus();

        function buildWindowContent(contentHTML, fn,url) {
            var centerDIV = $('<div region="center" border="false" style="padding:5px;"></div>').html(contentHTML);
            if (url && url != '')
                centerDIV.empty().load(url,options.onload);
            $('<div class="easyui-layout" fit="true"></div>')
			.append(centerDIV)
			.append('<div region="south" border="false" style="padding-top:5px;height:40px; overflow:hidden; text-align:center;background:#fafafa;border-top:#eee 1px solid;"> <button id="AB" class="sexybutton"><span><span><span class="ok">确定</span></span></span></button> &nbsp; <button title="ESC 关闭" id="AC" class="sexybutton"><span><span><span class="cancel">取消</span></span></span></button></div>')
			.appendTo($(self).empty())
			.layout();

            $('button[id="AC"]').click(function () {
                $(self).window('close'); return false;
            });

            $('#AB',self).unbind('click').click(fn);
        }
    }
    $.hxlMessage = {
        alertInfo: function (title, msg) {
            $.messager.alert(title, msg, 'info');
        },
        alertError: function (title, msg) {
            $.messager.alert(title, msg, 'error');
        },
        alerWarning: function (title, msg) {
            $.messager.alert(title, msg, 'warning');
        }
    }

    //Dialog
    $.fn.hDialog = function (options) {
        var defaults = {
            width: 300,
            height: 200,
            title: '此处标题',
            html: '',
            iconCls: '',
            modal:true,
            submit: function () { alert('可执行代码.'); }
        }
        var id = $(this).attr('id');
        options = $.extend(defaults, options);
        var self = this;

        $(self).dialog({
            title: options.title,
            height: options.height,
            width: options.width,
            iconCls: options.iconCls,
            buttons: [{
                text: '确定',
                iconCls: 'icon-ok',
                handler: options.submit
            }, {
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    $('#' + id).dialog('close'); return false;
                }
            }]
        });

        function createContent() {
            $('.dialog-content', $(self)).empty().append('<div id="' + id + '_content" style="padding:5px;"></div>');
            $('#' + id + "_content").html(options.html);
        }
        createContent();
    }

    function createtip(el) {
        var box = $(el);
        var msg = box.attr('tip');
        var tip = $("<div class=\"validatebox-tip\">" + "<span class=\"validatebox-tip-content\">" + "</span>" + "<span class=\"validatebox-tip-pointer\">" + "</span>" + "</div>").appendTo("body");
        tip.find(".validatebox-tip-content").html(msg);
        el.data("tip", tip);
        tip.css({ display: "block", left: box.offset().left + box.outerWidth(), top: box.offset().top });
    }

    function removetip(el) {
        var tip = el.data("tip");
        if (tip) {
            tip.remove();
            $(el).removeData("tip");
        }
    }

    $.fn.tip = function (options) {
        return this.each(function () {
            var msg = $(this).attr('tip');
            if (msg) {
                switch (options.trigger) {
                    case "hover":
                        $(this).hover(function () { createtip($(this)); }, function () { removetip($(this)) });
                        break;
                    default:
                        $(this).focus(function () {
                            createtip($(this));
                        }).blur(function () {
                            removetip($(this));
                        });
                        break;
                }
            }
        })
    }
})(jQuery)




