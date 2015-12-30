$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);
    $('#ff_lab_user_id').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_ID);
    $('#ff_lab_user_name').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Name);
    $('#ff_lab_user_password').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Password);
    $('#ff_lab_user_islockedout').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_IsLockedOut);
    $('#ff_lab_user_userphoto').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_UserPhoto);
    $('#ff_lab_user_sex').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Sex);
    $('#ff_lab_user_birthday').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Birthday);
    $('#ff_lab_user_nationalid').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_NationalID);
    $('#ff_lab_user_politicalid').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_PoliticalID);
    $('#ff_lab_user_idcard').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_IdCard);
    $('#ff_lab_user_telephone').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Telephone);
    $('#ff_lab_user_userqq').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_UserQQ);
    $('#ff_lab_user_email').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Email);
    $('#ff_lab_user_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_IsDelete);
    $('#ff_lab_user_adddate').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_AddDate);
    $('#ff_lab_user_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_UpdateDate);
    $('#ff_lab_user_department_id').html($.LCLPageModel.Resource.PageLanguageResource.User_Model_Department_ID);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '用户',
    Page_Command_Grid_Operate: '操作',
    Page_Command_Add: '添加',
    Page_Command_Edit: '修改',
    Page_Command_Del: '删除',
    Page_Command_Save: '保存',
    Page_Command_Cancel: '取消',
    Page_Command_Search: '查询',
    Page_Command_Locked: '锁定',
    Page_Command_InitPwd: '重置密码',
    Page_label_Search_title: '信息查询',
    Page_label_Search_key: '搜',
    LCLMessageBox_AlertTitle: '温馨提示',
    LCLMessageBox_Message1: '请选择一行',
    LCLMessageBox_Message2: '请先勾选要删除的数据',
    LCLMessageBox_Message3: '是否删除选中数据',
    User_Model_ID: '编号',
    User_Model_Name: '登录名称',
    User_Model_Password: '登录密码',
    User_Model_IsLockedOut: '是否锁定',
    User_Model_UserPhoto: '用户照片',
    User_Model_Sex: '性别',
    User_Model_Birthday: '出生年月',
    User_Model_NationalID: '民族',
    User_Model_PoliticalID: '政治面貌',
    User_Model_IdCard: '身份证号',
    User_Model_Telephone: '电话',
    User_Model_UserQQ: '用户QQ',
    User_Model_Email: '电子邮箱',
    User_Model_IsDelete: '是否删除',
    User_Model_AddDate: '添加时间',
    User_Model_UpdateDate: '更新时间',
    User_Model_Department_ID: '所属部门',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'User',
    Page_Command_Grid_Operate: 'Operate',
    Page_Command_Add: 'Add',
    Page_Command_Edit: 'Edit',
    Page_Command_Del: 'Delete',
    Page_Command_Save: 'Save',
    Page_Command_Cancel: 'Cancel',
    Page_Command_Search: 'Search',
    Page_Command_Locked: 'LockedUser',
    Page_Command_InitPwd: 'InitPassword',
    Page_label_Search_title: 'info query',
    Page_label_Search_key: 'key',
    LCLMessageBox_AlertTitle: 'AlertTitle',
    LCLMessageBox_Message1: 'Please select row',
    LCLMessageBox_Message2: 'Please delete data',
    LCLMessageBox_Message3: 'is delete data',
    User_Model_ID: 'ID',
    User_Model_Name: 'Name',
    User_Model_Password: 'Password',
    User_Model_IsLockedOut: 'IsLockedOut',
    User_Model_UserPhoto: 'UserPhoto',
    User_Model_Sex: 'Sex',
    User_Model_Birthday: 'Birthday',
    User_Model_NationalID: 'NationalID',
    User_Model_PoliticalID: 'PoliticalID',
    User_Model_IdCard: 'IdCard',
    User_Model_Telephone: 'Telephone',
    User_Model_UserQQ: 'UserQQ',
    User_Model_Email: 'Email',
    User_Model_IsDelete: 'IsDelete',
    User_Model_AddDate: 'AddDate',
    User_Model_UpdateDate: 'UpdateDate',
    User_Model_Department_ID: 'Department_ID',

};

