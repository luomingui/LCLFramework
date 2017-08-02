using LCL.Domain.Entities;
using LCL.Domain.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace LCL.WebAPI
{
    /// <summary>
    /// 自定义Controller Selector
    /// http://www.cnblogs.com/TianFang/p/5185866.html
    /// http://www.cnblogs.com/sword-successful/p/4945807.html
    /// </summary>
    public class MyControllerSelector : DefaultHttpControllerSelector
    {
        private const string AREA_ROUTE_VARIABLE_NAME = "area";
        private const string CATEGORY_ROUTE_VARIABLE_NAME = "category";
        private const string THE_FIX_CONTROLLER_FOLDER_NAME = "Controllers";

        private readonly HttpConfiguration m_configuration;
        private readonly Lazy<ConcurrentDictionary<string, Type>> m_apiControllerTypes;

        static Dictionary<string, Type> _entityMap;
        static MyControllerSelector()
        {
            _entityMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

            _entityMap["User"] = typeof(User);
            _entityMap["Role"] = typeof(Role);
            _entityMap["Product"] = typeof(Product);
            _entityMap["Category"] = typeof(Category);
            _entityMap["SalesLine"] = typeof(SalesLine);
        }

        public MyControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            m_configuration = configuration;
            m_apiControllerTypes = new Lazy<ConcurrentDictionary<string, Type>>(GetAllControllerTypes);
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var dec = GetApiController(request);

            return GetEntityController(request, dec);
        }
        private HttpControllerDescriptor GetEntityController(HttpRequestMessage request, HttpControllerDescriptor dec)
        {
            var controllerName = base.GetControllerName(request);

            Type entityType = null;
            if (!_entityMap.TryGetValue(controllerName.ToLower(), out entityType))
            {
                return base.SelectController(request);
            }
            if (dec == null)
            {
                dec = new HttpControllerDescriptor(m_configuration, controllerName, typeof(Controllers.EntityController<>).MakeGenericType(entityType));
            }
            else
            {
                dec.Configuration = m_configuration;
                dec.ControllerName = controllerName;
                dec.ControllerType = typeof(Controllers.EntityController<>).MakeGenericType(entityType);
            }
            return dec;
        }

        private static string GetRouteValueByName(HttpRequestMessage request, string strRouteName)
        {
            IHttpRouteData data = request.GetRouteData();
            if (data.Values.ContainsKey(strRouteName))
            {
                return data.Values[strRouteName] as string;
            }
            return null;
        }

        private static ConcurrentDictionary<string, Type> GetAllControllerTypes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<string, Type> types = assemblies.SelectMany(a => a.GetTypes().Where(t => !t.IsAbstract && t.Name.EndsWith(ControllerSuffix, StringComparison.OrdinalIgnoreCase) && typeof(IHttpController).IsAssignableFrom(t))).ToDictionary(t => t.FullName, t => t);
            return new ConcurrentDictionary<string, Type>(types);
        }

        private HttpControllerDescriptor GetApiController(HttpRequestMessage request)
        {
            string strAreaName = GetRouteValueByName(request, AREA_ROUTE_VARIABLE_NAME);
            string strCategoryName = GetRouteValueByName(request, CATEGORY_ROUTE_VARIABLE_NAME);
            string strControllerName = GetControllerName(request);
            Type type;
            try
            {
                type = GetControllerType(strAreaName, strCategoryName, strControllerName);
            }
            catch (Exception)
            {
                return null;
            }
            return new HttpControllerDescriptor(m_configuration, strControllerName, type);
        }

        private Type GetControllerType(string areaName, string categoryName, string controllerName)
        {
            IEnumerable<KeyValuePair<string, Type>> query = m_apiControllerTypes.Value.AsEnumerable();
            string strControllerSearchingName;
            if (string.IsNullOrEmpty(areaName))
            {
                strControllerSearchingName = THE_FIX_CONTROLLER_FOLDER_NAME + "." + controllerName;
            }
            else
            {
                if (string.IsNullOrEmpty(categoryName))
                {
                    strControllerSearchingName = THE_FIX_CONTROLLER_FOLDER_NAME + "." + areaName + "." + controllerName;
                }
                else
                {
                    strControllerSearchingName = THE_FIX_CONTROLLER_FOLDER_NAME + "." + areaName + "." + categoryName + "." + controllerName;
                }
            }
            return query.Where(x => x.Key.IndexOf(strControllerSearchingName, StringComparison.OrdinalIgnoreCase) != -1).Select(x => x.Value).Single();
        }

    }
}