using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UIShell.Threading;

namespace UIShell.RbacPermissionService
{
    public class EventManager
    {
        public static string RootPath;

        private EventManager()
        {
        }

        public static readonly int TimerMinutesInterval = 5;
        static EventManager()
        {
            if (ScheduleConfigs.GetConfig().TimerMinutesInterval > 0)
            {
                TimerMinutesInterval = ScheduleConfigs.GetConfig().TimerMinutesInterval;
            }
        }
        public static void Execute()
        {
            EventInfo[] simpleItems = ScheduleConfigs.GetConfig().Events;
            Event[] items;
            List<Event> list = new List<Event>();
            foreach (EventInfo newEvent in simpleItems)
            {
                if (!newEvent.Enabled)
                {
                    continue;
                }
                Event e = new Event();
                e.Key = newEvent.Key;
                e.Minutes = newEvent.Minutes;
                e.ScheduleType = newEvent.ScheduleType;
                e.TimeOfDay = newEvent.TimeOfDay;

                list.Add(e);
            }
            items = list.ToArray();
            Event item = null;

            if (items != null)
            {

                for (int i = 0; i < items.Length; i++)
                {
                    item = items[i];
                    if (item.ShouldExecute)
                    {
                        item.UpdateTime();
                        IEvent e = item.IEventInstance;
                        ManagedThreadPool.QueueUserWorkItem(new WaitCallback(e.Execute));
                    }
                }
            }
        }
    }
}
