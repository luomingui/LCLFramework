using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIShell.RbacPermissionService
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        List<Department> GetXzqyDepartment(Guid xzqyId);
        string GetByName(Guid id);

    }
    public class DepartmentRepository : EntityFrameworkRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public List<Department> GetXzqyDepartment(Guid xzqyId)
        {
            List<Department> list = new List<Department>();
            if (xzqyId != null && xzqyId != Guid.Empty)
            {
                ISpecification<Department> spec = Specification<Department>.Eval(p => p.Xzqy.ID == xzqyId && p.IsDelete == false);
                list = this.FindAll(spec).ToList();
            }
            return list;
        }

        protected override void DoRemove(Department entity)
        {
            DbFactory.DBA.ExecuteText("UPDATE [User] SET IsDelete=1 WHERE Department_ID='" + entity.ID + "'");
            base.DoRemove(entity);
        }
        public string GetByName(Guid id)
        {
            var model= this.GetByKey(id);
            return model.Name;
        }
    }
}
