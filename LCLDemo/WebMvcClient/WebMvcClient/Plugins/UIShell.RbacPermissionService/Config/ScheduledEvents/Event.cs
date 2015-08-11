using LCL;
using System;
using System.Xml.Serialization;

namespace UIShell.RbacPermissionService
{
    public class Event
    {
        public Event()
        {

        }
        private IEvent _ievent = null;
        public IEvent IEventInstance
        {
            get
            {
                LoadIEvent();
                return _ievent;
            }
        }
        private void LoadIEvent()
        {
            if (_ievent == null)
            {
                if (this.ScheduleType == null)
                {
                    Logger.LogInfo("计划任务没有定义其 type 属性");
                    
                }

                Type type = Type.GetType(this.ScheduleType);
                if (type == null)
                {
                    Logger.LogInfo(string.Format("计划任务 {0} 无法被正确识别", this.ScheduleType));
                }
                else
                {
                    _ievent = (IEvent)Activator.CreateInstance(type);
                    if (_ievent == null)
                    {
                        Logger.LogInfo(string.Format("计划任务 {0} 未能正确加载", this.ScheduleType));
                    }
                }
            }
        }
        private string _key;
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }
        private int _timeOfDay = -1;
        public int TimeOfDay
        {
            get { return this._timeOfDay; }
            set { this._timeOfDay = value; }
        }
        private int _minutes = 60;
        public int Minutes
        {
            get
            {
                if (this._minutes < EventManager.TimerMinutesInterval)
                {
                    return EventManager.TimerMinutesInterval;
                }
                return this._minutes;
            }
            set { this._minutes = value; }
        }
        private string _scheduleType;
        [XmlAttribute("type")]
        public string ScheduleType
        {
            get { return this._scheduleType; }
            set { this._scheduleType = value; }
        }
        private DateTime _lastCompleted;
        [XmlIgnoreAttribute]
        public DateTime LastCompleted
        {
            get { return this._lastCompleted; }
            set
            {
                dateWasSet = true;
                this._lastCompleted = value;
            }
        }
        bool dateWasSet = false;
        [XmlIgnoreAttribute]
        public bool ShouldExecute
        {
            get
            {
                if (!dateWasSet) 
                {
                    LastCompleted = EventDb.Instance.GetLastExecuteScheduledEventDateTime(this.Key, Environment.MachineName);
                }
                if (this.TimeOfDay > -1)
                {
                    DateTime dtNow = DateTime.Now; 
                    DateTime dtCompare = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day);
                    return LastCompleted < dtCompare.AddMinutes(this.TimeOfDay) && dtCompare.AddMinutes(this.TimeOfDay) <= DateTime.Now;

                }
                else
                {
                    return LastCompleted.AddMinutes(this.Minutes) < DateTime.Now;
                }
            }
        }
        public void UpdateTime()
        {
            this.LastCompleted = DateTime.Now;
            EventDb.Instance.SetLastExecuteScheduledEventDateTime(this.Key, Environment.MachineName, this.LastCompleted);
        }
        public static void SetLastExecuteScheduledEventDateTime(string key, string servername, DateTime lastexecuted)
        {
           EventDb.Instance.SetLastExecuteScheduledEventDateTime(key, servername, lastexecuted);
        }
        public static DateTime GetLastExecuteScheduledEventDateTime(string key, string servername)
        {
            return EventDb.Instance.GetLastExecuteScheduledEventDateTime(key, servername);
        }
    }
}
