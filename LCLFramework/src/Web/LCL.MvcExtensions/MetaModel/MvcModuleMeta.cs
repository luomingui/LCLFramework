using LCL.MetaModel;
using LCL.Reflection;

namespace LCL.MvcExtensions
{
    public class MvcModuleMeta : ModuleMeta
    {
        protected override void CustomControllerActionList()
        {
            if (EntityType != null)
            {
                if (string.IsNullOrWhiteSpace(KeyLabel)) KeyLabel = EntityType.Name;
                System.Reflection.MethodInfo[] mi = EntityType.GetMethods();
                foreach (System.Reflection.MethodInfo m in mi)
                {
                    if (m.IsPublic)
                    {
                        //if (typeof(System.Web.Mvc.ActionResult).IsAssignableFrom(m.ReturnType))
                        {
                            string name = m.Name;
                            string label = m.Name;
                            var permissssion = m.GetSingleAttribute<PermissionAttribute>();
                            if (permissssion != null)
                            {
                                name = permissssion.Name;
                                label = permissssion.Label;

                                CustomOpertions.Add(new ModuleOperation { Label = label, Name = name });
                            }
                        }
                    }
                }
            }
        }
    }
}
