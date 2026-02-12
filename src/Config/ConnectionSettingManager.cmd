@echo off

rem ***************************************************************************
rem * PDFKeeper -- Open Source PDF Document Management
rem * Copyright (C) 2009-2026 Robert F. Frasca
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
rem Start of Localized strings
rem
set Title=PDFKeeper Connection Setting Manager
set MenuChoice1=1. Add Oracle ODP.NET folder path
set MenuChoice2=2. Add Oracle Wallet folder path
set MenuChoice3=3. Add MySQL port
set MenuChoice4=4. Delete Oracle ODP.NET folder path
set MenuChoice5=5. Delete Oracle Wallet folder path
set MenuChoice6=6. Delete MySQL port
set MenuPrompt=Select the option or Q to quit:
set Choice1Message=Enter or paste the Oracle ODP.NET folder absolute path name:
set Choice1ErrorMessage1=Error: Oracle ODP.NET folder path name not specified.
set Choice1ErrorMessage2=Error: Oracle ODP.NET folder path name is invalid.
set Choice2Message=Enter or paste the Oracle Wallet folder absolute path name:
set Choice2ErrorMessage1=Error: Oracle Wallet folder path name not specified.
set Choice2ErrorMessage2=Error: Oracle Wallet folder path name is invalid.
set Choice3Message=Enter or paste the MySQL port number:
set Choice3ErrorMessage=Error: MySQL port number not specified.
rem
rem End of Localized strings
rem

title %Title%
echo.
echo %MenuChoice1%
echo %MenuChoice2%
echo %MenuChoice3%
echo.
echo %MenuChoice4%
echo %MenuChoice5%
echo %MenuChoice6%
echo.
choice /c 123456q /n /m "%MenuPrompt%"
if %ERRORLEVEL%==1 goto AddOracleOdpNetPath
if %ERRORLEVEL%==2 goto AddOracleWalletPath
if %ERRORLEVEL%==3 goto AddMySqlPort
if %ERRORLEVEL%==4 goto DeleteOracleOdpNetPath
if %ERRORLEVEL%==5 goto DeleteOracleWalletPath
if %ERRORLEVEL%==6 goto DeleteMySqlPort
if %ERRORLEVEL%==7 exit

:AddOracleOdpNetPath
set /p data=%Choice1Message% 
if "%data%"=="" (
	echo %Choice1ErrorMessage1%
	goto End
)
if not exist "%data%" (
	echo %Choice1ErrorMessage2%
	goto End
)
set value=OracleOdpNetPath
set data="%data%"
goto RegAdd

:AddOracleWalletPath
set /p data=%Choice2Message% 
if "%data%"=="" (
	echo %Choice2ErrorMessage1%
	goto End
)
if not exist "%data%" (
	echo %Choice2ErrorMessage2%
	goto End
)
set value=OracleWalletPath
set data="%data%"
goto RegAdd

:AddMySqlPort
set /p data=%Choice3Message% 
if "%data%"=="" (
	echo %Choice3ErrorMessage%
	goto End
)
set value=MySqlPort
set data="%data%"
goto RegAdd

:DeleteOracleOdpNetPath
set value=OracleOdpNetPath
goto RegDelete

:DeleteOracleWalletPath
set value=OracleWalletPath
goto RegDelete

:DeleteMySqlPort
set value=MySqlPort
goto RegDelete

:RegAdd
reg add "HKCU\SOFTWARE\Robert F. Frasca\PDFKeeper" /v %value% /d %data%
goto End

:RegDelete
reg delete "HKCU\SOFTWARE\Robert F. Frasca\PDFKeeper" /v %value%

:End
pause
exit
