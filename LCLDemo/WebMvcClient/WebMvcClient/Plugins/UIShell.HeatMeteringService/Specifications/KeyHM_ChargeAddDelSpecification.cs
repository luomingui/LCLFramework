using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ChargeAddDelSpecification : Specification<HM_ChargeAddDel> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ChargeAddDelSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ChargeAddDel, bool>> GetExpression() 
        { 
            Expression<Func<HM_ChargeAddDel, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "PID": // 父类别
                    spec = c => c.PID==Convert.ToInt32( _keyword); 
                    break; 
                case "Name": // 名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "IsOpen": // 是否开启
                    spec = c => c.IsOpen==Convert.ToBoolean( _keyword); 
                    break; 
                case "Money": // 金额
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

