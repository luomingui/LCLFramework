using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.Web.Template
{
    /// <summary>
    /// 模板相关。
    /// </summary>
    public sealed class TemplateRelevantAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
            {
                var currentTemplate = this.GetCurrentTemplate(filterContext.RequestContext);
                var template = string.IsNullOrEmpty(currentTemplate) ? TemplateService.DefaultTemplateName : currentTemplate;
                var controller = filterContext.RequestContext.RouteData.Values["Controller"].ToString();
                var action = filterContext.RequestContext.RouteData.Values["Action"].ToString();

                if (string.IsNullOrWhiteSpace(viewResult.ViewName)&&!string.IsNullOrWhiteSpace(template))
                {
                    viewResult.ViewName = string.Format(
                        TemplateService.TemplatePathFormat,
                        controller,
                        template,
                        action);

                    return;
                }
            }

            base.OnResultExecuting(filterContext);
        }

        private string GetCurrentTemplate(RequestContext context)
        {
            return TemplateService.Current.GetTemplate(context);
        }
    }
     
}
