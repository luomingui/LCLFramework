// ============================ 定义常量 =========================
var TreeImageFolderPath = "../images/";

var TREE_IMG_I = TreeImageFolderPath +"i.gif";
var TREE_IMG_F = TreeImageFolderPath +"f.gif";
var TREE_IMG_T = TreeImageFolderPath +"t.gif";
var TREE_IMG_L = TreeImageFolderPath +"l.gif";
var TREE_IMG_R = TreeImageFolderPath +"r.gif";

var TREE_IMG_MINUS  = TreeImageFolderPath +"minus.gif";
var TREE_IMG_FMINUS = TreeImageFolderPath +"fminus.gif";
var TREE_IMG_TMINUS = TreeImageFolderPath +"tminus.gif";
var TREE_IMG_LMINUS = TreeImageFolderPath +"lminus.gif";
var TREE_IMG_RMINUS = TreeImageFolderPath +"rminus.gif";

var TREE_IMG_PLUS  = TreeImageFolderPath +"plus.gif";
var TREE_IMG_FPLUS = TreeImageFolderPath +"fplus.gif";
var TREE_IMG_TPLUS = TreeImageFolderPath +"tplus.gif";
var TREE_IMG_LPLUS = TreeImageFolderPath +"lplus.gif";
var TREE_IMG_RPLUS = TreeImageFolderPath +"rplus.gif";

var TREE_IMG_WHITE = TreeImageFolderPath +"white.gif";

var FOLDER_ClOSE  = TreeImageFolderPath +"folder_close.gif";
var FOLDER_OPEN  = TreeImageFolderPath +"folder_open.gif";
var FILE_ClOSE        = TreeImageFolderPath +"passage.gif";
var FILE_OPEN        = TreeImageFolderPath +"passage.gif";

var TREE_ITEM_IMG_ID_PFIX  = "TreeItemImg_";       // 树项图标<Img>的ID前缀
var TREE_ITEM_TITLE_ID_PFIX = "TreeItemTitle_";    // 树项<Span>的ID前缀
var TREE_ITEMS_DIV_ID_PFIX = "TreeItemsDiv_";     // 树项组<Div>的ID前缀

var BACKCOLOR_SELECTED        = "#0A246A";
var BACKCOLOR_OVER               = "#808DB0";
var BACKCOLOR_OUT                 = "#F5F5F5";
var FRONTCOLOR_SELECTED      = "#FFFFFF";
var FRONTCOLOR_UNSELECTED = "#000000";


// ================================ 定义全局变量 =====================
var numTreeItem = 0;      // 树项的数目
var objPrevSelectedTreeItem = "";      // 预存选则的TreeItem
var strSelectedTreeItemID = "";    //缺省设置的树菜单项


// ================================ 定义对象 =======================
////////////////////////////////////////////////////////
// 函数名称：Tree
// 功能说明：定义Tree Class
// 参数说明：name -- 树的名称，可选
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
function Tree(name)
{
	this.name = name;
	this.childs = [];
}

////////////////////////////////////////////////////////
// 函数名称：Tree.prototype.toString
// 功能说明：定义Tree的视图
// 参数说明：无
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
Tree.prototype.toString = function()
{
	var str = "";
	var level = 0;    // 传入菜单的级别
	
	for(var i = 0; i < this.childs.length; i++)
	{
		if(this.childs.length == 1)    // 如果是唯一的孩子
		{
			this.childs[i].isOnlyChild = true;
		}
		
		if(this.childs.length > 1)    // 如果是最大的孩子
		{
			this.childs[0].isFirstChild = true;
		}
		
		if(this.childs.length - 1 == i)    // 如果是最小的孩子
		{
			this.childs[i].isLastChild = true;
		}

		str += this.childs[i].toString(level+1, "");	
		
	}
	
	return str;
};

////////////////////////////////////////////////////////
// 函数名称：Tree.prototype.addItem
// 功能说明：定义Tree的addItem方法，Tree添加Item
// 参数说明：obj -- TreeItem 对象
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
Tree.prototype.addItem = function(obj)
{
	this.childs.push(obj);
};

