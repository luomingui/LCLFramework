/*
ÆÁ±Î Êó±êÓÒ¼ü¡¢Ctrl+n¡¢shift+F10¡¢F5Ë¢ÐÂ¡¢ÍË¸ñ¼ü
ÆÁ±Î Alt+ ·½Ïò¼ü ¡û
ÆÁ±Î Alt+ ·½Ïò¼ü ¡ú
ÆÁ±Î ÍË¸ñÉ¾³ý¼ü
ÆÁ±Î F5
ÆÁ±Î F6
ÆÁ±Î F11
ÆÁ±Î Ctrl + R
ÆÁ±Î Ctrl + I
ÆÁ±Î Ctrl + O
ÆÁ±Î Ctrl + H
ÆÁ±Î Ctrl + L
ÆÁ±Î Ctrl + B
ÆÁ±Î Ctrl + W
ÆÁ±Î Ctrl + N
ÆÁ±Î Ctrl + D
ÆÁ±Î Ctrl + E
ÆÁ±Î shift+F10
*/

var ie= /msie [6789]/i.test(window.navigator.userAgent);
var WinSize;/*ä¯ÀÀÆ÷¿ÉÊÓÃæ»ý*/
var hr='\n¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª         \n';

document.onselectstart= function(){if(window.event.srcElement.tagName != 'INPUT' && event.srcElement.tagName != 'TEXTAREA')return false;}
document.oncontextmenu=function(){return false;}
document.ondragstart=function(){return false;}
function KeyDown(){	//ÆÁ±ÎÊó±êÓÒ¼ü¡¢Ctrl+n¡¢shift+F10¡¢F5Ë¢ÐÂ¡¢ÍË¸ñ¼ü
  if ((window.event.altKey && window.event.keyCode==37)||   //ÆÁ±Î Alt+ ·½Ïò¼ü ¡û
	  (window.event.altKey && window.event.keyCode==39)		//ÆÁ±Î Alt+ ·½Ïò¼ü ¡ú
         //(window.event.keyCode==8)              //ÆÁ±ÎÍË¸ñÉ¾³ý¼ü
	 ){
	   window.event.keyCode=0;
       window.event.returnValue=false;
     }
  if ((window.event.keyCode==116)||                       //ÆÁ±Î F5
	  (window.event.keyCode==117)||                       //ÆÁ±Î F6
	  (window.event.keyCode == 122) ||                    //ÆÁ±Î F11
    //  (window.event.keyCode == 123) ||                    //ÆÁ±Î F12
      (window.event.ctrlKey) ||                           //Ctrl
      (window.event.ctrlKey && window.event.keyCode==82)|| //Ctrl + R
	  (window.event.ctrlKey && window.event.keyCode==73)|| //Ctrl + I
      (window.event.ctrlKey && window.event.keyCode==79)|| //Ctrl + O
	  (window.event.ctrlKey && window.event.keyCode==72)|| //Ctrl + H
	  (window.event.ctrlKey && window.event.keyCode==76)|| //Ctrl + L
	  (window.event.ctrlKey && window.event.keyCode==66)|| //Ctrl + B
	  (window.event.ctrlKey && window.event.keyCode==87)|| //Ctrl + W
	  (window.event.ctrlKey && window.event.keyCode==78)|| //Ctrl + N
	  (window.event.ctrlKey && window.event.keyCode==68)|| //Ctrl + D
	  (window.event.ctrlKey && window.event.keyCode==69)|| //Ctrl + E
      (window.event.shiftKey && window.event.keyCode==121) //shift+F10
	  ) {
      try {
          window.event.keyCode = 0;
          window.event.returnValue = false;
      } catch (e) { 
       }
     }
  if ((window.event.altKey)&&(window.event.keyCode==115)){   //ÆÁ±ÎAlt+F4
      window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px");
      return false;
	}
}
document.onclick=function(){
  if (window.event.srcElement.tagName == "A" && window.event.shiftKey) //ÆÁ±Î shift ¼ÓÊó±ê×ó¼üÐÂ¿ªÒ»ÍøÒ³
    window.event.returnValue = false; 
}
document.onkeydown = function () { KeyDown(); }
//ÆÁ±Îjs´íÎó
function killerrors() { return true; } 
window.onerror = killerrors; 