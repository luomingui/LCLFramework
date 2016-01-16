$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_hm_heatplant_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_ID); 
    $('#ff_lab_hm_heatplant_pid').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_PID); 
    $('#ff_lab_hm_heatplant_name').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Name); 
    $('#ff_lab_hm_heatplant_nameshort').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_NameShort); 
    $('#ff_lab_hm_heatplant_address').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Address); 
    $('#ff_lab_hm_heatplant_adminname').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_AdminName); 
    $('#ff_lab_hm_heatplant_phone').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Phone); 
    $('#ff_lab_hm_heatplant_remark').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Remark); 
    $('#ff_lab_hm_heatplant_islast').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_IsLast); 
    $('#ff_lab_hm_heatplant_level').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_Level); 
    $('#ff_lab_hm_heatplant_nodepath').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_NodePath); 
    $('#ff_lab_hm_heatplant_orderby').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_OrderBy); 
    $('#ff_lab_hm_heatplant_parentid').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_ParentId); 
    $('#ff_lab_hm_heatplant_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_IsDelete); 
    $('#ff_lab_hm_heatplant_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_AddDate); 
    $('#ff_lab_hm_heatplant_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HM_HeatPlant_Model_UpdateDate); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '供热站', 
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
    HM_HeatPlant_Model_ID: '编号', 
    HM_HeatPlant_Model_PID: '供热站', 
    HM_HeatPlant_Model_Name: '名称', 
    HM_HeatPlant_Model_NameShort: '简称', 
    HM_HeatPlant_Model_Address: '地址', 
    HM_HeatPlant_Model_AdminName: '负责人', 
    HM_HeatPlant_Model_Phone: '联系电话', 
    HM_HeatPlant_Model_Remark: '备注', 
    HM_HeatPlant_Model_IsLast: '是否最后一级', 
    HM_HeatPlant_Model_Level: '树形深度', 
    HM_HeatPlant_Model_NodePath: '树形路径', 
    HM_HeatPlant_Model_OrderBy: '排序', 
    HM_HeatPlant_Model_ParentId: '上一级', 
    HM_HeatPlant_Model_IsDelete: '删除标记', 
    HM_HeatPlant_Model_AddDate: '添加时间', 
    HM_HeatPlant_Model_UpdateDate: '更新时间', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'HM_HeatPlant', 
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
    HM_HeatPlant_Model_ID: 'ID', 
    HM_HeatPlant_Model_PID: 'PID', 
    HM_HeatPlant_Model_Name: 'Name', 
    HM_HeatPlant_Model_NameShort: 'NameShort', 
    HM_HeatPlant_Model_Address: 'Address', 
    HM_HeatPlant_Model_AdminName: 'AdminName', 
    HM_HeatPlant_Model_Phone: 'Phone', 
    HM_HeatPlant_Model_Remark: 'Remark', 
    HM_HeatPlant_Model_IsLast: 'IsLast', 
    HM_HeatPlant_Model_Level: 'Level', 
    HM_HeatPlant_Model_NodePath: 'NodePath', 
    HM_HeatPlant_Model_OrderBy: 'OrderBy', 
    HM_HeatPlant_Model_ParentId: 'ParentId', 
    HM_HeatPlant_Model_IsDelete: 'IsDelete', 
    HM_HeatPlant_Model_AddDate: 'AddDate', 
    HM_HeatPlant_Model_UpdateDate: 'UpdateDate', 
}; 