////////////////////////////////////////////////////////
// 函数名称：TreeItem
// 功能说明：定义TreeItem Class
// 参数说明：title -- 树项的名称
//           url   -- 树项所连接的地址
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
function TreeItem(title, tip, url, id)
{		
	this.title = title;
	this.tip = (tip==null) ? "" : tip;
	this.url = url;
	this.itemId = id;

	this.isOnlyChild  = false;
	this.isFirstChild = false;
	this.isLastChild  = false;
	
	this.childs = [];
	this.level  = 0;
}

////////////////////////////////////////////////////////
// 函数名称：TreeItem.prototype.toString
// 功能说明：定义TreeItem的视图
// 参数说明：level   -- 树项所在层级
//           strIcon -- 树项父亲的前引图标
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
TreeItem.prototype.toString = function(level, strIcon)
{
	//numTreeItem++;
	//this.itemId = String(numTreeItem);    // 树项的ID放在View中确定
	
	var str = "";
	this.level = level;      // 树项的级别放在View中确定
	
	str += "<table border=0 cellspacing=0 cellpadding=0 ><tr><td nowrap >" ;
	
	str += strIcon;    // 加入前引图标	
	str += this.getLastIcon();    // 加入最后位置的图标
	
	str +=  "<span  id=" + TREE_ITEM_TITLE_ID_PFIX + this.itemId +" title='"+ this.tip + "'  style='cursor:pointer;  padding:2px 2px 2px 2px; vertical-align:bottom; *vertical-align:middle; '  url='" + this.url + "'  onclick='TreeTitle_OnClickEvent(this, " + this.childs.length + ")'"; 
		
	str +=  " onmouseover='TreeTitle_OnOverEvent(this,\"over\")' onmouseout='TreeTitle_OnOverEvent(this,\"out\")' >" + this.title + "</span>";    // 加入title
	
	str += "</td></tr></table>";
	
	str += this.getAllChilds(strIcon);  //递归出所有孩子
	
	return str;
};

////////////////////////////////////////////////////////
// 函数名称：TreeItem.prototype.getLastIcon
// 功能说明：获取TreeItem的最后位置的图标
// 参数说明：无
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
TreeItem.prototype.getLastIcon = function()
{
	var level = this.level;
	var str = "";
	
	// 最后位置的图标判断	
	if(this.childs.length == 0)    // 如果没有孩子
	{	
		
		if(this.isOnlyChild & (level == 1))  // 如果是唯一的孩子且在1级
		{
			str += "<img src=" + TREE_IMG_R + " align=middle border=0 >" ;
		}
		else if(this.isFirstChild & (level == 1))  // 如果是最大的孩子且在1级
		{
			str += "<img src=" + TREE_IMG_F + " align=middle border=0 >";
		}
		else if(this.isLastChild)    // 如果是排行最小的孩子
		{
			str += "<img src=" + TREE_IMG_L + " border=0 align=middle >";
		}
		else
		{
			str += "<img src=" + TREE_IMG_T + " border=0 align=middle >";				  
		}
		str += "<img src=" + FILE_ClOSE + " border=0 align=middle >";

	}
	else      // 如果有孩子
	{
		if(this.isOnlyChild & (level == 1))    // 如果是唯一的孩子且在1级
		{
			str += "<img src=" + TREE_IMG_RPLUS + " align=middle border=0 ";
		}
		else if(this.isFirstChild & (level == 1))    // 如果是最大的孩子且在1级
		{
			str += "<img src=" + TREE_IMG_FPLUS + " align=middle border=0 ";
		}
		else if(this.isLastChild)    // 如果是排行最小的孩子
		{
			str += "<img src=" + TREE_IMG_LPLUS + " align=middle border=0 ";
		}
		else
		{
			str += "<img src=" + TREE_IMG_TPLUS + " align=middle border=0 "	;				  
		}
		
		str += "id=" + TREE_ITEM_IMG_ID_PFIX + this.itemId + " style='cursor:pointer' onclick='TreeImg_OnClickEvent(this)'  >";      // 添加图标ID,添加图标Click事件
		str += "<img src=" + FOLDER_ClOSE + " border=0 align=middle >";
	}	
	return str;
};

