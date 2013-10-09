@echo off

rem **********************************************************************
rem *
rem * PDFKeeper -- PDF Document Storage for Small or Home Office
rem * Copyright (C) 2009-2011 Robert F. Frasca
rem *
rem * This program is free software: you can redistribute it and/or
rem * modify it under the terms of the GNU General Public License as
rem * published by the Free Software Foundation, either version 3 of the
rem * License, or (at your option) any later version.
rem *
rem * This program is distributed in the hope that it will be useful, but
rem * WITHOUT ANY WARRANTY; without even the implied warranty of
rem * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
rem * GNU General Public License for more details.
rem *
rem * You should have received a copy of the GNU General Public License
rem * along with this program.  If not, see
rem * <http://www.gnu.org/licenses/>.
rem *
rem **********************************************************************

title PDFKeeper User Settings Cleanup
reg delete HKEY_CURRENT_USER\Software\PDFKeeper
pause
