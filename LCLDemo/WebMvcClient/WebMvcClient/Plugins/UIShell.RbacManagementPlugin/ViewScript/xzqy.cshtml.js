/// <reference path="/Content/Code/LCL.JQuery.Base.js" />
/// <reference path="/Content/Core/LCL.JQuery.Core.js" />
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" />
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" />

//页面属性PageAttr （该行不允许删除）
var pageAttr = {
    SiteRoot: '',
    LanguageId: '2052',
    JsonServerURL: '',
    Added: true,
    toolbar: ''
};
//页面入口
$(document).ready(function () {
    //debugger;
    InitAttribute();
    InitLanguage();
    InitControls();
    InitEvent();
});
//初始化页面属性
function InitAttribute() {
    pageAttr.SiteRoot = $.LCLBase.SiteConfig.GetSiteRoot();
    pageAttr.LanguageId = $.LCLBase.SiteConfig.GetCurrLanguageID();
    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.RbacManagementPlugin/';
}
//初始化多语言
function InitLanguage() {
    $.LCLPageModel.Resource.InitLanguage();
}
//初始化控件
function InitControls() {
    InitTree();
}
//初始化事件
function InitEvent() {
    $('#btnAddxzqy').click(function () { pageFunc_xzqyAdd(); });
    $('#btnDelxzqy').click(function () { pageFunc_xzqyDel(); });
    $('#btnSavexzqy').click(function () { pageFunc_xzqySave(); });
}
function InitTree() {
    $('#ui_xzqy_tree').tree({
        url: pageAttr.JsonServerURL + 'Xzqy/AjaxEasyUITree_Xzqy',
        lines: true,
        onClick: function (node) {
            pageAttr.Added = false;
            $('#xzqy_Entity_ParentId').val(node.parentId);
            $('#xzqy_Entity_ParentName').val(node.parentName);

            $('#xzqy_Entity_ID').val(node.id);
            $('#xzqy_Entity_HelperCode').val(node.attributes.HelperCode);
            $('#xzqy_Entity_Name').val(node.text);
            $('#xzqy_Entity_Intro').val(node.attributes.Intro);
            $('#xzqy_Entity_IsLast').val(node.attributes.IsLast);
            $('#xzqy_Entity_Level').val(node.attributes.Level);
            $('#xzqy_Entity_NodePath').val(node.attributes.NodePath);
            $('#xzqy_Entity_OrderBy').val(node.attributes.OrderBy);
            $('#xzqy_Entity_IsDelete').val(node.attributes.IsDelete);
            $('#xzqy_Entity_AddDate').val(node.attributes.AddDate);
            $('#xzqy_Entity_UpdateDate').val(node.attributes.UpdateDate);
        }
    });
}
function TreeReload() {
    $('#ui_xzqy_tree').tree('reload');
}
function TreeGetSelected() {
    var node = $('#ui_xzqy_tree').tree('getSelected');
    if (!node) {
        alert("请选择");
        return;
    }
    return node;
}
function pageFunc_xzqyAdd() {
    var node = TreeGetSelected();
    if (node) {
        //新增下级
        pageAttr.Added = true;
        $('#xzqy_Entity_ParentId').val(node.id);
        $('#xzqy_Entity_ParentName').val(node.text);

        $('#xzqy_Entity_ID').val("");
        $('#xzqy_Entity_HelperCode').val("");
        $('#xzqy_Entity_Name').val("");
        $('xzqy_#Entity_Intro').val("");
    }
}
function pageFunc_xzqySave() {
    var ID = $("xzqy_Entity_ID").val();
    var HelperCode = $("#xzqy_Entity_HelperCode").val();
    var Name = $("#xzqy_Entity_Name").val();
    var Intro = $("#xzqy_Entity_Intro").val();
    var PID = $("#xzqy_Entity_ParentId").val();
    if (HelperCode.length <= 1) {
        $.messager.alert('系统消息', "区划代码不能为空.");
        $("#xzqy_Entity_HelperCode").focus();
        return;
    }
    if (Name.length <= 1) {
        $.messager.alert('系统消息', "区划名称不能为空.");
        $("#xzqy_Entity_Name").focus();
        return;
    }
    if (pageAttr.Added) {
        $.post(pageAttr.JsonServerURL + 'Xzqy/AjaxAdd',
            { ID: ID, Name: Name, HelperCode: HelperCode, ParentId: PID, Intro: Intro }, function (data) {
                $.messager.alert('系统消息', data.Message);
                TreeReload();
            }, "json");
    } else {
        $.post(pageAttr.JsonServerURL + 'Xzqy/AjaxEdit',
            { ID: ID, Name: Name, HelperCode: HelperCode, ParentId: PID, Intro: Intro }, function (data) {
                $.messager.alert('系统消息', data.Message);
                TreeReload();
            }, "json");
    }
}
function pageFunc_xzqyDel() {
    var node = TreeGetSelected();
    if (node) {
        $.messager.confirm('确认', '确认要删除选中记录吗?', function (y) {
            if (y) {
                //提交
                $.post(pageAttr.JsonServerURL + 'Xzqy/AjaxDelete/', node.id,
                function (msg) {
                    if (msg.Success) {
                        $.messager.alert('提示', msg.Message, 'info', function () {
                            //重新加载当前页
                            LCL.reload();
                        });
                    } else {
                        $.messager.alert('提示', msg.Message, 'info')
                    }
                }, "json");
            }
        });
    }
    else {
        alert('请选择');
    }
    return false;
}