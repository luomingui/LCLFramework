/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 客户信息 
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
    $('#btnAddhm_clientinfo').click(function () { pageFunc_hm_clientinfoAdd(); }); 
    $('#btnDelhm_clientinfo').click(function () { pageFunc_hm_clientinfoDel(); }); 
    $('#btnSearchhm_clientinfo').click(function () { pageFunc_SearchDatahm_clientinfo(); }); 
} 
function InitGrid() { 
    $('#grid_hm_clientinfo').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_ClientInfo/AjaxGetByPage', 
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
                { field: 'Floor', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Floor, width: 100 }, 
                { field: 'ClientType', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_ClientType, width: 100 }, 
                { field: 'HeatType', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatType, width: 100 }, 
                { field: 'HelpeCode', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HelpeCode, width: 100 }, 
                { field: 'Cardno', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Cardno, width: 100 }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Name, width: 100 }, 
                { field: 'NetInName', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_NetInName, width: 100 }, 
                { field: 'RoomNumber', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_RoomNumber, width: 100 }, 
                { field: 'HeatArea', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatArea, width: 100 }, 
                { field: 'HeatState', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatState, width: 100 }, 
                { 
                    field: 'Gender', title: $.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Gender, width: 100, formatter: function (value, row, index) { 
                        return value ? '<div class="icon-true" style="width:16px; height:16px;" >&nbsp;&nbsp;</div>' : 
                                       '<div class="icon-false" style="width:16px; height:16px;">&nbsp;&nbsp;</div>'; 
                    } 
                }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientinfoEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_clientinfoDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_clientinfo_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_clientinfoEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_clientinfo() { 
    $("#grid_hm_clientinfo").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_clientinfo'); 
} 
function pageFunc_hm_clientinfoAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_clientinfo').form('clear'); 
    $('#win_hm_clientinfo').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_clientinfo', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_clientinfoSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_clientinfo'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_clientinfo_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_clientinfoEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_clientinfo').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_clientinfo", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_clientinfoSave(); 
                } 
            }, { 
                id: 'btnCancelhm_clientinfo', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_clientinfo'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_clientinfo'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_clientinfo/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_clientinfo_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_clientinfo_Entity_ClientType').val(resultData.DataObject.ClientType); 
                $('#hm_clientinfo_Entity_HeatType').val(resultData.DataObject.HeatType); 
                $('#hm_clientinfo_Entity_HelpeCode').val(resultData.DataObject.HelpeCode); 
                $('#hm_clientinfo_Entity_Cardno').val(resultData.DataObject.Cardno); 
                $('#hm_clientinfo_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_clientinfo_Entity_NetInName').val(resultData.DataObject.NetInName); 
                $('#hm_clientinfo_Entity_RoomNumber').val(resultData.DataObject.RoomNumber); 
                $('#hm_clientinfo_Entity_BuildArea').val(resultData.DataObject.BuildArea); 
                $('#hm_clientinfo_Entity_InsideBuildArea').val(resultData.DataObject.InsideBuildArea); 
                $('#hm_clientinfo_Entity_Superelevation').val(resultData.DataObject.Superelevation); 
                $('#hm_clientinfo_Entity_BalconyState').val(resultData.DataObject.BalconyState); 
                $('#hm_clientinfo_Entity_BalconyArea').val(resultData.DataObject.BalconyArea); 
                $('#hm_clientinfo_Entity_BalconyHeatState').val(resultData.DataObject.BalconyHeatState); 
                $('#hm_clientinfo_Entity_BalconyHeatArea').val(resultData.DataObject.BalconyHeatArea); 
                $('#hm_clientinfo_Entity_InterlayerArea').val(resultData.DataObject.InterlayerArea); 
                $('#hm_clientinfo_Entity_InterlayerState').val(resultData.DataObject.InterlayerState); 
                $('#hm_clientinfo_Entity_InterlayerHeatArea').val(resultData.DataObject.InterlayerHeatArea); 
                $('#hm_clientinfo_Entity_InsideArea').val(resultData.DataObject.InsideArea); 
                $('#hm_clientinfo_Entity_HeatArea').val(resultData.DataObject.HeatArea); 
                $('#hm_clientinfo_Entity_UnitPriceType').val(resultData.DataObject.UnitPriceType); 
                $('#hm_clientinfo_Entity_NetworkDate').val(resultData.DataObject.NetworkDate); 
                $('#hm_clientinfo_Entity_IsNetwork').val(resultData.DataObject.IsNetwork); 
                $('#hm_clientinfo_Entity_BeginHeatDate').val(resultData.DataObject.BeginHeatDate); 
                $('#hm_clientinfo_Entity_TotalHeatSourceFactory').val(resultData.DataObject.TotalHeatSourceFactory); 
                $('#hm_clientinfo_Entity_HeatSource').val(resultData.DataObject.HeatSource); 
                $('#hm_clientinfo_Entity_Floor').val(resultData.DataObject.Floor); 
                $('#hm_clientinfo_Entity_LineType').val(resultData.DataObject.LineType); 
                $('#hm_clientinfo_Entity_HeatState').val(resultData.DataObject.HeatState); 
                $('#hm_clientinfo_Entity_Email').val(resultData.DataObject.Email); 
                $('#hm_clientinfo_Entity_Phone').val(resultData.DataObject.Phone); 
                $('#hm_clientinfo_Entity_JobAddress').val(resultData.DataObject.JobAddress); 
                $('#hm_clientinfo_Entity_HomeAddress').val(resultData.DataObject.HomeAddress); 
                $('#hm_clientinfo_Entity_Gender').val(resultData.DataObject.Gender); 
                $('#hm_clientinfo_Entity_Birthday').val(resultData.DataObject.Birthday); 
                $('#hm_clientinfo_Entity_ZipCode').val(resultData.DataObject.ZipCode); 
                $('#hm_clientinfo_Entity_IDCard').val(resultData.DataObject.IDCard); 
                $('#hm_clientinfo_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_clientinfo_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_clientinfo_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
                $('#hm_clientinfo_Entity_ChargeAnnual_ID').val(resultData.DataObject.ChargeAnnual_ID); 
                $('#hm_clientinfo_Entity_Village_ID').val(resultData.DataObject.Village_ID); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_clientinfoSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientInfo/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_ClientInfo/AjaxEdit'; 
    } 
 
    $('#ffhm_clientinfo').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_clientinfo').linkbutton('disable'); 
            param.ID = $('#hm_clientinfo_Entity_ID').val(); 
            param.ClientType = $('#hm_clientinfo_Entity_ClientType').val(); 
            param.HeatType = $('#hm_clientinfo_Entity_HeatType').val(); 
            param.HelpeCode = $('#hm_clientinfo_Entity_HelpeCode').val(); 
            param.Cardno = $('#hm_clientinfo_Entity_Cardno').val(); 
            param.Name = $('#hm_clientinfo_Entity_Name').val(); 
            param.NetInName = $('#hm_clientinfo_Entity_NetInName').val(); 
            param.RoomNumber = $('#hm_clientinfo_Entity_RoomNumber').val(); 
            param.BuildArea = $('#hm_clientinfo_Entity_BuildArea').val(); 
            param.InsideBuildArea = $('#hm_clientinfo_Entity_InsideBuildArea').val(); 
            param.Superelevation = $('#hm_clientinfo_Entity_Superelevation').val(); 
            param.BalconyState = $('#hm_clientinfo_Entity_BalconyState').val(); 
            param.BalconyArea = $('#hm_clientinfo_Entity_BalconyArea').val(); 
            param.BalconyHeatState = $('#hm_clientinfo_Entity_BalconyHeatState').val(); 
            param.BalconyHeatArea = $('#hm_clientinfo_Entity_BalconyHeatArea').val(); 
            param.InterlayerArea = $('#hm_clientinfo_Entity_InterlayerArea').val(); 
            param.InterlayerState = $('#hm_clientinfo_Entity_InterlayerState').val(); 
            param.InterlayerHeatArea = $('#hm_clientinfo_Entity_InterlayerHeatArea').val(); 
            param.InsideArea = $('#hm_clientinfo_Entity_InsideArea').val(); 
            param.HeatArea = $('#hm_clientinfo_Entity_HeatArea').val(); 
            param.UnitPriceType = $('#hm_clientinfo_Entity_UnitPriceType').val(); 
            param.NetworkDate = $('#hm_clientinfo_Entity_NetworkDate').val(); 
            param.IsNetwork = $('#hm_clientinfo_Entity_IsNetwork').val(); 
            param.BeginHeatDate = $('#hm_clientinfo_Entity_BeginHeatDate').val(); 
            param.TotalHeatSourceFactory = $('#hm_clientinfo_Entity_TotalHeatSourceFactory').val(); 
            param.HeatSource = $('#hm_clientinfo_Entity_HeatSource').val(); 
            param.Floor = $('#hm_clientinfo_Entity_Floor').val(); 
            param.LineType = $('#hm_clientinfo_Entity_LineType').val(); 
            param.HeatState = $('#hm_clientinfo_Entity_HeatState').val(); 
            param.Email = $('#hm_clientinfo_Entity_Email').val(); 
            param.Phone = $('#hm_clientinfo_Entity_Phone').val(); 
            param.JobAddress = $('#hm_clientinfo_Entity_JobAddress').val(); 
            param.HomeAddress = $('#hm_clientinfo_Entity_HomeAddress').val(); 
            param.Gender = $('#hm_clientinfo_Entity_Gender').val(); 
            param.Birthday = $('#hm_clientinfo_Entity_Birthday').val(); 
            param.ZipCode = $('#hm_clientinfo_Entity_ZipCode').val(); 
            param.IDCard = $('#hm_clientinfo_Entity_IDCard').val(); 
            param.IsDelete = $('#hm_clientinfo_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_clientinfo_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_clientinfo_Entity_UpdateDate').val(); 
            param.ChargeAnnual_ID = $('#hm_clientinfo_Entity_ChargeAnnual_ID').val(); 
            param.Village_ID = $('#hm_clientinfo_Entity_Village_ID').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_clientinfo').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_clientinfo'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_clientinfo'); 
                } 
            } 
            $('#btnSavehm_clientinfo').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_clientinfoDel() { 
    var rows = $("#grid_hm_clientinfo").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_ClientInfo/AjaxDeleteList/', parm, 
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
function grid_hm_clientinfo_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_clientinfo", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_clientinfoAdd(); } 
    }, '-', { 
        id: "btnDelhm_clientinfo", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_clientinfoDel(); } 
    }]; 
    return ihtml; 
} 

