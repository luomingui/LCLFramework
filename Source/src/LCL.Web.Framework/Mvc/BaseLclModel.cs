using System.Collections.Generic;
using System.Web.Mvc;

namespace LCL.Web.Framework.Mvc
{
    /// <summary>
    /// Base  model
    /// model binders ModelBinders.Binders.Add(typeof(BaseLclModel), new LclModelBinder());
    /// </summary>
    public partial class BaseLclModel
    {
        public BaseLclModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
    }

    /// <summary>
    /// Base nopCommerce entity model
    /// </summary>
    public partial class BaseNopEntityModel : BaseLclModel
    {
        public virtual int Id { get; set; }
    }
}
