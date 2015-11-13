/*******************************************************  
*   
* 作者：罗敏贵  
* 说明：  
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 20154月18日 星期六 
*   
*******************************************************/
using LCL;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIShell.RbacPermissionService
{
    public interface IXzqyRepository : IRepository<Xzqy>
    {
        List<Xzqy> GetXzqyRootsList();
        List<Xzqy> GetXzqyChildList(Guid? pid);
        List<Xzqy> GetFull();
        XzqyTreeModel GetXzqyTreeModel(Guid CountyId);
        string GetByName(Guid id);
    }
    public class XzqyRepository : EntityFrameworkRepository<Xzqy>, IXzqyRepository
    {
        public XzqyRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public List<Xzqy> GetXzqyChildList(Guid? pid)
        {
            if (pid == null) pid = Guid.Empty;
            ISpecification<Xzqy> spec = Specification<Xzqy>.Eval(p => p.ParentId == Guid.Empty);
            ISpecification<Xzqy> spec1 = Specification<Xzqy>.Eval(p => p.ParentId == pid);
            IEnumerable<Xzqy> list = this.FindAll(spec);
            if (pid != Guid.Empty)
            {
                list = this.FindAll(spec1);
            }
            return list.ToList();
        }
        public List<Xzqy> GetXzqyRootsList()
        {
            ISpecification<Xzqy> spec =
               Specification<Xzqy>.Eval(p => p.ParentId == Guid.Empty);
            var list = this.FindAll(spec, e => e.OrderBy, SortOrder.Ascending);

            return list.ToList();
        }
        public List<Xzqy> GetFull()
        {
            var dt = DbFactory.DBA.QueryDataTable("SELECT * FROM Xzqy ORDER BY NodePath ASC");
            var list = dt.ToArray<Xzqy>();
            return list.ToList();
        }
        public XzqyTreeModel GetXzqyTreeModel(Guid CountyId)
        {
            var dataTable = DbFactory.DBA.QueryDataTable(@"SELECT  
        a.ID AS ProvinceId,
        a.Name AS Province ,
        b.ID AS CityId,
        b.Name AS City ,
        c.ID AS CountyId,
        c.Name AS County
FROM    dbo.Xzqy a
        INNER JOIN dbo.Xzqy b ON b.ParentId = a.ID
        INNER JOIN dbo.Xzqy c ON c.ParentId = b.ID WHERE   c.ID = '" + CountyId + "'");

            var list = dataTable.ToArray<XzqyTreeModel>();

            if (list.Count() > 0)
                return list[0];
            else
                return null;
        }
        public string GetByName(Guid id)
        {
            try
            {
                var model = this.GetByKey(id);
                return model.Name;
            }
            catch (Exception)
            {
                return "保密";
            }
        }
    }
}

