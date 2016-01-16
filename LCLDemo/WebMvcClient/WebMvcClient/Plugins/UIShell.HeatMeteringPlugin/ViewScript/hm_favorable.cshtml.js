/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 预交优惠率 
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
    $('#btnAddhm_favorable').click(function () { pageFunc_hm_favorableAdd(); }); 
    $('#btnDelhm_favorable').click(function () { pageFunc_hm_favorableDel(); }); 
    $('#btnSearchhm_favorable').click(function () { pageFunc_SearchDatahm_favorable(); }); 
} 
function InitGrid() { 
    $('#grid_hm_favorable').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_Favorable/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_ID, width: 100,hidden:true }, 
                { field: 'BeginDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_BeginDate, width: 100 }, 
                { field: 'EndDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_EndDate, width: 100 }, 
                { field: 'Money', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_Money, width: 100 }, 
                { field: 'ClientTypeIdList', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_ClientTypeIdList, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ChargeAnnual_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Favorable_Model_ChargeAnnual_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_favorableEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_favorableDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_favorable_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_favorableEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_favorable() { 
    $("#grid_hm_favorable").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_favorable'); 
} 
function pageFunc_hm_favorableAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_favorable').form('clear'); 
    $('#win_hm_favorable').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_favorable', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_favorableSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_favorable'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_favorable_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_favorableEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_favorable').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_favorable", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_favorableSave(); 
                } 
            }, { 
                id: 'btnCancelhm_favorable', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_favorable'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_favorable'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_favorable/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_favorable_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_favorable_Entity_BeginDate').val(resultData.DataObject.BeginDate); 
                $('#hm_favorable_Entity_EndDate').val(resultData.DataObject.EndDate); 
                $('#hm_favorable_Entity_Money').val(resultData.DataObject.Money); 
                $('#hm_favorable_Entity_ClientTypeIdList').val(resultData.DataObject.ClientTypeIdList); 
                $('#hm_favorable_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_favorable_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_favorable_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_favorable_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_favorableSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Favorable/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Favorable/AjaxEdit'; 
    } 
 
    $('#ffhm_favorable').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_favorable').linkbutton('disable'); 
            param.ID = $('#hm_favorable_Entity_ID').val(); 
            param.BeginDate = $('#hm_favorable_Entity_BeginDate').val(); 
            param.EndDate = $('#hm_favorable_Entity_EndDate').val(); 
            param.Money = $('#hm_favorable_Entity_Money').val(); 
            param.ClientTypeIdList = $('#hm_favorable_Entity_ClientTypeIdList').val(); 
            param.IsDelete = $('#hm_favorable_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_favorable_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_favorable_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_favorable_Entity_ChargeAnnual_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_favorable').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_favorable'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_favorable'); 
                } 
            } 
            $('#btnSavehm_favorable').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_favorableDel() { 
    var rows = $("#grid_hm_favorable").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_Favorable/AjaxDeleteList/', parm, 
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
function grid_hm_favorable_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_favorable", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_favorableAdd(); } 
    }, '-', { 
        id: "btnDelhm_favorable", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_favorableDel(); } 
    }]; 
    return ihtml; 
} 

