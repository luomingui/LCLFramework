echo off
title  正在发布网站！
ECHO "★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★"
ECHO "★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★"
ECHO "★☆                                                      ☆★"
ECHO "★☆      创造无限可能-让我们的生活更美丽                 ☆★"
ECHO "★☆                                       --制作小罗     ☆★"
ECHO "★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★"
echo.
echo.
path %SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\

msbuild.exe LCL.sln /t:Rebuild /p:Configuration=Release /p:VisualStudioVersion=12.0


pause