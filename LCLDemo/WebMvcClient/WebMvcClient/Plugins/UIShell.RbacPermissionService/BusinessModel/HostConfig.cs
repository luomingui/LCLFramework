using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 配置文件服务器信息
    /// </summary>
    public partial class HostConfig : BaseModel
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主机IP 
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 服务器地点
        /// </summary>
        public string Addess { get; set; }
        /// <summary>
        /// FTP用户 
        /// </summary>
        public string FtpUser { get; set; }
        /// <summary>
        /// FTP密码 
        /// </summary>
        public string FtpPassword { get; set; }
        /// <summary>
        /// 该文件服务器在WEBSERVER上的网络映射盘 
        /// </summary>
        public string Netdisk { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public bool Flag { get; set; }
        /// <summary>
        /// 共享目录名 
        /// </summary>
        public string SharedDirName { get; set; }
        /// <summary>
        /// 共享目录访问帐户 
        /// </summary>
        public string SharedDirUser { get; set; }
        /// <summary>
        /// 共享目录访问密码 
        /// </summary>
        public string SharedDirPassword { get; set; }


    }
}
