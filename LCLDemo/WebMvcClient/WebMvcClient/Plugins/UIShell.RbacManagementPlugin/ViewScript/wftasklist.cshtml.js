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
    $('#btnSearchwftasklist').click(function () { pageFunc_SearchDatawftasklist(); });
}
function InitGrid() {
    $('#grid_wftasklist').datagrid({
        url: pageAttr.JsonServerURL + 'WFTaskList/AjaxGetByPage',
        iconCls: 'icon-edit',
        pagination: true,
        rownumbers: true,
        fitCloumns: true,
        columns: [[
                { field: 'TaskID', title: 'TaskID', width: 150, hidden: true },
                { field: 'TaskName', title: $.LCLPageModel.Resource.PageLanguageResource.WFTaskList_Model_TaskName, width: 150 },
                { field: 'BillName', title: $.LCLPageModel.Resource.PageLanguageResource.WFTaskList_Model_BillName, width: 200 },
                { field: 'TaskState', title: $.LCLPageModel.Resource.PageLanguageResource.WFTaskList_Model_TaskState, width: 100 },
                { field: 'RoutName', title: $.LCLPageModel.Resource.PageLanguageResource.WFTaskList_Model_RoutName, width: 100 },
                { field: 'AddDate', title: $.LCLPageModel.Resource.PageLanguageResource.WFTaskList_Model_AddDate, width: 150 },
                {
                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center',
                    formatter: function (value, rec, index) {
                        return '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_FinishTask(\'' + rec.TaskID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_ExecCommand + '</a>&nbsp;'
                             + '&nbsp;<a href="javascript:void(0)" onclick="pageFunc_TaskShow(\'' + rec.TaskID + '\')">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_TaskShow + '</a>&nbsp;';
                    }
                }
        ]]
    });
}
function pageFunc_SearchDatawftasklist() {
    $("#grid_wftasklist").datagrid('load', {
        Name: $('#ui_wfrout_search').find('[name=Keyword]').val()
    });
    clearSelect('grid_wftasklist');
}
function pageFunc_FinishTask(TaskID)
{
    var data = TaskID;
    var ajaxURL = pageAttr.JsonServerURL + 'WFTaskList/frmTask?taskId=' + TaskID;
    $.LCLCore.Ajax.CallAjaxPostJsonData(ajaxURL, data, function (data) {
        $("<div/>").dialog({
            id: "ui_ExecCommand_dialog",
            href: data,
            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_ExecCommand,
            height: 350,
            width: 460,
            modal: true
        });
        $.LCLCore.PageMask.ClosePageMask();
    }, function (XMLHttpRequest, textStatus, errorThrown) {
        $.LCLCore.PageMask.ClosePageMask();
    })
}
function pageFunc_TaskShow(TaskID) {
    $("<div/>").dialog({
        id: "ui_ExecCommand_dialog",
        href: pageAttr.JsonServerURL + 'WFTaskList/frmTask?taskId=' + TaskID,
        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_ExecCommand,
        height: 350,
        width: 460,
        modal: true,
        buttons: [{
            id: "ui_ExecCommand_btn",
            text: '提 交',
            handler: function () {

            }
        }]
    });
}