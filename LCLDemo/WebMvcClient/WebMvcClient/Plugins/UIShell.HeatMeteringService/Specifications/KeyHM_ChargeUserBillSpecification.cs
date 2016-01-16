using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ChargeUserBillSpecification : Specification<HM_ChargeUserBill> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ChargeUserBillSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ChargeUserBill, bool>> GetExpression() 
        { 
            Expression<Func<HM_ChargeUserBill, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "BillNumber": // 领用票据数量
                    spec = c => c.BillNumber==Convert.ToInt32( _keyword); 
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
            } 
            return spec; 
        } 
    } 
} 

