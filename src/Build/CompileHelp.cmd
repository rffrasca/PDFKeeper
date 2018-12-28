@echo off
rem ***************************************************************************
rem * PDFKeeper -- Open Source PDF Document Storage Solution
rem * Copyright (C) 2009-2019  Robert F. Frasca
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
for /f "tokens=3*" %%f in ('reg query HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Components\28E7A37130464D115AF3000972A8B18B /v 464395C9F2F73B73988FE798E4B390AE') do (
    "%%f %%g" "..\..\..\Help\en-US\PDFKeeper.hhp"
)
exit 0
