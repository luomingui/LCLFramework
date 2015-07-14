using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Messaging;

namespace LCL.UnitTest
{
    public static class Helper
    {
        public static void ClassInitialize(TestContext context)
        {
            new TestServerApp().Start();
        }

        public const string EventBus_MessageQueue = @"EventBusMQ";

        public static void ClearMessageQueue(string queue)
        {
            string mqName = CreateMessageQueueName(queue);
            if (!MessageQueue.Exists(mqName))
                MessageQueue.Create(mqName, true);
            else
            {
                using (MessageQueue mq = new MessageQueue(mqName, false, false, QueueAccessMode.SendAndReceive))
                {
                    mq.Purge();
                    mq.Close();
                }
            }
        }
        public static int GetMessageQueueCount(string queue)
        {
            string mqName = CreateMessageQueueName(queue);
            if (!MessageQueue.Exists(mqName))
            {
                MessageQueue.Create(mqName, true);
                return 0;
            }
            else
            {
                int ret = 0;
                using (MessageQueue mq = new MessageQueue(mqName, false, false, QueueAccessMode.SendAndReceive))
                {
                    ret = mq.GetAllMessages().Length;
                    mq.Close();
                }
                return ret;
            }
        }
        public static Message[] GetAllMessages(string queue)
        {
            string mqName = CreateMessageQueueName(queue);
            if (!MessageQueue.Exists(mqName))
            {
                MessageQueue.Create(mqName, true);
                return null;
            }
            else
            {
                Message[] ret = null;
                using (MessageQueue mq = new MessageQueue(mqName, false, false, QueueAccessMode.SendAndReceive))
                {
                    ret = mq.GetAllMessages();
                    mq.Close();
                }
                return ret;
            }
        }
        private static string CreateMessageQueueName(string mq)
        {
            return string.Format(@".\private$\{0}", mq);
        }
    }
}
