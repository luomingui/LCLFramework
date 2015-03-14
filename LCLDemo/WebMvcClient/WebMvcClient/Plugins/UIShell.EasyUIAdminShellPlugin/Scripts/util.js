/*
* 字符串长度，中文算2
*/
String.prototype.len = function () {
    var len = this.length;
    var reLen = 0;
    for (var i = 0; i < len; i++) {
        if (this.charCodeAt(i) < 27 || this.charCodeAt(i) > 126) {
            reLen += 2;
        } else {
            reLen++;
        }
    }
    return reLen;
}

/*
* 类似C# 的 string.Format("{0}/{1}", para1, para2)函数
*/
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题，谢谢何以笙箫的指出
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}


/*
* 日期格式转换
* formatDate('/Date(1369377245460)/', 'dd/MM HH:mm:ss')
*/
function formatDate(str, format) {
    if (!str) return '';

    var i = parseInt(str.match(/[-]*\d+/g)[0]);
    if (i < 0) return '';
    var d = new Date(i);
    if (d.toString() == 'Invalid Date') return '';

    //处理客户端时区不同导致的问题
    //480 是UTC+8
    var utc8Offset = 480;
    d.setMinutes(d.getMinutes() + (d.getTimezoneOffset() + 480));

    format = format || 'MM/dd hh:mm:ss tt';

    var hour = d.getHours();
    var month = FormatNum(d.getMonth() + 1)

    var re = format.replace('YYYY', d.getFullYear())
    .replace('YY', FormatNum(d.getFullYear() % 100))
    .replace('MM', FormatNum(month))
    .replace('dd', FormatNum(d.getDate()))
    .replace('hh', hour == 0 ? '12' : FormatNum(hour <= 12 ? hour : hour - 12))
    .replace('HH', FormatNum(hour))
    .replace('mm', FormatNum(d.getMinutes()))
    .replace('ss', FormatNum(d.getSeconds()))
    .replace('tt', (hour < 12 ? 'AM' : 'PM'));

    return re;

    function FormatNum(num) {
        num = Number(num);
        return num < 10 ? ('0' + num) : num.toString();
    }
}

/*
* 日期格式转换
*/
String.prototype.toDate = function () {
    if (/^\d{4}-\d{1,2}-\d{1,2}$/.test(this)) { return this; }
    return formatDate(this, "YYYY-MM-dd");
}
String.prototype.toTime = function (args) {
    if (/^\d{4}-\d{1,2}-\d{1,2} \d{2}:\d{2}:\d{2}$/.test(this)) { return this; }
    if (args == null) {
        return formatDate(this, "YYYY-MM-dd HH:mm:ss");
    }
    return formatDate(this, args);
}

/** 
* 时间对象的格式化; 
*/
Date.prototype.format = function (format) {
    /* 
    * eg:format="yyyy-MM-dd hh:mm:ss"; 
    */
    var o = {
        "M+": this.getMonth() + 1, // month  
        "d+": this.getDate(), // day  
        "h+": this.getHours(), // hour  
        "m+": this.getMinutes(), // minute  
        "s+": this.getSeconds(), // second  
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter  
        "S": this.getMilliseconds()
        // millisecond  
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4
                        - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1
                            ? o[k]
                            : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

Date.prototype.AddYear = function (nYear) {
    var dt = this.setFullYear(this.getFullYear() + nYear);
    return (new Date(dt)).format('yyyy-MM-dd');
}

//不被 Frame
function cannotBeFrame() {
    if (top.location !== self.location) {
        top.location = self.location;
    }
}
