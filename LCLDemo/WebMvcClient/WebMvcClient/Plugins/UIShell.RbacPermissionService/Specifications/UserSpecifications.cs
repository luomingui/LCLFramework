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
    public class KeyUserSpecification : Specification<User>
    {
        string _keyword = "";
        string _fieldName = "";
        public KeyUserSpecification(string keyword, string fieldName)
        {
            _keyword = keyword;
            _fieldName = fieldName;
        }
        public override Expression<Func<User, bool>> GetExpression()
        {
            Expression<Func<User, bool>> spec = null;
            switch (_fieldName)
            {
                case "Name":
                    spec = c => c.Name.IndexOf(_keyword) != -1;
                    break;
                case "Sex":
                    spec = c => c.Sex.IndexOf(_keyword) != -1;
                    break;
                case "Birthday":
                    spec = c => c.Birthday.IndexOf(_keyword) != -1;
                    break;
                case "NationalID":
                    spec = c => c.NationalID.IndexOf(_keyword) != -1;
                    break;
                case "PoliticalID":
                    spec = c => c.PoliticalID.IndexOf(_keyword) != -1;
                    break;
                case "IdCard":
                    spec = c => c.IdCard.IndexOf(_keyword) != -1;
                    break;
                case "Telephone":
                    spec = c => c.Telephone.IndexOf(_keyword) != -1;
                    break;
                case "Email":
                    spec = c => c.Email.IndexOf(_keyword) != -1;
                    break;
                case "UserQQ":
                    spec = c => c.UserQQ.IndexOf(_keyword) != -1;
                    break;
            }
            return spec;
        }
    }
}
