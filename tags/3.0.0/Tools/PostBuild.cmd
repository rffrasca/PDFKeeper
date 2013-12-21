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

copy "..\..\..\..\Tools\sumatrapdfrestrict.ini"
copy "..\..\..\..\Libraries\SumatraPDF.exe"
copy "..\..\..\..\Libraries\gxps-?.??-win32.exe"
move gxps-?.??-win32.exe gxps-win32.exe
copy "..\..\..\..\Help\PDFKeeper*.chm"

exit
