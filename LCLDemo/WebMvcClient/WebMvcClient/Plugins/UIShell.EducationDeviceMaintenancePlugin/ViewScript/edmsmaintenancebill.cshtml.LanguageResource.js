$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_edmsmaintenancebill_id').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_ID); 
    $('#ff_lab_edmsmaintenancebill_maintenancetype').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_MaintenanceType); 
    $('#ff_lab_edmsmaintenancebill_maintainperson').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_MaintainPerson); 
    $('#ff_lab_edmsmaintenancebill_maintainpersonphone').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_MaintainPersonPhone); 
    $('#ff_lab_edmsmaintenancebill_repairunit').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_RepairUnit); 
    $('#ff_lab_edmsmaintenancebill_fulfilldate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_FulfillDate); 
    $('#ff_lab_edmsmaintenancebill_responsetime').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_ResponseTime); 
    $('#ff_lab_edmsmaintenancebill_faultphenomenon').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_FaultPhenomenon); 
    $('#ff_lab_edmsmaintenancebill_faultjudge').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_FaultJudge); 
    $('#ff_lab_edmsmaintenancebill_solvingskills').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_SolvingSkills); 
    $('#ff_lab_edmsmaintenancebill_record').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_Record); 
    $('#ff_lab_edmsmaintenancebill_visitcost').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_VisitCost); 
    $('#ff_lab_edmsmaintenancebill_maintenancestatus').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_MaintenanceStatus); 
    $('#ff_lab_edmsmaintenancebill_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_IsDelete); 
    $('#ff_lab_edmsmaintenancebill_adddate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_AddDate); 
    $('#ff_lab_edmsmaintenancebill_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSMaintenanceBill_Model_UpdateDate); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '维修单', 
    Page_Command_Grid_Operate: '操作', 
    Page_Command_Add: '添加', 
    Page_Command_Edit: '修改', 
    Page_Command_Del: '删除', 
    Page_Command_Save: '保存', 
    Page_Command_Cancel: '取消', 
    Page_Command_Search: '查询', 
    Page_label_Search_title: '信息查询', 
    Page_label_Search_key: '搜', 
    LCLMessageBox_AlertTitle: '温馨提示', 
    LCLMessageBox_Message1: '请选择一行', 
    LCLMessageBox_Message2: '请先勾选要删除的数据', 
    LCLMessageBox_Message3: '是否删除选中数据', 
    EDMSMaintenanceBill_Model_ID: '编号', 
    EDMSMaintenanceBill_Model_MaintenanceType: 'MaintenanceType', 
    EDMSMaintenanceBill_Model_MaintainPerson: '维修人', 
    EDMSMaintenanceBill_Model_MaintainPersonPhone: '维修人电话', 
    EDMSMaintenanceBill_Model_RepairUnit: '维修单位', 
    EDMSMaintenanceBill_Model_FulfillDate: '完成时间', 
    EDMSMaintenanceBill_Model_ResponseTime: '响应时间', 
    EDMSMaintenanceBill_Model_FaultPhenomenon: '故障现象', 
    EDMSMaintenanceBill_Model_FaultJudge: '故障判断', 
    EDMSMaintenanceBill_Model_SolvingSkills: '解决技巧', 
    EDMSMaintenanceBill_Model_Record: '维修记录', 
    EDMSMaintenanceBill_Model_VisitCost: '上门费', 
    EDMSMaintenanceBill_Model_MaintenanceStatus: '维修状态', 
    EDMSMaintenanceBill_Model_IsDelete: '删除标记', 
    EDMSMaintenanceBill_Model_AddDate: '添加时间', 
    EDMSMaintenanceBill_Model_UpdateDate: '更新时间', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'EDMSMaintenanceBill', 
    Page_Command_Grid_Operate: 'Operate', 
    Page_Command_Add: 'Add', 
    Page_Command_Edit: 'Edit', 
    Page_Command_Del: 'Delete', 
    Page_Command_Save: 'Save', 
    Page_Command_Cancel: 'Cancel', 
    Page_Command_Search: 'Search', 
    Page_label_Search_title: 'info query', 
    Page_label_Search_key: 'key', 
    LCLMessageBox_AlertTitle: 'AlertTitle', 
    LCLMessageBox_Message1: 'Please select row', 
    LCLMessageBox_Message2: 'Please delete data', 
    LCLMessageBox_Message3: 'is delete data', 
    EDMSMaintenanceBill_Model_ID: 'ID', 
    EDMSMaintenanceBill_Model_MaintenanceType: 'MaintenanceType', 
    EDMSMaintenanceBill_Model_MaintainPerson: 'MaintainPerson', 
    EDMSMaintenanceBill_Model_MaintainPersonPhone: 'MaintainPersonPhone', 
    EDMSMaintenanceBill_Model_RepairUnit: 'RepairUnit', 
    EDMSMaintenanceBill_Model_FulfillDate: 'FulfillDate', 
    EDMSMaintenanceBill_Model_ResponseTime: 'ResponseTime', 
    EDMSMaintenanceBill_Model_FaultPhenomenon: 'FaultPhenomenon', 
    EDMSMaintenanceBill_Model_FaultJudge: 'FaultJudge', 
    EDMSMaintenanceBill_Model_SolvingSkills: 'SolvingSkills', 
    EDMSMaintenanceBill_Model_Record: 'Record', 
    EDMSMaintenanceBill_Model_VisitCost: 'VisitCost', 
    EDMSMaintenanceBill_Model_MaintenanceStatus: 'MaintenanceStatus', 
    EDMSMaintenanceBill_Model_IsDelete: 'IsDelete', 
    EDMSMaintenanceBill_Model_AddDate: 'AddDate', 
    EDMSMaintenanceBill_Model_UpdateDate: 'UpdateDate', 
}; 

