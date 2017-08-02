using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Swashbuckle.Swagger;

namespace LCL.WebAPI.Utility
{
    /// <summary>
    /// 添加自定义HTTP Header
    /// 在 SwaggerConfig.cs 的 EnableSwagger 配置匿名方法类添加一行注册代码
    /// c.OperationFilter<HttpHeaderFilter>();
    /// http://www.360doc.com/content/17/0719/10/45553713_672511391.shtml
    /// </summary>
    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            //判断是否添加权限过滤器
            var isAuthorized = filterPipeline.Select(filterInfo=>filterInfo.Instance)
                .Any(filter=>filter is IAuthorizationFilter);
            //判断是否允许匿名方法
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAuthorized && !allowAnonymous) {
                operation.parameters.Add(new Parameter {
                    name="access-key",
                    @in="header",
                    description="用户访问Key",
                    required=false,
                    type="string"
                });
            }
        }
    }
}