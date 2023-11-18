@echo off

rem ***************************************************************************
rem * PDFKeeper -- Open Source PDF Document Management
rem * Copyright (C) 2009-2023 Robert F. Frasca
rem *
rem * This file is part of PDFKeeper.
rem *
rem * PDFKeeper is free software: you can redistribute it and/or modify
rem * it under the terms of the GNU General Public License as published by
rem * the Free Software Foundation, either version 3 of the License, or
rem * (at your option) any later version.
rem *
rem * PDFKeeper is distributed in the hope that it will be useful,
rem * but WITHOUT ANY WARRANTY; without even the implied warranty of
rem * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
rem * GNU General Public License for more details.
rem *
rem * You should have received a copy of the GNU General Public License
rem * along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
rem ***************************************************************************

rem
rem Start of localized strings
rem
set Title=PDFKeeper Multi-User Database Schema Setup
set MenuHeader=Compatible Database Management Systems
set MenuChoice1=1. Oracle Database
set MenuChoice2=2. Oracle Cloud Autonomous Database
set MenuPrompt=Select the database management system or Q to quit:
set OracleMessage1=Enter the database connect string in the format:
set OracleMessage2=username/password@host:port/service_name
set OracleMessage3=username: database account that must be a member of SYSDBA.
set OracleMessage4=Note, SYSTEM is a member of SYSDBA.
set OracleMessage5=host: host name or IP address of the database server computer.
set OracleMessage6=If connecting to a database located on the same computer,
set OracleMessage7=the host name or IP address would be localhost or 127.0.0.1.
set OracleMessage8=port: listening port number on the database server.
set OracleMessage9=If not specified, the default port number 1521 is assumed.
set OracleMessage10=service_name: service name of the database instance or
set OracleMessage11=pluggable database to access.
set OracleMessage12=For Oracle Database Express Edition, if not specified, the
set OracleMessage13=default service name XE is assumed.
set OracleMessage14=For Oracle Database Express Edition, it is recommended
set OracleMessage15=to use the default pluggable database XEPDB1.
set OracleMessage16=admin/password@TNS_name
set OracleMessage17=admin is the administrator account and TNS_name is the service name
set OracleMessage18=of the Autonomous Database instance.
set CommonMessage=Enter connect string:
set CommonErrorMessage=Error: Database connect string not specified.
rem
rem End of localized strings
rem

title %Title%
echo ==========================================================================
echo  %MenuHeader%
echo ==========================================================================
echo.
echo %MenuChoice1%
echo %MenuChoice2%
echo.
choice /c 12q /n /m "%MenuPrompt%"
if %ERRORLEVEL%==1 (
	set operation=Setup
	goto OracleSetup
)
if %ERRORLEVEL%==2 (
	set operation=Setup
	goto OracleCloudSetup
)
if %ERRORLEVEL%==3 (
	exit
)

:OracleSetup
echo.
echo %OracleMessage1%
echo.
echo %OracleMessage2%
echo.
echo * %OracleMessage3%
echo   %OracleMessage4%
echo.
echo * %OracleMessage5%
echo   %OracleMessage6% 
echo   %OracleMessage7%
echo.
echo * %OracleMessage8%
echo   %OracleMessage9%
echo.
echo * %OracleMessage10%
echo   %OracleMessage11%
echo   %OracleMessage12%
echo   %OracleMessage13%
echo   %OracleMessage14%
echo   %OracleMessage15%
echo.
goto Main

:OracleCloudSetup
echo.
echo %OracleMessage1%
echo.
echo %OracleMessage16%
echo.
echo * %OracleMessage17%
echo   %OracleMessage18%
echo.

:Main
set /p connectString=%CommonMessage% 
if "%connectString%"=="" (
	echo %CommonErrorMessage%
	goto end
)
sqlplus %connectString% @OracleDatabaseSchema%operation%.sql

:end
pause
exit
