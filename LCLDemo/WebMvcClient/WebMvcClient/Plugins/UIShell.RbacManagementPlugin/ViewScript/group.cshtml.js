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
    $('#btnAddgroup').click(function () { pageFunc_groupAdd(); }); 
    $('#btnDelgroup').click(function () { pageFunc_groupDel(); }); 
    $('#btnSearchgroup').click(function () { pageFunc_SearchDatagroup(); }); 
} 
function InitGrid() { 
    $('#grid_group').datagrid({ 
        url: pageAttr.JsonServerURL + 'Group/AjaxGetByPage', 
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
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_Name, width: 200 }, 
                { field: 'Remark', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_Remark, width: 500 }, 
                { field: 'GroupType', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_GroupType, width: 100, hidden: true },
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.Group_Model_UpdateDate, width: 100,hidden:true }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_groupEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_groupDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_group_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_groupEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatagroup() { 
    $("#grid_group").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_group'); 
} 
function pageFunc_groupAdd() { 
    pageAttr.Added = true; 
    $('#ffgroup').form('clear'); 
    $('#win_group').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavegroup', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_groupSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_group'); 
            } 
        }], 
        onLoad: function () { 
            $('#group_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_groupEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_group').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavegroup", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_groupSave(); 
                } 
            }, { 
                id: 'btnCancelgroup', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_group'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_group'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'group/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#group_Entity_ID').val(resultData.DataObject.ID); 
                $('#group_Entity_Name').val(resultData.DataObject.Name); 
                $('#group_Entity_Remark').val(resultData.DataObject.Remark); 
                $('#group_Entity_GroupType').val(resultData.DataObject.GroupType); 
                $('#group_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#group_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#group_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_groupSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'Group/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'Group/AjaxEdit'; 
    } 
 
    $('#ffgroup').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavegroup').linkbutton('disable'); 
            param.ID = $('#group_Entity_ID').val(); 
            param.Name = $('#group_Entity_Name').val(); 
            param.Remark = $('#group_Entity_Remark').val(); 
            param.GroupType = $('#group_Entity_GroupType').val(); 
            param.IsDelete = $('#group_Entity_IsDelete').val(); 
            param.AddDate = $('#group_Entity_AddDate').val(); 
            param.UpdateDate = $('#group_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavegroup').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_group'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_group'); 
                } 
            } 
            $('#btnSavegroup').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_groupDel() { 
    var rows = $("#grid_group").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'Group/AjaxDeleteList/', parm, 
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
function grid_group_toolbar() { 
    var ihtml = '<div id="tbar_group">' 
        + '<a id="btnAddgroup" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-add">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;' 
        + '<a id="btnDelgroup" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-remove">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;' 
        + '<a href="javascript:void(0)" /></div>' 
    return ihtml; 
} 

