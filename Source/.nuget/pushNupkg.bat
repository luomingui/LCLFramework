nuget pack ..\src\LCL.Web.Framework\LCL.Web.Framework.csproj

nuget pack ..\src\LCL\LCL.csproj
nuget pack ..\src\LCL.Repositories.EntityFramework\LCL.Repositories.EntityFramework.csproj
nuget pack ..\src\LCL.Repositories.MongoDB\LCL.Repositories.MongoDB.csproj
nuget pack ..\src\LCL.Repositories.NHibernate\LCL.Repositories.NHibernate.csproj

nuget push *.nupkg  -s http://localhost:96 luomingui

pause