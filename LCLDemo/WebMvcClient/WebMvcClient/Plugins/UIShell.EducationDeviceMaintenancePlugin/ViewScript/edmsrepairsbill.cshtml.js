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
    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.EducationDeviceMaintenancePlugin/';
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
    $('#btnSaveedmsrepairsbill').click(function () { pageFunc_edmsrepairsbillSave(); });
}
function pageFunc_edmsrepairsbillSave() {
    //debugger;
    //页面遮盖层
    $(document.body).LoadingMask(pageAttr.LanguageId, $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]['LCLMessageBox_Message4']);
    var ajaxUrl = "";
    ajaxUrl = pageAttr.JsonServerURL + 'EDMSRepairsBill/AjaxAdd';
    $('#ffedmsrepairsbill').form('submit', {
        url: ajaxUrl,
        onSubmit: function (param) {
            $('#btnSaveedmsrepairsbill').linkbutton('disable');
            param.ID = $('#edmsrepairsbill_Entity_ID').val();
            param.RepairsPerson = $('#edmsrepairsbill_Entity_RepairsPerson').val();
            param.RepairsPersonPhone = $('#edmsrepairsbill_Entity_RepairsPersonPhone').val();
            param.DeviceName = $('#edmsrepairsbill_Entity_DeviceName').val();
            param.DeviceSite_ID = $('#edmsrepairsbill_Entity_DeviceSite_ID').val();
            param.DeviceSite_Name = $('#edmsrepairsbill_Entity_DeviceSite_Name').val();
            param.DeviceBrand = $('#edmsrepairsbill_Entity_DeviceBrand').val();
            param.DeviceModel = $('#edmsrepairsbill_Entity_DeviceModel').val();
            param.DeviceDescribe = $('#edmsrepairsbill_Entity_DeviceDescribe').val();
            param.IsRepairsSubmit = $('#edmsrepairsbill_Entity_IsRepairsSubmit').val();
            param.IsDelete = $('#edmsrepairsbill_Entity_IsDelete').val();
            param.AddDate = $('#edmsrepairsbill_Entity_AddDate').val();
            param.UpdateDate = $('#edmsrepairsbill_Entity_UpdateDate').val();

            if ($(this).form('validate'))
                return true;
            else {
                $('#btnSaveedmsrepairsbill').linkbutton('enable');
                return false;
            }
        },
        success: function (data) {
            var resultData = eval('(' + data + ')');
            $('#btnSaveedmsrepairsbill').linkbutton('enable');
            $.LCLMessageBox.Alert(resultData.Message);
            $(document.body).UnLoadingMask();
        }
    });
}
