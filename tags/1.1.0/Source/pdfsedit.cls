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
::requires odbcsql.cls
::requires pdf.cls
::requires about.cls

::class SummaryEditor public subclass userdialog inherit,
					         AdvancedControls,
					         MessageExtensions

::method Init
  self~Init:super
  self~CreateCenter(350,200,'PDFKeeper Summary Editor',,,,
			    'Tahoma',.local~font.size)

::method DefineDialog
  self~AddIcon(15,'..\Graphics\logo.ico')
  self~connectHelp('HelpTopics')
  self~AddGroupBox(5,10,340,35,'PDF Document',,100)
  self~AddEntryLine(101,,10,25,275,12,'AUTOSCROLLH READONLY NOTAB')
  self~AddButton(102,290,25,50,13,'&View','View')
  self~AddGroupBox(5,55,340,120,'PDF Document Summary',,103)
  self~AddText(10,70,40,12,'Title:',,104)
  self~AddEntryLine(105,,55,70,285,12,'AUTOSCROLLH')
  self~AddText(10,90,40,12,'Author:',,106)
  self~AddComboBox(107,,55,90,285,12,'AUTOSCROLLH SORT VSCROLL')
  self~AddText(10,110,40,12,'Subject:',,108)
  self~AddComboBox(109,,55,110,285,12,'AUTOSCROLLH SORT VSCROLL')
  self~AddText(10,130,40,12,'Keywords:',,110)
  self~AddEntryLine(111,,55,130,285,,
			 37,'MULTILINE VSCROLL AUTOSCROLLV')
  self~AddButton(112,5,185,50,12,'&About','About')
  self~AddButton(113,195,185,50,12,'&Help','HelpTopics')
  self~AddOKCancelRightBottom

::method InitDialog
  expose combo1 combo2
  combo1 = self~GetComboBox(107)
  combo2 = self~GetComboBox(109)
  self~ConnectComboBoxNotify(107,'DROPDOWN','FillAuthors')
  self~ConnectComboBoxNotify(109,'DROPDOWN','FillSubjects')
  self~GetEditControl(101)~SetLimit(512)
  self~GetEditControl(105)~SetLimit(4000)
  self~GetEditControl(107)~SetLimit(4000)
  self~GetEditControl(109)~SetLimit(4000)
  self~GetEditControl(111)~SetLimit(4000)
  self~DATA101=.local~pdf.file
  self~DATA105=.local~pdf.title
  self~DATA107=.local~pdf.author
  self~DATA109=.local~pdf.subject
  self~DATA111=.local~pdf.keywords

::method IndicateBeginProcessing
  self~DisableItem(101)
  self~DisableItem(102)
  self~DisableItem(105)
  self~DisableItem(111)
  self~DisableItem(112)
  self~DisableItem(113)
  self~DisableItem(1)
  self~DisableItem(2)
  self~Cursor_Wait
  parse value self~CursorPos with curX curY
  self~SetCursorPos(curX + 1, curY + 1)

::method IndicateEndProcessing
  self~RestoreCursorShape
  parse value self~CursorPos with curX curY
  self~SetCursorPos(curX - 1, curY - 1)
  self~EnableItem(101)
  self~EnableItem(102)
  self~EnableItem(105)
  self~EnableItem(107)
  self~EnableItem(109)
  self~EnableItem(111)
  self~EnableItem(112)
  self~EnableItem(113)
  self~EnableItem(1)
  self~EnableItem(2)

::method View
  self~DisableItem(107)
  self~DisableItem(109)
  self~IndicateBeginProcessing
  pdf = .PDF~New
  pdf~View(1,.local~pdf.file)
  self~IndicateEndProcessing

::method FillAuthors
  expose combo1
  self~IndicateBeginProcessing
  combo1~DeleteAll
  odbcsql=.ODBCSQL~New
  query = "select doc_author,count(doc_author) from",
	  "pdfkeeper.docs group by doc_author"
  c1. = ODBCSQL~Command(query)
  if c1. = -1 then do
	self~IndicateEndProcessing
	return
  end
  do i = 1 to c1.doc_author.0
	combo1~Add(c1.doc_author.i)
  end
  self~IndicateEndProcessing

::method FillSubjects
  expose combo2
  self~IndicateBeginProcessing
  combo2~DeleteAll
  self~SetValue(107,strip(self~GetValue(107))) 
  author = self~GetValue(107)
  odbcsql=.ODBCSQL~New
  query = "select doc_subject from pdfkeeper.docs where",
	  "doc_author =q'["author"]' group by doc_subject"
  c1. = ODBCSQL~Command(query)
  if c1. = -1 then do
	self~IndicateEndProcessing
	return
  end
  do i = 1 to c1.doc_subject.0
	combo2~Add(c1.doc_subject.i)
  end
  self~IndicateEndProcessing

::method About
  dlg = .About~New
  dlg~Execute('SHOWTOP',15)
  dlg~DeInstall

