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
    $('#btnAdduser').click(function () { pageFunc_userAdd(); }); 
    $('#btnDeluser').click(function () { pageFunc_userDel(); }); 
    $('#btnSearchuser').click(function () { pageFunc_SearchDatauser(); });
    $('#btnLockeduser').click(function () { pageFunc_userLocked(); });
    $('#btnInitPwduser').click(function () { pageFunc_userInitPwd(); });
} 
function InitGrid() { 
    $('#grid_user').datagrid({ 
        url: pageAttr.JsonServerURL + 'User/AjaxGetByPage', 
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
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_Name, width: 100 }, 
                { 
                    field: 'IsLockedOut', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_IsLockedOut, width: 80, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' :
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>';
                    } 
                }, 
                { field: 'Sex', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_Sex, width: 50 }, 
                { field: 'Birthday', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_Birthday, width: 50 }, 
                { field: 'NationalID', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_NationalID, width: 100 }, 
                { field: 'PoliticalID', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_PoliticalID, width: 100 }, 
                { field: 'IdCard', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_IdCard, width: 100 }, 
                { field: 'Telephone', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_Telephone, width: 100 }, 
                { field: 'UserQQ', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_UserQQ, width: 100 }, 
                { field: 'Email', title: $.LCLPageModel.Resource.PageLanguageResource.User_Model_Email, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="pageFunc_userEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="pageFunc_userDel()"> ' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_user_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_userEdit(); 
        } 
    }); 
} 
function pageFunc_SearchDatauser() { 
    $("#grid_user").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_user'); 
} 
function pageFunc_userAdd() { 
    pageAttr.Added = true; 
    $('#ffuser').form('clear'); 
    $('#win_user').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 800, 
        height: 300, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSaveuser', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_userSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_user'); 
            } 
        }], 
        onLoad: function () { 
            $('#user_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_userEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_user').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 800, 
            height: 300, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSaveuser", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_userSave(); 
                } 
            }, { 
                id: 'btnCanceluser', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_user'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_user'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'user/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#user_Entity_ID').val(resultData.DataObject.ID); 
                $('#user_Entity_Name').val(resultData.DataObject.Name); 
                $('#user_Entity_Password').val(resultData.DataObject.Password); 
                $('#user_Entity_IsLockedOut').val(resultData.DataObject.IsLockedOut); 
                $('#user_Entity_UserPhoto').val(resultData.DataObject.UserPhoto); 
                $('#user_Entity_Sex').val(resultData.DataObject.Sex); 
                $('#user_Entity_Birthday').val(resultData.DataObject.Birthday); 
                $('#user_Entity_NationalID').val(resultData.DataObject.NationalID); 
                $('#user_Entity_PoliticalID').val(resultData.DataObject.PoliticalID); 
                $('#user_Entity_IdCard').val(resultData.DataObject.IdCard); 
                $('#user_Entity_Telephone').val(resultData.DataObject.Telephone); 
                $('#user_Entity_UserQQ').val(resultData.DataObject.UserQQ); 
                $('#user_Entity_Email').val(resultData.DataObject.Email); 
                $('#user_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#user_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#user_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#user_Entity_Department_ID').val(resultData.DataObject.Department_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_userSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'User/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'User/AjaxEdit'; 
    } 
 
    $('#ffuser').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSaveuser').linkbutton('disable');
            param.ID = $('#user_Entity_ID').val(); 
            param.Name = $('#user_Entity_Name').val(); 
            param.Password = $('#user_Entity_Password').val(); 
            param.IsLockedOut = $('#user_Entity_IsLockedOut').val(); 
            param.UserPhoto = $('#user_Entity_UserPhoto').val(); 
            param.Sex = $('#user_Entity_Sex').val(); 
            param.Birthday = $('#user_Entity_Birthday').val(); 
            param.NationalID = $('#user_Entity_NationalID').val(); 
            param.PoliticalID = $('#user_Entity_PoliticalID').val(); 
            param.IdCard = $('#user_Entity_IdCard').val(); 
            param.Telephone = $('#user_Entity_Telephone').val(); 
            param.UserQQ = $('#user_Entity_UserQQ').val(); 
            param.Email = $('#user_Entity_Email').val(); 
            param.IsDelete = $('#user_Entity_IsDelete').val(); 
            param.AddDate = $('#user_Entity_AddDate').val(); 
            param.UpdateDate = $('#user_Entity_UpdateDate').val(); 
            param.Department_ID = $('#user_Entity_Department_ID').val();

            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSaveuser').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_user'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_user'); 
                } 
            } 
            $('#btnSaveuser').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_userDel() { 
    var rows = $("#grid_user").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'User/AjaxDeleteList/', parm, 
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
function pageFunc_userLocked() {
    var rows = $("#grid_user").datagrid("getChecked");
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
            $.post(pageAttr.JsonServerURL + 'User/LockedUser/', parm,
            function (resultData) {
                if (resultData.Success) {
                    $.LCLMessageBox.Alert(resultData.Message, function () {
                        InitGrid();
                    });
                } else {
                    $.LCLMessageBox.Alert(resultData.Message);
                }
            }, "json");
        }
    });
}
function pageFunc_userInitPwd()
{
    var rows = $("#grid_user").datagrid("getChecked");
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
            $.post(pageAttr.JsonServerURL + 'User/ResetPassword/', parm,
            function (resultData) {
                if (resultData.Success) {
                    $.LCLMessageBox.Alert(resultData.Message, function () {
                        InitGrid();
                    });
                } else {
                    $.LCLMessageBox.Alert(resultData.Message);
                }
            }, "json");
        }
    });
}
function grid_user_toolbar() { 
    var ihtml = '<div id="tbar_ + tm.TableName.ToLower() + ">' 
        + '<a id="btnAdduser" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-add">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;'
        + '<a id="btnLockeduser" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-lock">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Locked + '</a>&nbsp;'
        + '<a id="btnInitPwduser" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-key">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_InitPwd + '</a>&nbsp;'
        + '<a id="btnDeluser" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-remove">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;' 
        + '<a href="javascript:void(0)" /></div>' 
    return ihtml; 
} 

