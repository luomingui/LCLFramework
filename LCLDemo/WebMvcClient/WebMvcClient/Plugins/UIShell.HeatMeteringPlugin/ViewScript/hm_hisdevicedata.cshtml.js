/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 抄表数据 
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
    $('#btnAddhm_hisdevicedata').click(function () { pageFunc_hm_hisdevicedataAdd(); }); 
    $('#btnDelhm_hisdevicedata').click(function () { pageFunc_hm_hisdevicedataDel(); }); 
    $('#btnSearchhm_hisdevicedata').click(function () { pageFunc_SearchDatahm_hisdevicedata(); }); 
} 
function InitGrid() { 
    $('#grid_hm_hisdevicedata').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_HisDeviceData/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_ID, width: 100,hidden:true }, 
                { field: 'TotalHeat', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_TotalHeat, width: 100 }, 
                { field: 'SupplyWaterT', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_SupplyWaterT, width: 100 }, 
                { field: 'BackWaterT', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_BackWaterT, width: 100 }, 
                { field: 'DifferenceT', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_DifferenceT, width: 100 }, 
                { field: 'AdminName', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_AdminName, width: 100 }, 
                { field: 'RealTime', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_RealTime, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'DeviceInfo_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HisDeviceData_Model_DeviceInfo_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_hisdevicedataEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_hisdevicedataDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_hisdevicedata_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_hisdevicedataEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_hisdevicedata() { 
    $("#grid_hm_hisdevicedata").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_hisdevicedata'); 
} 
function pageFunc_hm_hisdevicedataAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_hisdevicedata').form('clear'); 
    $('#win_hm_hisdevicedata').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_hisdevicedata', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_hisdevicedataSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_hisdevicedata'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_hisdevicedata_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_hisdevicedataEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_hisdevicedata').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_hisdevicedata", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_hisdevicedataSave(); 
                } 
            }, { 
                id: 'btnCancelhm_hisdevicedata', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_hisdevicedata'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_hisdevicedata'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_hisdevicedata/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_hisdevicedata_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_hisdevicedata_Entity_TotalHeat').val(resultData.DataObject.TotalHeat); 
                $('#hm_hisdevicedata_Entity_SupplyWaterT').val(resultData.DataObject.SupplyWaterT); 
                $('#hm_hisdevicedata_Entity_BackWaterT').val(resultData.DataObject.BackWaterT); 
                $('#hm_hisdevicedata_Entity_DifferenceT').val(resultData.DataObject.DifferenceT); 
                $('#hm_hisdevicedata_Entity_AdminName').val(resultData.DataObject.AdminName); 
                $('#hm_hisdevicedata_Entity_RealTime').val(resultData.DataObject.RealTime); 
                $('#hm_hisdevicedata_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_hisdevicedata_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_hisdevicedata_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_hisdevicedata_Entity_DeviceInfo_ID').val(resultData.DataObject.DeviceInfo_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_hisdevicedataSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_HisDeviceData/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_HisDeviceData/AjaxEdit'; 
    } 
 
    $('#ffhm_hisdevicedata').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_hisdevicedata').linkbutton('disable'); 
            param.ID = $('#hm_hisdevicedata_Entity_ID').val(); 
            param.TotalHeat = $('#hm_hisdevicedata_Entity_TotalHeat').val(); 
            param.SupplyWaterT = $('#hm_hisdevicedata_Entity_SupplyWaterT').val(); 
            param.BackWaterT = $('#hm_hisdevicedata_Entity_BackWaterT').val(); 
            param.DifferenceT = $('#hm_hisdevicedata_Entity_DifferenceT').val(); 
            param.AdminName = $('#hm_hisdevicedata_Entity_AdminName').val(); 
            param.RealTime = $('#hm_hisdevicedata_Entity_RealTime').val(); 
            param.IsDelete = $('#hm_hisdevicedata_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_hisdevicedata_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_hisdevicedata_Entity_UpdateDate').val(); 
            param.DeviceInfo_ID = $('#hm_hisdevicedata_Entity_DeviceInfo_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_hisdevicedata').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_hisdevicedata'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_hisdevicedata'); 
                } 
            } 
            $('#btnSavehm_hisdevicedata').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_hisdevicedataDel() { 
    var rows = $("#grid_hm_hisdevicedata").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_HisDeviceData/AjaxDeleteList/', parm, 
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
function grid_hm_hisdevicedata_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_hisdevicedata", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_hisdevicedataAdd(); } 
    }, '-', { 
        id: "btnDelhm_hisdevicedata", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_hisdevicedataDel(); } 
    }]; 
    return ihtml; 
} 

