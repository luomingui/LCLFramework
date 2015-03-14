using LCL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 日志记录器。
    /// </summary>
    public abstract class LoggerBase
    {
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void LogInfo(string message) { }
        /// <summary>
        /// Logs the warn.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void LogWarn(string message) { }
        /// <summary>
        /// Logs the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void LogDebug(string message) { }
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The e.</param>
        public virtual void LogError(string title, Exception e) { }
        /// <summary>
        /// 记录 Sql 执行过程。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionSchema"></param>
        public virtual void LogDbAccessed(string sql, IDbDataParameter[] parameters, DbConnectionSchema connectionSchema) { }
    }
}