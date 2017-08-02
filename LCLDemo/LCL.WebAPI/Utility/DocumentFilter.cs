using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace LCL.WebAPI.Utility
{
    public class DocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// This method is for applying the filter
        /// </summary>
        /// <param name="swaggerDoc">Swagger Document</param>
        /// <param name="schemaRegistry">Schema Registry</param>
        /// <param name="apiExplorer">API Explorer</param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var methods = swaggerDoc.paths.Select(i => i.Value);
            List<string> tags = new List<string>();
            foreach (var method in methods)
            {
                if (method.delete != null)
                {
                    tags.AddRange(method.delete.tags);
                }

                if (method.get != null)
                {
                    tags.AddRange(method.get.tags);
                }

                if (method.put != null)
                {
                    tags.AddRange(method.put.tags);
                }

                if (method.post != null)
                {
                    tags.AddRange(method.post.tags);
                }

                if (method.patch != null)
                {
                    tags.AddRange(method.patch.tags);
                }
            }

            swaggerDoc.tags = new List<Tag>();
            foreach (var tag in tags)
            {
                swaggerDoc.tags.Add(new Tag() { name = tag, description = "This is a group of methods for " + tag });
            }
        }
    }
}