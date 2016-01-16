using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;
using UIShell.HeatMeteringService.Controllers;

namespace UIShell.HeatMeteringPlugin
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            app.ModuleOperations += app_ModuleOperations;
            app.AllPluginsIntialized += app_AllPluginsIntialized;
        }
        void app_ModuleOperations(object sender, AppInitEventArgs e)
        {
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "LCL",
            //    Image = ".icon-application",
            //    Bundle = this,
            //    Children = 
            //        { 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "票据入库", CustomUI="/UIShell.HeatMeteringPlugin/HM_Bill/Index",EntityType=typeof(HM_BillController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "票据分类", CustomUI="/UIShell.HeatMeteringPlugin/HM_BillType/Index",EntityType=typeof(HM_BillTypeController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "费用增减类别", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeAddDel/Index",EntityType=typeof(HM_ChargeAddDelController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "年度供热单价", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeAnnual/Index",EntityType=typeof(HM_ChargeAnnualController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "收费员区域配置", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeUser/Index",EntityType=typeof(HM_ChargeUserController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "收费员领用票据", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeUserBill/Index",EntityType=typeof(HM_ChargeUserBillController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "客户票据", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientBill/Index",EntityType=typeof(HM_ClientBillController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "热计量采暖费收费单", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientCharge/Index",EntityType=typeof(HM_ClientChargeController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "供热合同", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientCompact/Index",EntityType=typeof(HM_ClientCompactController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "热计量采暖费结算单", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientHeatCharge/Index",EntityType=typeof(HM_ClientHeatChargeController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "客户信息", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientInfo/Index",EntityType=typeof(HM_ClientInfoController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "采暖变更历史", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientInfoHistory/Index",EntityType=typeof(HM_ClientInfoHistoryController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "客户设备管理", CustomUI="/UIShell.HeatMeteringPlugin/HM_DeviceInfo/Index",EntityType=typeof(HM_DeviceInfoController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "预交优惠率", CustomUI="/UIShell.HeatMeteringPlugin/HM_Favorable/Index",EntityType=typeof(HM_FavorableController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "供热站", CustomUI="/UIShell.HeatMeteringPlugin/HM_HeatPlant/Index",EntityType=typeof(HM_HeatPlantController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "抄表数据", CustomUI="/UIShell.HeatMeteringPlugin/HM_HisDeviceData/Index",EntityType=typeof(HM_HisDeviceDataController)}, 
            //            new MvcModuleMeta{ Image = ".icon-application",  Label = "小区", CustomUI="/UIShell.HeatMeteringPlugin/HM_Village/Index",EntityType=typeof(HM_VillageController)}, 
            //        }
            //});
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "计量收费",
                Image = "icon-money-yen",
                Bundle = this,
                Children = 
                    { 
                        new MvcModuleMeta{ Image = "icon-coins-add",  Label = "客户收费", CustomUI="/UIShell.HeatMeteringPlugin/HM_Village/Index",EntityType=typeof(HM_VillageController)}, 
                        new MvcModuleMeta{ Image = "icon-coins-delete",  Label = "客户退费", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientInfo/Index",EntityType=typeof(HM_ClientInfoController)}, 
                        new MvcModuleMeta{ Image = "icon-money-pound",  Label = "客户停热", CustomUI="/UIShell.HeatMeteringPlugin/HM_DeviceInfo/Index",EntityType=typeof(HM_DeviceInfoController)}, 
                        new MvcModuleMeta{ Image = "icon-vcard-delete",  Label = "客户解约", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientCompact/Index",EntityType=typeof(HM_ClientCompactController)}, 
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "客户管理",
                Image = "icon-group32",
                Bundle = this,
                Children = 
                    { 
                        new MvcModuleMeta{ Image = "icon-rgb",  Label = "小区管理", CustomUI="/UIShell.HeatMeteringPlugin/HM_Village/Index",EntityType=typeof(HM_VillageController)}, 
                        new MvcModuleMeta{ Image = "icon-user-home",  Label = "客户信息", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientInfo/Index",EntityType=typeof(HM_ClientInfoController)}, 
                        new MvcModuleMeta{ Image = "icon-vcard",  Label = "客户设备", CustomUI="/UIShell.HeatMeteringPlugin/HM_DeviceInfo/Index",EntityType=typeof(HM_DeviceInfoController)}, 
                        new MvcModuleMeta{ Image = "icon-vcard-add",  Label = "供热合同", CustomUI="/UIShell.HeatMeteringPlugin/HM_ClientCompact/Index",EntityType=typeof(HM_ClientCompactController)}, 
                        new MvcModuleMeta{ Image="icon-user-group", Label = "一键导入", CustomUI="/UIShell.RbacManagementPlugin/HM_ClientInfo/Import"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "热费设置",
                Image = "icon-cog",
                Bundle = this,
                Children = 
                    { 
                        new MvcModuleMeta{ Image = "icon-coins-add",  Label = "供热单价", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeAnnual/Index",EntityType=typeof(HM_ChargeAnnualController)}, 
                        new MvcModuleMeta{ Image = "icon-money-add",  Label = "费用增减", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeAddDel/Index",EntityType=typeof(HM_ChargeAddDelController)}, 
                        new MvcModuleMeta{ Image = "icon-money-dollar",  Label = "预交优惠", CustomUI="/UIShell.HeatMeteringPlugin/HM_Favorable/Index",EntityType=typeof(HM_FavorableController)}, 
                        new MvcModuleMeta{ Image = "icon-chart-pie",  Label = "区域配置", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeUser/Index",EntityType=typeof(HM_ChargeUserController)}, 
                        new MvcModuleMeta{ Image = "icon-color",  Label = "领用票据", CustomUI="/UIShell.HeatMeteringPlugin/HM_ChargeUserBill/Index",EntityType=typeof(HM_ChargeUserBillController)},      
                        new MvcModuleMeta{ Image = "icon-chart-line",  Label = "供暖热站", CustomUI="/UIShell.HeatMeteringPlugin/HM_HeatPlant/Index",EntityType=typeof(HM_HeatPlantController)}, 
                        new MvcModuleMeta{ Image = "icon-application-go",  Label = "票据入库", CustomUI="/UIShell.HeatMeteringPlugin/HM_Bill/Index",EntityType=typeof(HM_BillController)}, 
                        new MvcModuleMeta{ Image = "icon-outline",  Label = "票据分类", CustomUI="/UIShell.HeatMeteringPlugin/HM_BillType/Index",EntityType=typeof(HM_BillTypeController)}, 
                    }
            }); 
        }
        void app_AllPluginsIntialized(object sender, System.EventArgs e)
        {

        }
    }
}
