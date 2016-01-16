using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_DeviceInfoSpecification : Specification<HM_DeviceInfo> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_DeviceInfoSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_DeviceInfo, bool>> GetExpression() 
        { 
            Expression<Func<HM_DeviceInfo, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "DeviceType": // 设备类型
                    spec = c => c.DeviceType==Convert.ToInt32( _keyword); 
                    break; 
                case "IsOpen": // 表开启状态
                    spec = c => c.IsOpen==Convert.ToBoolean( _keyword); 
                    break; 
                case "HeatUnitType": // 计量单位
                    spec = c => c.HeatUnitType == (HeatUnitType)Convert.ToInt32(_keyword); 
                    break; 
                case "DeviceMac": // 厂商编码
                    spec = c => c.DeviceMac.IndexOf(_keyword) != 0; 
                    break; 
                case "DeviceNumber": // 设备号
                    spec = c => c.DeviceNumber.IndexOf(_keyword) != 0; 
                    break; 
                case "Remark": // 备注
                    spec = c => c.Remark.IndexOf(_keyword) != 0; 
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
                case "ClientInfo_ID": // ClientInfo_ID
                    spec = c => c.ClientInfo.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

