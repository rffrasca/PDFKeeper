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

::requires oodplain.cls

::class About public subclass userdialog

::method Init
  self~Init:super
  self~CreateCenter(200,145,'About',,,'Tahoma',.local~font.size)

::method DefineDialog
  self~AddIcon(15,'..\Graphics\logo.ico')
  self~AddBitmapButton(100,89,5,21,19,,,'..\Graphics\logo.bmp',,,,,
				        'STRETCH DISABLED')
  self~AddText(5,30,190,12,.local~appl.title .local~appl.version,,
	      'CENTER',101)
  self~AddText(5,40,190,12,.local~appl.description,'CENTER',102)
  self~AddText(5,50,190,12,.local~appl.copyright,'CENTER',103)
  self~AddText(5,65,190,12,.local~gpl.1,'CENTER',104)
  self~AddText(5,75,190,24,.local~gpl.2,'CENTER',105)
  self~AddText(5,100,190,24,.local~3rdparty,'CENTER',106)
  self~AddButton(1,80,130,40,12,'Ok','Ok')
