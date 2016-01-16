using LCL.Specifications;
using System;
using System.Linq.Expressions;

namespace UIShell.RbacPermissionService
{
    public class KeyWFActorSpecification : Specification<WFActor>
    {
        string _keyword = "";
        string _fieldName = "";
        public KeyWFActorSpecification(string keyword, string fieldName)
        {
            _keyword = keyword;
            _fieldName = fieldName;
        }
        public override Expression<Func<WFActor, bool>> GetExpression()
        {
            Expression<Func<WFActor, bool>> spec = null;
            switch (_fieldName)
            {
                case "ID": // 编号
                    break;
                case "Rout_ID": // 流程ID
                    spec = c => c.Rout_ID == Guid.Parse(_keyword);
                    break;
                case "SortNo": // 步骤序号
                    spec = c => c.SortNo == Convert.ToInt32(_keyword);
                    break;
                case "Name": // 步骤描述
                    spec = c => c.Name.IndexOf(_keyword) != 0;
                    break;
                case "BillAddess": // 单据地址
                    spec = c => c.BillAddess.IndexOf(_keyword) != 0;
                    break;
                case "IsSerial": // 操作人处理串行还是并行
                    break;
                case "IsManyPeople": // 处理方式
                    spec = c => c.IsManyPeople == Convert.ToInt32(_keyword);
                    break;
                case "IsDelete": // 删除标记
                    break;
                case "AddDate": // 添加时间
                    break;
                case "UpdateDate": // 更新时间
                    break;
            }
            return spec;
        }
    }
}

