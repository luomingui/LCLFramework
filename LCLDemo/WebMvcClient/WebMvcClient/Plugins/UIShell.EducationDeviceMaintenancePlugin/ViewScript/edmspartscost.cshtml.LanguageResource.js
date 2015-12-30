$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_edmspartscost_id').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_ID); 
    $('#ff_lab_edmspartscost_costtype').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_CostType); 
    $('#ff_lab_edmspartscost_name').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Name); 
    $('#ff_lab_edmspartscost_devicebrand').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_DeviceBrand); 
    $('#ff_lab_edmspartscost_devicemodel').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_DeviceModel); 
    $('#ff_lab_edmspartscost_quantity').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Quantity); 
    $('#ff_lab_edmspartscost_unit').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Unit); 
    $('#ff_lab_edmspartscost_unitcost').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_UnitCost); 
    $('#ff_lab_edmspartscost_money').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Money); 
    $('#ff_lab_edmspartscost_warranty').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Warranty); 
    $('#ff_lab_edmspartscost_remark').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_Remark); 
    $('#ff_lab_edmspartscost_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_IsDelete); 
    $('#ff_lab_edmspartscost_adddate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_AddDate); 
    $('#ff_lab_edmspartscost_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_UpdateDate); 
    $('#ff_lab_edmspartscost_maintenancebill_id').html($.LCLPageModel.Resource.PageLanguageResource.EDMSPartsCost_Model_MaintenanceBill_ID); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '配件/服务费', 
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
    EDMSPartsCost_Model_ID: '编号', 
    EDMSPartsCost_Model_CostType: '费用类型', 
    EDMSPartsCost_Model_Name: '名称', 
    EDMSPartsCost_Model_DeviceBrand: '品牌', 
    EDMSPartsCost_Model_DeviceModel: '型号', 
    EDMSPartsCost_Model_Quantity: '数量', 
    EDMSPartsCost_Model_Unit: '单位', 
    EDMSPartsCost_Model_UnitCost: '单价', 
    EDMSPartsCost_Model_Money: '金额', 
    EDMSPartsCost_Model_Warranty: '质保期', 
    EDMSPartsCost_Model_Remark: '备注', 
    EDMSPartsCost_Model_IsDelete: '删除标记', 
    EDMSPartsCost_Model_AddDate: '添加时间', 
    EDMSPartsCost_Model_UpdateDate: '更新时间', 
    EDMSPartsCost_Model_MaintenanceBill_ID: 'MaintenanceBill_ID', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'EDMSPartsCost', 
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
    EDMSPartsCost_Model_ID: 'ID', 
    EDMSPartsCost_Model_CostType: 'CostType', 
    EDMSPartsCost_Model_Name: 'Name', 
    EDMSPartsCost_Model_DeviceBrand: 'DeviceBrand', 
    EDMSPartsCost_Model_DeviceModel: 'DeviceModel', 
    EDMSPartsCost_Model_Quantity: 'Quantity', 
    EDMSPartsCost_Model_Unit: 'Unit', 
    EDMSPartsCost_Model_UnitCost: 'UnitCost', 
    EDMSPartsCost_Model_Money: 'Money', 
    EDMSPartsCost_Model_Warranty: 'Warranty', 
    EDMSPartsCost_Model_Remark: 'Remark', 
    EDMSPartsCost_Model_IsDelete: 'IsDelete', 
    EDMSPartsCost_Model_AddDate: 'AddDate', 
    EDMSPartsCost_Model_UpdateDate: 'UpdateDate', 
    EDMSPartsCost_Model_MaintenanceBill_ID: 'MaintenanceBill_ID', 
}; 

