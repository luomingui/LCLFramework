@echo off
::压缩应用路径。PS:目录不能有中文，最后不带\
@set /p dir_path=输入压缩目录:
::压缩应用路径
@set AJAXZIPPath="D:\Program Files\Microsoft\Microsoft Ajax Minifier 4\"
::重命名后缀
@set file_temp=.js

::使用新目录
rd    build_temp /s /Q
mkdir  build_temp
xcopy  %dir_path%\*.js build_temp /s /r /y
echo %dir_path%\build_temp
cd /D build_temp

rem ::直接进入目录
rem cd /D %dir_path%
::启动压缩应用
call %AJAXZIPPath%AjaxMinCommandPromptVars.bat

::压缩JS文件
for /R %%i in (*.js) do (
	echo %%i
	COPY %%i %%i%file_temp%
	del %%i
	ajaxmin -h %%i%file_temp% -o %%i
	rem COPY %%i%file_temp% %%i
	del %%i%file_temp%
)

pause
