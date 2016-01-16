$.LCLPageModel.Resource.InitLanguageResource = function () {
    // 资源初始化 
    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId];
    $('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save);
    $('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel);
    $('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search);
    $('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title);
    $('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key);
    $('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title);
    $('#ff_lab_hm_clientinfo_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_ID);
    $('#ff_lab_hm_clientinfo_clienttype').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_ClientType);
    $('#ff_lab_hm_clientinfo_heattype').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatType);
    $('#ff_lab_hm_clientinfo_helpecode').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HelpeCode);
    $('#ff_lab_hm_clientinfo_cardno').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Cardno);
    $('#ff_lab_hm_clientinfo_name').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Name);
    $('#ff_lab_hm_clientinfo_netinname').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_NetInName);
    $('#ff_lab_hm_clientinfo_roomnumber').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_RoomNumber);
    $('#ff_lab_hm_clientinfo_buildarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BuildArea);
    $('#ff_lab_hm_clientinfo_insidebuildarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_InsideBuildArea);
    $('#ff_lab_hm_clientinfo_superelevation').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Superelevation);
    $('#ff_lab_hm_clientinfo_balconystate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BalconyState);
    $('#ff_lab_hm_clientinfo_balconyarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BalconyArea);
    $('#ff_lab_hm_clientinfo_balconyheatstate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BalconyHeatState);
    $('#ff_lab_hm_clientinfo_balconyheatarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BalconyHeatArea);
    $('#ff_lab_hm_clientinfo_interlayerarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_InterlayerArea);
    $('#ff_lab_hm_clientinfo_interlayerstate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_InterlayerState);
    $('#ff_lab_hm_clientinfo_interlayerheatarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_InterlayerHeatArea);
    $('#ff_lab_hm_clientinfo_insidearea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_InsideArea);
    $('#ff_lab_hm_clientinfo_heatarea').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatArea);
    $('#ff_lab_hm_clientinfo_unitpricetype').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_UnitPriceType);
    $('#ff_lab_hm_clientinfo_networkdate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_NetworkDate);
    $('#ff_lab_hm_clientinfo_isnetwork').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_IsNetwork);
    $('#ff_lab_hm_clientinfo_beginheatdate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_BeginHeatDate);
    $('#ff_lab_hm_clientinfo_totalheatsourcefactory').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_TotalHeatSourceFactory);
    $('#ff_lab_hm_clientinfo_heatsource').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatSource);
    $('#ff_lab_hm_clientinfo_floor').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Floor);
    $('#ff_lab_hm_clientinfo_linetype').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_LineType);
    $('#ff_lab_hm_clientinfo_heatstate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HeatState);
    $('#ff_lab_hm_clientinfo_email').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Email);
    $('#ff_lab_hm_clientinfo_phone').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Phone);
    $('#ff_lab_hm_clientinfo_jobaddress').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_JobAddress);
    $('#ff_lab_hm_clientinfo_homeaddress').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_HomeAddress);
    $('#ff_lab_hm_clientinfo_gender').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Gender);
    $('#ff_lab_hm_clientinfo_birthday').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Birthday);
    $('#ff_lab_hm_clientinfo_zipcode').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_ZipCode);
    $('#ff_lab_hm_clientinfo_idcard').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_IDCard);
    $('#ff_lab_hm_clientinfo_isdelete').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_IsDelete);
    $('#ff_lab_hm_clientinfo_adddate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_AddDate);
    $('#ff_lab_hm_clientinfo_updatedate').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_UpdateDate);
    $('#ff_lab_hm_clientinfo_chargeannual_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_ChargeAnnual_ID);
    $('#ff_lab_hm_clientinfo_village_id').html($.LCLPageModel.Resource.PageLanguageResource.HM_ClientInfo_Model_Village_ID);

}
//初始化页面中文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = {
    Page_title: '客户信息',
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
    HM_ClientInfo_Model_ID: '编号',
    HM_ClientInfo_Model_ClientType: '客户类型',
    HM_ClientInfo_Model_HeatType: '取暖类型',
    HM_ClientInfo_Model_HelpeCode: '用户编码',
    HM_ClientInfo_Model_Cardno: '房间卡号',
    HM_ClientInfo_Model_Name: '用户名称',
    HM_ClientInfo_Model_NetInName: '入网用户名',
    HM_ClientInfo_Model_RoomNumber: '房间号',
    HM_ClientInfo_Model_BuildArea: '建筑面积',
    HM_ClientInfo_Model_InsideBuildArea: '套内建筑面积',
    HM_ClientInfo_Model_Superelevation: '超高',
    HM_ClientInfo_Model_BalconyState: '阳台状态',
    HM_ClientInfo_Model_BalconyArea: '阳台面积',
    HM_ClientInfo_Model_BalconyHeatState: '阳台采暖状态',
    HM_ClientInfo_Model_BalconyHeatArea: '阳台收费面积',
    HM_ClientInfo_Model_InterlayerArea: '阁楼夹层面积',
    HM_ClientInfo_Model_InterlayerState: '阁楼夹层采暖状态',
    HM_ClientInfo_Model_InterlayerHeatArea: '阁楼夹层收费面积',
    HM_ClientInfo_Model_InsideArea: '套内面积',
    HM_ClientInfo_Model_HeatArea: '收费面积',
    HM_ClientInfo_Model_UnitPriceType: '单价类别',
    HM_ClientInfo_Model_NetworkDate: '入网日期',
    HM_ClientInfo_Model_IsNetwork: '拆网/入网',
    HM_ClientInfo_Model_BeginHeatDate: '开始供暖日期',
    HM_ClientInfo_Model_TotalHeatSourceFactory: '总热源厂',
    HM_ClientInfo_Model_HeatSource: '热源',
    HM_ClientInfo_Model_Floor: '楼层',
    HM_ClientInfo_Model_LineType: '线别',
    HM_ClientInfo_Model_HeatState: '报停/强停/复热',
    HM_ClientInfo_Model_Email: '邮箱',
    HM_ClientInfo_Model_Phone: '联系电话',
    HM_ClientInfo_Model_JobAddress: '工作地址',
    HM_ClientInfo_Model_HomeAddress: '家庭地址',
    HM_ClientInfo_Model_Gender: '性别',
    HM_ClientInfo_Model_Birthday: '生日',
    HM_ClientInfo_Model_ZipCode: '邮政编码',
    HM_ClientInfo_Model_IDCard: '身份证号',
    HM_ClientInfo_Model_IsDelete: '删除标记',
    HM_ClientInfo_Model_AddDate: '添加时间',
    HM_ClientInfo_Model_UpdateDate: '更新时间',
    HM_ClientInfo_Model_ChargeAnnual_ID: 'ChargeAnnual_ID',
    HM_ClientInfo_Model_Village_ID: 'Village_ID',

};
//初始化页面英文资源 
$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = {
    Page_title: 'HM_ClientInfo',
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
    HM_ClientInfo_Model_ID: 'ID',
    HM_ClientInfo_Model_ClientType: 'ClientType',
    HM_ClientInfo_Model_HeatType: 'HeatType',
    HM_ClientInfo_Model_HelpeCode: 'HelpeCode',
    HM_ClientInfo_Model_Cardno: 'Cardno',
    HM_ClientInfo_Model_Name: 'Name',
    HM_ClientInfo_Model_NetInName: 'NetInName',
    HM_ClientInfo_Model_RoomNumber: 'RoomNumber',
    HM_ClientInfo_Model_BuildArea: 'BuildArea',
    HM_ClientInfo_Model_InsideBuildArea: 'InsideBuildArea',
    HM_ClientInfo_Model_Superelevation: 'Superelevation',
    HM_ClientInfo_Model_BalconyState: 'BalconyState',
    HM_ClientInfo_Model_BalconyArea: 'BalconyArea',
    HM_ClientInfo_Model_BalconyHeatState: 'BalconyHeatState',
    HM_ClientInfo_Model_BalconyHeatArea: 'BalconyHeatArea',
    HM_ClientInfo_Model_InterlayerArea: 'InterlayerArea',
    HM_ClientInfo_Model_InterlayerState: 'InterlayerState',
    HM_ClientInfo_Model_InterlayerHeatArea: 'InterlayerHeatArea',
    HM_ClientInfo_Model_InsideArea: 'InsideArea',
    HM_ClientInfo_Model_HeatArea: 'HeatArea',
    HM_ClientInfo_Model_UnitPriceType: 'UnitPriceType',
    HM_ClientInfo_Model_NetworkDate: 'NetworkDate',
    HM_ClientInfo_Model_IsNetwork: 'IsNetwork',
    HM_ClientInfo_Model_BeginHeatDate: 'BeginHeatDate',
    HM_ClientInfo_Model_TotalHeatSourceFactory: 'TotalHeatSourceFactory',
    HM_ClientInfo_Model_HeatSource: 'HeatSource',
    HM_ClientInfo_Model_Floor: 'Floor',
    HM_ClientInfo_Model_LineType: 'LineType',
    HM_ClientInfo_Model_HeatState: 'HeatState',
    HM_ClientInfo_Model_Email: 'Email',
    HM_ClientInfo_Model_Phone: 'Phone',
    HM_ClientInfo_Model_JobAddress: 'JobAddress',
    HM_ClientInfo_Model_HomeAddress: 'HomeAddress',
    HM_ClientInfo_Model_Gender: 'Gender',
    HM_ClientInfo_Model_Birthday: 'Birthday',
    HM_ClientInfo_Model_ZipCode: 'ZipCode',
    HM_ClientInfo_Model_IDCard: 'IDCard',
    HM_ClientInfo_Model_IsDelete: 'IsDelete',
    HM_ClientInfo_Model_AddDate: 'AddDate',
    HM_ClientInfo_Model_UpdateDate: 'UpdateDate',
    HM_ClientInfo_Model_ChargeAnnual_ID: 'ChargeAnnual_ID',
    HM_ClientInfo_Model_Village_ID: 'Village_ID',

};

