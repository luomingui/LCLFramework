using LCL;
using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class ScheduleController : RbacController
    {
        [Permission("首页", "Index")]
        public ActionResult Index(int? currentPageNum, int? pageSize)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<ScheduledEvents>.DefaultPageSize;
            }

            var list = FindAll();

            var contactLitViewModel = new PagedListViewModel<ScheduledEvents>(currentPageNum.Value, pageSize.Value, list.ToList());
            return View(contactLitViewModel);

        }
        [Permission("执行", "ExecCommand")]
        public ActionResult ExecCommand(string Key)
        {
            if (!string.IsNullOrWhiteSpace(Key))
            {
                EventInfo[] events = ScheduleConfigs.GetConfig().Events;
                foreach (EventInfo ev in events)
                {
                    if (ev.Key == Key)
                    {
                        ((IEvent)Activator.CreateInstance(Type.GetType(ev.ScheduleType))).Execute(HttpContext);
                        Event.SetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName, DateTime.Now);
                        break;
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddOrEdit(int? currentPageNum, int? pageSize, string Key, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<ScheduledEvents>.DefaultPageSize;
            }
            if (string.IsNullOrWhiteSpace(Key))
            {
                return View(new AddOrEditViewModel<ScheduledEvents>
                {
                    Added = true,
                    Entity = new ScheduledEvents(),
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var list = FindAll();
                var model = list.FirstOrDefault(p => p.Key == Key);
                return View(new AddOrEditViewModel<ScheduledEvents>
                {
                    Added = false,
                    Entity = model,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value,
                });
            }
        }
        public ActionResult Add(AddOrEditViewModel<ScheduledEvents> model, FormCollection collection)
        {
            int Entity_ExetimeType = 0;
            if (collection.GetValues("Entity.ExetimeType") != null)
            {
                Entity_ExetimeType = int.Parse(collection.GetValue("Entity.ExetimeType").AttemptedValue);
            }

            #region MyRegion
            ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
            foreach (EventInfo ev1 in sci.Events)
            {
                if (ev1.Key == model.Entity.Key.Trim())
                {
                    ModelState.AddModelError("Key", "消息：计划任务名称已经存在！");
                    return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
                }
            }

            EventInfo ev = new EventInfo();
            ev.Key = model.Entity.Key;
            ev.Enabled = true;
            ev.IsSystemEvent = false;
            ev.ScheduleType = model.Entity.ScheduleType.ToString();
            model.Entity.ExetimeType = Entity_ExetimeType == 0 ? false : true;

            if (model.Entity.ExetimeType)
            {
                ev.TimeOfDay = model.Entity.hour * 60 + model.Entity.minute;
                ev.Minutes = sci.TimerMinutesInterval;
            }
            else
            {
                ev.Minutes = model.Entity.timeserval;
                ev.TimeOfDay = -1;
            }
            EventInfo[] es = new EventInfo[sci.Events.Length + 1];
            for (int i = 0; i < sci.Events.Length; i++)
            {
                es[i] = sci.Events[i];
            }
            es[es.Length - 1] = ev;
            sci.Events = es;
            ScheduleConfigs.SaveConfig(sci);
            #endregion
            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public ActionResult Edit(AddOrEditViewModel<ScheduledEvents> model, FormCollection collection)
        {
            var key = LRequest.GetFormString("Key");
            #region MyRegion
            ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
            foreach (EventInfo ev1 in sci.Events)
            {
                if (ev1.Key == model.Entity.Key.Trim())
                {
                    ModelState.AddModelError("Key", "消息：计划任务名称已经存在！");
                    return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
                }
            }
            foreach (EventInfo ev1 in sci.Events)
            {
                if (ev1.Key == key)
                {
                    ev1.Key = model.Entity.Key.Trim();
                    ev1.ScheduleType = model.Entity.ScheduleType.Trim();

                    if (model.Entity.ExetimeType)
                    {
                        ev1.TimeOfDay = model.Entity.hour * 60 + model.Entity.minute;
                        ev1.Minutes = sci.TimerMinutesInterval;
                    }
                    else
                    {
                        if (model.Entity.timeserval < sci.TimerMinutesInterval)
                            ev1.Minutes = sci.TimerMinutesInterval;
                        else
                            ev1.Minutes = model.Entity.timeserval;
                        ev1.TimeOfDay = -1;
                    }
                    if (!ev1.IsSystemEvent)
                    {
                        ev1.Enabled = model.Entity.Enable;
                    }
                    break;
                }
            }
            ScheduleConfigs.SaveConfig(sci);
            #endregion
            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public ActionResult Delete(int? currentPageNum, int? pageSize, string Key)
        {

            ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
            sci.Events = sci.Events.Where(p => p.Key != Key).ToArray();
            ScheduleConfigs.SaveConfig(sci);

            return RedirectToAction("Index", new { currentPageNum = currentPageNum.Value, pageSize = pageSize.Value });
        }
        public List<ScheduledEvents> FindAll()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", typeof(string));
            dt.Columns.Add("scheduletype", typeof(string));
            dt.Columns.Add("exetime", typeof(string));
            dt.Columns.Add("lastexecuted", typeof(DateTime));
            dt.Columns.Add("issystemevent", typeof(bool));
            dt.Columns.Add("enable", typeof(bool));
            EventInfo[] events = ScheduleConfigs.GetConfig().Events;
            foreach (EventInfo ev in events)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = ev.Key;
                dr["scheduletype"] = ev.ScheduleType;
                if (ev.TimeOfDay != -1)
                {
                    dr["exetime"] = "定时执行:" + (ev.TimeOfDay / 60) + "时" + (ev.TimeOfDay % 60) + "分";
                }
                else
                {
                    dr["exetime"] = "周期执行:" + ev.Minutes + "分钟";
                }
                DateTime lastExecute = Event.GetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName);
                if (lastExecute == DateTime.MinValue)
                {
                    dr["lastexecuted"] = Convert.ToDateTime("1999-01-01").ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    dr["lastexecuted"] = lastExecute.ToString("yyyy-MM-dd HH:mm:ss");
                }
                dr["issystemevent"] = ev.IsSystemEvent.ToString();// ? "系统级" : "非系统级";
                dr["enable"] = ev.Enabled.ToString();// ? "启用" : "禁用";
                dt.Rows.Add(dr);
            }
            var list = dt.ToArray<ScheduledEvents>();
            return list.ToList();
        }
    }
}
