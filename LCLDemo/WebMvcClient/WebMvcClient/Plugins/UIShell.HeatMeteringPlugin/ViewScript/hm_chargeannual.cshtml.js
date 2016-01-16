/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 年度供热单价 
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
    $('#btnAddhm_chargeannual').click(function () { pageFunc_hm_chargeannualAdd(); }); 
    $('#btnDelhm_chargeannual').click(function () { pageFunc_hm_chargeannualDel(); }); 
    $('#btnSearchhm_chargeannual').click(function () { pageFunc_SearchDatahm_chargeannual(); }); 
} 
function InitGrid() { 
    $('#grid_hm_chargeannual').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ChargeAnnual/AjaxGetByPage', 
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
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Name, width: 100 }, 
                { 
                    field: 'IsOpen', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_IsOpen, width: 100, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' : 
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>'; 
                    } 
                }, 
                { field: 'BeginDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_BeginDate, width: 100 }, 
                { field: 'EndDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_EndDate, width: 100 }, 
                { field: 'GongreDay', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_GongreDay, width: 100 }, 
                { field: 'DnaBeginDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_DnaBeginDate, width: 100 }, 
                { field: 'BreakMoney', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_BreakMoney, width: 100 }, 
                { field: 'StopHeatRatio', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_StopHeatRatio, width: 100 }, 
                { field: 'Fixedportion', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Fixedportion, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeannualEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeannualDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_chargeannual_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_chargeannualEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_chargeannual() { 
    $("#grid_hm_chargeannual").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_chargeannual'); 
} 
function pageFunc_hm_chargeannualAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_chargeannual').form('clear'); 
    $('#win_hm_chargeannual').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 380, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_chargeannual', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_chargeannualSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_chargeannual'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_chargeannual_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_chargeannualEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_chargeannual').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_chargeannual", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_chargeannualSave(); 
                } 
            }, { 
                id: 'btnCancelhm_chargeannual', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_chargeannual'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_chargeannual'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_chargeannual/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_chargeannual_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_chargeannual_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_chargeannual_Entity_IsOpen').val(resultData.DataObject.IsOpen); 
                $('#hm_chargeannual_Entity_BeginDate').val(resultData.DataObject.BeginDate); 
                $('#hm_chargeannual_Entity_EndDate').val(resultData.DataObject.EndDate); 
                $('#hm_chargeannual_Entity_GongreDay').val(resultData.DataObject.GongreDay); 
                $('#hm_chargeannual_Entity_DnaBeginDate').val(resultData.DataObject.DnaBeginDate); 
                $('#hm_chargeannual_Entity_BreakMoney').val(resultData.DataObject.BreakMoney); 
                $('#hm_chargeannual_Entity_StopHeatRatio').val(resultData.DataObject.StopHeatRatio); 
                $('#hm_chargeannual_Entity_Fixedportion').val(resultData.DataObject.Fixedportion); 
                $('#hm_chargeannual_Entity_Gongjian').val(resultData.DataObject.Gongjian); 
                $('#hm_chargeannual_Entity_Resident').val(resultData.DataObject.Resident); 
                $('#hm_chargeannual_Entity_Dishang').val(resultData.DataObject.Dishang); 
                $('#hm_chargeannual_Entity_Gongjian1').val(resultData.DataObject.Gongjian1); 
                $('#hm_chargeannual_Entity_Resident1').val(resultData.DataObject.Resident1); 
                $('#hm_chargeannual_Entity_Dishang1').val(resultData.DataObject.Dishang1); 
                $('#hm_chargeannual_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_chargeannual_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_chargeannual_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_chargeannualSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeAnnual/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeAnnual/AjaxEdit'; 
    } 
 
    $('#ffhm_chargeannual').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_chargeannual').linkbutton('disable'); 
            param.ID = $('#hm_chargeannual_Entity_ID').val(); 
            param.Name = $('#hm_chargeannual_Entity_Name').val(); 
            param.IsOpen = $('#hm_chargeannual_Entity_IsOpen').val(); 
            param.BeginDate = $('#hm_chargeannual_Entity_BeginDate').val(); 
            param.EndDate = $('#hm_chargeannual_Entity_EndDate').val(); 
            param.GongreDay = $('#hm_chargeannual_Entity_GongreDay').val(); 
            param.DnaBeginDate = $('#hm_chargeannual_Entity_DnaBeginDate').val(); 
            param.BreakMoney = $('#hm_chargeannual_Entity_BreakMoney').val(); 
            param.StopHeatRatio = $('#hm_chargeannual_Entity_StopHeatRatio').val(); 
            param.Fixedportion = $('#hm_chargeannual_Entity_Fixedportion').val(); 
            param.Gongjian = $('#hm_chargeannual_Entity_Gongjian').val(); 
            param.Resident = $('#hm_chargeannual_Entity_Resident').val(); 
            param.Dishang = $('#hm_chargeannual_Entity_Dishang').val(); 
            param.Gongjian1 = $('#hm_chargeannual_Entity_Gongjian1').val(); 
            param.Resident1 = $('#hm_chargeannual_Entity_Resident1').val(); 
            param.Dishang1 = $('#hm_chargeannual_Entity_Dishang1').val(); 
            param.IsDelete = $('#hm_chargeannual_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_chargeannual_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_chargeannual_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_chargeannual').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_chargeannual'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_chargeannual'); 
                } 
            } 
            $('#btnSavehm_chargeannual').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_chargeannualDel() { 
    var rows = $("#grid_hm_chargeannual").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ChargeAnnual/AjaxDeleteList/', parm, 
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
function grid_hm_chargeannual_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_chargeannual", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_chargeannualAdd(); } 
    }, '-', { 
        id: "btnDelhm_chargeannual", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_chargeannualDel(); } 
    }]; 
    return ihtml; 
} 

