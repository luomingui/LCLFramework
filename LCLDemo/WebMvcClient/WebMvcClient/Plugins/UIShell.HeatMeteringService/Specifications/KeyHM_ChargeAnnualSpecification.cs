using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ChargeAnnualSpecification : Specification<HM_ChargeAnnual> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ChargeAnnualSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ChargeAnnual, bool>> GetExpression() 
        { 
            Expression<Func<HM_ChargeAnnual, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "Name": // 标识名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "IsOpen": // 是否当前供热年度
                    spec = c => c.IsOpen==Convert.ToBoolean( _keyword); 
                    break; 
                case "BeginDate": // 供热开始时间
                    spec = c => c.BeginDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "EndDate": // 供热结束时间
                    spec = c => c.EndDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "DnaBeginDate": // 缔纳开始日期
                    spec = c => c.DnaBeginDate==Convert.ToDateTime( _keyword); 
                    break; 
                case "BreakMoney": // 违约金比例(60%=0.6)
                    break; 
                case "StopHeatRatio": // 停热基础费比例(60%=0.6)
                    break; 
                case "AreaRatio": // 两部制热价比例 0.5
                    break; 
                case "HeatRatio": // 两部制热价比例 0.5
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
            } 
            return spec; 
        } 
    } 
} 

