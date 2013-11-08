@echo off

rem ***************************************************************************
rem *
rem * PDFKeeper -- PDF Document Capture, Storage, and Search
rem * Copyright (C) 2009-2013 Robert F. Frasca
rem *
rem * This file is part of PDFKeeper.
rem *
rem * PDFKeeper is free software: you can redistribute it and/or modify it
rem * under the terms of the GNU General Public License as published by the
rem * Free Software Foundation, either version 3 of the License, or
rem * (at your option) any later version.
rem *
rem * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
rem * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
rem * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
rem * for more details.
rem *
rem * You should have received a copy of the GNU General Public License along
rem * with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
rem *
rem ***************************************************************************

set title=PDFKeeper Database Setup
set instructions1=Enter the database connect string in the format:
set instructions2=username@host:port
set instructions3=The username must be a member of SYSDBA. Note, SYSTEM is a
set instructions4=member of SYSDBA.
set instructions5=Specify localhost for the host if logged onto the same
set instructions6=system as Oracle Database XE Server.
set instructions7=If no port number is specified, the default (1521) is used.
set promptString=Enter connect string:
set errorMessage=Error: Database connect string not specified.

title %title%

echo.
echo %instructions1%
echo.
echo %instructions2%
echo.
echo - %instructions3%
echo   %instructions4%
echo - %instructions5%
echo   %instructions6% 
echo - %instructions7%
echo.
set /p connectString=%promptString% 
if "%connectString%"=="" (
	echo.
	echo %errorMessage%
	echo.
	goto End
)

sqlplus %connectString% @DatabaseSetup.sql

:End
set connectString=
del sqlnet.log 2>nul
endlocal
pause
