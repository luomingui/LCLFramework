nuget pack ..\src\LCL.Web.Framework\LCL.Web.Framework.csproj

nuget pack ..\src\LCL.Core\LCL.Core.csproj
nuget pack ..\src\LCL.Repositories.MongoDB\LCL.Repositories.MongoDB.csproj
nuget pack ..\src\LCL.Repositories.EntityFramework\LCL.Repositories.EntityFramework.csproj

nuget push *.nupkg  -s http://localhost true

pause