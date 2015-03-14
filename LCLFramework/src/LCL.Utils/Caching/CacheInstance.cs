using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace LCL.Caching
{
    /// <summary>
    /// 缓存的几个默认实例。
    /// 
    /// 应用层可修改这些属性来实现自己的缓存逻辑。
    /// </summary>
    public static class CacheInstance
    {
        /// <summary>
        /// 默认使用的硬盘 sqlce 缓存文件。
        /// </summary>
        public static readonly string CACHE_FILE_NAME = "Rafy_Disk_Cache.sdf";

        private static Cache _memory, _disk, _memoryDisk;

        /// <summary>
        /// 内存缓存
        /// </summary>
        public static Cache Memory
        {
            get
            {
                if (_memory == null)
                {
                    _memory = new Cache(new MemoryCacheProvider());
                }
                return _memory;
            }
        }

        /// <summary>
        /// 硬盘缓存。
        /// 默认使用 SqlCe 的硬盘缓存
        /// </summary>
        public static Cache Disk
        {
            get
            {
                //由于有时并不会使用硬盘缓存，所以这个属性需要使用懒加载。
                if (_disk == null)
                {
                    string dbFileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, CACHE_FILE_NAME);
                    _disk = new Cache(new SQLCompactCacheProvider(dbFileName));
                }
                return _disk;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _disk = value;
            }
        }

        /// <summary>
        /// 一个先用内存，后用硬盘的二级缓存
        /// 默认使用 SqlCe 作为二级缓存的硬盘缓存
        /// </summary>
        public static Cache MemoryDisk
        {
            get
            {
                //由于有时并不会使用硬盘缓存，所以这个属性需要使用懒加载。
                if (_memoryDisk == null)
                {
                    string dbFileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, CACHE_FILE_NAME);
                    _memoryDisk = new Cache(new HybirdCacheProvider(dbFileName));
                }
                return _memoryDisk;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _memoryDisk = value;
            }
        }
    }
}
