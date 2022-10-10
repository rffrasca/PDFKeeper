@echo off
rem ***************************************************************************
rem * PDFKeeper -- Open Source PDF Document Management
rem * Copyright (C) 2009-2022 Robert F. Frasca
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
set Title=Single-User Database Index Sync Fix
set Prompt=Apply fix to database? (y or n):
set Status1=PDFKeeper.sqlite does not exist!
rem
rem End of localized strings
rem

title PDFKeeper %Title%

set database=%APPDATA%\Robert F. Frasca\PDFKeeper\PDFKeeper.sqlite
for /f "usebackq tokens=3" %%f in (`reg query "HKCU\Software\Robert F. Frasca\PDFKeeper" /v LocalDatabasePath`) do (
	set database=%%f\PDFKeeper.sqlite
)
cls
if not exist "%database%" (
	echo %Status1%
	goto end
)

choice /c yn /n /m "%Prompt%"
if %ERRORLEVEL%==2 exit

sqlite3 "%database%" < SqliteDatabaseIndexSyncFix.sql

:end
pause
exit
