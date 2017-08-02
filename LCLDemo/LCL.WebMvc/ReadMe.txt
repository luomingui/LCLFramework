ASP.NET Domain-Driven Design
AND

The type 'Repositories.EntityFrameworkRepository`1' does not implement the interface 'Repositories.IRepository`1'.

None of the constructors found with '
Autofac.Core.Activators.Reflection.DefaultConstructorFinder' 
on type 'EntityFrameworkRepositoryContext' 
can be invoked with the available services and parameters:
Cannot resolve parameter 'DbContext efContext' of constructor
'Void .ctor(LCL.Repositories.EntityFramework.DbContext)'.

The requested service 'IOrderService' has not been registered. 
To avoid this exception, either register a component to provide the service, check 
for service registration using IsRegistered(), or use the ResolveOptional() 
method to resolve an optional dependency.

No constructors on type 'LCL.Domain.Repositories.RepositoryContext' 
can be found with the constructor finder 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder'.

SMDiagnostics, 
Anonymously Hosted DynamicMethods Assembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' 
on type 'LCL.Repositories.EntityFramework.EntityFrameworkRepositoryContext' 
can be invoked with the available services and parameters:
Cannot resolve parameter 'System.Data.Entity.DbContext efContext' of 
constructor 'Void .ctor(System.Data.Entity.DbContext)'.
 
No constructors on type 'LCL.Domain.Repositories.RepositoryContext' 
can be found with the constructor finder 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder'.

No constructors on type 'EntityFrameworkRepositoryContext' can be 
found with the constructor finder 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder'. 


The Entity Framework provider type 'System.Data.Entity.SqlServerCompact.SqlCeProviderServices, 
EntityFramework.SqlServerCompact' registered in the application config file for the 
ADO.NET provider with invariant name 'System.Data.SqlServerCe.4.0' could not be loaded. Make 
sure that the assembly-qualified name is used and that the assembly is available to the running 
application. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

:“Native components of SQL Server Compact engine are not loaded. 
Please reinstall Microsoft SQL Server Compact.”


:“The ADO.NET provider with invariant name 'System.Data.SqlServerCe' 
is either not registered in the machine or application config file, 
or could not be loaded. See the inner exception for details.”

Cannot create a DbEntityEntry<ISoftDelete> from a non-generic DbEntityEntry for 
objects of type 'ShoppingCartItem'.

The type 'LCL.Domain.Repositories.EntityFramework.CategorizationRepository' is 
not assignable to service 'LCL.Domain.Repositories.IRepositoryContext'.

The requested service 'LCL.Infrastructure.ITypeFinder' has not been registered. 
To avoid this exception, either register a component to provide the service, check 
for service registration using IsRegistered(), or use the ResolveOptional() method 
to resolve an optional dependency.
 
An error occurred while executing the command definition. See the inner exception for details.
已有打开的与此 Command 相关联的 DataReader，必须首先将它关闭。

