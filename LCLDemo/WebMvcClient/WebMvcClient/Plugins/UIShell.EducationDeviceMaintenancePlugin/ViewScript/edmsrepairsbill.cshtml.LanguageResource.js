$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);
    $('#ff_lab_edmsrepairsbill_id').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_ID);
    $('#ff_lab_edmsrepairsbill_repairsperson').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_RepairsPerson);
    $('#ff_lab_edmsrepairsbill_repairspersonphone').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_RepairsPersonPhone);
    $('#ff_lab_edmsrepairsbill_devicename').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceName);
    $('#ff_lab_edmsrepairsbill_devicesite_id').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceSite_ID);
    $('#ff_lab_edmsrepairsbill_devicesite_name').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceSite_Name);
    $('#ff_lab_edmsrepairsbill_devicebrand').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceBrand);
    $('#ff_lab_edmsrepairsbill_devicemodel').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceModel);
    $('#ff_lab_edmsrepairsbill_devicedescribe').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_DeviceDescribe);
    $('#ff_lab_edmsrepairsbill_isrepairssubmit').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_IsRepairsSubmit);
    $('#ff_lab_edmsrepairsbill_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_IsDelete);
    $('#ff_lab_edmsrepairsbill_adddate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_AddDate);
    $('#ff_lab_edmsrepairsbill_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSRepairsBill_Model_UpdateDate);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '',
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
    LCLMessageBox_Message4: '正在努力加载中...',
    EDMSRepairsBill_Model_ID: '编号',
    EDMSRepairsBill_Model_RepairsPerson: '报修人',
    EDMSRepairsBill_Model_RepairsPersonPhone: '报修人电话',
    EDMSRepairsBill_Model_DeviceName: '设备名称',
    EDMSRepairsBill_Model_DeviceSite_ID: '设备地点',
    EDMSRepairsBill_Model_DeviceSite_Name: '设备地点',
    EDMSRepairsBill_Model_DeviceBrand: '品牌',
    EDMSRepairsBill_Model_DeviceModel: '型号',
    EDMSRepairsBill_Model_DeviceDescribe: '故障描述',
    EDMSRepairsBill_Model_IsRepairsSubmit: '是否提交报修',
    EDMSRepairsBill_Model_IsDelete: '删除标记',
    EDMSRepairsBill_Model_AddDate: '添加时间',
    EDMSRepairsBill_Model_UpdateDate: '更新时间',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'EDMSRepairsBill',
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
    LCLMessageBox_Message4: 'Are trying to load...',
    EDMSRepairsBill_Model_ID: 'ID',
    EDMSRepairsBill_Model_RepairsPerson: 'RepairsPerson',
    EDMSRepairsBill_Model_RepairsPersonPhone: 'RepairsPersonPhone',
    EDMSRepairsBill_Model_DeviceName: 'DeviceName',
    EDMSRepairsBill_Model_DeviceSite_ID: 'DeviceSite_ID',
    EDMSRepairsBill_Model_DeviceSite_Name: 'DeviceSite_Name',
    EDMSRepairsBill_Model_DeviceBrand: 'DeviceBrand',
    EDMSRepairsBill_Model_DeviceModel: 'DeviceModel',
    EDMSRepairsBill_Model_DeviceDescribe: 'DeviceDescribe',
    EDMSRepairsBill_Model_IsRepairsSubmit: 'IsRepairsSubmit',
    EDMSRepairsBill_Model_IsDelete: 'IsDelete',
    EDMSRepairsBill_Model_AddDate: 'AddDate',
    EDMSRepairsBill_Model_UpdateDate: 'UpdateDate',

};

