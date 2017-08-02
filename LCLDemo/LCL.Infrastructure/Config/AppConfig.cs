using LCL;
using LCL.LData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Infrastructure.Config
{
   public class AppConfig
    {
        public static DbSetting DbSetting
        {
            get
            {
                return DbSetting.FindOrCreate("LCL");
            }
        }
  
        /// <summary>
        /// 根据指定的角色名称获得所对应的权限键（Permission Key）名。
        /// </summary>
        /// <param name="roleName">角色名称。</param>
        /// <returns>权限键（Permission Key）名。</returns>
        public string GetKeyNameByRoleName(string roleName)
        {
            var permissionKey = (PermissionKeys)Enum.Parse(typeof(PermissionKeys), roleName);

            return permissionKey.ToString();
        }

    }
}
