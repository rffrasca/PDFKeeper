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

::requires oodwin32.cls

::class Entry public subclass userdialog inherit AdvancedControls

::method Init
  expose dlg.type dlg.title dlg.caption dlg.prefill
  use arg dlg.type,dlg.title,dlg.caption,dlg.prefill
  self~Init:super

  /* Set window size */
  if dlg.type = 'TIMESTAMP' then do
	x = 150
	y = 50
  end
  if dlg.type = 'MULTI' | dlg.type = 'MULTI_RO' then do
	x = 400
	y = 205
  end

  self~CreateCenter(x,y,dlg.title,,,'Tahoma',.local~font.size)

::method DefineDialog
  expose dlg.type dlg.caption
  self~AddIcon(15,'..\Graphics\logo.ico')

  /* Set static text control dimensions */
  if dlg.type = 'TIMESTAMP' then
	static.x = 40
  if dlg.type = 'MULTI' | dlg.type = 'MULTI_RO' then
	static.x = 100
  self~AddText(5,10,static.x,12,dlg.caption,,100)

  /* Set entry field dimensions*/
  if dlg.type = 'TIMESTAMP' then
	self~AddEntryLine(101,,50,10,95,12)
  if dlg.type = 'MULTI' then
	self~AddEntryLine(101,,5,20,390,,
			  156,'MULTILINE VSCROLL AUTOSCROLLV')
  if dlg.type = 'MULTI_RO' then
	self~AddEntryLine(101,,5,20,390,,
			  156,'MULTILINE READONLY AUTOSCROLLV')
  
  /* Create dialog buttons */
  if dlg.type = 'MULTI_RO' then
	self~AddButton(2,355,190,40,12,'Close')
  else
	self~AddOKCancelRightBottom

::method InitDialog
  expose dlg.type dlg.prefill
  self~DATA101=dlg.prefill

  if dlg.type = 'TIMESTAMP' then
	limit = '19'
  if dlg.type = 'MULTI'|dlg.type = 'MULTI_RO' then
	limit = '4000'
  self~GetEditControl(101)~SetLimit(limit)

::method Ok
  expose dlg.type dlg.caption
  self~SetValue(101,strip(self~GetValue(101)))
  .local~entrytext = self~GetValue(101)
  if .local~entrytext = '' & dlg.type = 'TIMESTAMP' then do
	call ErrorDialog dlg.caption 'not specified!'
	return
  end
  
  self~OK:super

::method Cancel
  self~Cancel:super
