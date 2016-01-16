/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 票据分类 
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
    $('#btnAddhm_billtype').click(function () { pageFunc_hm_billtypeAdd(); }); 
    $('#btnDelhm_billtype').click(function () { pageFunc_hm_billtypeDel(); }); 
    $('#btnSearchhm_billtype').click(function () { pageFunc_SearchDatahm_billtype(); }); 
} 
function InitGrid() { 
    $('#grid_hm_billtype').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_BillType/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_ID, width: 100,hidden:true }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_Name, width: 100 }, 
                { field: 'PageSum', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_PageSum, width: 100 }, 
                { field: 'BillLength', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_BillLength, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_BillType_Model_UpdateDate, width: 100,hidden:true }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_billtypeEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_billtypeDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_billtype_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_billtypeEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_billtype() { 
    $("#grid_hm_billtype").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_billtype'); 
} 
function pageFunc_hm_billtypeAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_billtype').form('clear'); 
    $('#win_hm_billtype').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_billtype', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_billtypeSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_billtype'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_billtype_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_billtypeEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_billtype').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_billtype", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_billtypeSave(); 
                } 
            }, { 
                id: 'btnCancelhm_billtype', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_billtype'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_billtype'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_billtype/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_billtype_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_billtype_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_billtype_Entity_PageSum').val(resultData.DataObject.PageSum); 
                $('#hm_billtype_Entity_BillLength').val(resultData.DataObject.BillLength); 
                $('#hm_billtype_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_billtype_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_billtype_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_billtypeSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_BillType/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_BillType/AjaxEdit'; 
    } 
 
    $('#ffhm_billtype').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_billtype').linkbutton('disable'); 
            param.ID = $('#hm_billtype_Entity_ID').val(); 
            param.Name = $('#hm_billtype_Entity_Name').val(); 
            param.PageSum = $('#hm_billtype_Entity_PageSum').val(); 
            param.BillLength = $('#hm_billtype_Entity_BillLength').val(); 
            param.IsDelete = $('#hm_billtype_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_billtype_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_billtype_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_billtype').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_billtype'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_billtype'); 
                } 
            } 
            $('#btnSavehm_billtype').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_billtypeDel() { 
    var rows = $("#grid_hm_billtype").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_BillType/AjaxDeleteList/', parm, 
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
function grid_hm_billtype_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_billtype", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_billtypeAdd(); } 
    }, '-', { 
        id: "btnDelhm_billtype", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_billtypeDel(); } 
    }]; 
    return ihtml; 
} 

