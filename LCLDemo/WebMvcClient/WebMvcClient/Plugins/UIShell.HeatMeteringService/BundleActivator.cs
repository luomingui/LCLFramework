using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using System.Data.Entity;
using System.Diagnostics;

namespace UIShell.HeatMeteringService
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

        }
        void app_AllPluginsIntialized(object sender, System.EventArgs e)
        {
            DatabaseInitializer.Initialize();
            ServiceLocator.Instance.Register<DbContext, HMSContext>();
            #region 扩展仓库
            ServiceLocator.Instance.Register<IHM_BillRepository, HM_BillRepository>();
            ServiceLocator.Instance.Register<IHM_BillTypeRepository, HM_BillTypeRepository>();
            ServiceLocator.Instance.Register<IHM_ChargeAddDelRepository, HM_ChargeAddDelRepository>();
            ServiceLocator.Instance.Register<IHM_ChargeAnnualRepository, HM_ChargeAnnualRepository>();
            ServiceLocator.Instance.Register<IHM_ChargeUserRepository, HM_ChargeUserRepository>();
            ServiceLocator.Instance.Register<IHM_ChargeUserBillRepository, HM_ChargeUserBillRepository>();
            ServiceLocator.Instance.Register<IHM_ClientBillRepository, HM_ClientBillRepository>();
            ServiceLocator.Instance.Register<IHM_ClientChargeRepository, HM_ClientChargeRepository>();
            ServiceLocator.Instance.Register<IHM_ClientCompactRepository, HM_ClientCompactRepository>();
            ServiceLocator.Instance.Register<IHM_ClientHeatChargeRepository, HM_ClientHeatChargeRepository>();
            ServiceLocator.Instance.Register<IHM_ClientInfoRepository, HM_ClientInfoRepository>();
            ServiceLocator.Instance.Register<IHM_ClientInfoHistoryRepository, HM_ClientInfoHistoryRepository>();
            ServiceLocator.Instance.Register<IHM_DeviceInfoRepository, HM_DeviceInfoRepository>();
            ServiceLocator.Instance.Register<IHM_FavorableRepository, HM_FavorableRepository>();
            ServiceLocator.Instance.Register<IHM_HeatPlantRepository, HM_HeatPlantRepository>();
            ServiceLocator.Instance.Register<IHM_HisDeviceDataRepository, HM_HisDeviceDataRepository>();
            ServiceLocator.Instance.Register<IHM_VillageRepository, HM_VillageRepository>();
            #endregion
        }
    }
}
