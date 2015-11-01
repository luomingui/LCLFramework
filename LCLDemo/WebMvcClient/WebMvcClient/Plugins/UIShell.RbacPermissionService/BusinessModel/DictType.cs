using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    ///  字典类型
    /// </summary>
    public partial class DictType:BaseTreeModel
    {
        public DictType()
        {
            //this.Dictionarys = new HashSet<Dictionary>();
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        //public ICollection<Dictionary> Dictionarys { get; set; }
    }
}
