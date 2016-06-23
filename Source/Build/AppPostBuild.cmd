@echo off

rem ***************************************************************************
rem *
rem * PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
rem * Copyright (C) 2009-2016 Robert F. Frasca
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

copy "..\..\..\Externals\SumatraPDF.exe"
copy "..\..\..\Externals\gswin32c.exe"
copy "..\..\..\Externals\gsdll32.dll"
copy "..\..\..\Resources\sumatrapdfrestrict.ini"
copy "..\..\..\Help\PDFKeeper*.chm"

exit
