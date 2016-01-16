using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_BillTypeSpecification : Specification<HM_BillType> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_BillTypeSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_BillType, bool>> GetExpression() 
        { 
            Expression<Func<HM_BillType, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "Name": // 票据种类名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "PageSum": // 每本页数
                    spec = c => c.PageSum==Convert.ToInt32( _keyword); 
                    break; 
                case "BillLength": // 票据编号长度
                    spec = c => c.BillLength==Convert.ToInt32( _keyword); 
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

