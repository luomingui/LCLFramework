using LCL.Domain.Model;
using LCL.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Security;

namespace LCL.WebMvc
{
    public class LCLMembershipProvider : MembershipProvider
    {
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval = false;
        private bool requiresQuestionAndAnswer = false;
        private bool requiresUniqueEmail = true;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private int minRequiredPasswordLength;
        private int minRequiredNonalphanumericCharacters;
        private string passwordStrengthRegularExpression;
        private MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Clear;

        private LCLMembershipUser ConvertFrom(User userObj)
        {
            if (userObj == null)
                return null;

            LCLMembershipUser user = new LCLMembershipUser("LCLMembershipProvider",
                userObj.UserName,
                userObj.ID,
                userObj.Email,
                "",
                "",
                true,
                userObj.IsDisabled,
                userObj.DateRegistered,
                userObj.DateLastLogon,
                DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue,
                userObj.Contact,
                userObj.PhoneNumber,
                userObj.ContactAddress,
                userObj.DeliveryAddress);

            return user;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "LCLMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership Provider for LCL");
            }

            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonalphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public LCLMembershipUser CreateUser(string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            string contact,
            string phoneNumber,
            Address contactAddress,
            Address deliveryAddress,
            out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            LCLMembershipUser user = GetUser(username, true) as LCLMembershipUser;
            if (user == null)
            {

                var userDataObjects = new List<User>
                {
                    new User
                    {
                        UserName = username,
                        Password = password,
                        Contact = contact,
                        DateLastLogon = null,
                        DateRegistered = DateTime.Now,
                        Email = email,
                        IsDisabled = false,
                        PhoneNumber = phoneNumber,
                        ContactAddress = contactAddress,
                        DeliveryAddress = deliveryAddress
                    }
                };
                RF.Service<IUserService>().CreateUsers(userDataObjects);
                status = MembershipCreateStatus.Success;
                return GetUser(username, true) as LCLMembershipUser;
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
        }

        public override MembershipUser CreateUser(string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, null, null, null, null, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                var dataObject = RF.Service<IUserService>().GetUserByName(username);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return this.enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return this.enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();

            var dataObject = RF.Service<IUserService>().GetUserByEmail(emailToMatch);
            totalRecords = 1;
            col.Add(this.ConvertFrom(dataObject));
            return col;

        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();

            var dataObject = RF.Service<IUserService>().GetUserByName(usernameToMatch);
            totalRecords = 1;
            col.Add(this.ConvertFrom(dataObject));
            return col;

        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();

            var dataObjects = RF.Service<IUserService>().GetUsers();
            if (dataObjects != null)
            {
                totalRecords = dataObjects.Count;
                foreach (var dataObject in dataObjects)
                    col.Add(this.ConvertFrom(dataObject));
            }
            else
                totalRecords = 0;
            return col;


        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var dataObject = RF.Service<IUserService>().GetUserByName(username);
            if (dataObject == null)
                return null;
            return ConvertFrom(dataObject);


        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var dataObject = RF.Service<IUserService>().GetUserByKey((Guid)providerUserKey);
            if (dataObject == null)
                return null;
            return ConvertFrom(dataObject);
        }

        public override string GetUserNameByEmail(string email)
        {
            var dataObject = RF.Service<IUserService>().GetUserByEmail(email);
            if (dataObject == null)
                return null;
            return dataObject.UserName;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return this.maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this.minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return this.minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return this.passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return this.passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return this.passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return this.requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return this.requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return RF.Service<IUserService>().ValidateUser(username, password);
        }
    }
    /// <summary>
    /// 表示用于系统的MembershipUser类型。
    /// </summary>
    public class LCLMembershipUser : MembershipUser
    {
        private string providerName;


        private string passwordQuestion;
        private string comment;
        private bool isApproved;
        private bool isDisabled;
        private DateTime dateRegistered;
        private DateTime? dateLastLogon;
        private DateTime minValue1;
        private DateTime minValue2;
        private DateTime minValue3;
        private Address contactAddress;
        private Address deliveryAddress;
        #region Ctor

