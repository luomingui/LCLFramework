/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 费用增减类别 
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
    $('#btnAddhm_chargeadddel').click(function () { pageFunc_hm_chargeadddelAdd(); }); 
    $('#btnDelhm_chargeadddel').click(function () { pageFunc_hm_chargeadddelDel(); }); 
    $('#btnSearchhm_chargeadddel').click(function () { pageFunc_SearchDatahm_chargeadddel(); }); 
} 
function InitGrid() { 
    $('#grid_hm_chargeadddel').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ChargeAddDel/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_ID, width: 100,hidden:true }, 
                { field: 'PID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_PID, width: 100 }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_Name, width: 100 }, 
                { field: 'IsOpen', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_IsOpen, width: 100 }, 
                { 
                    field: 'IsOpen', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_IsOpen, width: 100, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' : 
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>'; 
                    } 
                }, 
                { field: 'Money', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_Money, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAddDel_Model_ChargeAnnual_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeadddelEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_chargeadddelDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_chargeadddel_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_chargeadddelEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_chargeadddel() { 
    $("#grid_hm_chargeadddel").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_chargeadddel'); 
} 
function pageFunc_hm_chargeadddelAdd() {
    pageAttr.Added = true; 
    $('#ffhm_chargeadddel').form('clear'); 
    $('#win_hm_chargeadddel').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_chargeadddel', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_chargeadddelSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_chargeadddel'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_chargeadddel_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_chargeadddelEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_chargeadddel').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_chargeadddel", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_chargeadddelSave(); 
                } 
            }, { 
                id: 'btnCancelhm_chargeadddel', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_chargeadddel'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_chargeadddel'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_chargeadddel/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_chargeadddel_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_chargeadddel_Entity_PID').val(resultData.DataObject.PID); 
                $('#hm_chargeadddel_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_chargeadddel_Entity_IsOpen').val(resultData.DataObject.IsOpen); 
                $('#hm_chargeadddel_Entity_Money').val(resultData.DataObject.Money); 
                $('#hm_chargeadddel_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_chargeadddel_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_chargeadddel_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_chargeadddel_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_chargeadddelSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeAddDel/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ChargeAddDel/AjaxEdit'; 
    } 
 
    $('#ffhm_chargeadddel').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_chargeadddel').linkbutton('disable'); 
            param.ID = $('#hm_chargeadddel_Entity_ID').val(); 
            param.PID = $('#hm_chargeadddel_Entity_PID').val(); 
            param.Name = $('#hm_chargeadddel_Entity_Name').val(); 
            param.IsOpen = $('#hm_chargeadddel_Entity_IsOpen').val(); 
            param.Money = $('#hm_chargeadddel_Entity_Money').val(); 
            param.IsDelete = $('#hm_chargeadddel_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_chargeadddel_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_chargeadddel_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_chargeadddel_Entity_ChargeAnnual_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_chargeadddel').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_chargeadddel'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_chargeadddel'); 
                } 
            } 
            $('#btnSavehm_chargeadddel').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_chargeadddelDel() { 
    var rows = $("#grid_hm_chargeadddel").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ChargeAddDel/AjaxDeleteList/', parm, 
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
function grid_hm_chargeadddel_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_chargeadddel", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_chargeadddelAdd(); } 
    }, '-', { 
        id: "btnDelhm_chargeadddel", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_chargeadddelDel(); } 
    }]; 
    return ihtml; 
} 

