:: 引用文件DLL
set src=E:\X\Bin
for /r %%f in (*.*) do copy /y %src%\%%~nxf %%~nxf

pause