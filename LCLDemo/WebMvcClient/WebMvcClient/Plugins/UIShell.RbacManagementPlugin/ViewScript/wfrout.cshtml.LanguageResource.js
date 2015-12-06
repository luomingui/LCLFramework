/// <reference path="/Content/Core/LCL.PageModel.js" />

$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#ff_lab_wfrout_id').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_ID);
    $('#ff_lab_wfrout_name').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Name);
    $('#ff_lab_wfrout_deptid').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_DeptId);
    $('#ff_lab_wfrout_version').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_Version);
    $('#ff_lab_wfrout_state').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_State);
    $('#ff_lab_wfrout_isenable').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_IsEnable);
    $('#ff_lab_wfrout_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_IsDelete);
    $('#ff_lab_wfrout_adddate').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_AddDate);
    $('#ff_lab_wfrout_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.WFRout_Model_UpdateDate);

    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '流程',
    Page_Command_Grid_Operate: "操作",
    Page_Command_Add: "添加",
    Page_Command_Edit: "修改",
    Page_Command_Del: "删除",
    Page_Command_Save: "保存",
    Page_Command_Cancel: "取消",
    Page_Command_Search: "查询",
    Page_label_Search_title: "信息查询",
    Page_label_Search_key: "搜",
    LCLMessageBox_AlertTitle: '温馨提示',
    LCLMessageBox_Message1: '请选择一行',
    LCLMessageBox_Message2: '请先勾选要删除的数据',
    LCLMessageBox_Message3: '是否删除选中数据',
    WFRout_Model_ID: '编号',
    WFRout_Model_Name: '流程描述',
    WFRout_Model_DeptId: '部门ID',
    WFRout_Model_Version: '版本号',
    WFRout_Model_State: '状态',
    WFRout_Model_IsEnable: '是否启用',
    WFRout_Model_IsDelete: '是否删除',
    WFRout_Model_AddDate: '添加时间',
    WFRout_Model_UpdateDate: '更新时间',
    WFRout_Model_Actor: '步骤',
};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'WFRout',
    Page_Command_Grid_Operate: "Operate",
    Page_Command_Add: "Add",
    Page_Command_Edit: "Edit",
    Page_Command_Del: "Delete",
    Page_Command_Save: "Save",
    Page_Command_Cancel: "Cancel",
    Page_Command_Search: "Search",
    Page_label_Search_title: "info query",
    Page_label_Search_key: "key",
    LCLMessageBox_AlertTitle: 'AlertTitle',
    LCLMessageBox_Message1: 'Please select row',
    LCLMessageBox_Message2: 'Please delete data',
    LCLMessageBox_Message3: 'is delete data',
    WFRout_Model_ID: 'ID',
    WFRout_Model_Name: 'Name',
    WFRout_Model_DeptId: 'DeptId',
    WFRout_Model_Version: 'Version',
    WFRout_Model_State: 'State',
    WFRout_Model_IsEnable: 'IsEnable',
    WFRout_Model_IsDelete: 'IsDelete',
    WFRout_Model_AddDate: 'AddDate',
    WFRout_Model_UpdateDate: 'UpdateDate',
    WFRout_Model_Actor: 'Actor',

};

