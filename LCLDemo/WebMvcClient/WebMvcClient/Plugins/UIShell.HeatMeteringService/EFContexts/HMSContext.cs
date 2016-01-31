using LCL;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    public class HMSContext : RbacDbContext
    {
        public HMSContext()
        {

        }
        //HMS
        public DbSet<HM_Village> Village { get; set; }
        public DbSet<HM_HisDeviceData> HisDeviceData { get; set; }
        public DbSet<HM_HeatPlant> HeatPlant { get; set; }
        public DbSet<HM_DeviceInfo> DeviceInfo { get; set; }
        public DbSet<HM_ClientInfo> ClientInfo { get; set; }
        public DbSet<HM_ClientInfoHistory> ClientInfoHistory { get; set; }
        public DbSet<HM_ClientHeatCharge> ClientHeatCharge { get; set; }
        public DbSet<HM_ClientCharge> ClientCharge { get; set; }
        public DbSet<HM_ChargeAnnual> ChargeAnnual { get; set; }
        public DbSet<HM_ChargeAddDel> ChargeAddDel { get; set; }
        public DbSet<HM_ClientCompact> ClientCompact { get; set; }
        public DbSet<HM_Favorable> Favorable { get; set; }
        public DbSet<HM_Bill> Bill { get; set; }
        public DbSet<HM_BillType> BillType { get; set; }
        public DbSet<HM_ChargeUserBill> ChargeUserBill { get; set; }
        public DbSet<HM_ClientBill> ClientBill { get; set; }

    }
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    internal static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            try
            {
                Database.SetInitializer<HMSContext>(new DropCreateDatabaseIfModelChanges<HMSContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new HMSContext())
                {
                    db.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("DatabaseInitializer ", ex);
            }
        }
    }
    /// <summary>
    /// 数据库初始化策略
    /// </summary>
    internal class SampleData : CreateDatabaseIfNotExists<HMSContext>
    {
        protected override void Seed(HMSContext context)
        {
            var rout = context.Set<WFRout>().Add(new WFRout { Name = "请假审批流程", State = 1, IsEnable = true, Version = "1.0" });
            var actor1 = context.Set<WFActor>().Add(new WFActor { Rout_ID = rout.ID, SortNo = 1, Name = "组长审批" });
            var actor2 = context.Set<WFActor>().Add(new WFActor { Rout_ID = rout.ID, SortNo = 2, Name = "部门经里审批" });
            var actor3 = context.Set<WFActor>().Add(new WFActor { Rout_ID = rout.ID, SortNo = 3, Name = "财务审批" });
            var actor4 = context.Set<WFActor>().Add(new WFActor { Rout_ID = rout.ID, SortNo = 4, Name = "老板审批" });

            var rout1 = context.Set<WFRout>().Add(new WFRout { Name = "维修审批流程", State = 1, IsEnable = true, Version = "1.0" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 1, Name = "申请校内报修", BillAddess = "/UIShell.EducationDeviceMaintenancePlugin/EDMSMaintenanceBill/Index" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 2, Name = "申请校外维修", BillAddess = "/UIShell.RbacManagementPlugin/WFTaskList/frmTask" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 3, Name = "校长审批", BillAddess = "/UIShell.RbacManagementPlugin/WFTaskList/frmTask" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 4, Name = "校外维修响应", BillAddess = "/UIShell.EducationDeviceMaintenancePlugin/EDMSMaintenanceBill/Index" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 5, Name = "学校验收", BillAddess = "/UIShell.RbacManagementPlugin/WFTaskList/frmTask" });
            context.Set<WFActor>().Add(new WFActor { Rout_ID = rout1.ID, SortNo = 6, Name = "评价维修服务", BillAddess = "/UIShell.RbacManagementPlugin/EDMSEvaluate/Index" });

            var dep0 = context.Set<Department>().Add(new Department { ParentId = Guid.Empty, NodePath = "永新科技", Name = "永新科技", OrderBy = 0, Level = 0, IsLast = false, DepartmentType = DepartmentType.公司, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });
            var dep1 = context.Set<Department>().Add(new Department { ParentId = dep0.ID, NodePath = dep0.Name + "/研发部", Name = "研发部", OrderBy = 1, Level = 1, IsLast = true, DepartmentType = DepartmentType.部门, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });
            var dep2 = context.Set<Department>().Add(new Department { ParentId = dep0.ID, NodePath = dep0.Name + "/市场部", Name = "市场部", OrderBy = 2, Level = 1, IsLast = true, DepartmentType = DepartmentType.部门, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });

            var role1 = context.Set<Role>().Add(new Role { Name = "系统管理员", RoleType = 1, Remark = "系统管理员" });
            var role2 = context.Set<Role>().Add(new Role { Name = "业务管理者", RoleType = 1, Remark = "业务管理者" });
            var role3 = context.Set<Role>().Add(new Role { Name = "业务操作者", RoleType = 1, Remark = "业务操作者" });

            int flgInt = 0;
            for (int i = 0; i < 50; i++)
            {
                var dep = new Department();
                var list = new List<Role>();
                switch (flgInt)
                {
                    case 0:
                        list.Add(role1);
                        dep = dep1;
                        break;
                    case 1:
                        list.Add(role2);
                        list.Add(role3);
                        dep = dep2;
                        break;
                    default:
                        flgInt = 0;
                        list.Add(role1);
                        dep = dep1;
                        break;
                }
                flgInt++;

                var urse = context.Set<User>().Add(new User
                {
                    Name = "员工" + i,
                    IsLockedOut = false,
                    Password = "123456",
                    IdCard = "362430" + i + "00000000000",
                    Sex = "男",
                    Telephone = "130262" + i + "0000",
                    Birthday = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"),
                    UserQQ = "271391233" + i,
                    PoliticalID = "政治面貌",
                    NationalID = "汉族",
                    Email = "luo." + i + "@163.com",
                    Department = dep
                });
                if (flgInt < 5)
                    context.Set<WFActorUser>().Add(new WFActorUser { Actor = actor1, User = urse });
                if (flgInt > 5 && flgInt < 10)
                    context.Set<WFActorUser>().Add(new WFActorUser { Actor = actor2, User = urse });
                if (flgInt > 10 && flgInt < 15)
                    context.Set<WFActorUser>().Add(new WFActorUser { Actor = actor3, User = urse });
            }
            context.Set<HostConfig>().Add(new HostConfig
            {
                Name = "LCL",
                IP = "127.0.0.1",
                Flag = true,
                Addess = "luo/lcl",
                Netdisk = "网络映射",
                SharedDirName = "共享目录名",
                SharedDirPassword = "共享密码",
                SharedDirUser = "共享用户",
                FtpUser = "FTP用户",
                FtpPassword = "FTP密码",
            });
            context.Set<Group>().Add(new Group
            {
                Name = "收费员",
                Remark = "管理员",
            });

            //HM
            var ca = context.Set<HM_ChargeAnnual>().Add(new HM_ChargeAnnual
             {
                 Name = "2016供热",
                 BeginDate = Convert.ToDateTime("2015-11-15"),
                 EndDate = Convert.ToDateTime("2016-03-15"),
                 GongreDay = 120,
                 IsOpen = true,
                 BreakMoney = 0.6,
                 DnaBeginDate = Convert.ToDateTime("2016-03-16"),
                 StopHeatRatio = 0.6,
                 Dishang = 0.11,
                 Dishang1 = 0.87,
                 Fixedportion = 0.35,
                 Gongjian = 1,
                 Gongjian1 = 2,
                 Resident = 22,
                 Resident1 = 32
             });
            var village = context.Set<HM_Village>().Add(new HM_Village
            {
                Name = "永新公寓",
                EnName = "yongxin",
                Pinyi = "yongxin",
                Alias = "永新公寓",
                Address = "",
                TotalArea = 3333333,
                Summary = "",
                Office = ""
            });
            var village1 = context.Set<HM_Village>().Add(new HM_Village
            {
                ParentId = village.ID,
                Name = "一号楼",
                EnName = "yhao",
                Pinyi = "yhao",
                Alias = "一号楼",
                Address = "",
                TotalArea = 3333333,
                Summary = "",
                Office = ""
            });
            var village2 = context.Set<HM_Village>().Add(new HM_Village
            {
                ParentId = village1.ID,
                Name = "一单元",
                EnName = "yhao",
                Pinyi = "yhao",
                Alias = "一单元",
                Address = "",
                TotalArea = 3333333,
                Summary = "",
                Office = ""
            });

            for (int i = 0; i < 20; i++)
            {
                context.Set<HM_ClientInfo>().Add(new HM_ClientInfo
                {
                    Name = "客户" + i,
                    Birthday = DateTime.Now.AddDays(-1000),
                    Cardno = "1-1-" + i,
                    ClientType = "居民",
                    Village = village2,
                    Email = "luo.mingui"+i+"@163.com",
                    Floor = 1,
                    Gender = true,
                    HeatArea = 98+i,
                    HeatState = 1,
                    HeatType = 2,
                    HelpeCode = "6396000" + i,
                    HomeAddress = "",
                    IDCard = "",
                    JobAddress = "",
                    RoomNumber = "",
                    Phone = "",
                    ZipCode = ""
                });
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder errors = new StringBuilder();
                IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in validationResult)
                {
                    errors.Append(result.Entry + ":" + result.Entry + "\r\n");
                    ICollection<DbValidationError> validationError = result.ValidationErrors;
                    foreach (DbValidationError err in validationError)
                    {
                        errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                    }
                }
                throw new Exception(errors.ToString());
            }
        }
    }
}