////////////////////////////////////////////////////////
// 函数名称：TreeItem.prototype.getAllChilds
// 功能说明：获取所有孩子的视图
// 参数说明：strIcon - 父亲的前引图标
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
TreeItem.prototype.getAllChilds = function(strIcon)
{
	var str = "";
	
	// 前引图标判断
	if(!this.isLastChild)    // 不是最小的孩子且不在1级
	{
		strIcon += "<img src=" + TREE_IMG_I + " border=0 align=middle >";
	}
	else if(this.isLastChild)    // 是最小的孩子且不在1级
	{
		strIcon += "<img src=" + TREE_IMG_WHITE + " border=0 align=middle >";
	}
	
	str += "<div id=" + TREE_ITEMS_DIV_ID_PFIX + this.itemId + " style='display:none'>"    // 菜单项隐藏
	for(var i=0; i<this.childs.length; i++)
	{
		if(this.childs.length == 1)    // 如果是唯一的孩子
		{
			this.childs[i].isOnlyChild = true;
		}
		
		if(this.childs.length > 1)    // 如果是最大的孩子
		{
			this.childs[0].isFirstChild = true;
		}
		
		if(this.childs.length - 1 == i)    // 如果是最小的孩子
		{
			this.childs[i].isLastChild = true;
		}
		
		str += this.childs[i].toString(this.level+1, strIcon);	    // 递归调用
		
	}
	str += "</div>";	
	
	return str;
};

////////////////////////////////////////////////////////
// 函数名称：TreeItem.prototype.addItem
// 功能说明：定义TreeItem的addItem方法，TreeItem添加子TreeItem
// 参数说明：obj -- TreeItem对象
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
TreeItem.prototype.addItem = function(obj)
{
	this.childs.push(obj);	
};



// =============================== 定义事件 =========================

////////////////////////////////////////////////////////
// 函数名称：TreeImg_OnClickEvent
// 功能说明：定义图标单击事件
// 参数说明：obj -- 图标对象
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
function TreeImg_OnClickEvent(obj)
{
	if(obj.src.indexOf("plus.gif")>0)
	{
		changeItemStatus(obj,"expand");
	}
	else if(obj.src.indexOf("minus.gif")>0)
	{
		changeItemStatus(obj,"collapse");	
	}
}

////////////////////////////////////////////////////////
// 函数名称：TreeTitle_OnClickEvent
// 功能说明：定义标题单击事件
// 参数说明：obj -- 标题对象
//           childslength -- TreeItem的孩子数
// 开发人员：tu.zhengjun
// 完成日期：2007-2-25
///////////////////////////////////////////////////////
function TreeTitle_OnClickEvent(obj, childslength)
{
	// 改变title的背景色
	if(objPrevSelectedTreeItem != "")
	{
		objPrevSelectedTreeItem.style.background = "";
		objPrevSelectedTreeItem.style.color = "";

		if(objPrevSelectedTreeItem.previousSibling.src.indexOf("file_") != -1)
			objPrevSelectedTreeItem.previousSibling.src = objPrevSelectedTreeItem.previousSibling.src.replace("open.gif", "close.gif"); //改变文件图标

	}
	obj.style.background = BACKCOLOR_SELECTED ;
	obj.style.color = FRONTCOLOR_SELECTED ;
	objPrevSelectedTreeItem = obj;

	obj.previousSibling.src = obj.previousSibling.src.replace("close.gif", "open.gif");  //改变文件图标

	// 收缩树项
	if(childslength > 0)
	{
		var objDest = document.getElementById(obj.id.replace("Title", "Img"));
		TreeImg_OnClickEvent(objDest)    // 调用图标单击事件函数
	}
	
	// 重新载入内容页面
	var url = obj.getAttribute("url");
	if(url != "null" && url != "" && url != "undefined")  
	{
		top.right.location.href = url;
	}
	
}


