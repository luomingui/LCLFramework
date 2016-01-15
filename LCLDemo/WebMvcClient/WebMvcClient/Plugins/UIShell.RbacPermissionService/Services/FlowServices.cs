using LCL;
using LCL.DomainServices;
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService.Services
{
    /// <summary>
    ///   var service = DomainServiceFactory.Create<FlowServices>();
    ///   service.Invoke();
    ///   var list = service.Result;
    /// </summary>
    [Serializable]
    [Contract, ContractImpl]
    public class FlowServices : BaseServices
    {
        public FlowServices()
        {
            Arguments = new Result(false);
            Result = new Result(false);
        }
        [ServiceInput]
        public Result Arguments { get; set; }
        public FlowAction FlowAction { get; set; }
        [ServiceOutput]
        public Result Result { get; set; }
        protected override void ExecuteCode()
        {
            Result = new Result(false);
            switch (FlowAction)
            {
                case FlowAction.申请工作:
                    Apply(Arguments);
                    break;
                case FlowAction.我的任务:
                    MyTaskList();
                    break;
                case FlowAction.任务地址:
                    GetTaskUrl(Arguments);
                    break;
                case FlowAction.我的审批:
                    break;
                case FlowAction.处理任务:
                    Approveing(Arguments);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 待我处理的工作
        /// </summary>
        private void MyTaskList()
        {
            var gridModel = new EasyUIGridModel<MyTaskListViewModel>();
            Result = new Result(true);
            try
            {
                var data = DbFactory.DBA.QueryDataTable(@"select t.ID as TaskID,t1.Name as TaskName,t2.Name as BillName,t3.Name as RoutName,t.State as TaskState,t2.State as ItemState,t2.AddDate from WFTaskList t
inner join WFActor t1 on t.Actor_ID=t1.ID
inner join WFItem t2 on t.Item_ID=t2.ID
inner join WFRout t3 on t1.Rout_ID=t3.ID");
                gridModel.rows = data.ToArray<MyTaskListViewModel>().ToList();
                gridModel.total = gridModel.rows.Count;
            }
            catch (Exception)
            {
                Result = new Result(false);
            }
            Result.DataObject = gridModel;
        }
        /// <summary>
        /// 申请工作
        /// </summary>
        private void Apply(Result Arguments)
        {
            Guid itemId = Guid.Parse(Arguments.Attributes["itemId"].ToString());
            Guid userId = RbacPrincipal.CurrentUser.UserId;
            string Pbo_Type = Arguments.Attributes["Pbo_Type"].ToString();
            string memo = Arguments.Attributes["Memo"].ToString();
            Result = new Result(true);

            var rout = RF.Concrete<IWFRoutRepository>().GetRoutName(WFRoutType.维修审批流程.ToString());
          
            WFItem item = new WFItem();
            item.ID = Guid.NewGuid();
            item.ApplyUserID = userId;
            item.Pbo_ID = itemId;
            item.Pbo_Type = Convert.ToInt32(Pbo_Type);
            item.Pbo_Adderss = "EDMSRepairsBill";
            item.Name = rout.Name + "(" + DateTime.Now.Minute + ")";
            item.Rout_ID = rout.ID;
            RF.Concrete<IWFItemRepository>().Create(item);

            //查找所选流程的第一个步骤
            var actor = RF.Concrete<IWFActorRepository>().GetByRoutId(rout.ID)[0];
            //插入任务列表taskList 
            RF.Concrete<IWFTaskListRepository>().Create(new WFTaskList
            {
                Actor_ID = actor.ID,
                Item_ID = item.ID,
                State = 0,
                Version = 1,
            });
            //插入任务历史记录
            RF.Concrete<IWFTaskHistoryRepository>().Create(new WFTaskHistory
            {
                Item_ID = itemId,
                Actor_ID = actor.ID,
                OperateUserID = userId,
                IsExamine = false,
                Memo = memo,
            });
            RF.Concrete<IWFTaskHistoryRepository>().Context.Commit();
            Result.DataObject = item;
        }
        /// <summary>
        /// 根据任务编号获取任务地址
        /// </summary>
        /// <param name="Arguments"></param>
        private void GetTaskUrl(Result Arguments)
        {
            Guid taskId = Guid.Parse(Arguments.Attributes["taskId"].ToString());
            Result = new Result(true);
            string billaddress = "", PboID = "", TaskID = "";
            try
            {
                var data = DbFactory.DBA.QueryDataTable(@"select ac.BillAddess,it.Pbo_ID,tk.ID as TaskID from WFTaskList tk
inner join WFActor ac on tk.Actor_ID=ac.ID
inner join WFItem it on tk.Item_ID=it.ID where tk.ID='" + taskId + "'");
                if (data.Rows.Count > 0)
                {
                    billaddress = data.Rows[0]["BillAddess"].ToString();
                    PboID = data.Rows[0]["Pbo_ID"].ToString();
                    TaskID = data.Rows[0]["TaskID"].ToString();
                    billaddress = billaddress + "?pboId=" + PboID + "&TaskID=" + TaskID;
                }
                Result.Attributes.Add("taskUrl", billaddress);
            }
            catch (Exception ex)
            {
                Result = new Result(false, ex.HResult, ex.Message);
                Result.Attributes.Add("taskUrl", "");
            }
        }
        /// <summary>
        /// 处理任务
        /// </summary>
        private void Approveing(Result Arguments)
        {
            Result = new Result(true);

            Guid taskId = Guid.Parse(Arguments.Attributes["taskId"].ToString());
            bool isExamine = bool.Parse(Arguments.Attributes["isExamine"].ToString());
            int state =Convert.ToInt32(Arguments.Attributes["State"].ToString());
            string memo = Arguments.Attributes["Memo"].ToString();
            Guid userId = RbacPrincipal.CurrentUser.UserId;
            //获取任务信息
            var data = DbFactory.DBA.QueryDataTable(@"select tk.ID as TaskID,tk.Actor_ID as ActorID,
ac.SortNo as ActorSortNo,ac.Rout_ID as RoutID,tk.Item_ID as ItemID,ac.IsManyPeople,ac.IsSerial from WFTaskList tk
inner join WFActor ac on tk.Actor_ID=ac.ID where tk.ID ='" + taskId + "'");

            var taskInfo = data.ToArray<ItemTaskInfo>()[0];
            if (taskInfo == null)
                return;

            taskInfo.ActorUserID = userId;
            if (taskInfo.IsActorUser())
            {
                //更新单据
                var repo= RF.Concrete<IWFItemRepository>();
                var model= repo.GetByKey(taskInfo.ItemID);
                model.State = state;
                repo.Update(model);

                //处理方式
                switch (taskInfo.IsManyPeople)
                {
                    case 1://通过人数或者通过率
                        break;
                    case 2://多人通过
                        if (taskInfo.IsTaskActorAgree())
                        {
                            taskInfo.UpdateActionNextStep(isExamine, memo);
                        }
                        break;
                    case 3://一人通过
                        taskInfo.UpdateActionNextStep(isExamine, memo);
                        break;
                    default:
                        taskInfo.UpdateActionNextStep(isExamine, memo);
                        break;
                }
            }
            else
            {
                Result = new Result(false, 1, "你没有权限");
            }
        }
    }
    public class ItemTaskInfo
    {
        public Guid TaskID { get; set; }
        public Guid ItemID { get; set; }
        public Guid ActorID { get; set; }
        public Guid ActorUserID { get; set; }
        public int ActorSortNo { get; set; }
        public bool IsSerial { get; set; }
        public int IsManyPeople { get; set; }
        public Guid RoutID { get; set; }
        /// <summary>
        /// 判断当前用户有没有处理当前任务的权限
        /// </summary>
        /// <returns></returns>
        public bool IsActorUser()
        {
            //管理员跳出三界之外
            if (RbacPrincipal.CurrentUser.UserName == "admin")
                return true;

            var data = DbFactory.DBA.QueryDataTable(@"select * from WFActorUser au where au.Actor_ID='" + ActorID + "' and au.User_ID='" + ActorUserID + "'");
            if (data != null && data.Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断是否还有人没有通过
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="actorId"></param>
        /// <returns></returns>
        public bool IsTaskActorAgree()
        {
            int hisCount = DbFactory.DBA.QueryDataTable("select distinct operateuser_id  from wftaskhistory where item_id=" +ItemID + " and actor_id=" + ActorID + "").Rows.Count;
            int auCount = DbFactory.DBA.QueryDataTable("select * from wfactoruser  where actor_id=" + ActorID + "").Rows.Count;
            if (hisCount == auCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否最后一步
        /// </summary>
        /// <returns></returns>
        public bool IsActionFinalStep(out Guid actionLast_id)
        {
            actionLast_id = Guid.Empty;
            string sql = @"select id from wfactor where rout_Id ='"+RoutID+"' and SortNo="+(ActorSortNo+1) +" order by sortNo";
            var id = DbFactory.DBA.QueryValue(sql);
            if (id != null)
            {
                actionLast_id = Guid.Parse(DbFactory.DBA.QueryValue(sql).ToString());
                return false;
            }
            else
            {
                return true ;
            }
        }
        /// <summary>
        /// 下一步
        /// </summary>
        public void UpdateActionNextStep(bool isExamine, string memo)
        {
            if (!isExamine)
            {//拒绝 打回到 上一步
                var actionModel= GetActionLastStep();
                DbFactory.DBA.ExecuteText(@"update WFTaskList set Version=Version+1,Actor_ID='" + actionModel.ID + "' where ID='" + TaskID + "'");
                return;
            }
            Guid actionLast_id;
            bool isActionLast = IsActionFinalStep(out actionLast_id);
            if (isActionLast)
            {//最后一步
                RF.Concrete<IWFTaskListRepository>().Update(new WFTaskList
                {
                    ID = TaskID,
                    Actor_ID = Guid.Empty,
                    Item_ID=ItemID,
                    State = 2,
                    Version = 2,
                    UpdateDate = DateTime.Now
                });
            }
            else
            {
                RF.Concrete<IWFTaskListRepository>().Update(new WFTaskList
                {
                    ID = TaskID,
                    Actor_ID = actionLast_id,
                    Item_ID=ItemID,
                    Version = 1,
                    State = 2,
                });
            }
            RF.Concrete<IWFTaskHistoryRepository>().Create(new WFTaskHistory
            {
                ID = Guid.NewGuid(),
                Actor_ID = TaskID,
                Item_ID = ItemID,
                OperateUserID = ActorUserID,
                IsExamine = isExamine,
                Memo = memo
            });
        }
        /// <summary>
        /// 上一步
        /// </summary>
        public WFActor GetActionLastStep()
        {
            var repo = RF.Concrete<IWFActorRepository>();
            var list = repo.FindAll(new KeyWFActorSpecification("Rout_ID", RoutID.ToString()));
            var model= list.FirstOrDefault<WFActor>(e => e.SortNo == ActorSortNo - 1);
            return model;
        }
        /// <summary>
        /// 获取流程第一步
        /// </summary>
        /// <returns></returns>
        public Guid GetFirstStep()
        {
            var id = DbFactory.DBA.QueryValue(@"SELECT TOP 1 Id FROM WFActor where Rout_ID='" + RoutID + "' order by SortNo");
            return Guid.Parse(id.ToString());
        }
    }
    public enum FlowAction
    {
        申请工作,
        我的任务,
        任务地址,
        处理任务,
        我的审批,
    }
}
