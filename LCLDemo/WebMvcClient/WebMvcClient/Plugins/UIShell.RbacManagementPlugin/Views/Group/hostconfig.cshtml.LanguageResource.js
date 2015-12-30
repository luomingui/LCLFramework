$.LCLPageModel.Resource.InitLanguageResource = function () { 
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; 
$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); 
$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); 
$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); 
$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); 
$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); 
$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); 
    $('#ff_lab_hostconfig_id').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_ID); 
    $('#ff_lab_hostconfig_name').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Name); 
    $('#ff_lab_hostconfig_ip').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_IP); 
    $('#ff_lab_hostconfig_addess').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Addess); 
    $('#ff_lab_hostconfig_ftpuser').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_FtpUser); 
    $('#ff_lab_hostconfig_ftppassword').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_FtpPassword); 
    $('#ff_lab_hostconfig_netdisk').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Netdisk); 
    $('#ff_lab_hostconfig_flag').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_Flag); 
    $('#ff_lab_hostconfig_shareddirname').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirName); 
    $('#ff_lab_hostconfig_shareddiruser').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirUser); 
    $('#ff_lab_hostconfig_shareddirpassword').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_SharedDirPassword); 
    $('#ff_lab_hostconfig_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_IsDelete); 
    $('#ff_lab_hostconfig_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_AddDate); 
    $('#ff_lab_hostconfig_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HostConfig_Model_UpdateDate); 

} 
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { 
    Page_title: '配置文件服务器信息', 
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
    HostConfig_Model_ID: '编号', 
    HostConfig_Model_Name: '主机名称', 
    HostConfig_Model_IP: '主机IP', 
    HostConfig_Model_Addess: '服务器地点', 
    HostConfig_Model_FtpUser: 'FTP用户', 
    HostConfig_Model_FtpPassword: 'FTP密码', 
    HostConfig_Model_Netdisk: '该文件服务器在WEBSERVER上的网络映射盘', 
    HostConfig_Model_Flag: '有效标志', 
    HostConfig_Model_SharedDirName: '共享目录名', 
    HostConfig_Model_SharedDirUser: '共享目录访问帐户', 
    HostConfig_Model_SharedDirPassword: '共享目录访问密码', 
    HostConfig_Model_IsDelete: '删除标记', 
    HostConfig_Model_AddDate: '添加时间', 
    HostConfig_Model_UpdateDate: '更新时间', 
}; 
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { 
    Page_title: 'HostConfig', 
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
    HostConfig_Model_ID: 'ID', 
    HostConfig_Model_Name: 'Name', 
    HostConfig_Model_IP: 'IP', 
    HostConfig_Model_Addess: 'Addess', 
    HostConfig_Model_FtpUser: 'FtpUser', 
    HostConfig_Model_FtpPassword: 'FtpPassword', 
    HostConfig_Model_Netdisk: 'Netdisk', 
    HostConfig_Model_Flag: 'Flag', 
    HostConfig_Model_SharedDirName: 'SharedDirName', 
    HostConfig_Model_SharedDirUser: 'SharedDirUser', 
    HostConfig_Model_SharedDirPassword: 'SharedDirPassword', 
    HostConfig_Model_IsDelete: 'IsDelete', 
    HostConfig_Model_AddDate: 'AddDate', 
    HostConfig_Model_UpdateDate: 'UpdateDate', 
}; 

