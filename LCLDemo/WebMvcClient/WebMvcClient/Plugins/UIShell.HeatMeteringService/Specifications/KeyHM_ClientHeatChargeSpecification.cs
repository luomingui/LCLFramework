using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ClientHeatChargeSpecification : Specification<HM_ClientHeatCharge> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ClientHeatChargeSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ClientHeatCharge, bool>> GetExpression() 
        { 
            Expression<Func<HM_ClientHeatCharge, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "BeginHeat": // 供热开始表数
                    break; 
                case "EndHeat": // 供热结束表数
                    break; 
                case "UseHeat": // 用热量（KMH）
                    break; 
                case "MoneyHeat": // 计量热费
                    break; 
                case "MoneyBaseHeat": // 基础热费
                    break; 
                case "MoneyAdvance": // 预收金额
                    break; 
                case "MoneyOrRefunded": // 退（补）金额
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
                case "ChargeAnnual_ID": // ChargeAnnual_ID
                    spec = c => c.ChargeAnnual.ID== Guid.Parse(_keyword); 
                    break; 
                case "ChargeUser_ID": // ChargeUser_ID
                    spec = c => c.ChargeUser.ID== Guid.Parse(_keyword); 
                    break; 
                case "ClientInfo_ID": // ClientInfo_ID
                    spec = c => c.ClientInfo.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

