using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_HisDeviceDataSpecification : Specification<HM_HisDeviceData> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_HisDeviceDataSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_HisDeviceData, bool>> GetExpression() 
        { 
            Expression<Func<HM_HisDeviceData, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "TotalHeat": // 累计用热量
                    break; 
                case "SupplyWaterT": // 入口温度
                    break; 
                case "BackWaterT": // 出口温度
                    break; 
                case "DifferenceT": // 温差
                    break; 
                case "AdminName": // 抄表人
                    spec = c => c.AdminName.IndexOf(_keyword) != 0; 
                    break; 
                case "RealTime": // 抄表时间
                    spec = c => c.RealTime==Convert.ToDateTime( _keyword); 
                    break; 
                case "IsDelete": // 删除标记
                    spec = c => c.IsDelete==Convert.ToBoolean( _keyword); 
                    break; 
                case "AddDate": // 添加时间
                    spec = c => c.AddDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "UpdateDate": // 更新时间
                    spec = c => c.UpdateDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "DeviceInfo_ID": // DeviceInfo_ID
                    spec = c => c.DeviceInfo.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