////////////////////////////////////////////////////////
// 函数名称：TreeTitle_OnOverEvent
// 功能说明：定义标题鼠标悬浮事件
// 参数说明：obj -- 标题对象
//           childslength -- TreeItem的孩子数
// 开发人员：tu.zhengjun
// 完成日期：2007-9-14
///////////////////////////////////////////////////////
function TreeTitle_OnOverEvent(obj, str)
{
	if(obj == objPrevSelectedTreeItem) return false;

	if(str == "over")
	{
		obj.style.background = BACKCOLOR_OVER ;
		obj.style.color = FRONTCOLOR_SELECTED ;
	}
	else
	{
		obj.style.background = BACKCOLOR_OUT ;
		obj.style.color = FRONTCOLOR_UNSELECTED ;
	}
}


// ================================= 定义方法 ===========================

////////////////////////////////////////////////////////
// 功能说明：展开收缩树项
// 开发人员：tu.zhengjun
// 完成日期：2007-9-14
////////////////////////////////////////////////////////
function changeItemStatus(obj,str)
{
	if(str== null && str == "")
	{
		return false;
	}

	if(str=="expand")
	{
		obj.src = obj.src.replace("plus.gif", "minus.gif");
		obj.nextSibling.src = obj.nextSibling.src.replace("close.gif", "open.gif");
		
		var strDiv  = obj.id.replace(TREE_ITEM_IMG_ID_PFIX, TREE_ITEMS_DIV_ID_PFIX);
		var objDest = document.getElementById(strDiv);	
		objDest.style.display = "";
	}
	else if(str=="collapse")
	{
		obj.src = obj.src.replace("minus.gif", "plus.gif");
		obj.nextSibling.src = obj.nextSibling.src.replace("open.gif", "close.gif");
		
		var strDiv  = obj.id.replace(TREE_ITEM_IMG_ID_PFIX, TREE_ITEMS_DIV_ID_PFIX);
		var objDest = document.getElementById(strDiv);	
		objDest.style.display = "none";		
	}
}

////////////////////////////////////////////////////////
// 功能说明：展开收缩所有树项
// 开发人员：tu.zhengjun
// 完成日期：2007-9-14
////////////////////////////////////////////////////////
function changeAllItemsStatus(str)
{
	if(str == null || str == "")
	{
		return false;
	}

	var objItems = document.getElementsByTagName("img");

	for(var i=0; i < objItems.length; i++)
	{
		if(objItems[i].src.indexOf("minus.gif") != -1 || objItems[i].src.indexOf("plus.gif") != -1)
		{
			changeItemStatus(objItems[i],str)
		}
	}
}


/////////////////////////////////////////////////////////
// 函数名称：getTreeSelectedItem(xmlPath)
// 功能说明：获取XML文件缺省属性的值
// 参数说明：xmlPath -- xml文件地址
//                 ditem--XML项的属性
// 开发人员：tu.zhengjun
// 完成日期：2008-1-25
///////////////////////////////////////////////////////
function getTreeSelectedItemID(xmlPath)
{
	var xmlDOM = createXMLDOM();
	xmlDOM.async = "false";
	xmlDOM.load(xmlPath);
	var xmlNode = xmlDOM.documentElement; 
	var pid="0";

	findSelectedTreeItemID(pid, xmlNode)
}

/////////////////////////////////////////////////////////
// 函数名称：findSelectedTreeItem(pNode)
// 功能说明：遍历树的节点
// 参数说明：pNode -- 父节点
// 开发人员：tu.zhengjun
// 完成日期：2008-1-25
///////////////////////////////////////////////////////
function findSelectedTreeItemID(pid, parentNode)
{
	if(parentNode != null)
	{
		for (var i = 0; i < parentNode.childNodes.length; i++)
		{	
			if(xmlNode.childNodes[i].nodeType == 1)    //FF
			{	
				var myid = pid + "_" + i;
				if (parentNode.childNodes[i].getAttribute("Selected")=="true")
				{
						strSelectedTreeItemID = myid;
				}
				findSelectedTreeItemID(myid, parentNode.childNodes[i]);
			}
		}
	}	
}

