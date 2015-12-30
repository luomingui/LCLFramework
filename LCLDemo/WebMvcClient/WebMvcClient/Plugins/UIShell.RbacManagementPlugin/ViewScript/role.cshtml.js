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
    $('#btnSearchrole').click(function () { pageFunc_SearchDatarole(); }); 
} 
function InitGrid() { 
    $('#grid_role').datagrid({ 
        url: pageAttr.JsonServerURL + 'Role/AjaxGetByPage', 
        iconCls: 'icon-edit', 
        pagination: true, 
        rownumbers: true, 
        fitCloumns: true,
        fit:true,
        idField: "ID", 
        frozenColumns: [[ 
          { field: 'ck', checkbox: true } 
        ]], 
        hideColumn: [[ 
           { title: 'ID', field: 'ID' } 
        ]], 
        columns: [[ 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.Role_Model_Name, width: 100 }, 
                { field: 'Remark', title: $.LCLPageModel.Resource.PageLanguageResource.Role_Model_Remark, width: 200 }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.Role_Model_AddDate, width: 120 }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.Role_Model_UpdateDate, width: 120 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return   '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_authority(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_authority + '</a>&nbsp;'
                               + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_roleEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;'
                               + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_roleDel()"> ' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_role_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_roleEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatarole() { 
    $("#grid_role").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_role'); 
} 
function pageFunc_roleAdd() { 
    pageAttr.Added = true; 
    $('#ffrole').form('clear'); 
    $('#win_role').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSaverole', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_roleSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_role'); 
            } 
        }], 
        onLoad: function () { 
            $('#role_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_roleEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_role').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSaverole", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_roleSave(); 
                } 
            }, { 
                id: 'btnCancelrole', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_role'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_role'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'role/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#role_Entity_ID').val(resultData.DataObject.ID); 
                $('#role_Entity_Name').val(resultData.DataObject.Name); 
                $('#role_Entity_Remark').val(resultData.DataObject.Remark); 
                $('#role_Entity_RoleType').val(resultData.DataObject.RoleType); 
                $('#role_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#role_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#role_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_roleSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'Role/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'Role/AjaxEdit'; 
    } 
 
    $('#ffrole').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSaverole').linkbutton('disable'); 
            param.ID = $('#role_Entity_ID').val(); 
            param.Name = $('#role_Entity_Name').val(); 
            param.Remark = $('#role_Entity_Remark').val(); 
            param.RoleType = $('#role_Entity_RoleType').val(); 
            param.IsDelete = $('#role_Entity_IsDelete').val(); 
            param.AddDate = $('#role_Entity_AddDate').val(); 
            param.UpdateDate = $('#role_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSaverole').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_role'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_role'); 
                } 
            } 
            $('#btnSaverole').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_roleDel() { 
    var rows = $("#grid_role").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'Role/AjaxDeleteList/', parm, 
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
function pageFunc_authority(ID)
{
    if (ID != undefined && ID.length > 0) {
        $('<div/>').dialog({
            id: "ui_roleauth_dialog",
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_title + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_authority,
            width: 800,
            height: 500,
            resizable: true,
            href: pageAttr.JsonServerURL + "RoleAuthority/Index?roleId=" + $.LCLCore.ValidUI.Trim(ID),
            modal: true
        });
    }
}
function grid_role_toolbar() { 
    var ihtml = [{
        id: "btnAddrole",
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add,
        iconCls: 'icon-add',
        handler: function () { pageFunc_roleAdd(); }
    }, '-', {
        id: "btnDelrole",
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del,
        iconCls: 'icon-remove',
        handler: function () { pageFunc_roleDel(); }
    }];
    return ihtml;
} 
