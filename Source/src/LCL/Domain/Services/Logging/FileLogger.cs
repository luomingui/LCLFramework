
using LCL.Config;
using LCL.Infrastructure;
using LCL.LData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LCL.Domain.Services
{
    /// <summary>
    /// 一个默认使用文件进行记录的日志器。
    /// </summary>
    public class FileLogger : LoggerBase
    {
        public override void LogInfo(string message)
        {
            string msg = string.Format(@"{0}|{1}|Inform|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(msg);
        }
        public override void LogWarn(string message)
        {
            string msg = string.Format(@"{0}|{1}|Warn|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(msg, LogType.Warn);
        }
        public override void LogDebug(string message)
        {
            string msg = string.Format(@"{0}|{1}|Debug|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(msg, LogType.Debug);
        }
        public override void LogError(string title, Exception e)
        {
            var stackTrace = e.StackTrace;//需要记录完整的堆栈信息。
            e = e.GetBaseException();

            string msg = string.Format(@"{0}|{1}|Error|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, title + e.ToString());
            AppendAllText(msg, LogType.Error);
        }
        /// <summary>
        /// 记录 Sql 执行过程。
        /// 把 SQL 语句及参数，写到 SQL_TRACE_FILE 配置所对应的文件中。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionSchema"></param>
        public override void LogDbAccessed(string sql, IDbDataParameter[] parameters, DbConnectionSchema connectionSchema)
        {
            var content = sql;
            if (parameters.Length > 0)
            {
                var pValues = parameters.Select(p =>
                {
                    var value = p.Value;
                    if (value is string)
                    {
                        value = '"' + value.ToString() + '"';
                    }
                    return value;
                });
                content += Environment.NewLine + "Parameters:" + string.Join(",", pValues);
            }
            content = DateTime.Now + Environment.NewLine + "Database:  " + connectionSchema.Database + Environment.NewLine + content + "\r\n\r\n\r\n";
            AppendAllText(content, LogType.Trace);

        }
        private void AppendAllText(string contents, LogType logType = LogType.Info)
        {
            string t = "";
            try
            {
                t = EngineContext.Current.Resolve<LConfig>().LoggerPath;
            }
            catch (Exception)
            {

            }
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/" + t;
            string logfile = path + "/" + logType + DateTime.Now.ToString("yyyyMMdd") + ".log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.AppendAllText(logfile, contents);
        }
    }
}