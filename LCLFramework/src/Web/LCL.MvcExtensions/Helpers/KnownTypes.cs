using System;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{

    internal static class KnownTypes
    {

        public static readonly Type ModelBinderType = typeof(IModelBinder);

        public static readonly Type ControllerActivatorType = typeof(IControllerActivator);

        public static readonly Type ControllerType = typeof(Controller);

        public static readonly Type ActionInvokerType = typeof(IActionInvoker);

        public static readonly Type FilterType = typeof(IMvcFilter);

        public static readonly Type FilterAttributeType = typeof(FilterAttribute);

        public static readonly Type FilterProviderType = typeof(IFilterProvider);

        public static readonly Type ViewPageActivatorType = typeof(IViewPageActivator);

        public static readonly Type ViewType = typeof(IView);

        public static readonly Type ViewEngineType = typeof(IViewEngine);

        public static readonly Type ActionResultType = typeof(ActionResult);

        public static readonly Type ValueProviderFactoryType = typeof(ValueProviderFactory);

    }
}