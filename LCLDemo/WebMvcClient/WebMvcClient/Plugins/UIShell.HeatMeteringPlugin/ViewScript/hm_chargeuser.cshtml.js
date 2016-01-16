/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 收费员区域配置 
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
    $('#btnAddhm_chargeuser').click(function () { pageFunc_hm_chargeuserAdd(); }); 
    $('#btnDelhm_chargeuser').click(function () { pageFunc_hm_chargeuserDel(); }); 
    $('#btnSearchhm_chargeuser').click(function () { pageFunc_SearchDatahm_chargeuser(); }); 
} 
function InitGrid() { 
    $('#grid_hm_chargeuser').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ChargeUser/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_ID, width: 100,hidden:true }, 
                { field: 'User_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_User_ID, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_ChargeAnnual_ID, width: 100 }, 
                { field: 'Village_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUser_Model_Village_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeuserEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeuserDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_chargeuser_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_chargeuserEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_chargeuser() { 
    $("#grid_hm_chargeuser").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_chargeuser'); 
} 
function pageFunc_hm_chargeuserAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_chargeuser').form('clear'); 
    $('#win_hm_chargeuser').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_chargeuser', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_chargeuserSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_chargeuser'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_chargeuser_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_chargeuserEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_chargeuser').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_chargeuser", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_chargeuserSave(); 
                } 
            }, { 
                id: 'btnCancelhm_chargeuser', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_chargeuser'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_chargeuser'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_chargeuser/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_chargeuser_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_chargeuser_Entity_User_ID').val(resultData.DataObject.User_ID); 
                $('#hm_chargeuser_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_chargeuser_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_chargeuser_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_chargeuser_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
                $('#hm_chargeuser_Entity_Village_ID').val(resultData.DataObject.Village_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_chargeuserSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeUser/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeUser/AjaxEdit'; 
    } 
 
    $('#ffhm_chargeuser').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_chargeuser').linkbutton('disable'); 
            param.ID = $('#hm_chargeuser_Entity_ID').val(); 
            param.User_ID = $('#hm_chargeuser_Entity_User_ID').val(); 
            param.IsDelete = $('#hm_chargeuser_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_chargeuser_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_chargeuser_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_chargeuser_Entity_ChargeAnnual_ID').val(); 
            param.Village_ID = $('#hm_chargeuser_Entity_Village_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_chargeuser').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_chargeuser'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_chargeuser'); 
                } 
            } 
            $('#btnSavehm_chargeuser').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_chargeuserDel() { 
    var rows = $("#grid_hm_chargeuser").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ChargeUser/AjaxDeleteList/', parm, 
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
function grid_hm_chargeuser_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_chargeuser", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_chargeuserAdd(); } 
    }, '-', { 
        id: "btnDelhm_chargeuser", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_chargeuserDel(); } 
    }]; 
    return ihtml; 
} 

