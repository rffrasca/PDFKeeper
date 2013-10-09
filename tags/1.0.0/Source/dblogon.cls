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

::requires oodwin32.cls
::requires odbcsql.cls

::class DBLogon public subclass userdialog inherit AdvancedControls

::method Init
  self~Init:super
  self~CreateCenter(200,110,'PDFKeeper',,,'Tahoma',.local~font.size)

::method DefineDialog
  self~AddIcon(15,'..\Graphics\logo.ico')
  self~AddGroupBox(5,10,190,75,'Database Logon',,100)
  self~AddText(10,25,45,12,'Username:',,101)
  self~AddEntryLine(102,,60,25,130,12,'AUTOSCROLLH')
  self~AddText(10,45,45,12,'Password:',,103)
  self~AddPasswordLine(104,,60,45,130,12)
  self~AddText(10,65,45,12,'Data Source:',,105)
  self~AddEntryLine(106,,60,65,130,12,'AUTOSCROLLH')
  self~AddOKCancelRightBottom

::method InitDialog
  self~GetEditControl(102)~SetLimit(30)
  self~GetEditControl(104)~SetLimit(30)
  self~GetEditControl(106)~SetLimit(260)
  self~DATA102=.local~username
  self~DATA106=.local~datasource

  if .local~odbc.driver = 'Oracle in XE' then
	self~DisableItem(106)

::method DisableControls
  self~DisableItem(102)
  self~DisableItem(104)
  self~DisableItem(106)
  self~DisableItem(1)
  self~DisableItem(2)

::method EnableControls
  self~EnableItem(102)
  self~EnableItem(104)

  if .local~odbc.driver = 'Oracle in XEClient' then
	self~EnableItem(106)

  self~EnableItem(1)
  self~EnableItem(2)

::method Ok
  self~SetValue(102,strip(self~GetValue(102)))
  self~SetValue(104,strip(self~GetValue(104)))
  self~SetValue(106,strip(self~GetValue(106)))

  .local~username = self~GetValue(102)
  .local~datasource = self~GetValue(106)
  password = self~GetValue(104)
  if .local~username = '' | password = '' |,
     .local~datasource = '' then do
	call ErrorDialog 'Username, Password, and',
			 'Data Source cannot be blank!'
	return
  end

  self~DisableControls

  ODBCSQL =.ODBCSQL~New
  if ODBCSQL~Connect(.local~username,password) = -1 then do
	drop password
	self~EnableControls
	return
  end
  drop password
  
  self~OK:super

::method Cancel
  self~Cancel:super