/////////////////////////////////////////////////////////
// 函数名称：clickSelectedTreeItem(id)
// 功能说明：遍历树的节点
// 参数说明：id -- 父节点
// 开发人员：tu.zhengjun
// 完成日期：2008-1-28
///////////////////////////////////////////////////////
function clickSelectedTreeItem(id)
{
	changeAllItemsStatus("collapse");

	var strID = id.split("_");
	var pid = "0";
	for (var i=1; i < strID.length; i++)
	{
		pid +=  "_" + strID[i];
		if (i == (strID.length-1))
		{
			var obj=document.getElementById(TREE_ITEM_TITLE_ID_PFIX+pid);
			TreeTitle_OnClickEvent(obj);
			break;
		}
		var obj=document.getElementById(TREE_ITEM_IMG_ID_PFIX+pid);
		TreeImg_OnClickEvent(obj);
	}
}

/////////////////////////////////////////////////////////
// 函数名称：initOutlookMenu
// 功能说明：初始化菜单状态
// 参数说明：xmlPath -- xml文件地址
// 开发人员：tu.zhengjun
// 完成日期：2008-1-25
///////////////////////////////////////////////////////
function initTreeMenu(xmlPath)
{
	
	getTreeSelectedItemID(xmlPath);
	if(strSelectedTreeItemID == "")    //判断左侧菜单的初始内容载入页面
	{
		var target = top.topframe.frametopmenu.target;
		if(target != "null" && target != null && target != "")
		{
			top.right.location.href= target;	
		}
	 	return;			
	}
	clickSelectedTreeItem(strSelectedTreeItemID);

}

// =========================== XML DOM ========================
/////////////////////////////////////////////////////////
// 函数名称：createTreeMenu
// 功能说明：TreeMenu的DOM创建方式
// 参数说明：xmlPath -- xml文件地址
// 开发人员：tu.zhengjun
// 完成日期：2007-10-12
///////////////////////////////////////////////////////
function createTreeMenu(xmlPath)
{
	var xmlDOM = createXMLObject(xmlPath)
	xmlDOM.async = false;
	var treemenu = new Tree(); 
	var xmlNode = xmlDOM.documentElement; 
	createTreeNode(treemenu, xmlNode, "0");
	document.write(treemenu);
	var urlParts=document.URL.split("?");
    if (urlParts[1])        //urlParts[1]为传参数部分。如果存在，进行分解
    {        
        var param =new Array;
        var parameterParts=urlParts[1].split("&");
        for (i=0;i<parameterParts.length;i++)
        {
            var pairParts=parameterParts[i].split("=");
            param[i]=pairParts[1];        //param数组存各参数
        }
 	}

	if (param[0] != "true")    //判断页面是否需要初始化
	{
		initTreeMenu(xmlPath);    //初始化菜单状态
	}
}

/////////////////////////////////////////////////////////
// 函数名称：createTreeNode
// 功能说明：TreeNode的DOM创建方式
// 参数说明：parentNode -- Model的父对象
//			 xmlNode -- DOM的父节点
// 开发人员：tu.zhengjun
// 完成日期：2007-10-12
///////////////////////////////////////////////////////
function createTreeNode(parentNode, xmlNode, pid)
{
	for(var i = 0; i < xmlNode.childNodes.length; i++)
	{
		if(xmlNode.childNodes[i].nodeType == 1)    //FF
		{	
			var title = xmlNode.childNodes[i].getAttribute("Title");
			var tip = xmlNode.childNodes[i].getAttribute("Tip");
			var target = xmlNode.childNodes[i].getAttribute("Target");
			var id = pid + "_" + i;
			var childNode = new TreeItem(title, tip, target, id);
			parentNode.addItem(childNode);
	
			createTreeNode(childNode, xmlNode.childNodes[i], id);
		}
	}
}

 //创建xml文档对象  
function createXMLObject(url)
{  
	try //Internet Explorer
	{
		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
		
	}
	catch(e)
	{
		try //Firefox, Mozilla, Opera, etc.
		{
		xmlDoc=document.implementation.createDocument("","",null);
		}
		catch(e) {alert(e.message)}
	}

	try 
	{
		xmlDoc.async=false;
		xmlDoc.load(url);
		return xmlDoc;
		//document.write("xmlDoc is loaded, ready for use");
	}
	catch(e) {alert(e.message)}
 }
 
function killErrors() { 
return true; 
} 
window.onerror = killErrors;
