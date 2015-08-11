/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 部门
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2015年5月18日
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class DepartmentController : RbacController<Department>
    {
        IRepository<Department> repo = RF.Concrete<IDepartmentRepository>();
        public DepartmentController()
        {

        }

        #region Department
        [Permission("首页", "Index")]
        [BizActivityLog("查看部门", "Name")]
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<Department>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;


            int deptype = 1;

            var villagelist = repo.FindAll(new DepartmentTypeSpecifications((DepartmentType)deptype)).ToList();

            var contactLitViewModel = new DepartmentPagedListViewModel(pageNum, pageSize.Value, villagelist.ToList());
            contactLitViewModel.DepartmentType = (DepartmentType)deptype;
            return View(contactLitViewModel);
        }
        [Permission("添加", "Add")]
        [BizActivityLog("添加部门", "Name")]
        public override ActionResult Add(AddOrEditViewModel<Department> model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                ModelErrorMsg();
                return View("AddOrEdit", model);
            }
            repo.Create(model.Entity);
            repo.Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        [Permission("删除部门", "Delete")]
        [BizActivityLog("删除部门", "Name")]
        public override ActionResult Delete(Department model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            var entity = RF.Concrete<IDepartmentRepository>().GetByKey(model.ID);
            return base.Delete(entity, currentPageNum, pageSize, collection);
        }
        #endregion

        #region DepartmentUser
        [Permission("部门员工", "DepartmentUser")]
        [BizActivityLog("查看部门员工", "Name")]
        public ActionResult DepartmentUser(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<User>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;
            Guid depId = Guid.Empty;
            string depIdstr = LRequest.GetString("depId");
            if (!string.IsNullOrWhiteSpace(depIdstr))
            {
                depId = Guid.Parse(depIdstr);
            }

            var villagelist = RF.Concrete<IUserRepository>().GetDepartmentUsers(depId);
            var pageList = new UserPagedListViewModel(pageNum, pageSize.Value, villagelist.ToList());
            pageList.DepId = depId;
            return View(pageList);
        }
        public ActionResult DepartmentUserAddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<User>.DefaultPageSize;
            }
            Guid depId = Guid.Empty;
            string depIdstr = LRequest.GetString("depId");
            if (!string.IsNullOrWhiteSpace(depIdstr))
            {
                depId = Guid.Parse(depIdstr);
            }
            SetPageRoleNames();
            if (!id.HasValue)
            {
                return View(new UserAddOrEditViewModel
                {
                    Added = true,
                    DepId = depId,
                    Entity = new User(),
                    LstRoles = new string[] { },
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var repo = RF.FindRepo<User>();
                var village = repo.GetByKey(id.Value);
                var list = RF.Concrete<IRoleRepository>().GetRoleByUserId(id.Value);
                string[] arrRole = list.Select(p => p.Name).ToArray();

                return View(new UserAddOrEditViewModel
                {
                    Added = false,
                    DepId = depId,
                    Entity = village,
                    LstRoles = arrRole,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        [Permission("添加部门员工", "DepartmentUserAdd")]
        [BizActivityLog("添加部门员工", "Name")]
        public ActionResult DepartmentUserAdd(UserAddOrEditViewModel model, FormCollection collection)
        {
            Guid depId = Guid.Empty;
            if (collection.GetValues("depId") != null)
            {
                depId = Guid.Parse(collection.GetValue("depId").AttemptedValue);
            }

            if (!ModelState.IsValid)
            {
                return View("DepartmentUserAddOrEdit", model);
            }

            string[] lstRoles = null;
            if (collection.GetValues("checkboxRole") != null)
            {
                string strRoles = collection.GetValue("checkboxRole").AttemptedValue;
                lstRoles = strRoles.Split(',');
            }

            var urepo = RF.Concrete<IUserRepository>();
            var drepo = RF.Concrete<IDepartmentRepository>();
            var rrepo = RF.Concrete<IRoleRepository>();

            using (var scope = new TransactionScope())
            {
                model.Entity.Department = drepo.GetByKey(depId);

                urepo.Create(model.Entity);
                urepo.Context.Commit();

                rrepo.AddUserToRoles(model.Entity.ID, lstRoles);

                scope.Complete();
            }

            return RedirectToAction("DepartmentUser", new { depId = depId, currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        [Permission("修改部门员工", "DepartmentUserEdit")]
        [BizActivityLog("修改部门员工", "Name")]
        public ActionResult DepartmentUserEdit(UserAddOrEditViewModel model, FormCollection collection)
        {
            Guid depId = Guid.Empty;
            if (collection.GetValues("depId") != null)
            {
                depId = Guid.Parse(collection.GetValue("depId").AttemptedValue);
            }

            RF.Concrete<IUserRepository>().Update(model.Entity);
            RF.Concrete<IUserRepository>().Context.Commit();

            return RedirectToAction("DepartmentUser", new { depId = depId, currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        [Permission("删除部门员工", "DepartmentUserDelete")]
        [BizActivityLog("删除部门员工", "Name")]
        public ActionResult DepartmentUserDelete(User model, int? currentPageNum, int? pageSize, FormCollection collection)
        {

            Guid depId = Guid.Empty;
            string depIdstr = LRequest.GetString("depId");
            if (!string.IsNullOrWhiteSpace(depIdstr))
            {
                depId = Guid.Parse(depIdstr);
            }

            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<User>.DefaultPageSize;
            }

            RF.Concrete<IUserRepository>().Delete(model);
            RF.Concrete<IUserRepository>().Context.Commit();

            return RedirectToAction("DepartmentUser", new { depId = depId, currentPageNum = currentPageNum, pageSize = pageSize });
        }
        public void SetPageRoleNames()
        {
            var list = RF.Concrete<IRoleRepository>().FindAll();
            if (ViewData.Keys.Contains("rolesList"))
            {
                ViewData.Remove("rolesList");
            }
            ViewData.Add("rolesList", list.ToList());
        }
        #endregion
    }
}

