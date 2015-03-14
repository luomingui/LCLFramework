using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public class LCLModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseLCLModel)
            {
                ((BaseLCLModel)model).BaseMvcModel(controllerContext, bindingContext);
            }
            return model;
        }

        protected override System.ComponentModel.PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            bindingContext.PropertyFilter = new System.Predicate<string>(pred);
            var values = base.GetModelProperties(controllerContext, bindingContext);
            return values;
        }

        protected bool pred(string target)
        {
            return true;
        }
    }
}
