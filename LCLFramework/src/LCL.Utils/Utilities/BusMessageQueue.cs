using System;
using System.Messaging;
using System.Collections.Generic;
using System.Text;

namespace LCL.Bus
{
    public interface IBusMessageQueue
    {
        void Clear();
        List<BusMessage> GetAll();
        BusMessage Receive();
        BusMessage ReceiveById(string id);
        void Publish(IEnumerable<BusMessage> messages);
        void Publish(BusMessage message);
    }
    public class BusMessageQueue : System.IDisposable, IBusMessageQueue
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private readonly MessageQueue messageQueue;
        private readonly object lockObj = new object();
        private readonly BinaryMessageFormatter formatter = new BinaryMessageFormatter();
        #endregion
        public BusMessageQueue()
        {
            try
            {
                CreateMessageQueue();
                string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
                this.messageQueue = new MessageQueue(mqName);
                this.messageQueue.Label = mqName;
                //Administrators
                this.messageQueue.SetPermissions("Everyone", System.Messaging.MessageQueueAccessRights.FullControl);
                this.messageQueue.Formatter = formatter;
                this.messageQueue.UseJournalQueue = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("BusMessageQueue:", ex);
            }
        }
        public BusMessageQueue(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                    EdoorBusMQ_MessageQueue = path;
                CreateMessageQueue();
                string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
                this.messageQueue = new MessageQueue(mqName);
                this.messageQueue.Label = mqName;
                //Administrators
                this.messageQueue.SetPermissions("Everyone", System.Messaging.MessageQueueAccessRights.FullControl);
                this.messageQueue.Formatter = formatter;
                this.messageQueue.UseJournalQueue = true;
            }
            catch (Exception ex)
            {
               Logger.LogError("BusMessageQueue:", ex);
            }
        }
        private void SendMessage(BusMessage message, MessageQueueTransaction transaction = null)
        {
            try
            {
                Message msmqMessage = new Message();
                msmqMessage.Label = message.Label;
                msmqMessage.Body = message;
                msmqMessage.Formatter = formatter;
                messageQueue.Send(msmqMessage);
            }
            catch (Exception ex)
            {
                Logger.LogError("SendMessage:", ex);
            }

        }
        #region IBus Members
        public void Publish(BusMessage message)
        {
            lock (lockObj)
            {
                SendMessage(message);
            }
        }
        public void Publish(IEnumerable<BusMessage> messages)
        {
            lock (lockObj)
            {
                foreach (var item in messages)
                {
                    SendMessage(item);
                }
            }
        }
        public List<BusMessage> GetAll()
        {
            List<BusMessage> list = new List<BusMessage>();
            var message = GetAllMessages();
            foreach (var item in message)
            {
                var msg = formatter.Read(item);
                var model = msg as BusMessage;
                model.ID = item.Id;
                list.Add(model);
            }
            return list;
        }
        public BusMessage Receive()
        {
            var msg = messageQueue.Receive();
            var item = formatter.Read(msg);
            return item as BusMessage;
        }
        public BusMessage ReceiveById(string id)
        {
            var msg = messageQueue.ReceiveById(id);
            var item = formatter.Read(msg);
            return item as BusMessage;
        }
        public void Clear()
        {
            lock (lockObj)
            {
                ClearMessageQueue();
            }
        }
        #endregion
        #region Hepler
        private void CreateMessageQueue()
        {
            string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
            if (!MessageQueue.Exists(mqName))
                MessageQueue.Create(mqName);
        }
        private void ClearMessageQueue()
        {
            string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
            if (!MessageQueue.Exists(mqName))
                MessageQueue.Create(mqName);
            else
            {
                using (MessageQueue mq = new MessageQueue(mqName))
                {
                    mq.Purge();
                    mq.Close();
                }
            }
        }
        private string CreateMessageQueueName(string mq)
        {
            return string.Format(@".\private$\{0}", mq);
        }
        private string EdoorBusMQ_MessageQueue = @"LCLBusMQ";
        private Message[] GetAllMessages()
        {
            string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
            if (!MessageQueue.Exists(mqName))
            {
                MessageQueue.Create(mqName, true);
                return null;
            }
            else
            {
                Message[] ret = null;
                using (MessageQueue mq = new MessageQueue(mqName))
                {
                    ret = mq.GetAllMessages();
                    mq.Close();
                }
                return ret;
            }
        }
        public int GetMessageQueueCount()
        {
            string mqName = CreateMessageQueueName(EdoorBusMQ_MessageQueue);
            if (!MessageQueue.Exists(mqName))
            {
                MessageQueue.Create(mqName, true);
                return 0;
            }
            else
            {
                int ret = 0;
                using (MessageQueue mq = new MessageQueue(mqName))
                {
                    ret = mq.GetAllMessages().Length;
                    mq.Close();
                }
                return ret;
            }
        }
        #endregion
        public void Dispose()
        {
            if (messageQueue != null)
            {
                messageQueue.Close();
                messageQueue.Dispose();
            }
        }
    }
    [Serializable]
    public class BusMessage
    {
        public BusMessage()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string Label { get; set; }
        /// <summary>
        ///  1:pdfToimg
        /// </summary>
        public int Type { get; set; }
        public object Body { get; set; }
    }
}
