using System;
using System.Collections;
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
            attributes = new Hashtable();
            children = new List<EasyUITreeModel>();
        }
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string parentId { get; set; }
        public string parentName { get; set; }
        public string iconCls { get; set; }
        public bool selected { get; set; }
        public bool Checked { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public Hashtable attributes { get; set; }
        public List<EasyUITreeModel> children { get; set; }
    }
}
