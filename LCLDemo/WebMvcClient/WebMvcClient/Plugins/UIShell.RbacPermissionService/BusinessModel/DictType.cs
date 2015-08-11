using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    ///  字典类型
    /// </summary>
    public partial class DictType : BaseModel
    {
        public DictType()
        {
            this.Dictionarys = new HashSet<Dictionary>();
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string DicDes { get; set; }

        public ICollection<Dictionary> Dictionarys { get; set; }
    }
}
