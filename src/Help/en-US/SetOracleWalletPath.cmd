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
set Title=PDFKeeper Set Oracle Wallet Path
set Message=Enter or paste the Oracle Wallet absolute path name:
set ErrorMessage=Error: Oracle Wallet path name not specified.
rem
rem End of localized strings
rem

title %Title%
set /p walletPath=%Message% 
if "%walletPath%"=="" (
	echo %ErrorMessage%
	goto end
)
reg add "HKCU\SOFTWARE\Robert F. Frasca\PDFKeeper" /v OracleWalletPath /d "%walletPath%"

:end
pause
exit
