$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_hm_clientheatcharge_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_ID); 
    $('#ff_lab_hm_clientheatcharge_beginheat').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_BeginHeat); 
    $('#ff_lab_hm_clientheatcharge_endheat').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_EndHeat); 
    $('#ff_lab_hm_clientheatcharge_useheat').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_UseHeat); 
    $('#ff_lab_hm_clientheatcharge_moneyheat').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_MoneyHeat); 
    $('#ff_lab_hm_clientheatcharge_moneybaseheat').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_MoneyBaseHeat); 
    $('#ff_lab_hm_clientheatcharge_moneyadvance').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_MoneyAdvance); 
    $('#ff_lab_hm_clientheatcharge_moneyorrefunded').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_MoneyOrRefunded); 
    $('#ff_lab_hm_clientheatcharge_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_IsDelete); 
    $('#ff_lab_hm_clientheatcharge_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_AddDate); 
    $('#ff_lab_hm_clientheatcharge_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_UpdateDate); 
    $('#ff_lab_hm_clientheatcharge_chargeannual_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_ChargeAnnual_ID); 
    $('#ff_lab_hm_clientheatcharge_chargeuser_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_ChargeUser_ID); 
    $('#ff_lab_hm_clientheatcharge_clientinfo_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientHeatCharge_Model_ClientInfo_ID); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '热计量采暖费结算单', 
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
    HM_ClientHeatCharge_Model_ID: '编号', 
    HM_ClientHeatCharge_Model_BeginHeat: '供热开始表数', 
    HM_ClientHeatCharge_Model_EndHeat: '供热结束表数', 
    HM_ClientHeatCharge_Model_UseHeat: '用热量（KMH）', 
    HM_ClientHeatCharge_Model_MoneyHeat: '计量热费', 
    HM_ClientHeatCharge_Model_MoneyBaseHeat: '基础热费', 
    HM_ClientHeatCharge_Model_MoneyAdvance: '预收金额', 
    HM_ClientHeatCharge_Model_MoneyOrRefunded: '退（补）金额', 
    HM_ClientHeatCharge_Model_IsDelete: '删除标记', 
    HM_ClientHeatCharge_Model_AddDate: '添加时间', 
    HM_ClientHeatCharge_Model_UpdateDate: '更新时间', 
    HM_ClientHeatCharge_Model_ChargeAnnual_ID: 'ChargeAnnual_ID', 
    HM_ClientHeatCharge_Model_ChargeUser_ID: 'ChargeUser_ID', 
    HM_ClientHeatCharge_Model_ClientInfo_ID: 'ClientInfo_ID', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'HM_ClientHeatCharge', 
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
    HM_ClientHeatCharge_Model_ID: 'ID', 
    HM_ClientHeatCharge_Model_BeginHeat: 'BeginHeat', 
    HM_ClientHeatCharge_Model_EndHeat: 'EndHeat', 
    HM_ClientHeatCharge_Model_UseHeat: 'UseHeat', 
    HM_ClientHeatCharge_Model_MoneyHeat: 'MoneyHeat', 
    HM_ClientHeatCharge_Model_MoneyBaseHeat: 'MoneyBaseHeat', 
    HM_ClientHeatCharge_Model_MoneyAdvance: 'MoneyAdvance', 
    HM_ClientHeatCharge_Model_MoneyOrRefunded: 'MoneyOrRefunded', 
    HM_ClientHeatCharge_Model_IsDelete: 'IsDelete', 
    HM_ClientHeatCharge_Model_AddDate: 'AddDate', 
    HM_ClientHeatCharge_Model_UpdateDate: 'UpdateDate', 
    HM_ClientHeatCharge_Model_ChargeAnnual_ID: 'ChargeAnnual_ID', 
    HM_ClientHeatCharge_Model_ChargeUser_ID: 'ChargeUser_ID', 
    HM_ClientHeatCharge_Model_ClientInfo_ID: 'ClientInfo_ID', 
}; 

