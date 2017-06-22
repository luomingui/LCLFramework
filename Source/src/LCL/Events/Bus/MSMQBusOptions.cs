using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Bus
{
    public class MSMQBusOptions
    {
        #region Public Properties
        public bool SharedModeDenyReceive { get; set; }
        public bool EnableCache { get; set; }
        public QueueAccessMode QueueAccessMode { get; set; }
        public string Path { get; set; }
        public bool UseInternalTransaction { get; set; }
        public IMessageFormatter MessageFormatter { get; set; }
        #endregion

        #region Ctor
        public MSMQBusOptions(string path, bool sharedModeDenyReceive, bool enableCache, QueueAccessMode queueAccessMode, bool useInternalTransaction, IMessageFormatter messageFormatter)
        {
            this.SharedModeDenyReceive = sharedModeDenyReceive;
            this.EnableCache = enableCache;
            this.QueueAccessMode = queueAccessMode;
            this.Path = path;
            this.UseInternalTransaction = useInternalTransaction;
            this.MessageFormatter = messageFormatter;
        }
      
        public MSMQBusOptions(string path)
            : this(path, false, false, QueueAccessMode.SendAndReceive, false, new XmlMessageFormatter())
        { }
       
        public MSMQBusOptions(string path, bool useInternalTransaction)
            : this(path, false, false, QueueAccessMode.SendAndReceive, useInternalTransaction, new XmlMessageFormatter())
        { }
        #endregion
    }
}
