/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 票据入库 
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
    $('#btnAddhm_bill').click(function () { pageFunc_hm_billAdd(); }); 
    $('#btnDelhm_bill').click(function () { pageFunc_hm_billDel(); }); 
    $('#btnSearchhm_bill').click(function () { pageFunc_SearchDatahm_bill(); }); 
} 
function InitGrid() { 
    $('#grid_hm_bill').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_Bill/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_ID, width: 100,hidden:true }, 
                { field: 'StartNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_StartNumber, width: 100 }, 
                { field: 'EndNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_EndNumber, width: 100 }, 
                { field: 'VersionNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_VersionNumber, width: 100 }, 
                { field: 'Quantity', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_Quantity, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'BillType_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_BillType_ID, width: 100 }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Bill_Model_ChargeAnnual_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_billEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_billDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_bill_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_billEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_bill() { 
    $("#grid_hm_bill").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_bill'); 
} 
function pageFunc_hm_billAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_bill').form('clear'); 
    $('#win_hm_bill').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_bill', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_billSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_bill'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_bill_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_billEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_bill').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_bill", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_billSave(); 
                } 
            }, { 
                id: 'btnCancelhm_bill', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_bill'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_bill'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_bill/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_bill_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_bill_Entity_StartNumber').val(resultData.DataObject.StartNumber); 
                $('#hm_bill_Entity_EndNumber').val(resultData.DataObject.EndNumber); 
                $('#hm_bill_Entity_VersionNumber').val(resultData.DataObject.VersionNumber); 
                $('#hm_bill_Entity_Quantity').val(resultData.DataObject.Quantity); 
                $('#hm_bill_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_bill_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_bill_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_bill_Entity_BillType_ID').val(resultData.DataObject.BillType_ID); 
                $('#hm_bill_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_billSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Bill/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Bill/AjaxEdit'; 
    } 
 
    $('#ffhm_bill').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_bill').linkbutton('disable'); 
            param.ID = $('#hm_bill_Entity_ID').val(); 
            param.StartNumber = $('#hm_bill_Entity_StartNumber').val(); 
            param.EndNumber = $('#hm_bill_Entity_EndNumber').val(); 
            param.VersionNumber = $('#hm_bill_Entity_VersionNumber').val(); 
            param.Quantity = $('#hm_bill_Entity_Quantity').val(); 
            param.IsDelete = $('#hm_bill_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_bill_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_bill_Entity_UpdateDate').val(); 
            param.BillType_ID = $('#hm_bill_Entity_BillType_ID').val(); 
            param.ChargeAnnual_ID = $('#hm_bill_Entity_ChargeAnnual_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_bill').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_bill'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_bill'); 
                } 
            } 
            $('#btnSavehm_bill').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_billDel() { 
    var rows = $("#grid_hm_bill").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_Bill/AjaxDeleteList/', parm, 
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
function grid_hm_bill_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_bill", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_billAdd(); } 
    }, '-', { 
        id: "btnDelhm_bill", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_billDel(); } 
    }]; 
    return ihtml; 
} 

