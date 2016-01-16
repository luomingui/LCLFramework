using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ClientInfoHistorySpecification : Specification<HM_ClientInfoHistory> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ClientInfoHistorySpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ClientInfoHistory, bool>> GetExpression() 
        { 
            Expression<Func<HM_ClientInfoHistory, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "User_ID": // 用户
                    spec = c => c.User_ID== Guid.Parse(_keyword); 
                    break; 
                case "RecordType": // 变更类别
                    spec = c => c.RecordType==Convert.ToInt32( _keyword); 
                    break; 
                case "Record": // 变更信息
                    spec = c => c.Record.IndexOf(_keyword) != 0; 
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
                case "ClientInfo_ID": // ClientInfo_ID
                    spec = c => c.ClientInfo.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

