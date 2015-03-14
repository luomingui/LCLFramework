/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace LCL.MvcExtensions
{
    public class XmlResult : ActionResult
    {
        public XmlResult(Object data)
        {
            this.Data = data;
        }
        public Object Data{ get;set;}
        public override void ExecuteResult(ControllerContext context)
        {
            if (Data == null)
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }
            context.HttpContext.Response.ContentType = "application/xml";
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(Data.GetType());
                xs.Serialize(ms, Data);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    context.HttpContext.Response.Output.Write(sr.ReadToEnd());
                }
            }
        }
    }
}