using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 创建 ModelBinder 自动 Trim 所有字符串
    /// http://www.cnblogs.com/ldp615/archive/2010/12/29/asp-net-mvc-create-model-binder-to-trim-all-string.html
    /// </summary>
    public class StringTrimModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = base.BindModel(controllerContext, bindingContext);
            if (value is string) return (value as string).Trim();
            return value;
        }
    }
}
