
using LCL.MetaModel.Attributes;
namespace UIShell.RbacPermissionService
{
    public enum EnumManyPeople
    {
        //（And：所有人均同意/Or：任何一个人同意即可/Vote：需要指定通过人数或者通过率）
        And,
        Or,
        Vote,
    }
    public enum WfRoutSetApprovalPerson
    {
        由提交人指定,
        自动按照角色层级关系分配,
        选择审批人,
    }
}
