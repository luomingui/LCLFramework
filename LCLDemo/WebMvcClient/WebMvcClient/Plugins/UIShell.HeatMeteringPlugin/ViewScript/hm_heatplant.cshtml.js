/// <reference path="/Content/Code/LCL.JQuery.Base.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Core.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.JSON.js" /> 
/// <reference path="/Content/Core/LCL.JQuery.Plugins.js" /> 
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 供热站 
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
    $('#btnAddhm_heatplant').click(function () { pageFunc_hm_heatplantAdd(); }); 
    $('#btnDelhm_heatplant').click(function () { pageFunc_hm_heatplantDel(); }); 
    $('#btnSearchhm_heatplant').click(function () { pageFunc_SearchDatahm_heatplant(); }); 
} 
function InitGrid() { 
    $('#grid_hm_heatplant').datagrid({ 
        url: pageAttr.JsonServerURL + 'HM_HeatPlant/AjaxGetByPage', 
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
                { field: 'ID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_ID, width: 100,hidden:true }, 
                { field: 'PID', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_PID, width: 100 }, 
                { field: 'Name', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Name, width: 100 }, 
                { field: 'NameShort', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_NameShort, width: 100 }, 
                { field: 'Address', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Address, width: 100 }, 
                { field: 'AdminName', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_AdminName, width: 100 }, 
                { field: 'Phone', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Phone, width: 100 }, 
                { field: 'Remark', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Remark, width: 100 }, 
                { field: 'IsLast', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_IsLast, width: 100,hidden:true }, 
                { field: 'Level', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Level, width: 100,hidden:true }, 
                { field: 'NodePath', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_NodePath, width: 100,hidden:true }, 
                { field: 'OrderBy', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_OrderBy, width: 100,hidden:true }, 
                { field: 'ParentId', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_ParentId, width: 100 }, 
                { field: 'IsDelete', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_IsDelete, width: 100,hidden:true }, 
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_AddDate, width: 100,hidden:true }, 
                { field: 'UpdateDate', title: $.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_UpdateDate, width: 100,hidden:true }, 
                { 
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', 
                    formatter: function (value, rec, index) { 
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_heatplantEdit(\'' + rec.ID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' 
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_hm_heatplantDel()">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; 
                    } 
                } 
        ]], 
        toolbar: grid_hm_heatplant_toolbar(), 
        onDblClickRow: function (rowIndex, rowData) { 
            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); 
            pageFunc_hm_heatplantEdit(rowData.ID); 
        } 
    }); 
} 
function pageFunc_SearchDatahm_heatplant() { 
    $("#grid_hm_heatplant").datagrid('load', { 
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() 
    }); 
    clearSelect('grid_hm_heatplant'); 
} 
function pageFunc_hm_heatplantAdd() { 
    pageAttr.Added = true; 
    $('#ffhm_heatplant').form('clear'); 
    $('#win_hm_heatplant').dialog({ 
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        width: 500, 
        height: 280, 
        iconCls: 'icon-add', 
        modal: true, 
        buttons: [{ 
            id: 'btnSavehm_heatplant', 
            iconCls: 'icon-ok', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
            handler: function () { 
                pageFunc_hm_heatplantSave(); 
            } 
        }, { 
            id: 'btnCancelwfrout', 
            iconCls: 'icon-cancel', 
            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
            handler: function () { 
                closeDialog('win_hm_heatplant'); 
            } 
        }], 
        onLoad: function () { 
            $('#hm_heatplant_Entity_Name').focus(); 
        } 
    }); 
} 
function pageFunc_hm_heatplantEdit(ID) { 
    if (ID != undefined && ID.length > 0) { 
        pageAttr.Added = false; 
        $('#win_hm_heatplant').dialog({ 
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, 
            width: 550, 
            height: 280, 
            iconCls: 'icon-edit', 
            modal: true, 
            buttons: [{ 
                id: "btnSavehm_heatplant", 
                iconCls: 'icon-ok', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, 
                handler: function () { 
                    pageFunc_hm_heatplantSave(); 
                } 
            }, { 
                id: 'btnCancelhm_heatplant', 
                iconCls: 'icon-cancel', 
                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, 
                handler: function () { 
                    closeDialog('win_hm_heatplant'); 
                } 
            }], 
            onClose: function () { 
                closeDialog('win_hm_heatplant'); 
            } 
        }); 
        var ajaxUrl = pageAttr.JsonServerURL + 'hm_heatplant/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); 
        $.post(ajaxUrl, function (resultData) { 
            if (resultData.Success) { 
                $('#hm_heatplant_Entity_ID').val(resultData.DataObject.ID); 
                $('#hm_heatplant_Entity_PID').val(resultData.DataObject.PID); 
                $('#hm_heatplant_Entity_Name').val(resultData.DataObject.Name); 
                $('#hm_heatplant_Entity_NameShort').val(resultData.DataObject.NameShort); 
                $('#hm_heatplant_Entity_Address').val(resultData.DataObject.Address); 
                $('#hm_heatplant_Entity_AdminName').val(resultData.DataObject.AdminName); 
                $('#hm_heatplant_Entity_Phone').val(resultData.DataObject.Phone); 
                $('#hm_heatplant_Entity_Remark').val(resultData.DataObject.Remark); 
                $('#hm_heatplant_Entity_IsLast').val(resultData.DataObject.IsLast); 
                $('#hm_heatplant_Entity_Level').val(resultData.DataObject.Level); 
                $('#hm_heatplant_Entity_NodePath').val(resultData.DataObject.NodePath); 
                $('#hm_heatplant_Entity_OrderBy').val(resultData.DataObject.OrderBy); 
                $('#hm_heatplant_Entity_ParentId').val(resultData.DataObject.ParentId); 
                $('#hm_heatplant_Entity_IsDelete').val(resultData.DataObject.IsDelete); 
                $('#hm_heatplant_Entity_AddDate').val(resultData.DataObject.AddDate); 
                $('#hm_heatplant_Entity_UpdateDate').val(resultData.DataObject.UpdateDate); 
            } 
        }, 'json'); 
 
    } else { 
        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); 
    } 
} 
function pageFunc_hm_heatplantSave() { 
    var ajaxUrl = ""; 
    if (pageAttr.Added) { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_HeatPlant/AjaxAdd'; 
    } else { 
        ajaxUrl = pageAttr.JsonServerURL + 'HM_HeatPlant/AjaxEdit'; 
    } 
 
    $('#ffhm_heatplant').form('submit', { 
        url: ajaxUrl, 
        onSubmit: function (param) { 
            $('#btnSavehm_heatplant').linkbutton('disable'); 
            param.ID = $('#hm_heatplant_Entity_ID').val(); 
            param.PID = $('#hm_heatplant_Entity_PID').val(); 
            param.Name = $('#hm_heatplant_Entity_Name').val(); 
            param.NameShort = $('#hm_heatplant_Entity_NameShort').val(); 
            param.Address = $('#hm_heatplant_Entity_Address').val(); 
            param.AdminName = $('#hm_heatplant_Entity_AdminName').val(); 
            param.Phone = $('#hm_heatplant_Entity_Phone').val(); 
            param.Remark = $('#hm_heatplant_Entity_Remark').val(); 
            param.IsLast = $('#hm_heatplant_Entity_IsLast').val(); 
            param.Level = $('#hm_heatplant_Entity_Level').val(); 
            param.NodePath = $('#hm_heatplant_Entity_NodePath').val(); 
            param.OrderBy = $('#hm_heatplant_Entity_OrderBy').val(); 
            param.ParentId = $('#hm_heatplant_Entity_ParentId').val(); 
            param.IsDelete = $('#hm_heatplant_Entity_IsDelete').val(); 
            param.AddDate = $('#hm_heatplant_Entity_AddDate').val(); 
            param.UpdateDate = $('#hm_heatplant_Entity_UpdateDate').val(); 
            if ($(this).form('validate')) 
                return true; 
            else { 
                $('#btnSavehm_heatplant').linkbutton('enable'); 
                return false; 
            } 
        }, 
        success: function (data) { 
            var resultData = eval('(' + data + ')'); 
            if (resultData.Success) { 
                flashTable('grid_hm_heatplant'); 
                if (pageAttr.Added) { 
 
                } else { 
                    closeDialog('win_hm_heatplant'); 
                } 
            } 
            $('#btnSavehm_heatplant').linkbutton('enable'); 
            $.LCLMessageBox.Alert(resultData.Message); 
        } 
    }); 
} 
function pageFunc_hm_heatplantDel() { 
    var rows = $("#grid_hm_heatplant").datagrid("getChecked"); 
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
            $.post(pageAttr.JsonServerURL + 'HM_HeatPlant/AjaxDeleteList/', parm, 
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
function grid_hm_heatplant_toolbar() { 
  var ihtml = [{ 
        id: "btnAddhm_heatplant", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, 
        iconCls: 'icon-add', 
        handler: function () { pageFunc_hm_heatplantAdd(); } 
    }, '-', { 
        id: "btnDelhm_heatplant", 
        text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del, 
        iconCls: 'icon-remove', 
        handler: function () { pageFunc_hm_heatplantDel(); } 
    }]; 
    return ihtml; 
} 

