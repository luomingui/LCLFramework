/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 收费员领用票据 
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
    $('#btnAddhm_chargeuserbill').click(function () { pageFunc_hm_chargeuserbillAdd(); }); 
    $('#btnDelhm_chargeuserbill').click(function () { pageFunc_hm_chargeuserbillDel(); }); 
    $('#btnSearchhm_chargeuserbill').click(function () { pageFunc_SearchDatahm_chargeuserbill(); }); 
} 
function InitGrid() { 
    $('#grid_hm_chargeuserbill').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ChargeUserBill/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_ID, width: 100,hidden:true }, 
                { field: 'BillNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_BillNumber, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_ChargeAnnual_ID, width: 100 }, 
                { field: 'ChargeUser_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeUserBill_Model_ChargeUser_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeuserbillEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeuserbillDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_chargeuserbill_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_chargeuserbillEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_chargeuserbill() { 
    $("#grid_hm_chargeuserbill").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_chargeuserbill'); 
} 
function pageFunc_hm_chargeuserbillAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_chargeuserbill').form('clear'); 
    $('#win_hm_chargeuserbill').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_chargeuserbill', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_chargeuserbillSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_chargeuserbill'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_chargeuserbill_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_chargeuserbillEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_chargeuserbill').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_chargeuserbill", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_chargeuserbillSave(); 
                } 
            }, { 
                id: 'btnCancelhm_chargeuserbill', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_chargeuserbill'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_chargeuserbill'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_chargeuserbill/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_chargeuserbill_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_chargeuserbill_Entity_BillNumber').val(resultData.DataObject.BillNumber); 
                $('#hm_chargeuserbill_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_chargeuserbill_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_chargeuserbill_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_chargeuserbill_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
                $('#hm_chargeuserbill_Entity_ChargeUser_ID').val(resultData.DataObject.ChargeUser_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_chargeuserbillSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeUserBill/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeUserBill/AjaxEdit'; 
    } 
 
    $('#ffhm_chargeuserbill').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_chargeuserbill').linkbutton('disable'); 
            param.ID = $('#hm_chargeuserbill_Entity_ID').val(); 
            param.BillNumber = $('#hm_chargeuserbill_Entity_BillNumber').val(); 
            param.IsDelete = $('#hm_chargeuserbill_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_chargeuserbill_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_chargeuserbill_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_chargeuserbill_Entity_ChargeAnnual_ID').val(); 
            param.ChargeUser_ID = $('#hm_chargeuserbill_Entity_ChargeUser_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_chargeuserbill').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_chargeuserbill'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_chargeuserbill'); 
                } 
            } 
            $('#btnSavehm_chargeuserbill').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_chargeuserbillDel() { 
    var rows = $("#grid_hm_chargeuserbill").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ChargeUserBill/AjaxDeleteList/', parm, 
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
function grid_hm_chargeuserbill_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_chargeuserbill", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_chargeuserbillAdd(); } 
    }, '-', { 
        id: "btnDelhm_chargeuserbill", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_chargeuserbillDel(); } 
    }]; 
    return ihtml; 
} 

