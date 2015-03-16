using LCL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LCL
{
    /// <summary>
    /// 一个默认使用文件进行记录的日志器。
    /// </summary>
    public class FileLogger : LoggerBase
    {
        /// <summary>
        /// 错误日志的文件名。
        /// </summary>
        public static readonly string FileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log.txt";
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void LogInfo(string message)
        {
            string msg = string.Format(@"{0}|{1}|Inform|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(FileName, msg);
        }
        /// <summary>
        /// Logs the warn.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void LogWarn(string message)
        {
            string msg = string.Format(@"{0}|{1}|Warn|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(FileName, msg);
        }
        /// <summary>
        /// Logs the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void LogDebug(string message)
        {
            string msg = string.Format(@"{0}|{1}|Debug|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            AppendAllText(FileName, msg);
        }
        public override void LogError(string title, Exception e)
        {
            var stackTrace = e.StackTrace;//需要记录完整的堆栈信息。
            e = e.GetBaseException();

            string msg = string.Format(@"{0}|{1}|Error|{2}" + Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId, e.ToString());
            AppendAllText(FileName, msg);
        }

        private string _sqlTraceFile;

        /// <summary>
        /// 记录 Sql 执行过程。
        /// 
        /// 把 SQL 语句及参数，写到 SQL_TRACE_FILE 配置所对应的文件中。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionSchema"></param>
        public override void LogDbAccessed(string sql, IDbDataParameter[] parameters, DbConnectionSchema connectionSchema)
        {
            if (_sqlTraceFile == null)
            {
                _sqlTraceFile = ConfigurationHelper.GetAppSettingOrDefault("SQL_TRACE_FILE", string.Empty);
            }
            if (_sqlTraceFile.Length > 0)
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
                AppendAllText(_sqlTraceFile, content);
            }
        }
        private void AppendAllText(string path, string contents)
        {
            File.AppendAllText(path, contents);
        }
    }
}