/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 客户信息 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2016年1月11日 
*   
*******************************************************/
using LCL;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    public interface IHM_ClientInfoRepository : IRepository<HM_ClientInfo>
    {
        void ImportClientDbo(System.Data.DataTable dtImportDoc);
    }
    public class HM_ClientInfoRepository : EntityFrameworkRepository<HM_ClientInfo>, IHM_ClientInfoRepository
    {
        public HM_ClientInfoRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public void ImportClientDbo(System.Data.DataTable dtImportDoc)
        {
            //小区名称	楼栋名称	单元名称	门牌号	客户编号	客户姓名	
            //助记码	联系电话	手机	收费面积	供热状态	客户类型	
            //工作单位	楼层	温控器MAC	控制器MAC	设备名称	网关名称
            var village = RF.Concrete<IHM_VillageRepository>();
            var client = RF.Concrete<IHM_ClientInfoRepository>();
            var device = RF.Concrete<IHM_DeviceInfoRepository>();
            for (int i = 0; i < dtImportDoc.Rows.Count; i++)
            {
                var row = dtImportDoc.Rows[i];

                var v1 = new HM_Village();
                v1.Name = row["小区名称"].ToString();
                v1.Type = 1;
                v1.EnName = "";
                v1.Alias = v1.Name;
                v1.Address = "";
                v1.Pinyi = PinYinConverter.GetShortPY(v1.Name);
                v1.Population = 0;
                v1.Summary = "";
                v1.Office = "";
                v1.TotalArea = 0;
                v1.ParentId = Guid.Empty;
                v1.NodePath = v1.Name;
                v1.Level = 1;
                v1.IsLast = false;
                v1.OrderBy = i;

                var model1 = IsVillage(row["小区名称"].ToString(), Guid.Empty);
                if (model1 != null)
                    v1 = model1;
                else
                {
                    village.Create(v1);
                    village.Context.Commit();
                }

                var v2 = new HM_Village();
                v2.Name = row["楼栋名称"].ToString();
                v2.Type = 2;
                v2.EnName = "";
                v2.Alias = v1.Name;
                v2.Address = "";
                v2.Pinyi = PinYinConverter.GetShortPY(v1.Name + v2.Name);
                v2.Population = 0;
                v2.Summary = "";
                v2.Office = "";
                v2.TotalArea = 0;
                v2.ParentId = v1.ID;
                v2.NodePath = v1.Name + "/" + v2.Name;
                v2.Level = 2;
                v2.IsLast = false;
                v2.OrderBy = i;

                var model2 = IsVillage(row["楼栋名称"].ToString(), v1.ID);
                if (model2 != null)
                    v2 = model2;
                else
                {
                    village.Create(v2);
                    village.Context.Commit();
                }

                var v3 = new HM_Village();
                v3.Name = row["单元名称"].ToString();
                v3.Type = 3;
                v3.EnName = "";
                v3.Alias = v3.Name;
                v3.Address = "";
                v3.Pinyi = PinYinConverter.GetShortPY(v1.Name + v2.Name + v3.Name);
                v3.Population = 0;
                v3.Summary = "";
                v3.Office = "";
                v3.TotalArea = 0;
                v3.ParentId = v2.ID;
                v3.NodePath = v1.Name + "/" + v2.Name + "/" + v3.Name;
                v3.Level = 3;
                v3.IsLast = false;
                v3.OrderBy = i;

                var model3 = IsVillage(row["单元名称"].ToString(), v2.ID);
                if (model3 != null)
                    v3 = model3;
                else
                {
                    village.Create(v3);
                    village.Context.Commit();
                }
                //客户信息
                var hous = new HM_ClientInfo();
                hous.Village = v3;
                hous.Name = dtImportDoc.Columns.Contains("客户姓名") ? row["客户姓名"].CastTo<string>() : "";
                hous.HeatArea = dtImportDoc.Columns.Contains("收费面积") ? row["收费面积"].GetObjTranNull<double>() : 0;
                hous.RoomNumber = dtImportDoc.Columns.Contains("客户编号") ? row["客户编号"].CastTo<string>() : ""; 
                hous.Cardno = dtImportDoc.Columns.Contains("门牌号") ? row["门牌号"].CastTo<string>() : ""; 
                hous.HelpeCode = dtImportDoc.Columns.Contains("助记码") ? row["助记码"].CastTo<string>() : "";
                hous.ClientType = dtImportDoc.Columns.Contains("客户类型") ? row["客户类型"].CastTo<string>() : ""; 
                hous.Birthday = Convert.ToDateTime("1999-1-1");
                hous.Floor = row["楼层"].GetObjTranNull<int>();
                hous.NetInName = "";
                hous.IDCard = "";
                hous.BeginHeatDate = Convert.ToDateTime("1999-1-1");
                hous.NetworkDate = Convert.ToDateTime("1999-1-1");
                hous.Phone = dtImportDoc.Columns.Contains("联系电话") ? row["联系电话"].CastTo<string>() : "";
                hous.ZipCode = "";
                hous.TotalHeatSourceFactory = "";
                hous.HeatSource = "";
                hous.LineType = "";
                hous.Email = "";
                hous.JobAddress = "";
                hous.HomeAddress = "";
                client.Create(hous);
                // 客户设备
                var dev = new HM_DeviceInfo();
                dev.ClientInfo = hous;
                dev.DeviceMac = dtImportDoc.Columns.Contains("温控器MAC") ? row["温控器MAC"].CastTo<string>() : "";
                dev.DeviceNumber = dtImportDoc.Columns.Contains("控制器MAC") ? row["控制器MAC"].CastTo<string>() : "";
                dev.DeviceType = 0;
                dev.HeatUnitType = HeatUnitType.KWH;
                dev.IsOpen = true;
                dev.Remark = "";
                device.Create(dev);
            }
            client.Context.Commit();
        }
        private HM_Village IsVillage(string name, Guid pid)
        {
            var repo = RF.Concrete<IHM_VillageRepository>();
            var model = repo.Find(p => p.Name == name && p.ParentId == pid);
            return model;
        }
    }
}

