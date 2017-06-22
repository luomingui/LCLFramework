using System;
using System.Diagnostics;

namespace LCL.Domain.Services
{
    /// <summary>
    /// 计算机日志
    /// </summary>
    public class EventLogger : LoggerBase
    {
        #region ILogger Members
        public void LogError(Exception e)
        {
            EventLog.WriteEntry("LCL.", e.Message + Environment.NewLine + e.StackTrace, EventLogEntryType.Error);
        }
        public override void LogError(string title, Exception e)
        {
            EventLog.WriteEntry("LCL", title + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace, EventLogEntryType.Error);
        }
        public void LogError(string message)
        {
            EventLog.WriteEntry("LCL.", message, EventLogEntryType.Error);
        }
        public override void LogDebug(string message)
        {
            EventLog.WriteEntry("LCL.", message, EventLogEntryType.Information);
        }
        public override void LogInfo(string message)
        {
            EventLog.WriteEntry("LCL.", message, EventLogEntryType.Information);
        }
        public void LogPerf(string message)
        {
            EventLog.WriteEntry("LCL.", message, EventLogEntryType.Information);
        }

        #endregion
    }
}
