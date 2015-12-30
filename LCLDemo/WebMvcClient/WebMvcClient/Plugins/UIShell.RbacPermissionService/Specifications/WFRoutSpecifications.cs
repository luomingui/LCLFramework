using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
     /// <summary>
    ///  根据关键字进行查询
    ///  Linq模糊查询 http://www.cnblogs.com/cracker/archive/2011/02/07/1949669.html
    /// </summary>
    public class KeyWFRoutSpecification : Specification<WFRout>
    {
        string _keyword = "";
        string _fieldName = "";
        public KeyWFRoutSpecification(string keyword, string fieldName)
        {
            _keyword = keyword;
            _fieldName = fieldName;
        }
        public override Expression<Func<WFRout, bool>> GetExpression()
        {
            Expression<Func<WFRout, bool>> spec = null;
            switch (_fieldName)
            {
                case "ID":
                    spec = c => c.ID == Guid.Parse(_keyword);
                    break;
                case "Name":
                    spec = c => c.Name.IndexOf(_keyword) != -1;
                    break;
            }
            return spec;
        }
    }
}
