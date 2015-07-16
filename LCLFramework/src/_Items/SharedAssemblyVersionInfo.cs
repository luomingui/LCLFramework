using System.Reflection;
using System.Resources;

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“内部版本号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion("6.0.0.0")]
[assembly: AssemblyFileVersion("6.0.0.0")]
[assembly: AssemblyInformationalVersion("6.0.0-alpha1")]
[assembly: SatelliteContractVersion("6.0.0.0")]

/*******************************************************
 * 
 * 创建时间：20130826
 * 说明：本文件用于记录 LCL 框架版本号及相应的变更记录。格式如下：
 * 暂时分为以下几类：
 * ★较大改动、添加接口、★修改接口、★删除接口、添加功能、重构、内部修改、BUG修改。
 * 
*******************************************************/

/*
4.5.3.0
 * ★删除接口 （LCL.DomainEntitys 换成 LCL ）
 * 添加接口 （IAggregateRoot，权限加入开关）
 * 重构（仓库加入 FindXXXX 方法并加入了缓存，取消了“数据门户”）
4.5.1.0
    BUG修改
        LCL.Repositories.EntityFramework.EntityFrameworkRepository.RemoveHoldingEntityInContext 
        EF Repository Update 实体状态问题 。
 5.0.0.0
    内部修改
        LCL.MvcExtensions.BaseRepoController.ErrorMsg
        打印ModelState.Errors信息到日志中。
*/