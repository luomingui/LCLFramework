using LCL.Specifications;
using System;
using System.Linq.Expressions;

namespace UIShell.RbacPermissionService
{
    public class DepartmentTypeSpecifications : Specification<Department>
    {
        DepartmentType _departmentType = 0;
        public DepartmentTypeSpecifications(DepartmentType depTypeId)
        {
            _departmentType = depTypeId;
        }
        public override Expression<Func<Department, bool>> GetExpression()
        {
            return exp => exp.DepartmentType == _departmentType && exp.IsDelete == false;
        }
    }
    public class DepartmentXzqySpecifications : Specification<Department>
    {
        Guid _xzqyId;
        public DepartmentXzqySpecifications(string xzqyId)
        {
            if (!string.IsNullOrWhiteSpace(xzqyId))
                _xzqyId = Guid.Parse(xzqyId);
        }
        public override Expression<Func<Department, bool>> GetExpression()
        {
            return exp => exp.Xzqy.ID == _xzqyId;
        }
    }
    public class DepartmentNameSpecifications : Specification<Department>
    {
        string _depName;
        public DepartmentNameSpecifications(string depName)
        {
            _depName = depName;
        }
        public override Expression<Func<Department, bool>> GetExpression()
        {
            return exp => exp.Name.IndexOf(_depName) != 0;
        }
    }
}
