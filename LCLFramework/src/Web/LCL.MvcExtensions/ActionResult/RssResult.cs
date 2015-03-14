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
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace LCL.MvcExtensions
{
    /* 示例
public ActionResult Rss()
{
    List<SyndicationItem> items = new SyndicationItem[] 
    {
        new SyndicationItem("Blog 1", "Joe's 1st Blog", null),
        new SyndicationItem("Blog 1", "Joe's 1st Blog", null),
        new SyndicationItem("Blog 1", "Joe's 1st Blog", null)
    }.ToList();

    SyndicationFeed feed = new SyndicationFeed("Joe's Blog Posts", "http://blogs.msdn.com/jowardel RSS Feed"
    ,Request.Url, items);
                
    return new RssResult(feed);
}
     */
    public class RssResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public RssResult() { }

        public RssResult(SyndicationFeed feed)
        {
            this.Feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            Rss20FeedFormatter formatter = new Rss20FeedFormatter(this.Feed);

            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}