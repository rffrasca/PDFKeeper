/********************************************************************
**
** PDFKeeper -- PDF Document Storage for Small or Home Office
** Copyright (C) 2009 Robert F. Frasca
**
** This program is free software: you can redistribute it and/or
** modify it under the terms of the GNU General Public License as
** published by the Free Software Foundation, either version 3 of
** the License, or (at your option) any later version.
**
** This program is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
** GNU General Public License for more details.
**
** You should have received a copy of the GNU General Public
** License along with this program.  If not, see 
** <http://www.gnu.org/licenses/>.
**
********************************************************************/

signal on any name Error

property=.Property~New

/* Get the Oracle XE ODBC driver to use */
if property~ODBCDriver = -1 then
	exit

/* Create application mutex semaphore */
sem = SysCreateMutexSem(.local~appl.title 'Search')
if SysRequestMutexSem(sem,1000) \= 0 then do
	call InfoDialog 'An instance of' .local~appl.title 'Search',
			'is already running!'
	exit
end

property~Get	/* Get application properties */

/* Show GUI */
dlg = .DBLogon~New
dlg~Execute('SHOWTOP',15)
res = result
dlg~DeInstall
if res = 1 then do
	dlg = .Search~New
	dlg~Execute('SHOWTOP',15)
	dlg~DeInstall
end

property~Set	/* Set application properties */

/* Release application mutex semaphore */
if SysReleaseMutexSem(sem) \= 0 then
	call ErrorDialog SysGetErrorText(rc)

exit

Error:	/* Error handler */
	signal off any
	errObj=condition("o")
	errObj~"_SIGL_"= SIGL
  	errQ=.queue~new
	call ErrorDialog 'Error' errObj~rc errObj~errortext '0a'x ,
                    	 'occurred on line' errObj~_sigl_ 'of' ,
                     	  errObj~program '0a'x ,
                     	  errObj~Message '0a'x
	exit

::requires oodplain.cls
::requires property.cls
::requires dblogon.cls
::requires pdfsearch.cls
