/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 小区 
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
    $('#btnAddhm_village').click(function () { pageFunc_hm_villageAdd(); }); 
    $('#btnDelhm_village').click(function () { pageFunc_hm_villageDel(); }); 
    $('#btnSearchhm_village').click(function () { pageFunc_SearchDatahm_village(); }); 
} 
function InitGrid() { 
    $('#grid_hm_village').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_Village/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_ID, width: 100,hidden:true }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Name, width: 100 }, 
                { field: 'Pinyi', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Pinyi, width: 100 }, 
                { field: 'Type', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Type, width: 100 }, 
                { field: 'EnName', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_EnName, width: 100 }, 
                { field: 'Alias', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Alias, width: 100 }, 
                { field: 'Population', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Population, width: 100 }, 
                { field: 'TotalArea', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_TotalArea, width: 100 }, 
                { field: 'Office', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Office, width: 100 }, 
                { field: 'Summary', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Summary, width: 100 }, 
                { field: 'Address', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Address, width: 100 }, 
                { field: 'IsLast', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_IsLast, width: 100,hidden:true }, 
                { field: 'Level', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Level, width: 100,hidden:true }, 
                { field: 'NodePath', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_NodePath, width: 100,hidden:true }, 
                { field: 'OrderBy', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_OrderBy, width: 100,hidden:true }, 
                { field: 'ParentId', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_ParentId, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_UpdateDate, width: 100,hidden:true }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_villageEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_villageDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_village_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_villageEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_village() { 
    $("#grid_hm_village").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_village'); 
} 
function pageFunc_hm_villageAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_village').form('clear'); 
    $('#win_hm_village').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_village', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_villageSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_village'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_village_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_villageEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_village').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_village", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_villageSave(); 
                } 
            }, { 
                id: 'btnCancelhm_village', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_village'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_village'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_village/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_village_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_village_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_village_Entity_Pinyi').val(resultData.DataObject.Pinyi); 
                $('#hm_village_Entity_Type').val(resultData.DataObject.Type); 
                $('#hm_village_Entity_EnName').val(resultData.DataObject.EnName); 
                $('#hm_village_Entity_Alias').val(resultData.DataObject.Alias); 
                $('#hm_village_Entity_Population').val(resultData.DataObject.Population); 
                $('#hm_village_Entity_TotalArea').val(resultData.DataObject.TotalArea); 
                $('#hm_village_Entity_Office').val(resultData.DataObject.Office); 
                $('#hm_village_Entity_Summary').val(resultData.DataObject.Summary); 
                $('#hm_village_Entity_Address').val(resultData.DataObject.Address); 
                $('#hm_village_Entity_IsLast').val(resultData.DataObject.IsLast); 
                $('#hm_village_Entity_Level').val(resultData.DataObject.Level); 
                $('#hm_village_Entity_NodePath').val(resultData.DataObject.NodePath); 
                $('#hm_village_Entity_OrderBy').val(resultData.DataObject.OrderBy); 
                $('#hm_village_Entity_ParentId').val(resultData.DataObject.ParentId); 
                $('#hm_village_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_village_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_village_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_villageSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Village/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_Village/AjaxEdit'; 
    } 
 
    $('#ffhm_village').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_village').linkbutton('disable'); 
            param.ID = $('#hm_village_Entity_ID').val(); 
            param.Name = $('#hm_village_Entity_Name').val(); 
            param.Pinyi = $('#hm_village_Entity_Pinyi').val(); 
            param.Type = $('#hm_village_Entity_Type').val(); 
            param.EnName = $('#hm_village_Entity_EnName').val(); 
            param.Alias = $('#hm_village_Entity_Alias').val(); 
            param.Population = $('#hm_village_Entity_Population').val(); 
            param.TotalArea = $('#hm_village_Entity_TotalArea').val(); 
            param.Office = $('#hm_village_Entity_Office').val(); 
            param.Summary = $('#hm_village_Entity_Summary').val(); 
            param.Address = $('#hm_village_Entity_Address').val(); 
            param.IsLast = $('#hm_village_Entity_IsLast').val(); 
            param.Level = $('#hm_village_Entity_Level').val(); 
            param.NodePath = $('#hm_village_Entity_NodePath').val(); 
            param.OrderBy = $('#hm_village_Entity_OrderBy').val(); 
            param.ParentId = $('#hm_village_Entity_ParentId').val(); 
            param.IsDelete = $('#hm_village_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_village_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_village_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_village').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_village'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_village'); 
                } 
            } 
            $('#btnSavehm_village').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_villageDel() { 
    var rows = $("#grid_hm_village").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_Village/AjaxDeleteList/', parm, 
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
function grid_hm_village_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_village", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_villageAdd(); } 
    }, '-', { 
        id: "btnDelhm_village", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_villageDel(); } 
    }]; 
    return ihtml; 
} 

