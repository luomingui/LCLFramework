$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);
    $('#ff_lab_wfactor_id').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_ID);
    $('#ff_lab_wfactor_sortno').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_SortNo);
    $('#ff_lab_wfactor_name').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_Name);
    $('#ff_lab_wfactor_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_IsDelete);
    $('#ff_lab_wfactor_adddate').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_AddDate);
    $('#ff_lab_wfactor_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_UpdateDate);
    $('#ff_lab_wfactor_rout_id').html($.LCLPageModel.Resource.PageLanguageResource.WFActor_Model_Rout_ID);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '步骤',
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
    LCLMessageBox_Message4: '是否添加选中数据',
    WFActor_Model_ID: '编号',
    WFActor_Model_SortNo: '步骤序号',
    WFActor_Model_Name: '步骤描述',
    WFActor_Model_IsDelete: 'IsDelete',
    WFActor_Model_AddDate: '添加时间',
    WFActor_Model_UpdateDate: '更新时间',
    WFActor_Model_Rout_ID: 'Rout_ID',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'WFActor',
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
    LCLMessageBox_Message4: 'is add data',
    WFActor_Model_ID: 'ID',
    WFActor_Model_SortNo: 'SortNo',
    WFActor_Model_Name: 'Name',
    WFActor_Model_IsDelete: 'IsDelete',
    WFActor_Model_AddDate: 'AddDate',
    WFActor_Model_UpdateDate: 'UpdateDate',
    WFActor_Model_Rout_ID: 'Rout_ID',

};

