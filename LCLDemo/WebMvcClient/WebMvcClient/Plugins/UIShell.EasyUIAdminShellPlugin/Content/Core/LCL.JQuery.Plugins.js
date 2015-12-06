(function () {
    $.extend($.fn, {
        LoadingMask: function (languageType, msg, maskDivClass, fixHeight, isNullPosition) {
            ///	<summary>
            /// 加载提示框
            /// </summary>
            /// <param name="languageType" type="String">语言类型：2052中文 1033英文</param>
            /// <param name="msg" type="String">消息内容</param>
            /// <param name="maskDivClass" type="String">加载消息样式</param>
            /// <param name="fixHeight" type="Int">覆盖层差量高度</param>
            /// <param name="isNullPosition" type="bool">是否不需要偏移位置，一般用在浮动层的相对位置上，设置了值则不需要为：top:0,left:0</param>
            /// <returns type="返回加载提示框" />
            this.UnLoadingMask();
            // 参数
            var op = {
                opacity: 0.8,
                z: 10000,
                bgcolor: '#ccc'
            };
            var original = $(document.body);
            var position = { top: 0, left: 0 };
            if (this[0] && this[0] !== window.document) {
                original = this;
                if (isNullPosition != "" && isNullPosition != undefined) {
                    position = { top: 0, left: 0 };
                } else {
                    position = original.position();
                }
            }
            // 创建一个 Mask 层，追加到对象中
            var maskDiv = $('<div class="maskdivgen">&nbsp;</div>');
            maskDiv.appendTo(original);
            var maskWidth = original.outerWidth();
            if (!maskWidth) {
                maskWidth = original.width();
            }
            var maskHeight = original.outerHeight();
            if (!maskHeight) {
                maskHeight = original.height() - 20;
            }
            if (fixHeight) {
                maskHeight = maskHeight - fixHeight;
            }
            maskDiv.css({
                position: 'absolute',
                top: position.top,
                left: position.left,
                'z-index': op.z,
                width: maskWidth,
                height: maskHeight,
                'background-color': op.bgcolor,
                opacity: 0
            });
            if (maskDivClass) {
                maskDiv.addClass(maskDivClass);
            } else {
                var message = "";
                if (msg == "" || msg == undefined) {
                    if (languageType == "" || languageType == undefined) {
                        languageType = "2052";
                    }
                    if (languageType == "2052") {
                        message = "加载中，请稍候...";
                    } else if (languageType == "1033") {
                        message = "Loading，Please Waiting...";
                    }
                } else {
                    message = msg;
                }
                var msgDiv = $('<div class="LodingDiv"><div class="LoadContext"><div class="LoadImg"></div><div id="Load_Message">' + message + '</div></div></div>');
                msgDiv.appendTo(maskDiv);
                var widthspace = (maskDiv.width() - msgDiv.width());
                var heightspace = (maskDiv.height() - msgDiv.height());
                msgDiv.css({
                    cursor: 'wait',
                    top: (heightspace / 2 - 2),
                    left: (widthspace / 2 - 2)
                });
            }
            maskDiv.fadeIn('fast', function () {
                // 淡入淡出效果
                $(this).fadeTo(200, op.opacity);
            })
            return maskDiv;
        },
        DisplayBuildData: function (languageType) {
            ///	<summary>
            /// 显示正在构建
            /// </summary>
            /// <param name="languageType" type="String">语言类型：zh-cn中文 en英文</param>
            /// <returns type="返回加载提示框" />
            var msg = "";
            if (languageType == "2052") {
                msg = "正在构建,请稍候...";
            } else if (languageType == "1033") {
                msg = "Building Now,Please Waiting...";
            }
            $("#Load_Message").html(msg);
        },
        UnLoadingMask: function () {
            ///	<summary>
            /// 取消提示框
            /// </summary>
            /// <returns type="取消加载提示框" />
            var original = $(document.body);
            if (this[0] && this[0] !== window.document) {
                original = $(this[0]);
            }
            original.find("> div.maskdivgen").fadeOut(50, 0, function () {
                $(this).remove();
            });
        },
        getUrlParam : function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    });
})();
//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}