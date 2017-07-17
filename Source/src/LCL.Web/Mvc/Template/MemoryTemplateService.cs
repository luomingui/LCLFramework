using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace LCL.Web.Template
{
    /// <summary>
    /// 基于内存的模板服务。
    /// </summary>
    public sealed class MemoryTemplateService : ITemplateService
    {
        private string _template = TemplateService.DefaultTemplateName;

        /// <inheritdoc />
        public string GetTemplate(RequestContext context)
        {
            return _template;
        }

        /// <inheritdoc />
        public void SetTemplate(RequestContext context, string template)
        {
            _template = template;
        }
    }
}
