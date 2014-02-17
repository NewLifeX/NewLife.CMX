:: 本脚本用于从源码库更新引用文件DLL

@echo off
cls
setlocal enabledelayedexpansion
title 更新引用文件DLL

:: 导出来源地址
:: 为了提高速度，可以采用本地地址
set svn=https://svn.newlifex.com/svn/X/trunk
set name=DLL
if exist C:\X (
	:: 先更新一次源
	svn info %svn%/%name%
	svn update C:\X\%name%

	set svn=C:\X
)
set url=%svn%/trunk

:: 引用文件DLL
set url=%svn%/%name%
for /r %%f in (*.*) do svn export --force %url%/%%~nxf %%~nxf

pause