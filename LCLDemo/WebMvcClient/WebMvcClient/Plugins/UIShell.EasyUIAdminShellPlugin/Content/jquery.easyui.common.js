/// <reference path="../../Doc/jquery-1.6.2-vsdoc.js" />
/// <reference path="../../Plugins/EasyUI/jquery.easyui.min.js" />
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
/**
* @author 风骑士
* @requires jQuery,EasyUI
* 初始化datagrid toolbar
*/
getToolBar = function (data) {
    if (data.toolbar != undefined && data.toolbar != '') {
        var toolbar = [];
        $.each(data.toolbar, function (index, row) {
            var handler = row.handler;
            row.handler = function () { eval(handler); };
            toolbar.push(row);
        });
        return toolbar;
    } else {
        return [];
    }
}
/**
* @author 孙宇
* 接收一个以逗号分割的字符串，返回List，list里每一项都是一个字符串（做编辑功能的时候 传入id 然后自动勾选combo系列组件）
* @returns list
*/
stringToList = function (value) {
    if (value != undefined && value != '') {
        var values = [];
        var t = value.split(',');
        for (var i = 0; i < t.length; i++) {
            values.push('' + t[i]); /* 避免将ID当成数字 */
        }
        return values;
    } else {
        return [];
    }
};
/**
*关闭Jquery EasyUi的弹出窗口(适用于Jquery Easy Ui)
*
*@paramdialogId将要关闭窗口的id
*/
function closeDialog(dialogId) {
    $('#' + dialogId).dialog('close');
}
/**
*清空指定表单中的内容,参数为目标form的id
*注：在使用Jquery EasyUI的弹出窗口录入新增内容时，每次打开必须清空上次输入的历史
*数据，此时通常采用的方法是对每个输入组件进行置空操作:$("#name").val(""),这样做，
*当输入组件比较多时会很繁琐，产生的js代码很长，这时可以将所有的输入组件放入个form表单
*中，然后调用以下方法即可。
*
*@param formId将要清空内容的form表单的id
*/
function resetContent(formId) {
    var clearForm = document.getElementById(formId);
    if (null != clearForm && typeof (clearForm) != "undefined") {
        clearForm.reset();
    }
}
/**
*刷新DataGrid列表(适用于Jquery Easy Ui中的dataGrid)
*注：建议采用此方法来刷新DataGrid列表数据(也即重新加载数据)，不建议直接使用语句
*$('#dataTableId').datagrid('reload');来刷新列表数据，因为采用后者，如果日后
*在修改项目时，要在系统中的所有刷新处进行其他一些操作，那么你将要修改系统中所有涉及刷新
*的代码，这个工作量非常大，而且容易遗漏；但是如果使用本方法来刷新列表，那么对于这种修
*该需求将很容易做到，而去不会出错，不遗漏。
*
*@paramdataTableId将要刷新数据的DataGrid依赖的table列表id
*/
function flashTable(dataTableId) {
    $('#' + dataTableId).datagrid('reload');
}
/**
*取消DataGrid中的行选择(适用于Jquery Easy Ui中的dataGrid)
*注意：解决了无法取消"全选checkbox"的选择,不过，前提是必须将列表展示
*数据的DataGrid所依赖的Table放入html文档的最全面，至少该table前没有
*其他checkbox组件。
*
*@paramdataTableId将要取消所选数据记录的目标table列表id
*/
function clearSelect(dataTableId) {
    $('#' + dataTableId).datagrid('clearSelections');
    //取消选择DataGrid中的全选
    $("input[type='checkbox']").eq(0).attr("checked", false);
}
/**
*自适应表格的宽度处理(适用于Jquery Easy Ui中的dataGrid的列宽),
*注：可以实现列表的各列宽度跟着浏览宽度的变化而变化，即采用该方法来设置DataGrid
*的列宽可以在不同分辨率的浏览器下自动伸缩从而满足不同分辨率浏览器的要求
*使用方法：(如:{field:'ymName',title:'编号',width:fillsize(0.08),align:'center'},)
*
*@parampercent当前列的列宽所占整个窗口宽度的百分比(以小数形式出现，如0.3代表30%)
*
*@return通过当前窗口和对应的百分比计算出来的具体宽度
*/
function fillsize(percent) {
    var bodyWidth = document.body.clientWidth;
    return (bodyWidth - 90) * percent;
}
/**
* 获取所选记录行(单选)
*
* @paramdataTableId目标记录所在的DataGrid列表的table的id
* @paramerrorMessage 如果没有选择一行(即没有选择或选择了多行)的提示信息
*
* @return 所选记录行对象，如果返回值为null,或者"null"(有时浏览器将null转换成了字符串"null")说明没有
*选择一行记录。
*/
function getSingleSelectRow(dataTableId, errorMessage) {
    var rows = $('#' + dataTableId).datagrid('getSelections');
    var num = rows.length;
    if (num == 1) {
        return rows[0];
    } else {
        $.messager.alert('提示消息', errorMessage, 'info');
        return null;
    }
}
/**
* 在DataGrid中获取所选记录的id,多个id用逗号分隔
* 注：该方法使用的前提是：DataGrid的idField属性对应到列表Json数据中的字段名必须为id
* @paramdataTableId目标记录所在的DataGrid列表table的id
*
* @return 所选记录的id字符串(多个id用逗号隔开)
*/
function getSelectIds(dataTableId, noOneSelectMessage) {
    var rows = $('#' + dataTableId).datagrid('getSelections');
    var num = rows.length;
    var ids = null;
    if (num < 1) {
        if (null != noOneSelectMessage) $.messager.alert('提示消息', noOneSelectMessage, 'info');
        return null;
    } else {
        for (var i = 0; i < num; i++) {
            if (null == ids || i == 0) {
                ids = rows[i].id;
            } else {
                ids = ids + "," + rows[i].id;
            }
        }
        return ids;
    }
}
/**
*删除所选记录(适用于Jquery Easy Ui中的dataGrid)(删除的依据字段是id)
*注：该方法会自动将所选记录的id(DataGrid的idField属性对应到列表Json数据中的字段名必须为id)
*动态组装成字符串，多个id使用逗号隔开(如：1,2,3,8,10)，然后存放入变量ids中传入后台，后台
*可以使用该参数名从request对象中获取所有id值字符串，此时在组装sql或者hql语句时可以采用in
*关键字来处理，简介方便。
*另外，后台代码必须在操作完之后以ajax的形式返回Json格式的提示信息，提示的json格式信息中必须有一个
*message字段，存放本次删除操作成功与失败等一些提示操作用户的信息。
*
*@paramdataTableId将要删除记录所在的列表table的id
*@paramrequestURL与后台服务器进行交互，进行具体删除操作的请求路径
*@paramconfirmMessage 删除确认信息
*/
function deleteNoteById(dataTableId, requestURL, confirmMessage) {
    if (null == confirmMessage || typeof (confirmMessage) == "undefined" || "" == confirmMessage) {
        confirmMessage = "确定删除所选记录?";
    }
    var rows = $('#' + dataTableId).datagrid('getSelections');
    var num = rows.length;
    var ids = null;
    if (num < 1) {
        $.messager.alert('提示消息', '请选择你要删除的记录!', 'info');
    } else {
        $.messager.confirm('确认', confirmMessage, function (r) {
            if (r) {
                for (var i = 0; i < num; i++) {
                    if (null == ids || i == 0) {
                        ids = rows[i].id;
                    } else {
                        ids = ids + "," + rows[i].id;
                    }
                }
                $.getJSON(requestURL, { "ids": ids }, function (data) {
                    if (null != data && null != data.message && "" != data.message) {
                        $.messager.alert('提示消息', data.message, 'info');
                        flashTable(dataTableId);
                    } else {
                        $.messager.alert('提示消息', '删除失败！', 'warning');
                    }
                    clearSelect(dataTableId);
                });
            }
        });
    }
}


