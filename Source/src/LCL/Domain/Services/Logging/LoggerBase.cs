using LCL.LData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LCL.Domain.Services
{
    /// <summary>
    /// 日志记录器。
    /// </summary>
    public abstract class LoggerBase
    {
        public virtual void LogInfo(string message) { }
        public virtual void LogWarn(string message) { }
        public virtual void LogDebug(string message) { }
        public virtual void LogError(string title, Exception e) { }
        /// <summary>
        /// 记录 Sql 执行过程。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionSchema"></param>
        public virtual void LogDbAccessed(string sql, IDbDataParameter[] parameters, DbConnectionSchema connectionSchema) { }
    }
    public enum LogType
    {
        Info, 
        Warn,
        Debug, 
        Error,
        Trace
    }
}