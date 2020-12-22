@echo off

REM This batch attempts to drop a (testbuilder unit test) database.
REM Creator: Martijn Verschuur
REM Creationdate: 14-11-2007

if %1 == "" goto usage
if %2 == "" goto usage
if %3 == "" goto usage
if %4 == "" goto usage

set SCRIPTFILE="C:\TFS.VMCITO32\Project ICE\Main\Source\Unittests\Cito.TestBuilder.UnitTests\UnitTestConfigFiles\DropTestbuilderTest.sql"
set OUTPUTFILE="DropTestbuilderTestOutput.txt"
Sqlcmd -U %2 -P %3 -S %1 -b -v testDBName=%4 -i %SCRIPTFILE% -o %OUTPUTFILE%
if ERRORLEVEL == 1 goto error

echo The testbuilder test database has been dropped successfull!

goto :eof

:missing
echo Missing following file: %SCRIPTFILE%
echo Fix this and rerun this script.
goto :eof

:error
echo An error occured. See %OUTPUTFILE% for more information!
goto :eof

:usage
echo Error in script usage. The correct usage is:
echo     %0 server[\instance] username password databasename
echo When no instance is specified the default instance on the server will be used.
echo:
echo For example:
echo     %0 myserver\development sa welcome TestBuilderTest
goto :eof