using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// AJAX调用JsonResult方法并返回自定义错误信息
    /// http://diaosbook.com/Post/2012/8/1/invoking-jsonresult-and-return-error-message-in-aspnet-mvc-ajax
    /// <code>
    /// [JsonExceptionFilterAttribute]
    /// public JsonResult FuckJson()
    /// {
    ///    try
    ///    {
    ///        throw new Exception("oh shit!");
    ///        return new JsonResult() 
    ///        {
    ///            Data = new List<string>() { "fuck", "shit" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet
    ///        };
    ///    }
    ///    catch (Exception ex)
    ///    {
    ///        throw ex;
    ///    }
    /// }
    /// AJAX请求代码，增加一个error的处理：
    /// $.ajax({
    ///    url: "/Fuck/FuckJson",
    ///    data: "",
    ///    dataType: "json",
    ///    type: "POST",
    ///    contentType: "application/json; charset=utf-8",
    ///    dataFilter: function (data) {
    ///        return data;
    ///    },
    ///    success: function (data) {
    ///        alert(data);
    ///    },
    ///    error: function (fuckedObject) {
    ///        try {
    ///            var json = $.parseJSON(fuckedObject.responseText);
    ///            alert(json.errorMessage);
    ///        } catch(e) { 
    ///            alert('something bad happened');
    ///        }
    ///}
    ///})
    /// </code>
    /// </summary>
    public class JsonExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = filterContext.Exception.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
