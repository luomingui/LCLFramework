using LCL;
using LCL.MetaModel;
/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 角色权限
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class RoleAuthorityController : RbacController<RoleAuthority>
    {
        IRoleRepository repo = RF.Concrete<IRoleRepository>();
        public RoleAuthorityController()
        {

        }
        [Permission("首页", "Index")]
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedResult<Xzqy>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;
            string roleId = LRequest.GetString("roleId");

            #region MyRegion
            ViewData["RoleId"] = roleId;
            DataTable dt = DbFactory.DBA.QueryDataTable("SELECT * FROM [Role] WHERE ID='" + roleId + "'");
            if (dt.Rows.Count > 0)
            {
                ViewData["RoleName"] = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                ViewData["RoleName"] = "";
            }
            #endregion

            var list = repo.GetRoleAuthority(Guid.Parse(roleId));

            var contactLitViewModel = new PagedResult<RoleAuthority>(pageNum, pageSize.Value, list);

            return View(contactLitViewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [Permission("保存", "Save")]
        [BizActivityLog("修改权限", "RoleId")]
        public ActionResult Save(FormCollection collection)
        {
            Guid _roleId = Guid.Empty;
            string[] permissions = { };
            if (collection.GetValues("RoleId") != null)
            {
                _roleId = Guid.Parse(collection.GetValue("RoleId").AttemptedValue);
            }
            if (collection.GetValues("moduleName") != null)
            {
                string strRoles = collection.GetValue("moduleName").AttemptedValue;
                permissions = strRoles.Split(',');
            }
            #region MyRegion
            if (permissions.Length > 0 && _roleId != Guid.Empty)
            {
                DbFactory.DBA.ExecuteText("DELETE RoleAuthority WHERE Role_ID='" + _roleId + "'");

                var role = RF.Concrete<IRoleRepository>().GetByKey(_roleId);
                var repo = RF.Concrete<IRoleAuthorityRepository>();
                for (int i = 0; i < permissions.Length; i++)
                {
                    RoleAuthority authority = new RoleAuthority();
                    authority.Role = role;
                    authority.AuthorityType = AuthorityType.菜单;

                    string text = permissions[i];

                    if (text.Contains("/"))
                    {
                        string[] p = text.Split('/');
                        if (p.Length == 2)
                        {
                            authority.BlockKey = p[0];
                            authority.ModuleKey = p[1];
                            authority.OperationKey = "";
                            authority.Url = "";
                            authority.Level = 2;
                            authority.NodePath = text;
                        }
                        if (p.Length == 3)
                        {
                            authority.BlockKey = p[0];
                            authority.ModuleKey = p[1];
                            authority.OperationKey = p[2];
                            authority.Level = 3;
                            authority.NodePath = text;

                            var model = CommonModel.Modules.FindModule(authority.BlockKey);
                            if (model != null)
                            {
                                var tet = model.Children.LastOrDefault(m => m.Label == authority.ModuleKey);
                                if (tet != null && !string.IsNullOrWhiteSpace(tet.CustomUI))
                                {
                                    string _url = tet.CustomUI.Substring(0, tet.CustomUI.LastIndexOf('/'));
                                    authority.Url = _url + "/" + authority.OperationKey;
                                }
                            }

                        }
                    }
                    else
                    {
                        authority.Level = 1;
                        authority.NodePath = text;
                        authority.BlockKey = text;
                        authority.ModuleKey = "";
                        authority.OperationKey = "";
                        authority.Url = "";
                    }
                    repo.AddDbo(authority);
                }
            }
            #endregion

            return Redirect("../Role/Index");
        }
        //public void ddlRoleAuthority(Guid dtId)
        //{
        //    var repo = RF.FindRepo<Role>();
        //    var list = repo.FindAll();

        //    List<SelectListItem> selitem = new List<SelectListItem>();
        //    if (list.Count() > 0)
        //    {
        //        var roots = list;
        //        foreach (var item in roots)
        //        {
        //            selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
        //        }
        //    }
        //    selitem.Insert(0, new SelectListItem { Text = "==所属角色==", Value = "-1" });
        //    ViewData["ddlRoleAuthority"] = selitem;
        //}


    }
}

