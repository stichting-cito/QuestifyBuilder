@echo off
SETLOCAL 
cls

rem VARS
set _ERROR=0
set _ERR_REASON="Success"
set _Possibilities=("C:\Program Files (x86)\Solutions Design\LLBLGen Pro v2.6\" "C:\Program Files\Solutions Design\LLBLGen Pro v2.6\")
set _LLBLGENLOC="NOTSET"

rem Determine LLBLGEN LOCATION
FOR %%i in  %_Possibilities% do (
	if EXIST  %%iLLBLGenPro.exe (set _LLBLGENLOC=%%i)
)
rem ERROR DETECTION
if %_LLBLGENLOC%=="NOTSET" (
set _ERROR="1"
set _ERR_REASON="NO LLBLGEN FOUND"
goto End
)

rem Reformat Path
rem Path starts and ends with " "  eg: "c:\program files\"  
rem remove last "   so  we get "c:\program files\

set _LLBLGENLOC=%_LLBLGENLOC:~0,-1%
echo %_LLBLGENLOC%

rem COPY REQUIRED FILES
Echo Start copying files
rem [1]

rem copy Templates
FOR /R ./Templates/ %%i in ("*") DO (
	xcopy "%%i" %_LLBLGENLOC%Templates"  /Y /Q
)

rem [2]
rem copy Tasks
FOR /R ./Tasks/ %%i in ("*") DO (
	xcopy "%%i" %_LLBLGENLOC%Tasks" /Y /Q
)

rem [3]
rem copy Preset (YES same Task target dir!)
FOR /R ./Preset/ %%i in ("*") DO (
	xcopy "%%i" %_LLBLGENLOC%Tasks" /Y /Q
)

rem [4]
rem copy TypeConverters
FOR /R ./TypeConverters/ %%i in ("*") DO (
	xcopy "%%i" %_LLBLGENLOC%TypeConverters" /Y /Q
)

rem [5]
rem copy TemplateBindings (That which glues Task to Template)
FOR /R ./TemplateBindings/ %%i in ("*") DO (
	xcopy "%%i" %_LLBLGENLOC%Templates" /Y /Q
)
rem DONE copying

rem Checkout Files
rem Get VS tools for TF.exe
if EXIST %VS110COMNTOOLS%vsvars32.bat (
	echo Getting Vs2012 Prompt.
	call "%VS110COMNTOOLS%vsvars32.bat"
	goto DoCheckOut
) ELSE (
	rem HMM ok,.. well maybe vs2010??
	if EXIST %VS100COMNTOOLS%vsvars32.bat(
		echo Getting Vs2010 Prompt.
		call "%VS100COMNTOOLS%vsvars32.bat"
		goto DoCheckOut
	) ELSE (
		rem ERROR 		
		set _ERROR="1"
		set _ERR_REASON="NO Visual Studio (2010 or 2012) FOUND!!??"
		goto End
	)
)
:DoCheckOut
rem Actual Chekcout
echo Checkout
tf checkout ../Cito.TestBuilder.ContentModel /recursive
tf checkout ../Cito.TestBuilder.ContentModel.DatabaseSpecific /recursive
tf checkout ../Questify.Builder.AppCommon/DTO /recursive
tf checkout ../Cito.TestBuilder.lgp

echo ----------------------------------------------------------------------------

rem done Checking out files

:StartLLBLGEN

echo.
echo Starting LLBLGEN
call %_LLBLGENLOC%LLBLGenPro.exe" ../Cito.TestBuilder.lgp

:End
if %_ERROR%=="1" (
rem ERROR
echo %_ERR_REASON%
) ELSE (
	rem NO ERROR	
)

ENDLOCAL