::method HelpTopics
  if FindWindow('HTML Help') > 0 then do
 	call InfoDialog 'An instance of HTML Help is already open!'
 	return
  end
  self~DisableItem(107)
  self~DisableItem(109)
  self~IndicateBeginProcessing
  rc = RxWinExec('hh ..\Documents\userguide.html')
  if rc < 32 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  call SysSleep 5
  self~IndicateEndProcessing

::method Ok
  tempdir = value('TMP',,'ENVIRONMENT')
  pdf = .PDF~New

  /* Strip leading and trailing spaces from all entries */
  self~SetValue(105,strip(self~GetValue(105)))
  self~SetValue(107,strip(self~GetValue(107)))
  self~SetValue(109,strip(self~GetValue(109)))
  self~SetValue(111,strip(self~GetValue(111)))

  /* Entry field validation */
  if self~GetValue(105) = '' | self~GetValue(107) = '' |,
     self~GetValue(109) = '' then do
	call ErrorDialog 'Title, Author, and Subject cannot be',
			 'blank!'
	return
  end
  if pos('|',self~GetValue(105)) > 0 |,
     pos('|',self~GetValue(107)) > 0 |,
     pos('|',self~GetValue(109)) > 0 |,
     pos('|',self~GetValue(111)) > 0 then do
	call ErrorDialog 'Title, Author, Subject, and Keywords',
			 'cannot contain any | characters!'
	return
  end

  /* Check if the PDF is still open for viewing */
  if FindWindow(filespec('name',.local~pdf.file)) > 0 then do
	call ErrorDialog .local~pdf.file 'is still open for viewing!'
	return
  end

  /* Give the user the option of backing up the PDF file */
  if AskDialog('Do you want to backup' .local~pdf.file 'before',
	       'any modifications are made to it?') = 1 then do
	backup.file = PDF~SelectSave
  	if backup.file = -1 then
		return
	
	self~DisableItem(107)
	self~DisableItem(109)
	self~IndicateBeginProcessing
	rc = SysFileCopy(.local~pdf.file,backup.file)
	if rc \= 0 then do
		self~IndicateEndProcessing
		call ErrorDialog SysGetErrorText(rc)
		return
	end
	self~IndicateEndProcessing
  end
  
  self~DisableItem(107)
  self~DisableItem(109)
  self~IndicateBeginProcessing

  /* Make sure the PDF file contains an Info Dictionary */
  if PDF~CheckDictionary(.local~pdf.file) = -1 then do
	self~IndicateEndProcessing
	return
  end

  /* Repair the PDF file */
  if PDF~Repair(.local~pdf.file) = -1 then do
	self~IndicateEndProcessing
	return
  end

  /* Create input file for the summary update */
  summary.0 = 8
  summary.1 = 'InfoKey: Title'
  summary.2 = 'InfoValue: 'self~GetValue(105)
  summary.3 = 'InfoKey: Author'
  summary.4 = 'InfoValue: 'self~GetValue(107)
  summary.5 = 'InfoKey: Subject'
  summary.6 = 'InfoValue: 'self~GetValue(109)
  summary.7 = 'InfoKey: Keywords'
  summary.8 = 'InfoValue: 'changestr('0d'x||'0a'x,,
			   self~GetValue(111),' ')
  input.file = SysTempFileName(tempdir'\pdftk???.txt')
  do i = 1 to summary.0
	if lineout(input.file,summary.i) \= 0 then do
		self~IndicateEndProcessing
		call ErrorDialog 'Unable to write to:' input.file
		return
	end
  end
  call lineout input.file

  /* Update the PDF file summary */
  out.file = SysTempFileName(tempdir'\pdftk???.pdf')
  '..\Binaries\pdftk "'.local~pdf.file'" update_info',
	   '"'input.file'" output "'out.file'" 1>nul 2>nul'
  if rc \= 0 then do
	self~IndicateEndProcessing
	call ErrorDialog 'Unable to update:' .local~pdf.file
	return
  end
  rc = SysFileDelete(input.file)
  if rc > 0 then do
	self~IndicateEndProcessing
	call ErrorDialog SysGetErrorText(rc)
	return
  end
  rc = SysFileCopy(out.file,.local~pdf.file)
  if rc \= 0 then do
	self~IndicateEndProcessing
	call ErrorDialog SysGetErrorText(rc)
	return
  end
  rc = SysFileDelete(out.file)
  if rc > 0 then do
	self~IndicateEndProcessing
	call ErrorDialog SysGetErrorText(rc)
	return
  end

  self~IndicateEndProcessing

  /* Give the user the option of viewing the PDF file */
  if AskDialog('Would you like to view',
	       .local~pdf.file'?') = 1 then do
	self~View
	
	self~DisableItem(107)
        self~DisableItem(109)
	self~IndicateBeginProcessing
	call SysSleep 5
	self~IndicateEndProcessing
  end

  self~OK:super

::method Cancel
  self~Cancel:super
