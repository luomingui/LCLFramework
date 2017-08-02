using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace LCL.WebAPI.Utility
{
    /// <summary>
    /// /http://www.cnblogs.com/sessionliang/p/6688372.html
    /// </summary>
    public class SwaggerVersionHelper
    {
        public static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {

            var attr = apiDesc.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<VersionedRoute>().FirstOrDefault();
            if (attr == null)
            {
                if (targetApiVersion == "v2")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



            int targetVersion;
            targetApiVersion = targetApiVersion.TrimStart('v');

            if (attr.Version != 0 && int.TryParse(targetApiVersion, out targetVersion))
            {
                return attr.Version == targetVersion;
            };

            return false;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class VersionedRoute : Attribute
    {
        public VersionedRoute(string name, int version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }
        public int Version { get; set; }
    }
}