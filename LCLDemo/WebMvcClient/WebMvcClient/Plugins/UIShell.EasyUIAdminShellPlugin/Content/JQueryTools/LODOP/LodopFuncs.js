function getLodop(oOBJECT,oEMBED){
/**************************
  本函数根据浏览器类型决定采用哪个对象作为控件实例：
  IE系列、IE内核系列的浏览器采用oOBJECT，
  其它浏览器(Firefox系列、Chrome系列、Opera系列、Safari系列等)采用oEMBED。
  在安装提示时，Firefox系列和Opera系列因提示不准确而放弃用本函数，
  其中Firefox系列用其浏览器界面“手工安装”提示比较好。
**************************/
  var LODOP=oEMBED;
  if (navigator.appVersion.indexOf('MSIE')>=0) LODOP=oOBJECT;
  if ((navigator.userAgent.indexOf('Firefox')<0)  
     &&(navigator.userAgent.indexOf('Opera')<0)) 
  {
      if (typeof(LODOP.VERSION)=='undefined'){
   	document.write("<h3><font color='#FF00FF'>打印控件未安装!点击这里<a href='/Content/JqueryTools/LODOP/install_lodop.exe'>执行安装</a>,安装后请刷新页面或重新进入。</font></h3>");
      } else if (LODOP.intVERSION<6000)  //版本6.0.0.0
	document.write("<h3><font color='#FF00FF'>打印控件需要升级!点击这里<a href='/Content/JqueryTools/LODOP/install_lodop.exe'>执行升级</a>,升级后请重新进入。</font></h3>");
  }   

  return LODOP;
}

