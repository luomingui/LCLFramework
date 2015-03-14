		
$(function(){			
	$.alerts = {
	show: function(className,boxWidth,whoMargin){
		var monitorWidth = screen.width;
		var monitorHeight = $(window).height();
		var shMargin = (monitorHeight - boxWidth)*0.5+160+ 'px';
		$(className).height(monitorHeight);
		$(whoMargin).css('margin-top',shMargin);
		$(className).show();
		
	},
	hide:function(className){
		$(className).css('display','none');
	}
				
	}
	showAlert = function(className,boxWidth,whoMargin){
		$.alerts.show(className,boxWidth,whoMargin);
	}
	hideAlert = function(className){
		$.alerts.hide(className);
	}
	//
	$("body").append('<div class="alertDiv"><table class="showhide"><tr><td><div id="letskillie6"><div class="r4"></div><div class="r2"></div><div class="r1"></div><div class="r1"></div><div class="content"><a rel="nofollow" id="close" >Close</a><span class="pic"></span><p><strong>重要提醒：</strong>使用本平台请采用IE8.0以上、chrome或firefox浏览器。</p><div class="fixed"></div><p class="browsers"><a rel="nofollow" class="ie8" href="http://www.microsoft.com/windows/internet-explorer/" target=_blank>IE 8</a><a rel="nofollow" class="firefox" href="download/Firefox-full-latest_v28.exe">Firefox</a><a rel="nofollow" class="chrome" href="download/ChromeStandaloneSetup_v33.exe">Chrome</a><div class="fixed"></div></p><p class="meta">     </p></div><div class="r1"></div><div class="r1"></div><div class="r2"></div><div class="r4"></div></div></td></tr></table></div>');
	$(".leavewordBtn").click(function(){
		showAlert('.alertDiv','600','.showhide');
	});
	$("#close").click(function(){
		hideAlert('.alertDiv');
	});
	showAlert('.alertDiv','600','.showhide');
		
})