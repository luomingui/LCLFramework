using LCL.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LCL.WebMvc.Models
{
    #region View Models
    public partial class UserAccountModel
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请重新输入密码以便确认")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "确认密码与输入的密码不符")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "电子邮件")]
        [Required(ErrorMessage = "请输入电子邮件")]
        [DataType(DataType.EmailAddress, ErrorMessage = "电子邮件格式不正确")]
        public string Email { get; set; }

        [Display(Name = "已禁用")]
        public bool? IsDisabled { get; set; }

        [Display(Name = "注册时间")]
        [DataType(DataType.Date)]
        public DateTime? DateRegistered { get; set; }

        [Display(Name = "注册时间")]
        public string DateRegisteredStr
        {
            get { return DateRegistered.HasValue ? DateRegistered.Value.ToShortDateString() : "N/A"; }
        }

        [Display(Name = "角色")]
        public Role Role { get; set; }

        [Display(Name = "角色")]
        public string RoleStr
        {
            get
            {
                if (Role != null && !string.IsNullOrEmpty(Role.Name))
                    return Role.Name;
                return "(未指定)";
            }
        }

        [Display(Name = "最后登录")]
        [DataType(DataType.Date)]
        public DateTime? DateLastLogon { get; set; }

        [Display(Name = "最后登录")]
        public string DateLastLogonStr
        {
            get { return DateLastLogon.HasValue ? DateLastLogon.Value.ToShortDateString() : "N/A"; }
        }

        [Display(Name = "联系人")]
        [Required(ErrorMessage = "请输入联系人")]
        public string Contact { get; set; }

        [Display(Name = "电话号码")]
        [Required(ErrorMessage = "请输入电话号码")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)",
            ErrorMessage = "电话号码格式不正确")]
        public string PhoneNumber { get; set; }

        [Display(Name = "联系地址 - 国家")]
        [Required(ErrorMessage = "请输入国家")]
        public string ContactAddress_Country { get; set; }

        [Display(Name = "联系地址 - 省/州")]
        [Required(ErrorMessage = "请输入省/州")]
        public string ContactAddress_State { get; set; }

        [Display(Name = "联系地址 - 市")]
        [Required(ErrorMessage = "请输入市")]
        public string ContactAddress_City { get; set; }

        [Display(Name = "联系地址 - 街道")]
        [Required(ErrorMessage = "请输入街道")]
        public string ContactAddress_Street { get; set; }

        [Display(Name = "联系地址 - 邮编")]
        [Required(ErrorMessage = "请输入邮编")]
        public string ContactAddress_Zip { get; set; }

        [Display(Name = "收货地址 - 国家")]
        [Required(ErrorMessage = "请输入国家")]
        public string DeliveryAddress_Country { get; set; }

        [Display(Name = "收货地址 - 省/州")]
        [Required(ErrorMessage = "请输入省/州")]
        public string DeliveryAddress_State { get; set; }

        [Display(Name = "收货地址 - 市")]
        [Required(ErrorMessage = "请输入市")]
        public string DeliveryAddress_City { get; set; }

        [Display(Name = "收货地址 - 街道")]
        [Required(ErrorMessage = "请输入街道")]
        public string DeliveryAddress_Street { get; set; }

        [Display(Name = "收货地址 - 邮编")]
        [Required(ErrorMessage = "请输入邮编")]
        public string DeliveryAddress_Zip { get; set; }

        public override string ToString()
        {
            return UserName;
        }

        public static UserAccountModel CreateFromDataObject(User d)
        {
            return new UserAccountModel
            {
                ID = d.ID,
                UserName = d.UserName,
                Password = d.Password,
                Email = d.Email,
                IsDisabled = d.IsDisabled,
                DateRegistered = d.DateRegistered,
                DateLastLogon = d.DateLastLogon,
                //Role = d.Role,
                Contact = d.Contact,
                PhoneNumber = d.PhoneNumber,
                ContactAddress_City = d.ContactAddress.City,
                ContactAddress_Street = d.ContactAddress.Street,
                ContactAddress_State = d.ContactAddress.State,
                ContactAddress_Country = d.ContactAddress.Country,
                ContactAddress_Zip = d.ContactAddress.Zip,
                DeliveryAddress_City = d.DeliveryAddress.City,
                DeliveryAddress_Street = d.DeliveryAddress.Street,
                DeliveryAddress_State = d.DeliveryAddress.State,
                DeliveryAddress_Country = d.DeliveryAddress.Country,
                DeliveryAddress_Zip = d.DeliveryAddress.Zip,
            };
        }

        public User ConvertToDataObject()
        {
            return new User
            {
                ID = this.ID,
                UserName = this.UserName,
                Password = this.Password,
                IsDisabled = this.IsDisabled.Value,
                Email = this.Email,
                DateRegistered = this.DateRegistered.Value,
                DateLastLogon = this.DateLastLogon,
                Contact = this.Contact,
                PhoneNumber = this.PhoneNumber,
                ContactAddress = new Address
                {
                    Country = ContactAddress_Country,
                    State = ContactAddress_State,
                    City = ContactAddress_City,
                    Street = ContactAddress_Street,
                    Zip = ContactAddress_Zip
                },
                DeliveryAddress = new Address
                {
                    Country = DeliveryAddress_Country,
                    State = DeliveryAddress_State,
                    City = DeliveryAddress_City,
                    Street = DeliveryAddress_Street,
                    Zip = DeliveryAddress_Zip
                }
            };
        }
    }
    #endregion
}