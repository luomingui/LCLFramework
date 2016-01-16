/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 客户设备管理 
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
    $('#btnAddhm_deviceinfo').click(function () { pageFunc_hm_deviceinfoAdd(); }); 
    $('#btnDelhm_deviceinfo').click(function () { pageFunc_hm_deviceinfoDel(); }); 
    $('#btnSearchhm_deviceinfo').click(function () { pageFunc_SearchDatahm_deviceinfo(); }); 
} 
function InitGrid() { 
    $('#grid_hm_deviceinfo').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_DeviceInfo/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_ID, width: 100,hidden:true }, 
                { field: 'DeviceType', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_DeviceType, width: 100 }, 
                { field: 'IsOpen', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_IsOpen, width: 100 }, 
                { 
                    field: 'IsOpen', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_IsOpen, width: 100, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' : 
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>'; 
                    } 
                }, 
                { field: 'HeatUnitType', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_HeatUnitType, width: 100 }, 
                { field: 'DeviceMac', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_DeviceMac, width: 100 }, 
                { field: 'DeviceNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_DeviceNumber, width: 100 }, 
                { field: 'Remark', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_Remark, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_UpdateDate, width: 100,hidden:true }, 
                { field: 'ClientInfo_ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_DeviceInfo_Model_ClientInfo_ID, width: 100 }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_deviceinfoEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_deviceinfoDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_deviceinfo_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_deviceinfoEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_deviceinfo() { 
    $("#grid_hm_deviceinfo").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_deviceinfo'); 
} 
function pageFunc_hm_deviceinfoAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_deviceinfo').form('clear'); 
    $('#win_hm_deviceinfo').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_deviceinfo', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_deviceinfoSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_deviceinfo'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_deviceinfo_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_deviceinfoEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_deviceinfo').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_deviceinfo", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_deviceinfoSave(); 
                } 
            }, { 
                id: 'btnCancelhm_deviceinfo', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_deviceinfo'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_deviceinfo'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_deviceinfo/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_deviceinfo_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_deviceinfo_Entity_DeviceType').val(resultData.DataObject.DeviceType); 
                $('#hm_deviceinfo_Entity_IsOpen').val(resultData.DataObject.IsOpen); 
                $('#hm_deviceinfo_Entity_HeatUnitType').val(resultData.DataObject.HeatUnitType); 
                $('#hm_deviceinfo_Entity_DeviceMac').val(resultData.DataObject.DeviceMac); 
                $('#hm_deviceinfo_Entity_DeviceNumber').val(resultData.DataObject.DeviceNumber); 
                $('#hm_deviceinfo_Entity_Remark').val(resultData.DataObject.Remark); 
                $('#hm_deviceinfo_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_deviceinfo_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_deviceinfo_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_deviceinfo_Entity_ClientInfo_ID').val(resultData.DataObject.ClientInfo_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_deviceinfoSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_DeviceInfo/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_DeviceInfo/AjaxEdit'; 
    } 
 
    $('#ffhm_deviceinfo').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_deviceinfo').linkbutton('disable'); 
            param.ID = $('#hm_deviceinfo_Entity_ID').val(); 
            param.DeviceType = $('#hm_deviceinfo_Entity_DeviceType').val(); 
            param.IsOpen = $('#hm_deviceinfo_Entity_IsOpen').val(); 
            param.HeatUnitType = $('#hm_deviceinfo_Entity_HeatUnitType').val(); 
            param.DeviceMac = $('#hm_deviceinfo_Entity_DeviceMac').val(); 
            param.DeviceNumber = $('#hm_deviceinfo_Entity_DeviceNumber').val(); 
            param.Remark = $('#hm_deviceinfo_Entity_Remark').val(); 
            param.IsDelete = $('#hm_deviceinfo_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_deviceinfo_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_deviceinfo_Entity_UpdateDate').val(); 
            param.ClientInfo_ID = $('#hm_deviceinfo_Entity_ClientInfo_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_deviceinfo').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_deviceinfo'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_deviceinfo'); 
                } 
            } 
            $('#btnSavehm_deviceinfo').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_deviceinfoDel() { 
    var rows = $("#grid_hm_deviceinfo").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_DeviceInfo/AjaxDeleteList/', parm, 
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
function grid_hm_deviceinfo_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_deviceinfo", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_deviceinfoAdd(); } 
    }, '-', { 
        id: "btnDelhm_deviceinfo", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_deviceinfoDel(); } 
    }]; 
    return ihtml; 
} 

