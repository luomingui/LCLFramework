::自动备份当前文件夹
::by luomg, 21:15 2010-10-13
::minguiluo@163.com
@echo off

set "Ymd=%date:~,4%%date:~5,2%%date:~8,2%"
md F:\Github2018\backup\LCL.NET_%ymd%
@echo 自动备份当前文件夹

xcopy /e /y "F:\Github2018\LCL" F:\Github2018\backup\LCL.NET_%ymd%

@echo 数据备份完成，3秒后程序退出。  
ping /n 3 127.0.0.1 >nul  
::exit
@pause


