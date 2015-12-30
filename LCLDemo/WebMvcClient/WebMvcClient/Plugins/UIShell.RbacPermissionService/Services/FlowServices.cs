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
    public class FlowServices : DomainService
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
        protected override void Execute()
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
            var identity = LEnvironment.Principal as LCLIdentity;
            //if (identity == null) { return; }

            Guid itemId = Guid.Parse(Arguments.Attributes["itemId"].ToString());
            Guid userId = Guid.Empty;
            Result = new Result(true);

            var rout = RF.Concrete<IWFRoutRepository>().GetRoutName(WFRoutType.维修审批流程.ToString());

            WFItem item = new WFItem();
            item.ApplyUserID = userId;
            item.Pbo_ID = itemId;
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
                Memo = "申请",
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
            try
            {
                string billaddress =  DbFactory.DBA.QueryValue(@"select BillAddess from WFTaskList tk
                   inner join WFActor ac on tk.Actor_ID=ac.ID where tk.ID='" + taskId + "'").ToString();

                Result.Attributes.Add("taskUrl", billaddress);
            }
            catch (Exception ex)
            {
                Result = new Result(false, ex.HResult, ex.Message);
                Result.Attributes.Add("taskUrl","");
            }
        }
        /// <summary>
        /// 处理任务
        /// </summary>
        private void Approveing(Result Arguments)
        {
            Guid taskId = Guid.Parse(Arguments.Attributes["taskId"].ToString());
            bool IsThrough = bool.Parse(Arguments.Attributes["IsThrough"].ToString());
           //获取任务信息
            var data = DbFactory.DBA.QueryDataTable(@"select 
tk.ID as TaskID,
tk.Actor_ID as ActorID,
ac.SortNo as ActorSortNo,
ac.Rout_ID as RoutID
from WFTaskList tk
inner join WFActor ac on tk.Actor_ID=ac.ID");

            var taskInfo = data.ToArray<ItemTaskInfo>()[0];

            if (taskInfo.IsActorUser())
            { 
                //处理任务
                if (IsThrough)
                {
                    //通过:update任务列表的步骤ID为下一步骤ID
                    RF.Concrete<IWFItemRepository>().Update(new WFItem{
                        ID=taskInfo.ItemID,
                        State=1,
                    });
                    //处理方式
                    switch (taskInfo.IsManyPeople)
                    { 
                        case 1:

                            break;
                        case 2:
                            break;
                        case 3:
                            break;

                    }
                }
                else {
                    //拒绝:update任务列表的步骤ID为第一步的ID
                }
                //下一步
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
           var data= DbFactory.DBA.QueryDataTable(@"select * from WFActorUser au where au.Actor_ID='"+ActorID+"' and au.User_ID='"+ActorUserID+"'");
           if (data!=null&&data.Rows.Count > 0)
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
        public bool IsTaskActorAgree(int itemId, int actorId)
        {
            return true;
        }
        /// <summary>
        /// 判断是否最后一步
        /// </summary>
        /// <returns></returns>
        public bool IsActionLast()
        {
            return false;
        }
    }
    public enum FlowAction
    {
        申请工作,
        我的任务,
        任务地址,
        节点审批,
        我的审批,
    }
}
