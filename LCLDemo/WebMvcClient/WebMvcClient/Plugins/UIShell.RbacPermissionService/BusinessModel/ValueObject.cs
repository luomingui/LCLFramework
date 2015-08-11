
using LCL.MetaModel.Attributes;
namespace UIShell.RbacPermissionService
{
    public enum UnitType
    {
        公司,
        学校
    }
    /// <summary>
    /// 企业种类
    /// http://baike.baidu.com/link?url=42rIxnQ7tqtdWjh2HZFKXls-uMuj0KlmwNcgXjiX_fOQYOZODZgpj_FwQIaEuqtQWzlzzcUMMfziJa-HHOj0JK
    /// </summary>
    public enum CompanyType
    {
        国有企业,
        集体所有制,
        私营企业,
        股份制企业,
        联营企业,
        外商投资企业,
        港澳台,
        股份合作企业
    }
    /// <summary>
    /// 企业经济类型   
    /// http://baike.baidu.com/link?url=nVRUSfCJY7U7BpXwapj-FZbkeiKq-_QjAW9_PbNVtPdv0DJp9C5d8NQd0_9_YVSTbP7DSyxSCrafvxtOuR8Xbq
    /// </summary>
    public enum EconomicType
    {
        国有经济,
        集体经济,
        私营经济,
        个体经济,
        联营经济,
        股份制,
        外商投资,
        港澳台投资,
        其它经济,
        经济类型
    }
    public enum AuthorityType
    {
        菜单, 数据
    }

    public enum EnumLogType
    {
        未指定类型 = 0,
        执行命令 = 1,
        打开模块 = 2,
        登录 = 3,
    }
}
