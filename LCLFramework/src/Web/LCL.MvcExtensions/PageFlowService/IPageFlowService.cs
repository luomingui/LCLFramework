using LCL.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.MvcExtensions
{
    public class PageNode
    {
        private const int DEFAULT_PRIORITY = 50;
        private const string NAME_ATTRIBUTE = "Name";
        private const string PRIORITY_ATTRIBUTE = "Priority";
        private const string VALUE_ATTRIBUTE = "Value";
        public PageNode()
        { 

        }
        public IPlugin Bundle { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Value { get; set; }
    }
    public class PageNodeContainer
    {
        private List<PageNode> _pagenodes = new List<PageNode>();
        public PageNode AddPageNode(PageNode module)
        {
            this._pagenodes.Add(module);

            return module;
        }
        public IEnumerable<PageNode> GetPageNodes()
        {
            return this._pagenodes;
        }
        public PageNode FindPageNode(string keyName)
        {
            foreach (var root in this._pagenodes)
            {
                if (root.Name == keyName)
                {
                    return root;
                }
            }
            return null;
        }
        
        public string DefaultLayoutPageNode
        {
            get{
                var pagenode = FindPageNode("LayoutPage");
                if (pagenode != null)
                    return pagenode.Value;
                else
                    return "";
            }
        }
    }
    public class PageFlowService
    {
        public static PageNodeContainer PageNodes = new PageNodeContainer();
    }
}
