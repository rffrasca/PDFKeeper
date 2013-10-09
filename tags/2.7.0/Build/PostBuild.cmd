@echo off

rem ***************************************************************************
rem *
rem * PDFKeeper -- PDF Document Storage for Small or Home Office
rem * Copyright (C) 2009-2012 Robert F. Frasca
rem *
rem * This program is free software: you can redistribute it and/or modify it
rem * under the terms of the GNU General Public License as published by the
rem * Free Software Foundation, either version 3 of the License, or
rem * (at your option) any later version.
rem *
rem * This program is distributed in the hope that it will be useful, but
rem * WITHOUT ANY WARRANTY; without even the implied warranty of
rem * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
rem * General Public License for more details.
rem *
rem * You should have received a copy of the GNU General Public License along
rem * with this program.  If not, see <http://www.gnu.org/licenses/>.
rem *
rem ***************************************************************************

copy "..\..\..\3rdParty\SumatraPDF\SumatraPDF.exe"
copy "..\..\..\Configuration\sumatrapdfrestrict.ini"

md ..\Help
md ..\Help\css
copy ..\..\..\Documentation\UserGuide.html ..\Help\*.*
copy ..\..\..\Documentation\css\*.css ..\Help\css\*.*

exit
