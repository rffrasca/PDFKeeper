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

set database=%APPDATA%\Robert F. Frasca\PDFKeeper\PDFKeeper.sqlite

rem
rem Start of localized strings
rem
set Title=Single-User Database Setup
set Prompt=Create database? (y or n):
set Status1=already exists!
set Status2=was created.
set Status3=is now ready for use!
rem
rem End of localized strings
rem

title PDFKeeper %Title%

if exist "%database%" (
	echo %database% %Status1%
	goto end
)

choice /c yn /n /m "%Prompt%"
if %ERRORLEVEL%==2 exit

md "%APPDATA%\Robert F. Frasca"
md "%APPDATA%\Robert F. Frasca\PDFKeeper"

sqlite3 "%database%" < SqliteDatabaseSetup.sql
if exist "%database%" (
	echo %database% %Status2%
	echo PDFKeeper %Status3%
)

:end
pause
exit
