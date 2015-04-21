using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFDemo.Document
{
    /// <summary>
    /// 属性文档对象
    /// </summary>
    class PropertyDoc
    {
        public string PropertyName { get; set; }

        public string Label { get; set; }

        public string Comment { get; set; }

        public override string ToString()
        {
            StringBuilder list = new StringBuilder();
            list.AppendLine("PropertyName: " + PropertyName + " Comment: " + Comment + " ");
            return list.ToString();
        }
    }
}
