using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace LCL.WebAPI.Utility
{
    /// <summary>
    /// SwaggerUI 有上传文件的功能和添加自定义HTTP Header 做法类似，
    /// 只是我们通过特殊的设置来标示API具有上传文件的功能
    /// 
    ///void TestSwaggerUploadFile([SwaggerFileUpload] file){ }
    /// </summary>
    public class UploadFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var parameters = apiDescription.ActionDescriptor.GetParameters();
            foreach (HttpParameterDescriptor parameterDesc in parameters)
            {
                var fileUploadAttr = parameterDesc.GetCustomAttributes<SwaggerFileUploadAttribute>().FirstOrDefault();
                if (fileUploadAttr != null)
                {
                    operation.consumes.Add("multipart/form-data");

                    operation.parameters.Add(new Parameter
                    {
                        name = parameterDesc.ParameterName + "_file",
                        @in = "formData",
                        description = "file to upload",
                        required = fileUploadAttr.Required,
                        type = "file"
                    });
                }
            }
        }
    }
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SwaggerFileUploadAttribute : Attribute
    {
        public bool Required { get; private set; }

        public SwaggerFileUploadAttribute(bool Required = true)
        {
            this.Required = Required;
        }
    }
}