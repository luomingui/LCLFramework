using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public class CompoundActionResult : ActionResult
    {
        List<ActionResult> m_results = new List<ActionResult>();

        public void AddResult(ActionResult result)
        {
            m_results.Add(result);
        }
        public void AddResultToBegining(ActionResult result)
        {
            m_results.Insert(0, result);
        }
        public void WrapResults(ActionResult beginResult, ActionResult endResult)
        {
            AddResultToBegining(beginResult);
            AddResult(endResult);
        }
        public void WrapResults(string beginResult, string endResult)
        {
            AddResultToBegining(new ContentResult() { Content = beginResult });
            AddResult(new ContentResult() { Content = endResult });
        }

        public override void ExecuteResult(ControllerContext context)
        {
            foreach (ActionResult result in m_results)
                if (result != null)
                    result.ExecuteResult(context);
        }
    }
}
