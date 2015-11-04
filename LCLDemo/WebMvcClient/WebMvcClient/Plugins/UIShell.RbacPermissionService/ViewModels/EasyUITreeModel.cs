using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.MvcExtensions
{
    public class EasyUITreeModel
    {
        public EasyUITreeModel()
        {
            state = "closed";
            Checked = false;
            children = null;
            iconCls = "icon-bullet_green";
        }
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string parentId { get; set; }
        public string iconCls { get; set; }
        public bool Checked { get; set; }
        public string attributes { get; set; }
        public List<EasyUITreeModel> children { get; set; }
    }
}
