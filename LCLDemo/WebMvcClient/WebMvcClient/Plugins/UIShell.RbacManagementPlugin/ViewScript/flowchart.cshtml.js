//页面入口
$(document).ready(function () {
    debugger;
    var options = {
        LanguageID: "2052", //多语言
        JsonServerURL: "JsonService/Flow.ashx?method=legend&routId=" + $.LCLCore.Request.QueryString("routId"),
        Title: "流程图显示"
    };
    $("#FlowChart").Flow(options);
});