using System;
using System.Diagnostics;

namespace LCL.Tests.Common
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User : MyEntity
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        /// <summary>
        /// 职员职位
        /// </summary>
        public virtual Position Position { get; set; }

        public void ChangeEmail(string Name, string Code)
        {
            Debug.WriteLine("User Name=" + Name + " Code=" + Code + "");
        }
    }

}
