/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 采暖变更历史 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2016年1月15日 
*   
*******************************************************/  
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
    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.HeatMeteringPlugin/'; 
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
    $('#btnAddhm_clientinfohistory').click(function () { pageFunc_hm_clientinfohistoryAdd(); }); 
    $('#btnDelhm_clientinfohistory').click(function () { pageFunc_hm_clientinfohistoryDel(); }); 
    $('#btnSearchhm_clientinfohistory').click(function () { pageFunc_SearchDatahm_clientinfohistory(); }); 
} 
function InitGrid() { 
    $('#grid_hm_clientinfohistory').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ClientInfoHistory/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_ID, width: 100,hidden:true }, 
                { field: 'User_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_User_ID, width: 100 }, 
                { field: 'RecordType', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_RecordType, width: 100 }, 
                { field: 'Record', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_Record, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_ChargeAnnual_ID, width: 100 }, 
                { field: 'ClientInfo_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfoHistory_Model_ClientInfo_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientinfohistoryEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientinfohistoryDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_clientinfohistory_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_clientinfohistoryEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_clientinfohistory() { 
    $("#grid_hm_clientinfohistory").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_clientinfohistory'); 
} 
function pageFunc_hm_clientinfohistoryAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_clientinfohistory').form('clear'); 
    $('#win_hm_clientinfohistory').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_clientinfohistory', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_clientinfohistorySave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_clientinfohistory'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_clientinfohistory_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_clientinfohistoryEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_clientinfohistory').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_clientinfohistory", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_clientinfohistorySave(); 
                } 
            }, { 
                id: 'btnCancelhm_clientinfohistory', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_clientinfohistory'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_clientinfohistory'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_clientinfohistory/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_clientinfohistory_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_clientinfohistory_Entity_User_ID').val(resultData.DataObject.User_ID); 
                $('#hm_clientinfohistory_Entity_RecordType').val(resultData.DataObject.RecordType); 
                $('#hm_clientinfohistory_Entity_Record').val(resultData.DataObject.Record); 
                $('#hm_clientinfohistory_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_clientinfohistory_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_clientinfohistory_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_clientinfohistory_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
                $('#hm_clientinfohistory_Entity_ClientInfo_ID').val(resultData.DataObject.ClientInfo_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_clientinfohistorySave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientInfoHistory/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientInfoHistory/AjaxEdit'; 
    } 
 
    $('#ffhm_clientinfohistory').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_clientinfohistory').linkbutton('disable'); 
            param.ID = $('#hm_clientinfohistory_Entity_ID').val(); 
            param.User_ID = $('#hm_clientinfohistory_Entity_User_ID').val(); 
            param.RecordType = $('#hm_clientinfohistory_Entity_RecordType').val(); 
            param.Record = $('#hm_clientinfohistory_Entity_Record').val(); 
            param.IsDelete = $('#hm_clientinfohistory_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_clientinfohistory_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_clientinfohistory_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_clientinfohistory_Entity_ChargeAnnual_ID').val(); 
            param.ClientInfo_ID = $('#hm_clientinfohistory_Entity_ClientInfo_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_clientinfohistory').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_clientinfohistory'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_clientinfohistory'); 
                } 
            } 
            $('#btnSavehm_clientinfohistory').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_clientinfohistoryDel() { 
    var rows = $("#grid_hm_clientinfohistory").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ClientInfoHistory/AjaxDeleteList/', parm, 
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
function grid_hm_clientinfohistory_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_clientinfohistory", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_clientinfohistoryAdd(); } 
    }, '-', { 
        id: "btnDelhm_clientinfohistory", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_clientinfohistoryDel(); } 
    }]; 
    return ihtml; 
} 

