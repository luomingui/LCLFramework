using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace LCL.Tests.Common
{
    /// <summary>
    /// 模型基类
    /// </summary>
    [Serializable]
    public class MyEntity : Entity
    {
        public MyEntity()
        {
            AddDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
