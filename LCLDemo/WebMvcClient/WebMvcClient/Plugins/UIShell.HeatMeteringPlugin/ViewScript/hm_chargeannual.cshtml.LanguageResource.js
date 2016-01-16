$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);
    $('#ff_lab_hm_chargeannual_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_ID);
    $('#ff_lab_hm_chargeannual_name').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Name);
    $('#ff_lab_hm_chargeannual_isopen').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_IsOpen);
    $('#ff_lab_hm_chargeannual_begindate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_BeginDate);
    $('#ff_lab_hm_chargeannual_enddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_EndDate);
    $('#ff_lab_hm_chargeannual_gongreday').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_GongreDay);
    $('#ff_lab_hm_chargeannual_dnabegindate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_DnaBeginDate);
    $('#ff_lab_hm_chargeannual_breakmoney').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_BreakMoney);
    $('#ff_lab_hm_chargeannual_stopheatratio').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_StopHeatRatio);
    $('#ff_lab_hm_chargeannual_fixedportion').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Fixedportion);
    $('#ff_lab_hm_chargeannual_gongjian').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Gongjian);
    $('#ff_lab_hm_chargeannual_resident').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Resident);
    $('#ff_lab_hm_chargeannual_dishang').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Dishang);
    $('#ff_lab_hm_chargeannual_gongjian1').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Gongjian1);
    $('#ff_lab_hm_chargeannual_resident1').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Resident1);
    $('#ff_lab_hm_chargeannual_dishang1').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_Dishang1);
    $('#ff_lab_hm_chargeannual_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_IsDelete);
    $('#ff_lab_hm_chargeannual_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_AddDate);
    $('#ff_lab_hm_chargeannual_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ChargeAnnual_Model_UpdateDate);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '年度供热单价',
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
    HM_ChargeAnnual_Model_ID: '编号',
    HM_ChargeAnnual_Model_Name: '标识名称',
    HM_ChargeAnnual_Model_IsOpen: '开启',
    HM_ChargeAnnual_Model_BeginDate: '开始时间',
    HM_ChargeAnnual_Model_EndDate: '结束时间',
    HM_ChargeAnnual_Model_GongreDay: '供热天数',
    HM_ChargeAnnual_Model_DnaBeginDate: '缔纳开始日期',
    HM_ChargeAnnual_Model_BreakMoney: '违约金比例',
    HM_ChargeAnnual_Model_StopHeatRatio: '停热基础费比例',
    HM_ChargeAnnual_Model_Fixedportion: '固定热费比例',
    HM_ChargeAnnual_Model_Gongjian: '公建',
    HM_ChargeAnnual_Model_Resident: '居民',
    HM_ChargeAnnual_Model_Dishang: '底商',
    HM_ChargeAnnual_Model_Gongjian1: '公建1',
    HM_ChargeAnnual_Model_Resident1: '居民1',
    HM_ChargeAnnual_Model_Dishang1: '底商1',
    HM_ChargeAnnual_Model_IsDelete: '删除标记',
    HM_ChargeAnnual_Model_AddDate: '添加时间',
    HM_ChargeAnnual_Model_UpdateDate: '更新时间',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'HM_ChargeAnnual',
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
    HM_ChargeAnnual_Model_ID: 'ID',
    HM_ChargeAnnual_Model_Name: 'Name',
    HM_ChargeAnnual_Model_IsOpen: 'IsOpen',
    HM_ChargeAnnual_Model_BeginDate: 'BeginDate',
    HM_ChargeAnnual_Model_EndDate: 'EndDate',
    HM_ChargeAnnual_Model_GongreDay: 'GongreDay',
    HM_ChargeAnnual_Model_DnaBeginDate: 'DnaBeginDate',
    HM_ChargeAnnual_Model_BreakMoney: 'BreakMoney',
    HM_ChargeAnnual_Model_StopHeatRatio: 'StopHeatRatio',
    HM_ChargeAnnual_Model_Fixedportion: 'Fixedportion',
    HM_ChargeAnnual_Model_Gongjian: 'Gongjian',
    HM_ChargeAnnual_Model_Resident: 'Resident',
    HM_ChargeAnnual_Model_Dishang: 'Dishang',
    HM_ChargeAnnual_Model_Gongjian1: 'Gongjian1',
    HM_ChargeAnnual_Model_Resident1: 'Resident1',
    HM_ChargeAnnual_Model_Dishang1: 'Dishang1',
    HM_ChargeAnnual_Model_IsDelete: 'IsDelete',
    HM_ChargeAnnual_Model_AddDate: 'AddDate',
    HM_ChargeAnnual_Model_UpdateDate: 'UpdateDate',

};

