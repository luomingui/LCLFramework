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
    toolbar: '',
    pboId:''
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
    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.EducationDeviceMaintenancePlugin/';
    pageAttr.pboId = $.LCLCore.Request.QueryString("pboId");
} 
//初始化多语言 
function InitLanguage() { 
    $.LCLPageModel.Resource.InitLanguage(); 
} 
//初始化控件 
function InitControls() { 

} 
//初始化事件 
function InitEvent() {     
    $('#btnSearedmsmaintenancebill').click(function () { pageFunc_edmsmaintenancebillSave(); });
} 
function pageFunc_edmsmaintenancebillSave() {
    var ajaxUrl = pageAttr.JsonServerURL + 'EDMSMaintenanceBill/AjaxAdd';

    $('#ffedmsmaintenancebill').form('submit', {
        url: ajaxUrl,
        onSubmit: function (param) {
            $('#btnSaveedmsmaintenancebill').linkbutton('disable');
            param.ID = $('#edmsmaintenancebill_Entity_ID').val();
            param.MaintenanceType = $('#edmsmaintenancebill_Entity_MaintenanceType').val();
            param.MaintainPerson = $('#edmsmaintenancebill_Entity_MaintainPerson').val();
            param.MaintainPersonPhone = $('#edmsmaintenancebill_Entity_MaintainPersonPhone').val();
            param.RepairUnit = $('#edmsmaintenancebill_Entity_RepairUnit').val();
            param.FulfillDate = $('#edmsmaintenancebill_Entity_FulfillDate').val();
            param.ResponseTime = $('#edmsmaintenancebill_Entity_ResponseTime').val();
            param.FaultPhenomenon = $('#edmsmaintenancebill_Entity_FaultPhenomenon').val();
            param.FaultJudge = $('#edmsmaintenancebill_Entity_FaultJudge').val();
            param.SolvingSkills = $('#edmsmaintenancebill_Entity_SolvingSkills').val();
            param.Record = $('#edmsmaintenancebill_Entity_Record').val();
            param.VisitCost = $('#edmsmaintenancebill_Entity_VisitCost').val();
            param.MaintenanceStatus = $('#edmsmaintenancebill_Entity_MaintenanceStatus').val();
            param.IsDelete = $('#edmsmaintenancebill_Entity_IsDelete').val();
            param.AddDate = $('#edmsmaintenancebill_Entity_AddDate').val();
            param.UpdateDate = $('#edmsmaintenancebill_Entity_UpdateDate').val();
            //param.pboId = pboId;
            param.taskId = taskId;
            if ($(this).form('validate'))
                return true;
            else {
                $('#btnSaveedmsmaintenancebill').linkbutton('enable');
                return false;
            }
        },
        success: function (data) {
            var resultData = eval('(' + data + ')');
            if (resultData.Success) {
                flashTable('grid_edmsmaintenancebill');
                if (pageAttr.Added) {

                } else {
                    closeDialog('win_edmsmaintenancebill');
                }
            }
            $('#btnSaveedmsmaintenancebill').linkbutton('enable');
            $.LCLMessageBox.Alert(resultData.Message);
        }
    });
}