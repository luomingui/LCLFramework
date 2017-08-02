using LCL.Domain.Model;
using LCL.Domain.Services;
using LCL.Domain.ValueObject;
using LCL.ServiceModel;
using LCL.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCL.WebMvc.Controllers
{
    [HandleError]
    public class AdminController : CommonController
    {
        #region Common Utility Actions
        [NonAction]
        private void SaveFile(HttpPostedFileBase postedFile, string filePath, string saveName)
        {
            string phyPath = Request.MapPath("~" + filePath);
            if (!Directory.Exists(phyPath))
            {
                Directory.CreateDirectory(phyPath);
            }
            try
            {
                postedFile.SaveAs(phyPath + saveName);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);

            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileData, string folder)
        {
            var result = string.Empty;
            if (fileData != null)
            {
                string ext = Path.GetExtension(fileData.FileName);
                result = Guid.NewGuid().ToString().Replace('-', '_') + ext;
                SaveFile(fileData, Url.Content("~/Images/Products/"), result);
            }
            return Content(result);
        }
        #endregion

        #region Administration
        [Authorize]
        public ActionResult Administration()
        {
            ViewBag.Message = "Please select the administration task below.";
            return View();
        }
        #endregion

        #region Categories
        [Authorize]
        public ActionResult Categories()
        {
            using (var proxy = this.Service<IProductService>())
            {
                var categories = proxy.GetCategories();
                return View(categories);
            }
        }

        [Authorize]
        public ActionResult EditCategory(string id)
        {
            using (var proxy = this.Service<IProductService>())
            {
                var model = proxy.GetCategoryByID(new Guid(id));
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditCategory(Category category)
        {
            using (var proxy = this.Service<IProductService>())
            {
                var categoryList = new List<Category>();
                proxy.UpdateCategories(categoryList);
                return RedirectToSuccess("更新商品分类成功！", "Categories", "Admin");
            }
        }

        [Authorize]
        public ActionResult DeleteCategory(Guid id)
        {
            using (var proxy = this.Service<IProductService>())
            {
                proxy.DeleteCategories(new IDList { id });
                return RedirectToSuccess("删除商品分类成功！", "Categories", "Admin");
            }
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCategory(Category category)
        {
            using (var proxy = this.Service<IProductService>())
            {
                proxy.CreateCategories(new List<Category>() { category });
                return RedirectToSuccess("添加商品分类成功！", "Categories", "Admin");
            }
        }
        #endregion

        #region Products
        [Authorize]
        public ActionResult Products()
        {
            using (var proxy = this.Service<IProductService>())
            {
                var model = proxy.GetProducts();
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EditProduct(string id)
        {
            using (var proxy = this.Service<IProductService>())
            {
                var model = proxy.GetProductByID(new Guid(id));
                var categories = proxy.GetCategories();
                var list = new Category { ID = Guid.Empty, Name = "(未分类)", Description = "(未分类)" };
                categories.Insert(0, list);
                ViewData["categories"] = new SelectList(categories, "ID", "Name", Guid.Empty.ToString());
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProduct(Product product)
        {
            using (var proxy = this.Service<IProductService>())
            {
                proxy.UpdateProducts(new List<Product>() { product });
                //if (product.ID != Guid.Empty.ToString())
                //    proxy.Channel.CategorizeProduct(new Guid(product.ID), new Guid(product.Category.ID));
                //else
                //    proxy.Channel.UncategorizeProduct(new Guid(product.ID));
                return RedirectToSuccess("更新商品信息成功！", "Products", "Admin");
            }
        }

        [Authorize]
        public ActionResult AddProduct()
        {
            using (var proxy = this.Service<IProductService>())
            {
                var categories = proxy.GetCategories();
                categories.Insert(0, new Category { ID = Guid.Empty, Name = "(未分类)", Description = "(未分类)" });
                ViewData["categories"] = new SelectList(categories, "ID", "Name", Guid.Empty.ToString());
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddProduct(Product product)
        {
            using (var proxy = this.Service<IProductService>())
            {
                if (string.IsNullOrEmpty(product.ImageUrl))
                {
                    var fileName = Guid.NewGuid().ToString() + ".png";
                    System.IO.File.Copy(Server.MapPath("~/Images/Products/ProductImage.png"), Server.MapPath(string.Format("~/Images/Products/{0}", fileName)));
                    product.ImageUrl = fileName;
                }

                //var addedProducts = proxy.Channel.CreateProducts(new List<Product> () { product });
                //if (product.Category != null &&
                //    product.Category.ID != Guid.Empty.ToString())
                //    proxy.Channel.CategorizeProduct(addedProducts[0].ID, product.Category.ID);

                return RedirectToSuccess("添加商品信息成功！", "Products", "Admin");
            }
        }

        [Authorize]
        public ActionResult DeleteProduct(Guid id)
        {
            using (var proxy = this.Service<IProductService>())
            {
                proxy.DeleteProducts(new IDList { id });
                return RedirectToSuccess("删除商品信息成功！", "Products", "Admin");
            }
        }
        #endregion

        #region User Accounts
        [Authorize]
        public ActionResult UserAccounts()
        {
            using (var proxy = this.Service<IUserService>())
            {
                var users = proxy.GetUsers();
                var model = new List<UserAccountModel>();
                users.ForEach(u => model.Add(UserAccountModel.CreateFromDataObject(u)));
                return View(model);
            }
        }

        [Authorize]
        public ActionResult AddUserAccount()
        {
            using (var proxy = this.Service<IUserService>())
            {
                var roles = proxy.GetRoles();
                if (roles == null)
                    roles = new List<Role>();

                roles.Insert(0, new Role { ID = Guid.Empty, Name = "(未指定)", Description = "(未指定)" });

                ViewData["roles"] = new SelectList(roles, "ID", "Name", Guid.Empty.ToString());
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserAccount(UserAccountModel model)
        {
            using (var proxy = this.Service<IUserService>())
            {
                var user = model.ConvertToDataObject();
                var createdUsers = proxy.CreateUsers(new List<Domain.Model.User>() { user });
                //if (model.Role.ID != Guid.Empty.ToString())
                //    proxy.AssignRole(new Guid(createdUsers[0].ID), new Guid(model.Role.ID));
                return RedirectToSuccess("创建用户账户成功！", "UserAccounts", "Admin");
            }
        }

        [Authorize]
        public ActionResult EditUserAccount(string id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                var user = proxy.GetUserByKey(new Guid(id));
                var model = UserAccountModel.CreateFromDataObject(user);
                var roles = proxy.GetRoles();
                if (roles == null)
                    roles = new List<Role>();
                roles.Insert(0, new Role { ID = Guid.Empty, Name = "(未指定)", Description = "(未指定)" });
                if (model.Role != null)
                    ViewData["roles"] = new SelectList(roles, "ID", "Name", model.Role.ID);
                else
                    ViewData["roles"] = new SelectList(roles, "ID", "Name", Guid.Empty.ToString());
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditUserAccount(UserAccountModel model)
        {
            using (var proxy = this.Service<IUserService>())
            {
                var user = model.ConvertToDataObject();
                proxy.UpdateUsers(new List<User>() { user });

                return RedirectToSuccess("更新用户账户成功！", "UserAccounts", "Admin");
            }
        }

        [Authorize]
        public ActionResult DisableUserAccount(Guid id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.DisableUser(new Domain.Model.User { ID = id });
                return RedirectToAction("UserAccounts");
            }
        }

        [Authorize]
        public ActionResult EnableUserAccount(Guid id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.EnableUser(new Domain.Model.User { ID = id });
                return RedirectToAction("UserAccounts");
            }
        }

        [Authorize]
        public ActionResult DeleteUserAccount(Guid id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.DeleteUsers(new List<Domain.Model.User>() { new User { ID = id } });
                return RedirectToSuccess("删除用户账户成功！", "UserAccounts", "Admin");
            }
        }
        #endregion

        #region Roles
        [Authorize]
        public ActionResult Roles()
        {
            using (var proxy = this.Service<IUserService>())
            {
                var model = proxy.GetRoles();
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EditRole(string id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                var model = proxy.GetRoleByKey(new Guid(id));
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditRole(Role model)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.UpdateRoles(new List<Role>() { model });
                return RedirectToSuccess("更新账户角色成功！", "Roles", "Admin");
            }
        }

        public ActionResult DeleteRole(Guid id)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.DeleteRoles(new IDList { id });
                return RedirectToSuccess("删除账户角色成功！", "Roles", "Admin");
            }
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role model)
        {
            using (var proxy = this.Service<IUserService>())
            {
                proxy.CreateRoles(new List<Role>() { model });
                return RedirectToSuccess("添加账户角色成功！", "Roles", "Admin");
            }
        }
        #endregion

        #region SalesOrders
        public ActionResult SalesOrders()
        {
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.GetAllSalesOrders();
                return View(model);
            }
        }

        public ActionResult SalesOrder(string id)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                var model = proxy.GetSalesOrder(new Guid(id));
                return View(model);
            }
        }

        public ActionResult DispatchOrder(string id)
        {
            using (var proxy = this.Service<IOrderService>())
            {
                proxy.Dispatch(new Guid(id));
                return RedirectToSuccess(string.Format("订单 {0} 已成功发货！", id.ToUpper()), "SalesOrders", "Admin");
            }
        }
        #endregion
    }
}