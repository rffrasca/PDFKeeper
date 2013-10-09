/********************************************************************
**
** PDFKeeper -- PDF Document Storage for Small or Home Office
** Copyright (C) 2009-2010 Robert F. Frasca
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

::requires oodplain.cls

::class ODBCSQL public

::method Init
  curdir = directory()
  call directory '..\Binaries'
  call RXFuncAdd 'SQLLoadFuncs','rexxsql','SQLLoadFuncs'
  call SqlLoadFuncs
  call RXFuncAdd 'ODBCLoadFuncs','rexxodbc','ODBCLoadFuncs'
  call ODBCLoadFuncs
  call directory curdir

::method Connect
  use arg db.username,db.password
  odbc_string = 'Driver={'.local~odbc.driver'};'||,
		'dbq='.local~datasource';Uid='db.username';'||,
		'Pwd='db.password
  if sqlconnect('c1',,,,odbc_string) < 0 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	self~Disconnect
   	return -1
  end
  return 0

::method Command	/* Execute a single SQL statement */
  use arg sqlstatement
  self~SetVariables
  if sqlcommand('c1',sqlstatement) < 0 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	return -1
  end
  if translate(left(sqlstatement,6)) = 'SELECT' then
	interpret return c1.
  else
	return 0

::method QueryBLOB
  use arg blob.column,blob.file,query.statement
  self~SetVariables
  if sqlprepare('c1',query.statement) < 0 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	return -1
  end
  if sqlopen(c1) < 0 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	return -1
  end
  if sqlfetch(c1,1) < -1 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	return -1
  end
  if sqlgetdata(c1,blob.column,,,blob.file) < 0 then do
	call ErrorDialog sqlca.interrm' - 'sqlca.sqlerrm
	return -1
  end
  call sqldispose c1
  return 0

::method Disconnect
  call sqldisconnect
  call sqldropfuncs

::method SetVariables private
  call sqlvariable 'AUTOCOMMIT','ON'
  call sqlvariable 'LONGLIMIT',64000000
  call sqlvariable 'IGNORETRUNCATE','ON'
