using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_VillageSpecification : Specification<HM_Village> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_VillageSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_Village, bool>> GetExpression() 
        { 
            Expression<Func<HM_Village, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "Name": // 小区名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "Pinyi": // 拼音简称
                    spec = c => c.Pinyi.IndexOf(_keyword) != 0; 
                    break; 
                case "Type": // 小区类型
                    spec = c => c.Type==Convert.ToInt32( _keyword); 
                    break; 
                case "EnName": // 外文名称
                    spec = c => c.EnName.IndexOf(_keyword) != 0; 
                    break; 
                case "Alias": // 别名
                    spec = c => c.Alias.IndexOf(_keyword) != 0; 
                    break; 
                case "Population": // 人口
                    spec = c => c.Population==Convert.ToInt32( _keyword); 
                    break; 
                case "TotalArea": // 面积
                    spec = c => c.TotalArea==Convert.ToInt32( _keyword); 
                    break; 
                case "Office": // 行政区域
                    spec = c => c.Office.IndexOf(_keyword) != 0; 
                    break; 
                case "Summary": // 概况
                    spec = c => c.Summary.IndexOf(_keyword) != 0; 
                    break; 
                case "Address": // 小区地址
                    spec = c => c.Address.IndexOf(_keyword) != 0; 
                    break; 
                case "IsLast": // 是否最后一级
                    spec = c => c.IsLast==Convert.ToBoolean( _keyword); 
                    break; 
                case "Level": // 树形深度
                    spec = c => c.Level==Convert.ToInt32( _keyword); 
                    break; 
                case "NodePath": // 树形路径
                    spec = c => c.NodePath.IndexOf(_keyword) != 0; 
                    break; 
                case "OrderBy": // 排序
                    spec = c => c.OrderBy==Convert.ToInt32( _keyword); 
                    break; 
                case "ParentId": // 上一级
                    spec = c => c.ParentId== Guid.Parse(_keyword); 
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

