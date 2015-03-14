nuget pack ..\src\LCL.Utils\LCL.Utils.csproj
nuget pack ..\src\Web\LCL.MvcExtensions\LCL.MvcExtensions.csproj

nuget pack ..\src\LCL\LCL.csproj
nuget pack ..\src\LCL.ObjectContainers.Unity\LCL.ObjectContainers.Unity.csproj
nuget pack ..\src\LCL.Repositories.EntityFramework\LCL.Repositories.EntityFramework.csproj

nuget push *.nupkg  -s http://localhost true

pause