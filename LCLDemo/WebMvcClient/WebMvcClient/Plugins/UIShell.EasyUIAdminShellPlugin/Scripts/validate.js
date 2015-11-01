//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}
(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
})(jQuery);
$.extend($.fn.validatebox.defaults.rules, {
    minLength: {
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '长度至少：{0}'
    },
    maxLength: {
        validator: function (value, param) {
            return value.length <= param[0];
        },
        message: '长度最多：{0}'
    },
    chs: {
        validator: function (value, param) {
            return /^[\u4e00-\u9fa5]+$/.test(value);
        },
        message: '请输入汉字'
    },
    upperCase: {
        validator: function (value, param) {
            return /^[A-Z,0-9]+$/.test(value);
        },
        message: '请输入大写英文字符、或数字'
    },
    lowerCase: {
        validator: function (value, param) {
            return /^[a-z,0-9]+$/.test(value);
        },
        message: '请输入小写英文字符、或数字'
    },
    zip: {
        validator: function (value, param) {
            return /^[1-9]\d{5}$/.test(value);
        },
        message: '邮政编码不存在'
    },
    qq: {
        validator: function (value, param) {
            return /^[1-9]\d{4,10}$/.test(value);
        },
        message: 'QQ号码不正确'
    },
    mobile: {
        validator: function (value, param) {
            return /^1[3,5,8]\d{9}$/.test(value);
        },
        message: '手机号码不正确'
    },
    tel: {
        validator: function (value, param) {
            return /^(\d{3,4}-)?\d{7,8}$/.test(value);
        },
        message: '电话号码不正确'
    },
    acctName: {
        validator: function (value, param) {
            return (/^[\u0391-\uFFE5\w]+$/.test(value)) && (value.len() >= 2);
        },
        message: '登录名称只允许汉字、英文字母、数字及下划线，长度至少2位，1个汉字长度为2'
    },
    safepass: {
        validator: function (value, param) {
            return value.length >= 6;
            //return safePassword(value);
        },
        message: '密码至少6位'
    },
    equalTo: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '两次输入的字符不一致'
    },
    equalNotTo: {
        validator: function (value, param) {
            return value != $(param[0]).val();
        },
        message: '两次输入的字符不能相同'
    },
    number: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    },
    idcard: {
        validator: function (value, param) {
            return idCard(value);
        },
        message: '请输入正确的身份证号码'
    },
    orgCode: {
        validator: function (value, param) {
            return isValidOrgCode(value);
        },
        message: '请输入正确的企业组织机构代码证号码'
    },
    busCode: {
        validator: function (value, param) {
            return isValidBusCode(value);
        },
        message: '请输入正确的营业执照编号'
    }
});

/* 密码由字母和数字组成，至少6位 */
var safePassword = function (value) {
    return !(/^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\"]*)|.{0,5})$|\s/.test(value));
}

/** 身份证 */
var idCard = function (value) {
    if (value.length == 18 && 18 != value.length) return false;
    var number = value.toLowerCase();
    var d, sum = 0, v = '10x98765432', w = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2], a = '11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91';
    var re = number.match(/^(\d{2})\d{4}(((\d{2})(\d{2})(\d{2})(\d{3}))|((\d{4})(\d{2})(\d{2})(\d{3}[x\d])))$/);
    if (re == null || a.indexOf(re[1]) < 0) return false;
    if (re[2].length == 9) {
        number = number.substr(0, 6) + '19' + number.substr(6);
        d = ['19' + re[4], re[5], re[6]].join('-');
    } else d = [re[9], re[10], re[11]].join('-');
    if (!isDateTime.call(d, 'yyyy-MM-dd')) return false;
    for (var i = 0; i < 17; i++) sum += number.charAt(i) * w[i];
    return (re[2].length == 9 || number.charAt(17) == v.charAt(sum % 11));
}

