using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LCL.MvcExtensions
{
    public class ActionExtension : ActionFilterAttribute
    {
        protected string m_controller;
        protected string m_action;
        protected bool m_atBegining = false;

        public ActionExtension()
        {
        }

        public ActionExtension(string action, string controller, bool atBegining = false)
        {
            m_controller = controller;
            m_action = action;
            m_atBegining = atBegining;
        }
        protected CompoundActionResult CompoundResult(dynamic context)
        {
            CompoundActionResult r = context.Result as CompoundActionResult;
            if (r == null)
            {
                r = new CompoundActionResult();
                r.AddResult(context.Result);
                context.Result = r;
            }
            return r;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(m_action) && !string.IsNullOrWhiteSpace(m_controller))
                AddActionResponse(m_action, m_controller, filterContext);
        }

        protected void AddActionResponse(string action, string controller, dynamic context)
        {
            HtmlHelper helper = GetHtmlHelper(context.Controller as Controller);
            var cr = new ContentResult() { Content = helper.Action(action, controller).ToString() };

            if(m_atBegining)
                CompoundResult(context).AddResultToBegining(cr);
            else
                CompoundResult(context).AddResult(cr);
        }

        public HtmlHelper GetHtmlHelper(Controller controller)
        {
            var viewContext = new ViewContext(controller.ControllerContext, new FakeView(), controller.ViewData, controller.TempData, TextWriter.Null);
            return new HtmlHelper(viewContext, new ViewPage());
        }

        public class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
