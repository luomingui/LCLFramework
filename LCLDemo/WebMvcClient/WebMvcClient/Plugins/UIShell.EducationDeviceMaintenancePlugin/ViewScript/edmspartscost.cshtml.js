/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
 
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
    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.RbacManagementPlugin/'; 
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
    $('#btnAddedmspartscost').click(function () { pageFunc_edmspartscostAdd(); }); 
    $('#btnDeledmspartscost').click(function () { pageFunc_edmspartscostDel(); }); 
    $('#btnSearchedmspartscost').click(function () { pageFunc_SearchDataedmspartscost(); }); 
} 
function InitGrid() { 
    $('#grid_edmspartscost').datagrid({ 
        url: pageAttr.JsonServerURL + 'EDMSPartsCost/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_ID, width: 100,hidden:true }, 
                { field: 'CostType', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_CostType, width: 100 }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Name, width: 100 }, 
                { field: 'DeviceBrand', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_DeviceBrand, width: 100 }, 
                { field: 'DeviceModel', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_DeviceModel, width: 100 }, 
                { field: 'Quantity', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Quantity, width: 100 }, 
                { field: 'Unit', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Unit, width: 100 }, 
                { field: 'UnitCost', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_UnitCost, width: 100 }, 
                { field: 'Money', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Money, width: 100 }, 
                { field: 'Warranty', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Warranty, width: 100 }, 
                { field: 'Remark', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Remark, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'MaintenanceBill_ID', title: $.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_MaintenanceBill_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_edmspartscostEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_edmspartscostDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_edmspartscost_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_edmspartscostEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDataedmspartscost() { 
    $("#grid_edmspartscost").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_edmspartscost'); 
} 
function pageFunc_edmspartscostAdd() { 
    pageAttr.Added = true; 
    $('#ffedmspartscost').form('clear'); 
    $('#win_edmspartscost').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSaveedmspartscost', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_edmspartscostSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_edmspartscost'); 
            } 
        }], 
        onLoad: function () { 
            $('#edmspartscost_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_edmspartscostEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_edmspartscost').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSaveedmspartscost", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_edmspartscostSave(); 
                } 
            }, { 
                id: 'btnCanceledmspartscost', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_edmspartscost'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_edmspartscost'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'edmspartscost/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#edmspartscost_Entity_ID').val(resultData.DataObject.ID); 
                $('#edmspartscost_Entity_CostType').val(resultData.DataObject.CostType); 
                $('#edmspartscost_Entity_Name').val(resultData.DataObject.Name); 
                $('#edmspartscost_Entity_DeviceBrand').val(resultData.DataObject.DeviceBrand); 
                $('#edmspartscost_Entity_DeviceModel').val(resultData.DataObject.DeviceModel); 
                $('#edmspartscost_Entity_Quantity').val(resultData.DataObject.Quantity); 
                $('#edmspartscost_Entity_Unit').val(resultData.DataObject.Unit); 
                $('#edmspartscost_Entity_UnitCost').val(resultData.DataObject.UnitCost); 
                $('#edmspartscost_Entity_Money').val(resultData.DataObject.Money); 
                $('#edmspartscost_Entity_Warranty').val(resultData.DataObject.Warranty); 
                $('#edmspartscost_Entity_Remark').val(resultData.DataObject.Remark); 
                $('#edmspartscost_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#edmspartscost_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#edmspartscost_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#edmspartscost_Entity_MaintenanceBill_ID').val(resultData.DataObject.MaintenanceBill_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_edmspartscostSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'EDMSPartsCost/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'EDMSPartsCost/AjaxEdit'; 
    } 
 
    $('#ffedmspartscost').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSaveedmspartscost').linkbutton('disable'); 
            param.ID = $('#edmspartscost_Entity_ID').val(); 
            param.CostType = $('#edmspartscost_Entity_CostType').val(); 
            param.Name = $('#edmspartscost_Entity_Name').val(); 
            param.DeviceBrand = $('#edmspartscost_Entity_DeviceBrand').val(); 
            param.DeviceModel = $('#edmspartscost_Entity_DeviceModel').val(); 
            param.Quantity = $('#edmspartscost_Entity_Quantity').val(); 
            param.Unit = $('#edmspartscost_Entity_Unit').val(); 
            param.UnitCost = $('#edmspartscost_Entity_UnitCost').val(); 
            param.Money = $('#edmspartscost_Entity_Money').val(); 
            param.Warranty = $('#edmspartscost_Entity_Warranty').val(); 
            param.Remark = $('#edmspartscost_Entity_Remark').val(); 
            param.IsDelete = $('#edmspartscost_Entity_IsDelete').val(); 
            param.AddDate = $('#edmspartscost_Entity_AddDate').val(); 
            param.UpdateDate = $('#edmspartscost_Entity_UpdateDate').val(); 
            param.MaintenanceBill_ID = $('#edmspartscost_Entity_MaintenanceBill_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSaveedmspartscost').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_edmspartscost'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_edmspartscost'); 
                } 
            } 
            $('#btnSaveedmspartscost').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_edmspartscostDel() { 
    var rows = $("#grid_edmspartscost").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'EDMSPartsCost/AjaxDeleteList/', parm, 
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
function grid_edmspartscost_toolbar() { 
    var ihtml = '<div id="tbar_edmspartscost">' 
        + '<a id="btnAddedmspartscost" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-add">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;' 
        + '<a id="btnDeledmspartscost" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-remove">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;' 
        + '<a href="javascript:void(0)" /></div>' 
    return ihtml; 
} 