        public LCLMembershipUser(string providerName, string userName, Guid iD, string email, string passwordQuestion, string comment, bool isApproved, bool isDisabled, DateTime dateRegistered, DateTime? dateLastLogon, DateTime minValue1, DateTime minValue2, DateTime minValue3, string contact, string phoneNumber, Address contactAddress, Address deliveryAddress)
        {
            this.providerName = providerName;


            this.passwordQuestion = passwordQuestion;
            this.comment = comment;
            this.isApproved = isApproved;
            this.isDisabled = isDisabled;
            this.dateRegistered = dateRegistered;
            this.dateLastLogon = dateLastLogon;
            this.minValue1 = minValue1;
            this.minValue2 = minValue2;
            this.minValue3 = minValue3;
            this.contactAddress = contactAddress;
            this.deliveryAddress = deliveryAddress;

            ID = iD.ToString();
            Contact = contact;
            PhoneNumber = phoneNumber;
            Email = email;
            UserName = userName;
        }
        #endregion

        #region Public Properties
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactAddress_Country { get; set; }

        public string ContactAddress_State { get; set; }

        public string ContactAddress_City { get; set; }

        public string ContactAddress_Street { get; set; }

        public string ContactAddress_Zip { get; set; }

        public string DeliveryAddress_Country { get; set; }

        public string DeliveryAddress_State { get; set; }

        public string DeliveryAddress_City { get; set; }

        public string DeliveryAddress_Street { get; set; }

        public string DeliveryAddress_Zip { get; set; }
        #endregion

    }

    /// 
    /// 角色提供程序
    /// 
    public class LCLRoleProvider : RoleProvider
    {
        private string rolesTable = "Roles";
        private string usersInRolesTable = "UsersInRoles";

        private string eventSource = "SqlRoleProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "一个异常被抛出，请查看事件日志。";

        //
        // 如果false，有异常的话就抛出。如果true，有异常就写入日志。 
        //
        private bool pWriteExceptionsToEventLog = false;
        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        //
        // System.Web.Security.RoleProvider properties.
        // 使用配置文件 (Web.config) 中指定的角色信息的应用程序的名称。
        private string pApplicationName;
        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        /// 
        /// 构造函数
        /// 
        public LCLRoleProvider()
        {

        }

