@echo off

REM This batch attempts to create and fill a testbuilder unit test database.
REM Creator: Martijn Verschuur
REM Creationdate: 14-11-2007

if "%1" == "" goto usage
if "%2" == "" goto usage
if "%3" == "" goto usage
if "%4" == "" goto usage

set SCRIPTFILE="C:\TFS.VMCITO32\Project ICE\Main\Source\Unittests\Cito.TestBuilder.UnitTests\UnitTestConfigFiles\CreateTestDatabase.sql"
set OUTPUTFILE="CreateTestDatabaseOutput.txt"
if not exist %SCRIPTFILE%  goto missing
Sqlcmd -U %2 -P %3 -S %1 -b -v testDBName=%4 -i %SCRIPTFILE% -o %OUTPUTFILE%
if ERRORLEVEL == 1 goto error

set SCRIPTFILE="C:\TFS.VMCITO32\Project ICE\Main\Source\TestBuilder\Cito.TestBuilder.DataBase\Create Scripts\DataBaseSchema.sql"
set OUTPUTFILE="DatabaseSchemaOutput.txt"
Sqlcmd -U %2 -P %3 -S %1 -b -d %4 -i %SCRIPTFILE% -o %OUTPUTFILE%
if ERRORLEVEL == 1 goto error

set SCRIPTFILE="C:\TFS.VMCITO32\Project ICE\Main\Source\Unittests\Cito.TestBuilder.UnitTests\UnitTestConfigFiles\TestbuilderTestData.sql"
set OUTPUTFILE="TestbuilderTestDataOutput.txt"
Sqlcmd -U %2 -P %3 -S %1 -b -d %4 -i %SCRIPTFILE% -o %OUTPUTFILE%
if ERRORLEVEL == 1 goto error

echo Creation of the tesbuilder test database has been successfull!

goto :eof

:missing
echo:
echo:
echo Missing following file: %SCRIPTFILE%
echo Fix this and rerun this script.
goto :eof

:error
echo:
echo:
echo An error occured. See %OUTPUTFILE% for more information!
goto :eof

:usage
echo:
echo:
echo Error in script usage. The correct usage is:
echo:
echo     %0 server[\instance] username password databasename
echo:
echo When no instance is specified the default instance on the server will be used.
echo:
echo Example:
echo     %0 myserver\development sa welcome TestBuilderTest
goto :eof