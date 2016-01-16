using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace UIShell.RbacPermissionService
{
    public class RbacPrincipal
    {
        public static bool Login(string username, string password)
        {
            bool IsFlg = false;
            MyUserDataPrincipal userData = null;
            if (username == "admin" && password == "123456")
            {
                userData = new MyUserDataPrincipal();
                userData.UserId = Guid.Empty;

                userData.UserModel.Add("UserName", "admin");
                userData.UserModel.Add("TelePhone", "13026209315");
                userData.UserModel.Add("DepId", Guid.Empty);
                userData.UserModel.Add("DepName", "LCL");
                IsFlg = true;
            }
            else
            {
                var repo = RF.Concrete<IUserRepository>();
                var user = repo.GetBy(username, password);
                if (user != null)
                {
                    var rolesIds = repo.GetRolesIds(user.ID);
                    var groupIds = repo.GetGroupIds(user.ID);

                    userData = new MyUserDataPrincipal();
                    userData.UserId = user.ID;
                    userData.RoleId = rolesIds;
                    userData.GroupId = groupIds;
                    userData.UserModel.Add("UserName", user.Name);
                    userData.UserModel.Add("TelePhone", user.Telephone);
                    userData.UserModel.Add("DepId", user.Department == null ? Guid.Empty : user.Department.ID);
                    userData.UserModel.Add("DepName", user.Department == null ? "" : user.Department.Name);
                    IsFlg = true;
                }
            }
            if (IsFlg)
                MyFormsAuthentication<MyUserDataPrincipal>.SetAuthCookie(username, userData, false); //保存Cookie
            return IsFlg;
        }
        public static void Logout()
        {
            LEnvironment.Principal = new MyUserDataPrincipal();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Clear();
        }
        public static CurrentUser CurrentUser
        {
            get
            {
                var model = new CurrentUser();
                var identity = LEnvironment.Principal as MyFormsPrincipal<MyUserDataPrincipal>;
                if (identity != null)
                {
                    model.RoleId = identity.UserData.RoleId;
                    model.GroupId = identity.UserData.GroupId;
                    model.UserId = identity.UserData.UserId;
                    model.UserName = identity.UserData.UserModel["UserName"].GetObjTranNull<string>();
                    model.TelePhone = identity.UserData.UserModel["TelePhone"].GetObjTranNull<string>();
                    model.DepId = identity.UserData.UserModel["DepId"].GetObjTranNull<Guid>();
                    model.DepName = identity.UserData.UserModel["DepName"].GetObjTranNull<string>();
                }
                return model;
            }
        }
    }

    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string TelePhone { get; set; }
        public Guid DepId { get; set; }
        public string DepName { get; set; }
        public List<Guid> RoleId { get; set; }
        public List<Guid> GroupId { get; set; }
    }
}
