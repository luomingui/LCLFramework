using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace LCL.MvcExtensions
{
    public class Account
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(20, ErrorMessage = "名称长度不能超过20个字符")]
        public string Name { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, ErrorMessage = "密码长度不能超过20个字符")]
        public string Password { get; set; }
        //验证
        public string VerifyCode { get; set; }

        public bool RememberMe { get; set; }
    }


    /// <summary>
    /// 用户修改密码模型
    /// </summary>
    //[NotMapped]
    public class UserChangePassword
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Display(Name = "原密码")]
        [Required(ErrorMessage = "原密码不能为空")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "原密码不能为空不能小于{0}和大于{1}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "新密码不能为空")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} 必须至少包含 {2} 个字符。")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        //[Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配。")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}