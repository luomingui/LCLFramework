/// <reference path="../../jquery.js" />

/**********************************************************************************
说明:
1. css文件  :Styles/Flow.css
2. js文件   :Flow.js 
3. 调用格式  :<div id="FlowChart"></div>
**********************************************************************************/
(function ($) {
    /*******************************私有方法区域***************************************************/
    /// <summary>
    /// 构造HTML
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>    
    function buildHTML(target, options) {
        // 1. 静态数据 --加载标题
        buildTitleHtml(target, options);
        debugger;
        // 2. 动态数据
        if (options.Data == null && options.JsonServerURL.length > 0) {
            // 拼接URL
            var jsonURL = options.JsonServerURL;
            $.ajax({
                url: jsonURL,
                type: "post",
                dataType: "json",
                success: function (data) {
                    debugger;
                    if (data.Succeed) {
                        // 1. 数据赋值到options.Data中
                        //    options.Title = data.Data.Title;
                        options.Data = data.Data.FlowChart;
                        // 2. 加载标题
                        LoadTitle(options, target, data);
                        // 2. 加载标题
                        //    setTitle(target, options);
                        // 2. 加载控件
                        loadControl(target, options);
                        // 3. 加载流程事件
                        initEvent(target, options);
                    }
                    else {
                        alert(data.ErrMsg);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //console.log(textStatus);
                    alert("流程图jQuery插件报错,请联系系统管理员!");
                }
            });
        } else {
            // 2. 加载控件
            loadControl(target, options);
            // 3. 加载流程事件
            initEvent(target, options);
        }
    }

    function LoadTitle(options, target, data) {
        jsonURL = options.JsonService;
        $.ajax({
            url: jsonURL,
            type: "post",
            data: "",
            dataType: "json",
            success: function (data) {
                if (data.Succeed) {
                    // 1. 数据赋值到options.Data中
                    options.Title = data.Data.Title
                    // 2. 加载标题
                    setTitle(target, options);
                }
                else {
                    alert(data.ErrMsg);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus);
                alert("流程图标题出错,请联系系统管理员!");
            }
        });
    }

    /**
    * 私有方法
    **/
    //获取所有html
    function loadControl(target, options) {
        var $target = $(target);
        var totalHtml = [];
        // 1. 加载流程图
        var flowTotalHtml = buildFlowHtml(target, options);
        var flowTotalHtmlstring = flowTotalHtml.join(" ");
        // 2. 构建任务节点html
        var taskTotalHtml = buildTaskTotalHtml(target, options);
        var taskTotalHtmlstring = taskTotalHtml.join(" ");

        totalHtml = flowTotalHtml.concat(taskTotalHtml);
        if (totalHtml.length) {
            totalHtml.unshift('<div class="flowImg"><table border="0" cellspacing="0" cellpadding="0" class="tbFlow">');
            totalHtml.push('</table></div>');
        }
        totalHtml.unshift('<div class="flow">');
        totalHtml.push('</div>');

        var totalHtmlString = totalHtml.join(" ");
        $target.append(totalHtmlString);
        //
        document.write(totalHtmlString);
    }

    /// <summary>
    /// 构建标题
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>  
    function buildTitleHtml(target, options) {
        var titleHtml = [];
        titleHtml.push('<div class="flowTitle"><span>');
        if (options.Title != null && options.Title.length > 0) {
            titleHtml.push(options.Title);
        } else {
            titleHtml.push("");
        }
        titleHtml.push('</span></div>');
        $(target).html(titleHtml.join(" "));
        document.write(titleHtml.join(" "));

        // 如果Title不存在,则隐藏
        if (options.Title == null || options.Title.length <= 0) {
            $(target).find(".flowTitle").hide();
        }
    }

    /// <summary>
    /// 构建流程图标html
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>  
    function buildFlowHtml(target, options) {
        // 1. 获取数据长度
        var flowlength = options.Data.length;
        // 2. 拼接HTML
        var flowhtml = [];
        if (options.Data) {
            $.each(options.Data, function (index, value) {
                if (value.Visible) {
                    flowhtml.push('<td>');
                    flowhtml.push('<div style="width: ' + options.ListWidth + 'px">');
                    flowhtml.push('<ul class="flowlist">');
                    //Status-1；完成 Status-2：运行 Status-3：未开始；流程异常按照未开始处理
                    if (index == 0) {
                        flowhtml.push('<li class="tdArrowStart" style="width:' + options.ArrowPercent + ';"></li>');
                        if (value.Status == 1 || value.Status == 2) {
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowYes' + (index + 1) + '.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                            flowhtml.push('<li class="tdArrowYes" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowYes.png) top left repeat-x;"></li>');
                        }
                        else {
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowNo' + (index + 1) + '.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                            flowhtml.push('<li class="tdArrowNo" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowNo.png) top left repeat-x;"></li>');
                        }
                    }
                    else if (flowlength > index + 1) {
                        if (value.Status == 1 || value.Status == 2) {
                            flowhtml.push('<li class="tdArrowYes" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowYes.png) top left repeat-x;"></li>');
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowYes' + (index + 1) + '.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                            flowhtml.push('<li class="tdArrowYes" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowYes.png) top left repeat-x;"></li>');
                        }
                        else {
                            flowhtml.push('<li class="tdArrowNo" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowNo.png) top left repeat-x;"></li>');
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowNo' + (index + 1) + '.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                            flowhtml.push('<li class="tdArrowNo" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowNo.png) top left repeat-x;"></li>');
                        }
                    }
                    else if (options.Data.length = index + 1) {
                        if (value.Status == 1 || value.Status == 2) {
                            flowhtml.push('<li class="tdArrowYes" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowYes.png) top left repeat-x;"></li>');
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowYes' + (index + 1) + 'End.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                        }
                        else {
                            flowhtml.push('<li class="tdArrowNo" style="width:' + options.ArrowPercent + ';background: url(' + options.RelativeUrl + 'Images/arrowNo.png) top left repeat-x;"></li>');
                            flowhtml.push('<li style="background: url(' + options.RelativeUrl + 'Images/' + (index + 1) + '_circle/flowNo' + (index + 1) + 'End.png) top left no-repeat; width: 42px;height: 42px; float: left"></li>');
                        }
                        flowhtml.push('<li class="tdArrowEnd" style="width:' + options.ArrowPercent + ';"></li>');
                    }

                    flowhtml.push('<li style="width: 100%; text-align: center; float: left">' + value.NodeName + '</li>');
                    if (value.Status == 2 && value.subNodes.length) {
                        flowhtml.push('<li class="imgTriangle">&nbsp;</li>');
                    }
                    else {
                        flowhtml.push('<li class="imgTriangleNull">&nbsp;</li>');
                    }
                    flowhtml.push('</ul>');
                    flowhtml.push('</div>');
                    flowhtml.push('</td>');
                }
            });

            if (flowhtml.length) {
                flowhtml.unshift('<tr>');
                flowhtml.push('</tr>');
            }
        }
        return flowhtml;
    }

    /// <summary>
    /// 构建任务节点html
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>  
    function buildTaskTotalHtml(target, options) {
        var taskTotalhtml = [];
        var taskMoreHtml = [];
        var resource = FlowResources[options.LanguageID];
        if (options.Data) {
            $.each(options.Data, function (index, value) {

                if (value != null && value.subNodes != null) {

                    $.each(value.subNodes, function (taskIndex, taskValue) {
                        taskTotalhtml.push('<div class="task">');
                        taskTotalhtml.push('<div class="taskTitle">');
                        taskTotalhtml.push('<div class="taskSerial">');
                        taskTotalhtml.push((index + 1) + '.' + (taskIndex + 1) + '</div>');
                        taskTotalhtml.push('<div class="rightTriangle"></div>');
                        taskTotalhtml.push('<div class="taskName">');
                        taskTotalhtml.push('<span class="txtBold">' + resource[taskValue.NodeName] + '：</span>' + taskValue.Text + '</div>');
                        taskTotalhtml.push('<div class="iconPlus">');
                        taskTotalhtml.push('</div></div></div>');

                        taskMoreHtml = buildTaskMoreHtml(target, options, taskValue, index, taskIndex);
                        taskTotalhtml = taskTotalhtml.concat(taskMoreHtml);
                    });
                }
            });

            if (taskTotalhtml.length) {
                taskTotalhtml.unshift('<tr><td colspan="' + options.Data.length + '"><div class="flowContent">');
                taskTotalhtml.push('</div></td></tr>');
            }
        }
        return taskTotalhtml;
    }

    /// <summary>
    /// 构建任务节点详细信息html
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>  
    /// <param name="flowIndex" type="int"></param>  
    /// <param name="taskIndex" type="int"></param>  
    function buildTaskMoreHtml(target, options, taskMore, flowIndex, taskIndex) {
        var resource = FlowResources[options.LanguageID];
        var taskMorehtml = [];

        if (!taskMore.subNodes.length) {
            return taskMorehtml;
        }

        //任务详情
        taskMorehtml.push('<div class="taskMore" style="display: none">');
        taskMorehtml.push('<div class="taskTitle">');
        taskMorehtml.push('<div class="taskSerial">');
        taskMorehtml.push((flowIndex + 1) + '.' + (taskIndex + 1) + '</div>');
        taskMorehtml.push('<div class="rightTriangle"></div>');
        taskMorehtml.push('<div class="taskName">');
        taskMorehtml.push('<span class="txtBold">' + resource[taskMore.NodeName] + '：</span>' + taskMore.Text + '</div>');
        taskMorehtml.push('<div class="iconReduce"></div></div>');
        taskMorehtml.push('<div class="taskLine"></div>');
        taskMorehtml.push('<table border="0" cellspacing="0" cellpadding="0" class="tbTaskMore">');

        $.each(taskMore.subNodes, function (index, value) {
            if (value.DisplayType == 0 && value.Text != undefined && value.Text.length > 0) { //文本显示
                taskMorehtml.push('<tr><td nowrap="nowrap" class="txtSubtitle">');
                taskMorehtml.push(resource[value.NodeName] + '：');
                taskMorehtml.push('</td><td class="tbTaskMoreDetail" style="width: ' + (options.Data.length * options.ListWidth - 150) + 'px;min-width:300px">');
                taskMorehtml.push(value.Text);
                taskMorehtml.push('</td></tr>');
            }
            else if (value.DisplayType == 1 && value.Text != undefined && value.Text.length > 0) { //链接显示
                taskMorehtml.push('<tr><td>' + '' + '</td>');
                taskMorehtml.push('<td class="txtLink">');
                taskMorehtml.push('<div onclick="' + locationUrl(value.Text));
                taskMorehtml.push('" style="width:50px">' + resource[value.NodeName]);
                taskMorehtml.push('</div></td></tr>')
            }
        });

        taskMorehtml.push('</table>');
        taskMorehtml.push('</div>');

        return taskMorehtml;
    }

    /// <summary>
    /// 事件绑定
    /// </summary>
    /// <param name="target" type="Object">插件本身</param>
    /// <param name="options" type="Object">配置参数</param>  
    function initEvent(target, options) {
        $(".task").bind("click", function () {
            $(this).hide();
            $(this).next('.taskMore').show();
        });
        $(".taskTitle").bind("click", function () {
            $(this).parent('.taskMore').hide();
            $(this).parent('.taskMore').prev('.task').show();
        });
        // 任务查看默认 展开
        if (!options.PBOID && options.PBOType) {
            $(".task").trigger("click");
        }
    }

    /// <summary>
    /// 跳转链接
    /// </summary>
    /// <param name="url" type="Object">跳转URL</param>
    /// <param name="options" type="Object">配置参数</param>  
    function locationUrl(url) {
        return 'window.location.href =\'' + url + '\'';
    }

    /// <summary>
    /// 存储配置参数
    /// </summary>
    /// <param name="target" type="Object">jquery插件本身</param>
    /// <param name="option" type="Object">新的配置参数</param>
    function setOption(target, option) {
        var oldOption = $(target).Flow("getOption");
        var newOption = $.extend(oldOption, option);
        $.data(target[0], "Flow", { options: newOption });
        return newOption;
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="target" type="Object">jquery插件本身</param>
    /// <param name="option" type="Object">新的配置参数</param>
    function setTitle(target, options) {
        if (!options.Title) {
            $(target).find(".flowTitle").show();
            $(target).find(".flowTitle").find("span").text(options.Title);
        }
    }
    /*******************************私有方法区域***************************************************/

    /*******************************插件区域******************************************************/
    /**
    * 根据参数判断是执行方法还是初始化
    */
    $.fn.Flow = function (method, options) {
        if (typeof method == "string") {
            return $.fn.Flow.methods[method](this, options);
        }
        debugger;
        method = method || {};

        //var opt = $.extend({}, $.fn.Flow.defaults, $.fn.Flow.parseOptions(this), method);
        //flow = $.data(this, "Flow", { options: opt });

        return this.each(function () {
            var flow = $.data(this, "Flow");
            var opt;
            if (flow) {
                opt = $.extend(flow.options, method);
            }
            else {
                opt = $.extend({}, $.fn.Flow.defaults, $.fn.Flow.parseOptions(this), method);
            }
            flow = $.data(this, "Flow", { options: opt });
            // 构造HTML
            buildHTML(this, flow.options);
        });
    };
    /*******************************插件区域******************************************************/

    /*******************************公共方法区域******************************************************/
    $.fn.Flow.methods = {
        /// <summary>
        /// 获取配置参数
        /// </summary>
        /// <param name="jq" type="Object">jquery插件本身</param>
        getOption: function (jq) {
            return $.data(jq[0], "Flow").options;
        },
        /// <summary>
        /// 设置PBOID,PBOType,ProcessID 加载流程图
        /// </summary>
        /// <param name="jq" type="Object">jquery插件本身</param>
        /// <param name="options" type="Object">新参数</param>
        setPBO: function (jq, options) {
            var newOptions = setOption(jq, options);
            buildHTML(jq, newOptions);
        },
        /// <summary>
        /// 根据数据加载流程图
        /// </summary>
        /// <param name="jq" type="Object">jquery插件本身</param>
        /// <param name="data" type="Object">需要加载的数据</param>
        setData: function (jq, data) {
            var options = { Data: data };
            $(jq).Flow("setPBO", options);
        },
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title" type="String">标题</param>
        setTitle: function (jq, title) {
            var titleOptions = { Title: title };
            var options = setOption(jq, titleOptions);
            setTitle(jq, title);
        }
    };
    /*******************************公共方法区域******************************************************/

    /*******************************参数区域**********************************************************/
    // 转换后的参数
    $.fn.Flow.parseOptions = function (target) {
        var $target = $(target);
        return {
            ID: $target.attr("id"), //控件ID
            LanguageID: "2052", //多语言
            CurrEmployeeNo: "", //当前操作人
            SiteRoot: "/", // 站点跟路径   
            Title: null, // 标题         
            Data: null, // json数据
            ListWidth: 140, //列表宽度,单位px 111,31;212,40;140,35
            ArrowPercent: "35%", //线比例,
            TaskMoreTdWith: 185,  //详细信息宽度，单位px
            PBOID: null, //对象序号 任务界面==>PBO_ID取TaskID,查看界面==>PBOID取对应的PBOID
            PBOType: null, // 对象类型,null:任务界面,"",任务界面,"DELIST":查看界面
            ProcessID: null, // 进程ID
            JsonService: "/WebUINewProject/Scripts/PDM/Flow/JsonService/FlowJsonService.ashx?1=1", //JSON服务地址
            JsonParameters: null, //JSON地址后面带的参数，比如：&Test=1&TestBB=2    
            RelativeUrl: "Styles/"    // 路径,不需要配置   
        };
    }

    // 参数默认值
    $.fn.Flow.defaults = {
        ID: null, // 控件ID
        LanguageID: "2052", //多语言
        CurrEmployeeNo: "", //当前操作人
        SiteRoot: null, // 站点跟路径    
        Title: null, // 标题     
        Data: null, // json数据 
        ListWidth: null, //列表宽度,单位px 111,31;212,40;140,35
        ArrowPercent: null, //线比例,
        TaskMoreTdWith: null,  //详细信息宽度，单位px
        PBOID: null, //对象序号 任务界面==>PBO_ID取TaskID,查看界面==>PBOID取对应的PBOID
        PBOType: null, // 对象类型,null:任务界面,"",任务界面,"DELIST":查看界面
        ProcessID: null, // 进程ID
        JsonService: null, //JSON服务地址
        JsonParameters: null, //JSON地址后面带的参数，比如：&Test=1&TestBB=2  
        RelativeUrl: null// 这个不需要配置,用来存储路径     
    };
    /*******************************参数区域**********************************************************/
})(jQuery);


/*****************************多语言区域***************************/
var FlowResources = new Array("2052", "1033");

FlowResources["2052"] = {
    TASK_NAME: '任务名称',
    FLOWNODE: '节点名称',
    EXECUTORS: '当前处理人',
    REMARK: '任务说明',
    DEAL: '立即处理',
    DEALROLE: '您的角色',
    VIEWROLE: '处理人角色'

};

FlowResources["1033"] = {
    TASK_NAME: 'TASK_NAME',
    FLOWNODE: 'node name',
    EXECUTORS: 'EXECUTORS',
    REMARK: 'REMARK',
    DEAL: 'DEAL',
    DEALROLE: 'Your Role',
    VIEWROLE: 'Handle Role'

};
/*****************************多语言区域***************************/
