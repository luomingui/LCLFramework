/*******************************************************
 * 
 * 作者：罗敏贵
 * 创建时间：2012612
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET 2.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 罗敏贵 2012612
 * 
*******************************************************/

namespace SF.Threading
{
    //public delegate void Action ();
    /// <summary>
    /// 
    /// </summary>
    public static class ThreadHelper
    {
        public static IAsyncMultiActions AsyncMultiActions
        {
            get
            {
                return SF.Threading.AsyncMultiActions.Instance;
            }
        }
        public static IParallelActions CreateParallelActions( )
        {
            return new ParallelActions();
        }
    }
}
