/// <summary>
/// 脚本开发框架-页面模型
/// </summary>
$.LCLPageModel = {
    Resource: {
        Language: { zh_CN: "2052", en_US: "1033" }, // 语言编码
        LanguageResourceInfo: [], // 所有语言资源
        PageLanguageResource: [], // 页面语言资源
        CommonLanguageResource: [], // 公共语言资源
        InitLanguageResource: null, // 初始化控件多语的方法 该方法在页面脚本中实现
        InitLanguage: function () {
            //页面配置的多语言输出
            this.InitLanguageResource();
            //原有的多语言输出
            var languageInfo = [];
            languageInfo[this.Language.zh_CN] = {
                validateMsg: "您漏填了必输项，请输入。",
                queryModelSearching: "正在查询"
            };
            languageInfo[this.Language.en_US] = {
                validateMsg: "Some values are empty,please fill it.",
                queryModelSearching: "Searching"
            };
            // 根据语言id设置当前语言资源
            this.CommonLanguageResource = languageInfo[pageAttr.LanguageId];
        }
    },
    /// <summary>
    /// 脚本开发框架-页面模型-命令操作
    /// </summary>
    PageCommand: {
        GetPageAllControlToJSON: function () {
            /// <summary>
            /// 该方法系统会动态生成
            /// </summary>
        },
        SetJSONToPageAllControl: function () {
            /// <summary>
            /// 该方法系统会动态生成
            /// </summary>
        }
    }
};