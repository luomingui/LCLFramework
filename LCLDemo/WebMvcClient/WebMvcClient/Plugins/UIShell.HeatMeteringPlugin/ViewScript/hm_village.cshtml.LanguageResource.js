$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_hm_village_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_ID); 
    $('#ff_lab_hm_village_name').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Name); 
    $('#ff_lab_hm_village_pinyi').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Pinyi); 
    $('#ff_lab_hm_village_type').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Type); 
    $('#ff_lab_hm_village_enname').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_EnName); 
    $('#ff_lab_hm_village_alias').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Alias); 
    $('#ff_lab_hm_village_population').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Population); 
    $('#ff_lab_hm_village_totalarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_TotalArea); 
    $('#ff_lab_hm_village_office').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Office); 
    $('#ff_lab_hm_village_summary').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Summary); 
    $('#ff_lab_hm_village_address').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Address); 
    $('#ff_lab_hm_village_islast').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_IsLast); 
    $('#ff_lab_hm_village_level').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_Level); 
    $('#ff_lab_hm_village_nodepath').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_NodePath); 
    $('#ff_lab_hm_village_orderby').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_OrderBy); 
    $('#ff_lab_hm_village_parentid').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_ParentId); 
    $('#ff_lab_hm_village_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_IsDelete); 
    $('#ff_lab_hm_village_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_AddDate); 
    $('#ff_lab_hm_village_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HM_Village_Model_UpdateDate); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '小区', 
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
    HM_Village_Model_ID: '编号', 
    HM_Village_Model_Name: '小区名称', 
    HM_Village_Model_Pinyi: '拼音简称', 
    HM_Village_Model_Type: '小区类型', 
    HM_Village_Model_EnName: '外文名称', 
    HM_Village_Model_Alias: '别名', 
    HM_Village_Model_Population: '人口', 
    HM_Village_Model_TotalArea: '面积', 
    HM_Village_Model_Office: '行政区域', 
    HM_Village_Model_Summary: '概况', 
    HM_Village_Model_Address: '小区地址', 
    HM_Village_Model_IsLast: '是否最后一级', 
    HM_Village_Model_Level: '树形深度', 
    HM_Village_Model_NodePath: '树形路径', 
    HM_Village_Model_OrderBy: '排序', 
    HM_Village_Model_ParentId: '上一级', 
    HM_Village_Model_IsDelete: '删除标记', 
    HM_Village_Model_AddDate: '添加时间', 
    HM_Village_Model_UpdateDate: '更新时间', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'HM_Village', 
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
    HM_Village_Model_ID: 'ID', 
    HM_Village_Model_Name: 'Name', 
    HM_Village_Model_Pinyi: 'Pinyi', 
    HM_Village_Model_Type: 'Type', 
    HM_Village_Model_EnName: 'EnName', 
    HM_Village_Model_Alias: 'Alias', 
    HM_Village_Model_Population: 'Population', 
    HM_Village_Model_TotalArea: 'TotalArea', 
    HM_Village_Model_Office: 'Office', 
    HM_Village_Model_Summary: 'Summary', 
    HM_Village_Model_Address: 'Address', 
    HM_Village_Model_IsLast: 'IsLast', 
    HM_Village_Model_Level: 'Level', 
    HM_Village_Model_NodePath: 'NodePath', 
    HM_Village_Model_OrderBy: 'OrderBy', 
    HM_Village_Model_ParentId: 'ParentId', 
    HM_Village_Model_IsDelete: 'IsDelete', 
    HM_Village_Model_AddDate: 'AddDate', 
    HM_Village_Model_UpdateDate: 'UpdateDate', 
}; 

