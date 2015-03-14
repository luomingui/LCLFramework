$(function () {
    
    if (typeof ($.cookie('loginName')) != 'undefined') {
        $('#Name').val($.cookie('loginName'));
    }
    
    $("#qrcode").qrcode({
        render: "table", //table方式 
        width: 72, //宽度 
        height: 72, //高度 
        text: "www.helloweba.com" //任意内容 
    });

    $("#imgCode").bind("click", function () {
        this.src = "GetValidateCode";
    });

    //enter、tab=>submit
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            switch (event.target.id) {
                case "Name":
                    $('#Password').focus();
                    break;
                case "Password":
                    $('#VerifyCode').focus();
                    break;
                case "VerifyCode":
                    $("#btnLogin").click();
                    break;
                default:
                    $("#btnLogin").click();
                    break;
            }
        }
    });
    $('#loginrunbox').kxbdSuperMarquee({
        distance: 860,
        time: 5,
        direction: 'left',
        btnGo: { left: '#og_prev', right: '#og_next' }
    });
 
    $('#runbox1').kxbdSuperMarquee({
        distance: 100,
        time: 4,
        direction: 'down'
    });
 
    cannotBeFrame();
})
