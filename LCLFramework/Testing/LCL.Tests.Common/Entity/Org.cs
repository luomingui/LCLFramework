
using System;
using System.Collections.Generic;
namespace LCL.Tests.Common
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class Org : MyEntity
    {
        public Org()
        {
            this.Position = new HashSet<Position>();
        }
        public string Name { get; set; }
        public virtual ICollection<Position> Position { get; set; }
    }
}
