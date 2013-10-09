@echo off

rem *****************************************************************
rem *
rem * PDFKeeper -- PDF Document Storage for Small or Home Office
rem * Copyright (C) 2009 Robert F. Frasca
rem *
rem * This program is free software: you can redistribute it and/or
rem * modify it under the terms of the GNU General Public License as
rem * published by the Free Software Foundation, either version 3 of
rem * the License, or (at your option) any later version.
rem *
rem * This program is distributed in the hope that it will be
rem * useful, but WITHOUT ANY WARRANTY; without even the implied
rem * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
rem * PURPOSE.  See the GNU General Public License for more details.
rem *
rem * You should have received a copy of the GNU General Public
rem * License along with this program.  If not, see
rem * <http://www.gnu.org/licenses/>.
rem *
rem *****************************************************************

title PDFKeeper Document Loader

set homedir="%CD%"
cd ..
set bindir=%CD%\Binaries
cd /d %PDFKEEPER_UPLOAD%

for %%f in (*.pdf) do (
	echo Loading: %%f
	"%bindir%\pdfinfo" "%%f" | find "Title" > title.txt
	"%bindir%\pdfinfo" "%%f" | find "Author" > author.txt
	"%bindir%\pdfinfo" "%%f" | find "Subject" > subject.txt
	"%bindir%\pdfinfo" "%%f" | find "Keywords" > keywords.txt
	echo %%f > filename.txt

	cscript %homedir%\dbinput.vbs
	if not exist pdfdb.txt (
		echo Failed to create file: pdfdb.txt
		exit
	)

	sqlldr '/ as sysdba' control='%homedir%\sqlloader.ctl'
	if %ERRORLEVEL%==0 (
		del "%%f"
	)
	del pdfdb.txt
	type sqlloader.log >> pdfloader.log
	del sqlloader.log
)