/// <reference path="/Content/Code/LCL.JQuery.Base.js" />
/// <reference path="/Content/Core/LCL.JQuery.Core.js" />
/// <reference path="/Content/Core/LCL.PageModel.js" />
$.LCLMessageBox = {
    Alert: function (message, fn) {
        /// <summary>
        /// 弹出提示窗口
        /// </summary>
        /// <param name="message" type="string">需要提示的内容 (说明:优先取资源文件)</param>
        /// <param name="titleText" type="string">提示窗口头文本内容 (说明: 优先取资源文件)</param>

        // 1. 获取语言ID
        var languageID = $.LCLBase.SiteConfig.GetCurrLanguageID();

        // 2. 获取内容
        // 说明: 
        //    (a) 资源文件中存在,则取资源文件字段对应的数据; 
        //    (b)资源文件中不存在,则取自身
        var MessageTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID][message];
        if (MessageTemp) {
            message = MessageTemp;
        }

        // 3. 标题存在,则获取标题
        // 说明: 
        //     (a) 资源文件中存在,则取资源文件中字段对应的数据
        //     (b) 资源文件中不存在,则取自身
        var titleText = "温馨提示";
        var TitleTextTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID]["LCLMessageBox_AlertTitle"];
        if (TitleTextTemp) {
            titleText = TitleTextTemp;
        }

        // 4. 调用对话框
        $.LCLMessageBox.EasyUIMessager(titleText, message, 'info', fn)
    },
    Error: function (message, fn) {
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message" type="string">需要提示的内容 (说明:优先取资源文件)</param>
        /// <param name="titleText" type="string">提示窗口头文本内容 (说明: 优先取资源文件)</param>

        // 1. 获取语言ID
        var languageID = $.LCLBase.SiteConfig.GetCurrLanguageID();

        // 2. 获取内容
        // 说明: 
        //    (a) 资源文件中存在,则取资源文件字段对应的数据; 
        //    (b)资源文件中不存在,则取自身
        var MessageTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID][message];
        if (MessageTemp) {
            message = MessageTemp;
        }

        // 3. 标题存在,则获取标题
        // 说明: 
        //     (a) 资源文件中存在,则取资源文件中字段对应的数据
        //     (b) 资源文件中不存在,则取自身
        var titleText = "温馨提示";
        var TitleTextTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID]["LCLMessageBox_AlertTitle"];
        if (TitleTextTemp) {
            titleText = TitleTextTemp;
        }
        // 4. 调用对话框
        $.LCLMessageBox.EasyUIMessager(titleText, message, 'error', fn)
    },
    EasyUIMessager: function (title, msg, icon, fn) {
        /*
            显示警告窗口。参数：
            title：在头部面板显示的标题文本。
            msg：显示的消息文本。
            icon：显示的图标图像。可用值有：error,question,info,warning。
            fn: 在窗口关闭的时候触发该回调函数。 
        */
        $.messager.alert(title, msg, icon, fn);
    },
    Confirm: function (message, fn) {
        /// <summary>
        /// 显示一个包含“确定”和“取消”按钮的确认消息窗口
        /// </summary>
        /// <param name="title" type="string">在头部面板显示的标题文本</param>
        /// <param name="msg" type="string">显示的消息文本</param>
        /// <param name="fn" type="string">当用户点击“确定”按钮的时侯将传递一个true值给回调函数，否则传递一个false值</param>

        // 1. 获取语言ID
        var languageID = $.LCLBase.SiteConfig.GetCurrLanguageID();

        // 2. 获取内容
        // 说明: 
        //    (a) 资源文件中存在,则取资源文件字段对应的数据; 
        //    (b)资源文件中不存在,则取自身
        var MessageTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID][message];
        if (MessageTemp) {
            message = MessageTemp;
        }

        // 3. 标题存在,则获取标题
        // 说明: 
        //     (a) 资源文件中存在,则取资源文件中字段对应的数据
        //     (b) 资源文件中不存在,则取自身
        var titleText = "温馨提示";
        var TitleTextTemp = $.LCLPageModel.Resource.LanguageResourceInfo[languageID]["LCLMessageBox_AlertTitle"];
        if (TitleTextTemp) {
            titleText = TitleTextTemp;
        }
        // 4. 调用对话框
        $.LCLMessageBox.EasyUIConfirm(titleText, message, fn)
    },
    EasyUIConfirm: function (title, msg, fn) {
        $.messager.confirm(title, msg, fn);
    }
}