/** 日期、时间 */
var isDateTime = function (format, reObj) {
    format = format || 'yyyy-MM-dd';
    var input = this, o = {}, d = new Date();
    var f1 = format.split(/[^a-z]+/gi), f2 = input.split(/\D+/g), f3 = format.split(/[a-z]+/gi), f4 = input.split(/\d+/g);
    var len = f1.length, len1 = f3.length;
    if (len != f2.length || len1 != f4.length) return false;
    for (var i = 0; i < len1; i++) if (f3[i] != f4[i]) return false;
    for (var i = 0; i < len; i++) o[f1[i]] = f2[i];
    o.yyyy = s(o.yyyy, o.yy, d.getFullYear(), 9999, 4);
    o.MM = s(o.MM, o.M, d.getMonth() + 1, 12);
    o.dd = s(o.dd, o.d, d.getDate(), 31);
    o.hh = s(o.hh, o.h, d.getHours(), 24);
    o.mm = s(o.mm, o.m, d.getMinutes());
    o.ss = s(o.ss, o.s, d.getSeconds());
    o.ms = s(o.ms, o.ms, d.getMilliseconds(), 999, 3);
    if (o.yyyy + o.MM + o.dd + o.hh + o.mm + o.ss + o.ms < 0) return false;
    if (o.yyyy < 100) o.yyyy += (o.yyyy > 30 ? 1900 : 2000);
    d = new Date(o.yyyy, o.MM - 1, o.dd, o.hh, o.mm, o.ss, o.ms);
    var reVal = d.getFullYear() == o.yyyy && d.getMonth() + 1 == o.MM && d.getDate() == o.dd && d.getHours() == o.hh && d.getMinutes() == o.mm && d.getSeconds() == o.ss && d.getMilliseconds() == o.ms;
    return reVal && reObj ? d : reVal;
    function s(s1, s2, s3, s4, s5) {
        s4 = s4 || 60, s5 = s5 || 2;
        var reVal = s3;
        if (s1 != undefined && s1 != '' || !isNaN(s1)) reVal = s1 * 1;
        if (s2 != undefined && s2 != '' && !isNaN(s2)) reVal = s2 * 1;
        return (reVal == s1 && s1.length != s5 || reVal > s4) ? -10000 : reVal;
    }
}
/** 企业组织机构代码证 */
function isValidOrgCode(orgCode) {
    var ret = false;
    var codeVal = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];
    var intVal = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35];
    var crcs = [3, 7, 9, 10, 5, 8, 4, 2];
    if (!("" == orgCode) && orgCode.length == 10) {
        var sum = 0;
        for (var i = 0; i < 8; i++) {
            var codeI = orgCode.substring(i, i + 1); var valI = -1;
            for (var j = 0; j < codeVal.length; j++) { if (codeI == codeVal[j]) { valI = intVal[j]; break; } } sum += valI * crcs[i];
        }
        var crc = 11 - (sum % 11); switch (crc) { case 10: { crc = "X"; break; } default: { break; } }
        if (crc == orgCode.substring(9)) { ret = true; } else { ret = false; }
    } else if ("" == orgCode) { ret = false; } else { ret = false; }
    return ret;
}
/** 营业执照编号 */
function isValidBusCode(busCode) {
    var ret = false;
    if (busCode.length == 15) {
        var sum = 0; var s = []; var p = []; var a = []; var m = 10; p[0] = m;
        for (var i = 0; i < busCode.length; i++) {
            a[i] = parseInt(busCode.substring(i, i + 1), m); s[i] = (p[i] % (m + 1)) + a[i];
            if (0 == s[i] % m) { p[i + 1] = 10 * 2; } else { p[i + 1] = (s[i] % m) * 2; }
        }
        if (1 == (s[14] % m)) { ret = true; } else { ret = false; }
    }
    else if ("" == busCode) { ret = false; } else { ret = false; }
    return ret;
}

/** 是否全中文 */
function isChs(value) {
    return /^[\u4e00-\u9fa5]+$/.test(value);
}
/** 是否含中文 */
function hasChs(value) {
    return /^[\u4e00-\u9fa5]/.test(value);
}
