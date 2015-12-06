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
    InitGrid();
}
//初始化事件
function InitEvent() {
    $('#btnAddwfrout').click(function () { pageFunc_wfroutAdd(); });
    $('#btnDelwfrout').click(function () { pageFunc_wfroutDel(); });
    $('#btnSearchwfrout').click(function () { pageFunc_SearchData(); });
}
function InitGrid() {
    $('#grid_wfrout').datagrid({
        url: pageAttr.JsonServerURL + 'WFRout/AjaxGetByPage',
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_title,
        iconCls: 'icon-edit',
        pagination: true,
        rownumbers: true,
        fitCloumns: true,
        idField: "ID",
        frozenColumns: [[
          { field: 'ck', checkbox: true }
        ]],
        hideColumn: [[
           { title: 'ID', field: 'ID' }
        ]],
        columns: [[
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_ID, width: 100 },
                { field: 'DeptId', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_DeptId, width: 100 },
                { field: 'Version', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Version, width: 100 },
                { field: 'State', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_State, width: 100 },
                {
                    field: 'IsEnable', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_IsEnable, width: 100, formatter: function (value, row, index) {
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' :
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>';
                    }
                },
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_UpdateDate, width: 120 },
                {
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center',
                    formatter: function (value, rec, index) {
                        return '&nbsp;<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="pageFunc_actorShow(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Actor + '</a>&nbsp;'
                             + '&nbsp;<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="pageFunc_wfroutEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;'
                             + '&nbsp;<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="pageFunc_wfroutDel()"> ' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;';
                    }
                }
        ]],
        toolbar: grid_toolbar(),
        onDblClickRow: function (rowIndex, rowData) {
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex);
            pageFunc_wfroutEdit(rowData.ID);
        }
    });
}
function pageFunc_SearchData() {
    $("#grid_wfrout").datagrid('load', {
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val()
    });
    clearSelect('grid_wfrout');
}
function pageFunc_wfroutAdd() {
    pageAttr.Added = true;
    $('#ffwfrout').form('clear');
    $('#win_wfrout').dialog({
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add,
        width: 500,
        height: 280,
        iconCls: 'icon-add',
        modal: true,
        buttons: [{
            id: 'btnSavewfrout',
            iconCls: 'icon-ok',
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save,
            handler: function () {
                pageFunc_wfroutSave();
            }
        }, {
            id: 'btnCancelwfrout',
            iconCls: 'icon-cancel',
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel,
            handler: function () {
                closeDialog('win_wfrout');
            }
        }],
        onLoad: function () {
            $('#wfrout_Entity_Name').focus();
        }
    });
}
function pageFunc_wfroutEdit(ID) {
    if (ID != undefined && ID.length > 0) {
        pageAttr.Added = false;
        $('#win_wfrout').dialog({
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit,
            width: 550,
            height: 280,
            iconCls: 'icon-edit',
            modal: true,
            buttons: [{
                id: "btnSavewfrout",
                iconCls: 'icon-ok',
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save,
                handler: function () {
                    pageFunc_wfroutSave();
                }
            }, {
                id: 'btnCancelwfrout',
                iconCls: 'icon-cancel',
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel,
                handler: function () {
                    closeDialog('win_wfrout');
                }
            }],
            onClose: function () {
                closeDialog('win_wfrout');
            }
        });
        var ajaxUrl = pageAttr.JsonServerURL + 'WFRout/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID);
        $.post(ajaxUrl, function (resultData) {
            if (resultData.Success) {
                $('#wfrout_Entity_ID').val(resultData.DataObject.ID);
                $('#wfrout_Entity_Name').val(resultData.DataObject.Name);
                $('#wfrout_Entity_DeptId').val(resultData.DataObject.DeptId);
                $('#wfrout_Entity_Version').val(resultData.DataObject.Version);
                $('#wfrout_Entity_State').val(resultData.DataObject.State);
                $('#wfrout_Entity_IsEnable').val(resultData.DataObject.IsEnable);
                $('#wfrout_Entity_IsDelete').val(resultData.DataObject.IsDelete);
                $('#wfrout_Entity_AddDate').val(resultData.DataObject.AddDate);
                $('#wfrout_Entity_UpdateDate').val(resultData.DataObject.UpdateDate);
            }
        }, 'json');

    } else {
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1);
    }
}
function pageFunc_wfroutSave() {
    var ajaxUrl = "";
    if (pageAttr.Added) {
        ajaxUrl = pageAttr.JsonServerURL + 'WFRout/AjaxAdd';
    } else {
        ajaxUrl = pageAttr.JsonServerURL + 'WFRout/AjaxEdit';
    }

    $('#ffwfrout').form('submit', {
        url: ajaxUrl,
        onSubmit: function (param) {
            $('#btnSavewfrout').linkbutton('disable');

            param.ID = $('#wfrout_Entity_ID').val();
            param.Name = $('#wfrout_Entity_Name').val();
            param.DeptId = $('#wfrout_Entity_DeptId').val();
            param.Version = $('#wfrout_Entity_Version').val();
            param.State = $('#wfrout_Entity_State').val();
            param.IsEnable = $(':radio').val();

            if ($(this).form('validate'))
                return true;
            else {
                $('#btnSavewfrout').linkbutton('enable');
                return false;
            }
        },
        success: function (data) {
            var resultData = eval('(' + data + ')');
            if (resultData.Success) {
                flashTable('grid_wfrout');
                if (pageAttr.Added) {

                } else {
                    closeDialog('win_wfrout');
                }
            }
            $('#btnSavewfrout').linkbutton('enable');
            $.LCLMessageBox.Alert(resultData.Message);
        }
    });
}
function pageFunc_wfroutDel() {
    var rows = $("#grid_wfrout").datagrid("getChecked");
    if (rows.length < 1) {
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message2);
        return;
    }
    var parm;
    $.each(rows, function (i, row) {
        if (i == 0) {
            parm = "idList=" + row.ID;
        } else {
            parm += "&idList=" + row.ID;
        }
    });
    $.LCLMessageBox.Confirm($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message3, function (r) {
        if (r) {
            $.post(pageAttr.JsonServerURL + 'WFRout/AjaxDeleteList/', parm,
            function (resultData) {
                if (resultData.Success) {
                    $.LCLMessageBox.Alert(resultData.Message,function () {
                        InitGrid();
                    });
                } else {
                    $.LCLMessageBox.Alert(resultData.Message);
                }
            }, "json");
        }
    });
}
function pageFunc_actorShow(ID) {
    if (ID != undefined && ID.length > 0) {
        $('#dlg').dialog({
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_title + $.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Actor,
            width: 800,
            height: 500,
            resizable: true,
            href: pageAttr.JsonServerURL + "WFActor/Index?routId=" + $.LCLCore.ValidUI.Trim(ID),
            modal: true
        });
    }
}
function grid_toolbar() {
    var ihtml = '<div id="tbar_wfrout">'
        + '<a id="btnAddwfrout" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-add">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;'
        + '<a id="btnDelwfrout" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-remove">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'
        + '<a href="javascript:void(0)" /></div>'
    return ihtml;
}