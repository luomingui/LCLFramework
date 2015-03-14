using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIShell.RbacManagementPlugin.ViewModels
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        //验证
        public string VerifyCode { get; set; }
        
        public bool RememberMe { get; set; }
    }
}