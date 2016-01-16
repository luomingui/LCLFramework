using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_HeatPlantSpecification : Specification<HM_HeatPlant> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_HeatPlantSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_HeatPlant, bool>> GetExpression() 
        { 
            Expression<Func<HM_HeatPlant, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "PID": // 供热站
                    spec = c => c.PID==Convert.ToInt32( _keyword); 
                    break; 
                case "Name": // 名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "NameShort": // 简称
                    spec = c => c.NameShort.IndexOf(_keyword) != 0; 
                    break; 
                case "Address": // 地址
                    spec = c => c.Address.IndexOf(_keyword) != 0; 
                    break; 
                case "AdminName": // 负责人
                    spec = c => c.AdminName.IndexOf(_keyword) != 0; 
                    break; 
                case "Phone": // 联系电话
                    spec = c => c.Phone.IndexOf(_keyword) != 0; 
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
            } 
            return spec; 
        } 
    } 
} 

