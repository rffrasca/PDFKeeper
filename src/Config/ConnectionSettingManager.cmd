@echo off

rem ***************************************************************************
rem * PDFKeeper -- Open Source PDF Document Management
rem * Copyright (C) 2009-2024 Robert F. Frasca
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
set MenuChoice1=1. Add Oracle Wallet Path
set MenuChoice2=2. Delete Oracle Wallet Path
set MenuChoice3=3. Add MySQL Port
set MenuChoice4=4. Delete MySQL Port
set MenuPrompt=Select the option or Q to quit:
set Choice1Message=Enter or paste the Oracle Wallet absolute path name:
set Choice1ErrorMessage1=Error: Oracle Wallet path name not specified.
set Choice1ErrorMessage2=Error: Oracle Wallet path name is invalid.
set Choice3Message=Enter or paste the MySQL Port number:
set Choice3ErrorMessage=Error: MySQL Port number not specified.
rem
rem End of Localized strings
rem

title %Title%
echo.
echo %MenuChoice1%
echo %MenuChoice2%
echo.
echo %MenuChoice3%
echo %MenuChoice4%
echo.
choice /c 1234q /n /m "%MenuPrompt%"
if %ERRORLEVEL%==1 goto AddOracleWalletPath
if %ERRORLEVEL%==2 goto DeleteOracleWalletPath
if %ERRORLEVEL%==3 goto AddMySqlPort
if %ERRORLEVEL%==4 goto DeleteMySqlPort
if %ERRORLEVEL%==5 exit

:AddOracleWalletPath
set /p data=%Choice1Message% 
if "%data%"=="" (
	echo %Choice1ErrorMessage1%
	goto End
)
if not exist "%data%" (
	echo %Choice1ErrorMessage2%
	goto End
)
set value=OracleWalletPath
set data="%data%"
goto RegAdd

:DeleteOracleWalletPath
set value=OracleWalletPath
goto RegDelete

:AddMySqlPort
set /p data=%Choice3Message% 
if "%data%"=="" (
	echo %Choice3ErrorMessage%
	goto End
)
set value=MySqlPort
set data="%data%"
goto RegAdd

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
