using System; 
using LCL.Specifications; 
using System.Linq.Expressions; 
 
namespace UIShell.HeatMeteringService 
{ 
     /// <summary> 
    ///  根据关键字进行查询 
    /// </summary> 
    public class KeyHM_ClientInfoSpecification : Specification<HM_ClientInfo> 
    { 
        string _keyword = ""; 
        string _fieldName = ""; 
        public KeyHM_ClientInfoSpecification(string keyword, string fieldName) 
        { 
            _keyword = keyword; 
            _fieldName = fieldName; 
        } 
        public override Expression<Func<HM_ClientInfo, bool>> GetExpression() 
        { 
            Expression<Func<HM_ClientInfo, bool>> spec = null; 
            switch (_fieldName) 
            { 
                case "ID": // 编号
                    spec = c => c.ID== Guid.Parse(_keyword); 
                    break; 
                case "ClientType": // 客户类型
                    spec = c => c.ClientType.IndexOf(_keyword) != 0; 
                    break; 
                case "HeatType": // 取暖类型
                    spec = c => c.HeatType==Convert.ToInt32( _keyword); 
                    break; 
                case "HelpeCode": // 用户编码
                    spec = c => c.HelpeCode.IndexOf(_keyword) != 0; 
                    break; 
                case "Cardno": // 房间卡号
                    spec = c => c.Cardno.IndexOf(_keyword) != 0; 
                    break; 
                case "Name": // 用户名称
                    spec = c => c.Name.IndexOf(_keyword) != 0; 
                    break; 
                case "RoomNumber": // 房间号
                    spec = c => c.RoomNumber.IndexOf(_keyword) != 0; 
                    break; 
                case "BuildArea": // 建筑面积
                    break; 
                case "InsideBuildArea": // 套内建筑面积
                    break; 
                case "Superelevation": // 超高
                    break; 
                case "InterlayerArea": // 阁楼夹层面积
                    break; 
                case "InterlayerHeatArea": // 阁楼夹层收费面积
                    break; 
                case "InsideArea": // 套内面积
                    break; 
                case "HeatArea": // 收费面积
                    break; 
                case "Floor": // 楼层
                    spec = c => c.Floor==Convert.ToInt32( _keyword); 
                    break; 
                case "HeatState": // 报停/强停/复热
                    spec = c => c.HeatState==Convert.ToInt32( _keyword); 
                    break; 
                case "Email": // 邮箱
                    spec = c => c.Email.IndexOf(_keyword) != 0; 
                    break; 
                case "Phone": // 联系电话
                    spec = c => c.Phone.IndexOf(_keyword) != 0; 
                    break; 
                case "JobAddress": // 工作地址
                    spec = c => c.JobAddress.IndexOf(_keyword) != 0; 
                    break; 
                case "HomeAddress": // 家庭地址
                    spec = c => c.HomeAddress.IndexOf(_keyword) != 0; 
                    break; 
                case "Gender": // 性别
                    spec = c => c.Gender==Convert.ToBoolean( _keyword); 
                    break; 
                case "Birthday": // 生日
                    spec = c => c.Birthday==Convert.ToDateTime( _keyword); 
                    break; 
                case "ZipCode": // 邮政编码
                    spec = c => c.ZipCode.IndexOf(_keyword) != 0; 
                    break; 
                case "IDCard": // 身份证号
                    spec = c => c.IDCard.IndexOf(_keyword) != 0; 
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
                case "Village_ID": // Village_ID
                    spec = c => c.Village.ID== Guid.Parse(_keyword); 
                    break; 
            } 
            return spec; 
        } 
    } 
} 