        /// 
        /// 初始化角色提供程序
        /// 接受提供程序的名称和配置设置的 NameValueCollection 作为输入。用于设置提供程序实例的属性值，其中包括特定于实现的值和配置文件（Machine.config 或 Web.config）中指定的选项。
        /// 
        /// 提供程序的名称
        /// 配置设置
        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "MyRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "LuoTong.MyRoleProvider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            else
                pApplicationName = config["applicationName"];

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                    pWriteExceptionsToEventLog = true;
            }
        }

        /// 
        /// 用户添加到角色
        /// 
        /// 一个用户名
        /// 一个角色名
        public void AddUserToRole(string username, string rolename)
        {
            if (!RoleExists(rolename))
                throw new ProviderException("没有此角色。");

            if (rolename.IndexOf(',') > 0)
                throw new ArgumentException("角色名中不能包含逗号。");

            if (username.IndexOf(',') > 0)
                throw new ArgumentException("用户名中不能包含逗号。");

            if (IsUserInRole(username, rolename))
                throw new ProviderException("用户已经在此角色中。");

            //RF.Service<IUserService>().AssignRole
        }

        /// 
        /// 角色提供程序.多个用户添加到多个角色
        /// 接受用户名列表和角色名列表作为输入，然后将指定的用户与在已配置的 ApplicationName 的数据源中指定的角色关联。
        /// 
        /// 用户名列表
        /// 角色名列表
        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                    throw new ProviderException("没有此角色。");

                if (rolename.IndexOf(',') > 0)
                    throw new ArgumentException("角色名中不能包含逗号。");
            }

            foreach (string username in usernames)
            {
                if (username.IndexOf(',') > 0)
                    throw new ArgumentException("用户名中不能包含逗号。");

                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                        throw new ProviderException("用户已经在此角色中。");
                }
            }
        }

        /// 
        /// 角色提供程序.创建角色
        /// 接受角色名作为输入，并将指定的角色添加到已配置的 ApplicationName 的数据源中。
        /// 
        /// 角色名
        public override void CreateRole(string rolename)
        {
            if (rolename.IndexOf(',') > 0)
                throw new ArgumentException("角色名中不能包含逗号。");

            if (RoleExists(rolename))
                throw new ProviderException("角色名已经存在。");

            var roles = new List<Role>
                {
                    new Role
                    {
                         Name=rolename,
                         Description=rolename
                    }
                };
            RF.Service<IUserService>().CreateRoles(roles);

        }

        /// 
        /// 角色提供程序.删除角色
        /// 接受角色名以及一个指示如果仍有用户与该角色关联时是否引发异常的布尔值作为输入。
        /// 
        /// 角色名
        /// 如果有用户与此角色关联是否引发异常
        /// 是否删除成功
        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
                throw new ProviderException("角色不存在。");

            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
                throw new ProviderException("不能删除含有用户的角色。");

          

            return true;
        }

        /// 
        /// 角色提供程序.返回所有角色名
        /// 从数据源返回角色名的列表。
        /// 
        /// 角色名列表
        public override string[] GetAllRoles()
        {
            return new string[0];
        }

        /// 
        /// 角色提供程序.根据用户名返回角色
        /// 接受用户名作为输入，并从数据源返回与指定的用户关联的角色名。
        /// 
        /// 
        /// 
        public override string[] GetRolesForUser(string username)
        {

            return new string[0];
        }

        /// 
        /// 角色提供程序.返回角色中所有用户。
        /// 接受角色名作为输入，并从数据源返回与角色关联的用户名。
        /// 
        /// 角色名
        /// 用户名列表
        public override string[] GetUsersInRole(string rolename)
        {
         
            return new string[0];
        }



        /// 
        /// 角色提供程序.叛断此用户是否属于此角色
        /// 接受用户名和角色名作为输入，并确定当前登录用户是否与已配置的 ApplicationName 的数据源中的角色关联。
        /// 
        /// 用户名
        /// 角色名
        /// 是否有关联
        public override bool IsUserInRole(string username, string rolename)
        {
            bool userIsInRole = false;

         

            return userIsInRole;
        }

        /// 
        /// 删除用户和角色的关联
        /// 
        /// 用户名
        /// 角色名
        public void RemoveUserFromRole(string username, string rolename)
        {
            if (!RoleExists(rolename))
                throw new ProviderException("角色名不存在。");

            if (!IsUserInRole(username, rolename))
                throw new ProviderException("用户不在角色中。");

         
        }

        /// 
        /// 角色提供程序.删除用户和角色的关联
        /// 接受用户名列表和角色名列表作为输入，然后删除指定用户与在已配置的 ApplicationName 的数据源中的指定角色的关联。
        /// 
        /// 用户名列表
        /// 角色名列表
        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                    throw new ProviderException("角色名不存在。");
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in rolenames)
                {
                    if (!IsUserInRole(username, rolename))
                        throw new ProviderException("用户不在角色中。");
                }
            }

          
        }



        /// 
        /// 角色提供程序.判断是否存在此角色
        /// 接受角色名作为输入，并确定在已配置的 ApplicationName 的数据源中是否存在该角色名。
        /// 
        /// 角色名
        /// 是否存在
        public override bool RoleExists(string rolename)
        {
            bool exists = false;

            return exists;
        }


        /// 
        /// 角色提供程序.在角色中查找用户
        /// 接受角色名和要搜索的用户名作为输入，并返回角色中的用户列表
        /// 
        /// 角色名
        /// 要搜索的用户名
        /// 用户列表
        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            return new string[0];
        }

        /// 
        /// 写入事件日志
        /// 
        /// 异常
        /// 操作
        private void WriteToEventLog(SqlException e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = exceptionMessage + "\n\n";
            message += "操作：" + action + "\n\n";
            message += "异常：" + e.ToString();

            log.WriteEntry(message);
        }
    }
}