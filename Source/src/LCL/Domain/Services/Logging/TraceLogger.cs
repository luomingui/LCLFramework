using System;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Threading;
using LCL.LData;

namespace LCL.Domain.Services
{
    public class TraceLogger : LoggerBase
    {
        #region Trace
        public static readonly TraceLogger Instance = new TraceLogger();
        private TraceListener listener;
        private readonly LiteSwitch liteSwitch = new LiteSwitch();
        public TraceLogger()
        {
            if (this.liteSwitch.Attributes.ContainsKey("listener"))
            {
                string str = this.liteSwitch.Attributes["listener"];
                foreach (TraceListener listener in Trace.Listeners)
                {
                    if (listener.Name == str)
                    {
                        this.listener = listener;
                        break;
                    }
                }
                if ((this.listener != null) && this.liteSwitch.Attributes.ContainsKey("dedicated"))
                {
                    string str2 = this.liteSwitch.Attributes["dedicated"];
                    if (str2 == "true")
                    {
                        Trace.Listeners.Remove(this.listener);
                    }
                }
            }
            this.liteSwitch.Enabled = true;
        }
        private void Indent()
        {
            if (this.Enabled)
            {
                if (this.listener != null)
                {
                    this.listener.IndentLevel++;
                }
                else
                {
                    Trace.Indent();
                }
            }
        }
        private void TraceMessage(string message)
        {
            if (this.Enabled)
            {
                if (this.listener != null)
                {
                    this.listener.WriteLine(message);
                    this.listener.Flush();
                }
                else
                {
                    Trace.WriteLine(message);
                }
            }
        }
        private void TraceAssert(bool condition, string message)
        {
            Trace.Assert(condition, message);
        }
        private void Unindent()
        {
            if (this.Enabled)
            {
                if (this.listener != null)
                {
                    this.listener.IndentLevel--;
                }
                else
                {
                    Trace.Unindent();
                }
            }
        }
        private bool Enabled
        {
            get
            {
                return this.liteSwitch.Enabled;
            }
            set
            {
                this.liteSwitch.Enabled = value;
            }
        }
        private TraceListener Listener
        {
            set
            {
                this.listener = value;
            }
        }
        private class LiteSwitch : BooleanSwitch
        {
            public LiteSwitch()
                : base("LCL", "LCL")
            {


            }

            protected override string[] GetSupportedAttributes()
            {
                return new string[] { "listener", "dedicated" };
            }
        }
        #endregion

        #region ILogger
        public override void LogInfo(string message)
        {
            string msg = string.Format(@"{0}|{1}|Inform|{2}\r\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            TraceMessage(msg);
        }
        public override void LogWarn(string message)
        {
            string msg = string.Format(@"{0}|{1}|Warn|{2}\r\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            TraceMessage(msg);
        }
        public override void LogDebug(string message)
        {
            string msg = string.Format(@"{0}|{1}|Debug|{2}\r\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, message);
            TraceMessage(msg);
        }
        public override void LogError(string title, Exception e)
        {
            var stackTrace = e.StackTrace;//需要记录完整的堆栈信息。
            e = e.GetBaseException();

            string msg = string.Format(@"{0}|{1}|Error|{2}\r\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, e.ToString());
            TraceMessage(msg);
        }
        public override void LogDbAccessed(string sql, IDbDataParameter[] parameters, DbConnectionSchema connectionSchema)
        {
            var content = sql;
            if (parameters.Length > 0)
            {
                var pValues = parameters.Select(p =>
                {
                    var value = p.Value;
                    if (value is string)
                        value = '"' + value.ToString() + '"';
                    return value;
                });
                content += Environment.NewLine + "Parameters:" + string.Join(",", pValues);
            }
            content = DateTime.Now + "\r\nDatabase:  " + connectionSchema.Database + "\r\n" + content + "\r\n\r\n\r\n";
            TraceMessage(content);
        }
        #endregion
    }
}
