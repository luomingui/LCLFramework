------RbacPermissionService设计基调------
1 使用Code First，基于Entity Framework开发
2 Model为实体层，DataAccess为数据访问层
3 该插件实现了基于RBAC模型的权限检测，并提供了数据库访问类

------WinForm/WPF/Web权限管理插件------
具体的权限管理插件依赖于RbacPermissionService

------其它业务插件------
具体业务插件仅依赖于PermissionService，通过扩展点来定义权限，并调用服务来检测权限



IOC使用注意事项：
1：多个构造函数的问题。
2：