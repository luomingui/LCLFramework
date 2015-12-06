$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);

    
    $('#ff_lab_xzqy_id').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_ID);
    $('#ff_lab_xzqy_helpercode').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_HelperCode);
    $('#ff_lab_xzqy_name').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_Name);
    $('#ff_lab_xzqy_intro').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_Intro);
    $('#ff_lab_xzqy_islast').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_IsLast);
    $('#ff_lab_xzqy_level').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_Level);
    $('#ff_lab_xzqy_nodepath').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_NodePath);
    $('#ff_lab_xzqy_orderby').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_OrderBy);
    $('#ff_lab_xzqy_parentid').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_ParentId);
    $('#ff_lab_xzqy_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_IsDelete);
    $('#ff_lab_xzqy_adddate').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_AddDate);
    $('#ff_lab_xzqy_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.Xzqy_Model_UpdateDate);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '行政区域',
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
    Xzqy_Model_ID: '编号',
    Xzqy_Model_HelperCode: '区划代码',
    Xzqy_Model_Name: '区划名称',
    Xzqy_Model_Intro: '区划介绍',
    Xzqy_Model_IsLast: '是否最后一级',
    Xzqy_Model_Level: '树形深度',
    Xzqy_Model_NodePath: '树形路径',
    Xzqy_Model_OrderBy: '排序',
    Xzqy_Model_ParentId: '上一级',
    Xzqy_Model_IsDelete: 'IsDelete',
    Xzqy_Model_AddDate: '添加时间',
    Xzqy_Model_UpdateDate: '更新时间',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'Xzqy',
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
    Xzqy_Model_ID: 'ID',
    Xzqy_Model_HelperCode: 'HelperCode',
    Xzqy_Model_Name: 'Name',
    Xzqy_Model_Intro: 'Intro',
    Xzqy_Model_IsLast: 'IsLast',
    Xzqy_Model_Level: 'Level',
    Xzqy_Model_NodePath: 'NodePath',
    Xzqy_Model_OrderBy: 'OrderBy',
    Xzqy_Model_ParentId: 'ParentId',
    Xzqy_Model_IsDelete: 'IsDelete',
    Xzqy_Model_AddDate: 'AddDate',
    Xzqy_Model_UpdateDate: 'UpdateDate',

};

