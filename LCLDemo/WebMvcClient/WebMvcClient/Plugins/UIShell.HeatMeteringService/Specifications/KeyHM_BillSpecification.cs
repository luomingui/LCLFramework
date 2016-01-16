using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_BillSpecification : Specification<HM_Bill> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_BillSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_Bill, bool>> GetExpression() 
        { 
            Expression<Func<HM_Bill, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "StartNumber": // 起始编号
                    spec = c => c.StartNumber==Convert.ToInt32( _keyword); 
                    break; 
                case "EndNumber": // 结束编号
                    spec = c => c.EndNumber==Convert.ToInt32( _keyword); 
                    break; 
                case "VersionNumber": // 版本编号
                    spec = c => c.VersionNumber==Convert.ToInt32( _keyword); 
                    break; 
                case "Quantity": // 票据规格 数目【本】
                    spec = c => c.Quantity==Convert.ToInt32( _keyword); 
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
                case "BillType_ID": // BillType_ID
                    spec = c => c.BillType.ID== Guid.Parse(_keyword); 
                    break; 
                case "ChargeAnnual_ID": // ChargeAnnual_ID
                    spec = c => c.ChargeAnnual.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

