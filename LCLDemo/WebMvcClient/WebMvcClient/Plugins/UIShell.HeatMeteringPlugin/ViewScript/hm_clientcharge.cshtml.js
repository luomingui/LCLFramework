/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 热计量采暖费收费单 
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
    $('#btnAddhm_clientcharge').click(function () { pageFunc_hm_clientchargeAdd(); }); 
    $('#btnDelhm_clientcharge').click(function () { pageFunc_hm_clientchargeDel(); }); 
    $('#btnSearchhm_clientcharge').click(function () { pageFunc_SearchDatahm_clientcharge(); }); 
} 
function InitGrid() { 
    $('#grid_hm_clientcharge').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ClientCharge/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_ID, width: 100,hidden:true }, 
                { field: 'BeginHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_BeginHeat, width: 100 }, 
                { field: 'EndHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_EndHeat, width: 100 }, 
                { field: 'UseHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_UseHeat, width: 100 }, 
                { field: 'MoneyHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_MoneyHeat, width: 100 }, 
                { field: 'MoneyBaseHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_MoneyBaseHeat, width: 100 }, 
                { field: 'MoneyAdvance', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_MoneyAdvance, width: 100 }, 
                { field: 'MoneyOrRefunded', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_MoneyOrRefunded, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_ChargeAnnual_ID, width: 100 }, 
                { field: 'ChargeUser_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_ChargeUser_ID, width: 100 }, 
                { field: 'ClientHeatCharge_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_ClientHeatCharge_ID, width: 100 }, 
                { field: 'ClientInfo_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientCharge_Model_ClientInfo_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientchargeEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientchargeDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_clientcharge_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_clientchargeEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_clientcharge() { 
    $("#grid_hm_clientcharge").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_clientcharge'); 
} 
function pageFunc_hm_clientchargeAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_clientcharge').form('clear'); 
    $('#win_hm_clientcharge').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_clientcharge', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_clientchargeSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_clientcharge'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_clientcharge_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_clientchargeEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_clientcharge').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_clientcharge", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_clientchargeSave(); 
                } 
            }, { 
                id: 'btnCancelhm_clientcharge', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_clientcharge'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_clientcharge'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_clientcharge/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_clientcharge_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_clientcharge_Entity_BeginHeat').val(resultData.DataObject.BeginHeat); 
                $('#hm_clientcharge_Entity_EndHeat').val(resultData.DataObject.EndHeat); 
                $('#hm_clientcharge_Entity_UseHeat').val(resultData.DataObject.UseHeat); 
                $('#hm_clientcharge_Entity_MoneyHeat').val(resultData.DataObject.MoneyHeat); 
                $('#hm_clientcharge_Entity_MoneyBaseHeat').val(resultData.DataObject.MoneyBaseHeat); 
                $('#hm_clientcharge_Entity_MoneyAdvance').val(resultData.DataObject.MoneyAdvance); 
                $('#hm_clientcharge_Entity_MoneyOrRefunded').val(resultData.DataObject.MoneyOrRefunded); 
                $('#hm_clientcharge_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_clientcharge_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_clientcharge_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_clientcharge_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
                $('#hm_clientcharge_Entity_ChargeUser_ID').val(resultData.DataObject.ChargeUser_ID); 
                $('#hm_clientcharge_Entity_ClientHeatCharge_ID').val(resultData.DataObject.ClientHeatCharge_ID); 
                $('#hm_clientcharge_Entity_ClientInfo_ID').val(resultData.DataObject.ClientInfo_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_clientchargeSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientCharge/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientCharge/AjaxEdit'; 
    } 
 
    $('#ffhm_clientcharge').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_clientcharge').linkbutton('disable'); 
            param.ID = $('#hm_clientcharge_Entity_ID').val(); 
            param.BeginHeat = $('#hm_clientcharge_Entity_BeginHeat').val(); 
            param.EndHeat = $('#hm_clientcharge_Entity_EndHeat').val(); 
            param.UseHeat = $('#hm_clientcharge_Entity_UseHeat').val(); 
            param.MoneyHeat = $('#hm_clientcharge_Entity_MoneyHeat').val(); 
            param.MoneyBaseHeat = $('#hm_clientcharge_Entity_MoneyBaseHeat').val(); 
            param.MoneyAdvance = $('#hm_clientcharge_Entity_MoneyAdvance').val(); 
            param.MoneyOrRefunded = $('#hm_clientcharge_Entity_MoneyOrRefunded').val(); 
            param.IsDelete = $('#hm_clientcharge_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_clientcharge_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_clientcharge_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_clientcharge_Entity_ChargeAnnual_ID').val(); 
            param.ChargeUser_ID = $('#hm_clientcharge_Entity_ChargeUser_ID').val(); 
            param.ClientHeatCharge_ID = $('#hm_clientcharge_Entity_ClientHeatCharge_ID').val(); 
            param.ClientInfo_ID = $('#hm_clientcharge_Entity_ClientInfo_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_clientcharge').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_clientcharge'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_clientcharge'); 
                } 
            } 
            $('#btnSavehm_clientcharge').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_clientchargeDel() { 
    var rows = $("#grid_hm_clientcharge").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ClientCharge/AjaxDeleteList/', parm, 
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
function grid_hm_clientcharge_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_clientcharge", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_clientchargeAdd(); } 
    }, '-', { 
        id: "btnDelhm_clientcharge", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_clientchargeDel(); } 
    }]; 
    return ihtml; 
} 

