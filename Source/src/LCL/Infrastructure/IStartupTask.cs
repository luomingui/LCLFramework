namespace LCL.Infrastructure
{
    /// <summary>
    /// 系统启动时执行任务
    /// </summary>
    public interface IStartupTask 
    {
        void Execute();
        int Order { get; }
    }
}
