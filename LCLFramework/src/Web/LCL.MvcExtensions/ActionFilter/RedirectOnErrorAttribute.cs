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
using System.Linq;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 重定向错误
    /// </summary>
    public abstract class RedirectOnErrorAttribute : ActionFilterAttribute
    {

        public Type Type { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            //### check for errors
            if (!Validate(filterContext))
                return;

            //### make sure the Type property is an Exception
            if (Type != null && !typeof(System.Exception).IsAssignableFrom(Type))
                throw new ArgumentException("RedirectOnErrorAttribute's Type property's value must derive from System.Exception.");

            //### if no exception occurred, stop processing this filter
            if (filterContext.Exception == null)
                return;

            //### get inner exception unless it is null (this should never happen?)
            Exception ex = filterContext.Exception.InnerException ?? filterContext.Exception;

            //### if exception was thrown because of Response.Redirect, ignore it
            if (ex.GetType() == typeof(System.Threading.ThreadAbortException))
                return;
            else if (Type == typeof(System.Threading.ThreadAbortException))
                throw new ArgumentException("Cannot catch exceptions of type 'ThreadAbortException'.");

            //### if the specified Type matches the thrown exception, process it
            if (IsExactMatch(ex))
                Redirect(filterContext);
            //### if this attribute has no specified Type, investigate further (this attribute is a catch-all error handler)
            else if (Type == null)
            {

                //### loop through all other RedirectToUrlOnErrorAttribute on this method
                foreach (RedirectOnErrorAttribute att in GetAllAttributes(filterContext))
                    //### ignore self
                    if (att.GetHashCode() == this.GetHashCode())
                        continue;
                    //### if another catch-all attribute is found, throw an exception
                    else if (att.Type == null)
                        throw new ArgumentException("Only one RedirectOnErrorAttribute per Action may be specified without its Type property provided.");
                    //### if an exact match is found, stop processing the catch-all. that attribute has priority
                    else if (att.IsExactMatch(ex))
                        return;

                //### no exact matches were found. if the specified Type for the catch-all fits, process here
                Redirect(filterContext);

            }
            else
                //### specified Type was not null, but did not match the thrown exception. don't process
                return;

        }
        
        public bool IsExactMatch(Exception exception)
        {
            if (Type != null && exception.GetType() == Type)
                return true;
            else
                return false;
        }

        private List<RedirectOnErrorAttribute> GetAllAttributes(ActionExecutedContext filterContext)
        {
            return filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(RedirectOnErrorAttribute), false)
                .Select(a => a as RedirectOnErrorAttribute)
                .ToList();
        }

        protected abstract bool Validate(ActionExecutedContext filterContext);
        protected abstract void Redirect(ActionExecutedContext filterContext);

    }
}
