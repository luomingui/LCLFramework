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
    $('#btnAddhostconfig').click(function () { pageFunc_hostconfigAdd(); }); 
    $('#btnDelhostconfig').click(function () { pageFunc_hostconfigDel(); }); 
    $('#btnSearchhostconfig').click(function () { pageFunc_SearchDatahostconfig(); }); 
} 
function InitGrid() { 
    $('#grid_hostconfig').datagrid({ 
        url: pageAttr.JsonServerURL + 'HostConfig/AjaxGetByPage', 
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
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Name, width: 100 }, 
                { field: 'IP', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_IP, width: 50 }, 
                { field: 'Addess', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Addess, width: 90 }, 
                { field: 'FtpUser', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_FtpUser, width: 90 }, 
                { field: 'FtpPassword', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_FtpPassword, width: 100 }, 
                { field: 'Netdisk', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Netdisk, width: 100 }, 
                { 
                    field: 'Flag', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Flag, width: 50, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' : 
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>'; 
                    } 
                }, 
                { field: 'SharedDirName', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirName, width: 100 }, 
                { field: 'SharedDirUser', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirUser, width: 100 }, 
                { field: 'SharedDirPassword', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirPassword, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_UpdateDate, width: 100,hidden:true }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hostconfigEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hostconfigDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hostconfig_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hostconfigEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahostconfig() { 
    $("#grid_hostconfig").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hostconfig'); 
} 
function pageFunc_hostconfigAdd() { 
    pageAttr.Added = true; 
    $('#ffhostconfig').form('clear'); 
    $('#win_hostconfig').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehostconfig', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hostconfigSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hostconfig'); 
            } 
        }], 
        onLoad: function () { 
            $('#hostconfig_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hostconfigEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hostconfig').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehostconfig", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hostconfigSave(); 
                } 
            }, { 
                id: 'btnCancelhostconfig', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hostconfig'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hostconfig'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hostconfig/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hostconfig_Entity_ID').val(resultData.DataObject.ID); 
                $('#hostconfig_Entity_Name').val(resultData.DataObject.Name); 
                $('#hostconfig_Entity_IP').val(resultData.DataObject.IP); 
                $('#hostconfig_Entity_Addess').val(resultData.DataObject.Addess); 
                $('#hostconfig_Entity_FtpUser').val(resultData.DataObject.FtpUser); 
                $('#hostconfig_Entity_FtpPassword').val(resultData.DataObject.FtpPassword); 
                $('#hostconfig_Entity_Netdisk').val(resultData.DataObject.Netdisk); 
                $('#hostconfig_Entity_Flag').val(resultData.DataObject.Flag); 
                $('#hostconfig_Entity_SharedDirName').val(resultData.DataObject.SharedDirName); 
                $('#hostconfig_Entity_SharedDirUser').val(resultData.DataObject.SharedDirUser); 
                $('#hostconfig_Entity_SharedDirPassword').val(resultData.DataObject.SharedDirPassword); 
                $('#hostconfig_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hostconfig_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hostconfig_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hostconfigSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HostConfig/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HostConfig/AjaxEdit'; 
    } 
 
    $('#ffhostconfig').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehostconfig').linkbutton('disable'); 
            param.ID = $('#hostconfig_Entity_ID').val(); 
            param.Name = $('#hostconfig_Entity_Name').val(); 
            param.IP = $('#hostconfig_Entity_IP').val(); 
            param.Addess = $('#hostconfig_Entity_Addess').val(); 
            param.FtpUser = $('#hostconfig_Entity_FtpUser').val(); 
            param.FtpPassword = $('#hostconfig_Entity_FtpPassword').val(); 
            param.Netdisk = $('#hostconfig_Entity_Netdisk').val(); 
            param.Flag = $('#hostconfig_Entity_Flag').val(); 
            param.SharedDirName = $('#hostconfig_Entity_SharedDirName').val(); 
            param.SharedDirUser = $('#hostconfig_Entity_SharedDirUser').val(); 
            param.SharedDirPassword = $('#hostconfig_Entity_SharedDirPassword').val(); 
            param.IsDelete = $('#hostconfig_Entity_IsDelete').val(); 
            param.AddDate = $('#hostconfig_Entity_AddDate').val(); 
            param.UpdateDate = $('#hostconfig_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehostconfig').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hostconfig'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hostconfig'); 
                } 
            } 
            $('#btnSavehostconfig').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hostconfigDel() { 
    var rows = $("#grid_hostconfig").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HostConfig/AjaxDeleteList/', parm, 
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
function grid_hostconfig_toolbar() { 
    var ihtml = '<div id="tbar_hostconfig">' 
        + '<a id="btnAddhostconfig" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-add">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;' 
        + '<a id="btnDelhostconfig" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-remove">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;' 
        + '<a href="javascript:void(0)" /></div>' 
    return ihtml; 
} 

