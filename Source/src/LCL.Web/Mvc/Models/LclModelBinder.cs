using System.ComponentModel;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class LclModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseLclModel)
            {
                ((BaseLclModel)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }

        protected override PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
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
