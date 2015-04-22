using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(CompanyInfoMD))]  
    public partial class CompanyInfo  
    {  
  
        public class CompanyInfoMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "机构类型")]
            public CompanyType CompanyType { get; set; }  
            [Display(Name = "经济类型")]
            public EconomicType EconomicType { get; set; }  
            [Display(Name = "注册日期")]  
            public DateTime RegisterDate { get; set; }  
            [Display(Name = "注册资金")]
            public double RegisterMoney { get; set; }  
            [Display(Name = "注册地址")]  
            public string RegisterAddress { get; set; }  
            [Display(Name = "法人代表")]  
            public string EgalPerson { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

