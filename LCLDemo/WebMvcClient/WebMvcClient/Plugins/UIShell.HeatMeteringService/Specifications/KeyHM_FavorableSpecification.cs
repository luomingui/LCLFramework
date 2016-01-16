using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_FavorableSpecification : Specification<HM_Favorable> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_FavorableSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_Favorable, bool>> GetExpression() 
        { 
            Expression<Func<HM_Favorable, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "BeginDate": // 优惠开始时间
                    spec = c => c.BeginDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "EndDate": // 优惠结束时间
                    spec = c => c.EndDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "Money": // 优惠率 5%
                    break; 
                case "ClientTypeIdList": // 优惠范围
                    spec = c => c.ClientTypeIdList.IndexOf(_keyword) != 0; 
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
            } 
            return spec; 
        } 
    } 
} 

