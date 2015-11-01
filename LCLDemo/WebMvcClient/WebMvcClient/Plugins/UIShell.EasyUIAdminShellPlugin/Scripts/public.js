var p = {//方法名，json数据， 成功调用方法，失败调用方法
    doTask: function (_url, _json, _successMethod, _errorMethod) {
        if (_json && _json.length > 0) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: WS_URL + _url,
                dataType: "json",
                data: _json,
                cache: false,
                success: function (data) {
                    //异常处理
                    if (data.d.flag < 0) {
                        p.exceptionHandling(data.d);
                        return;
                    }
                    if (typeof (_successMethod) != "undefined") {
                        _successMethod(data);
                    }
                },
                error: function (err) {
                    if (typeof (_errorMethod) != "undefined") {
                        _errorMethod('执行出错!');
                    }
                },
                beforeSend: function () {
                    $.messager.progress({ msg: '正在处理中……' });
                },
                complete: function () {
                    $.messager.progress('close');
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: WS_URL + _url,
                cache: false,
                success: function (data) {
                    //异常处理
                    if (data.d.flag < 0) {
                        p.exceptionHandling(data.d);
                        return;
                    }
                    if (typeof (_successMethod) != "undefined") {
                        _successMethod(data);
                    }
                },
                error: function (err) {
                    if (typeof (_errorMethod) != "undefined") {
                        _errorMethod('执行出错！');
                    }
                },
                beforeSend: function () {
                    $.messager.progress({ msg: '正在处理中……' });
                },
                complete: function () {
                    $.messager.progress('close');
                }
            });
        }
    },
    showMsg: function (title, msg) {
        var secs = 3000;
        if (arguments.length > 2) {
            secs = arguments[2];
        }
        $.messager.show({
            title: title,
            msg: msg,
            timeout: secs,
            width: 500,
            height: 'auto',
            showType: 'fade',
            style: { top: 50 }
        });
    },
    serializeToJSON: function (idtag, o) //idtag: id标记，以**开始，比如serializeToJSON('"o_')表示以"o_开头。o 返回对象名
    {
        //获取指定Id的数据序列化
        var d = $("[id^='" + idtag + "'][type!='radio'][name],:radio:checked").serializeToJSON();

        var other = "";//其他需要序列化的数据
        //获取numberbox数据序列化
        $('input[numberboxname]').each(function () {
            var val = $(this).val();
            if ($(this).val() != "") {
                other += ',"' + $(this).attr("numberboxname") + '":' + val + '';
            }
        });
        //获取combobox数据序列化        
        $('input[comboname]').each(function () {
            if ($(this).attr('class').match('combobox') && $(this).combobox('options').multiple) {
                other += ',"' + $(this).attr("comboname") + '":"' + $(this).combobox('getValues').sort().join(',') + '"';
            } else {
                other += ',"' + $(this).attr("comboname") + '":"' + $(this).combobox('getValue') + '"';
            }
        });

        if (other.length > 1) {
            if (d.length <= 2)//序列化的数据为空
            {
                d = '{' + other.substring(1) + '}'
            }
            else //序列化数据不为空
            {
                d = d.substring(0, d.length - 1) + other + '}';
            }
        }
        d = '{"' + o + '":' + d + '}';
        return d;
    },
    showReSubmitAlert: function () {
        var dateLast = new Date($('#hSubTime').val());
        var dateNow = new Date();
        var diffSecond = (dateNow.getTime() - dateLast.getTime()) / 1000
        if (diffSecond <= 60) {
            $.messager.alert('操作警告', '提交太快，请稍后再操作！');
            return false;
        }
        return true;
    },
    exceptionHandling: function (d) {
        switch (d.flag) {
            case -1:
                p.timeout();
                break;
            case -2:
                p.showMsg('没有权限', d.msg);
                break;
            default:
                break;
        }
        return;
    },
    timeout: function () {//不支持虚拟目录
        if (typeof (top.location) != 'undefined') {
            //获取当前网址，如：http://localhost:2005/Repair/AlarmCounty.aspx
            var curWwwPath = window.document.location.href;
            //获取主机地址之后的目录，如： Repair/AlarmCounty.aspx
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
            //获取主机地址，如： http://localhost:2005
            var localhostPaht = curWwwPath.substring(0, pos);            
            var url = localhostPaht;
            top.location.href = url;
        }
    }
}