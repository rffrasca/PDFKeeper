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
::requires about.cls
::requires entry.cls
::requires pdf.cls

::class Search public subclass userdialog inherit AdvancedControls,
						  MessageExtensions

::method Init
  expose tempdir checked
  tempdir = value('TMP',,'ENVIRONMENT')
  checked = 0
  self~Init:super
  self~CreateCenter(500,305,.local~appl.title 'Search',,
					      'MINIMIZEBOX',,,
				      	      'Tahoma',,
			    		      .local~font.size)

::method DefineDialog
  fontdiff = .local~font.size - 8
  self~AddIcon(15,'..\Graphics\logo.ico')
  self~connectHelp('HelpTopics')
  self~AddText(5,7,50,12,'Search Text:',,200)
  self~AddEntryLine(201,,60,7,390,12,'AUTOSCROLLH')
  self~AddButton(202,455,7,40,13,'&Search','Search')
  self~AddListControl(203,,0,25,500,256 + fontdiff,,
	'REPORT HSCROLL VSCROLL SINGLESEL SHOWSELALWAYS')
  self~AddEntryLine(204,,0,281 + fontdiff,500,13,,
		   'BORDER READONLY NOTAB')
  self~CreateMenu

::method InitDialog
  expose curlist
  curlist = self~GetListControl(203)
  curlist~SetSmallImages('..\Graphics\listicon.bmp',16,16)
  curlist~AddExtendedStyle('FULLROWSELECT GRIDLINES CHECKBOXES')
  self~ConnectListNotify(203,'COLUMNCLICK')
  self~ConnectListViewNotify(203,'CLICK')
  self~ConnectListViewNotify(203,'CHECKBOXCHANGED')
  self~ConnectCommonNotify(203,'DBLCLK')
  curlist~InsertColumn(0,'ID',30,'RIGHT')
  curlist~InsertColumn(1,'Title',95)
  curlist~InsertColumn(2,'Author',95)
  curlist~InsertColumn(3,'Subject',95)
  curlist~InsertColumn(4,'Added',55)

  /* Menu */
  self~AddPopupMenu('&File')
  self~AddMenuItem('&Save PDF Document to Disk',100,'GRAYED',,
		   'SavePDFDocumentToDisk')
  self~AddMenuSeparator
  self~AddMenuItem('E&xit',101,'END','Cancel')
  self~AddPopupMenu('&Edit')
  self~AddMenuItem('Document &Notes',110,'GRAYED',,
		   'EditDocumentNotes')
  self~AddMenuSeparator
  self~AddMenuItem('&Check all Documents',111,,
		   'GRAYED','CheckAllDocuments')
  self~AddMenuItem('&Uncheck all Documents',112,,
		   'GRAYED','UncheckAllDocuments')
  self~AddMenuItem('&Delete all Checked Documents',113,,
		   'GRAYED END','DeleteCheckedDocuments')
  self~AddPopupMenu('&View')
  self~AddMenuItem('Document &Keywords',120,''GRAYED,,
		   'ViewDocumentKeywords')
  self~AddMenuSeparator
  self~AddMenuItem('&Refresh',121,'GRAYED END','SearchDocs')
  self~AddPopupMenu('&Help','END')
  self~AddMenuItem('&Help Topics',130,,'HelpTopics')
  self~AddMenuSeparator
  self~AddMenuItem('&About' .local~appl.title,131,'END','About')
  self~SetMenu

::method IndicateBeginProcessing
  do i = 201 to 204
	self~DisableItem(i)
  end
  self~Cursor_Wait
  parse value self~CursorPos with curX curY
  self~SetCursorPos(curX + 1, curY + 1)

::method IndicateEndProcessing
  self~RestoreCursorShape
  parse value self~CursorPos with curX curY
  self~SetCursorPos(curX - 1, curY - 1)
  do i = 201 to 204
	self~EnableItem(i)
  end

::method SavePDFDocumentToDisk
  expose curlist docID tempdir
  if docID = '' then
	return

  /* Prompt for folder/file to save */
  object.name = curlist~ItemText(curlist~Selected,1)
  PDF=.PDF~New
  pdf.file = PDF~SelectSave(object.name)
  if pdf.file = -1 then
	return  

  /* Save the PDF to disk */
  self~IndicateBeginProcessing
  query = "select doc_pdf from pdfkeeper.docs where",
	  "doc_id ="docID
  ODBCSQL=.ODBCSQL~New
  if ODBCSQL~QueryBLOB("doc_pdf",pdf.file,query) = -1 then do
	self~IndicateEndProcessing
	return
  end
  self~IndicateEndProcessing
  call InfoDialog pdf.file 'has been saved.'

::method EditDocumentNotes
  expose curlist docID
  if docID = '' then
	return

  self~IndicateBeginProcessing
  query = "select doc_notes from pdfkeeper.docs where",
	  "doc_id ="docID
  ODBCSQL=.ODBCSQL~New
  c1. = ODBCSQL~Command(query)
  self~IndicateEndProcessing
  if c1. = -1 then
	return

  dlg.type = 'MULTI'
  dlg = .Entry~New(dlg.type,'Document Notes',,
			    'Notes:',c1.doc_notes.1)
  dlg~Execute('SHOWTOP',15)
  if result = 2 then
	return
 
  self~IndicateBeginProcessing
  update = "update pdfkeeper.docs set",
	   "doc_notes =q'[".local~entrytext"]',",
	   "doc_dummy = '' where doc_id ="docID
  if ODBCSQL~Command(update) = -1 then do
	self~IndicateEndProcessing
	return
  end
  self~IndicateEndProcessing

::method CheckAllDocuments
  expose curlist
  curList~CheckAll

::method UncheckAllDocuments
  expose curlist
  curList~UncheckAll

::method DeleteCheckedDocuments
  expose curlist checked
  if AskDialog('Delete all checked documents?') = 0 then
	return

  self~IndicateBeginProcessing
  ODBCSQL=.ODBCSQL~New
  do i = 0 to curlist~Items
	if curlist~IsChecked(i) then do
		delete = "delete from pdfkeeper.docs where",
  	   		 "doc_id ="curlist~ItemText(i,0)
		if odbcsql~Command(delete) = -1 then do
			self~IndicateEndProcessing
			return
		end
	end
  end
  self~IndicateEndProcessing
  self~SearchDocs

::method ViewDocumentKeywords
  expose curlist docID
  if docID = '' then
	return

  self~IndicateBeginProcessing
  query = "select doc_keywords from pdfkeeper.docs where",
	  "doc_id ="docID
  ODBCSQL=.ODBCSQL~New
  c1. = ODBCSQL~Command(query)
  self~IndicateEndProcessing
  if c1. = -1 then
	return

  dlg.type = 'MULTI_RO'
  dlg = .Entry~New(dlg.type,'Document Keywords',,
			    'Keywords:',c1.doc_keywords.1)
  dlg~Execute('SHOwTOP',15)

::method HelpTopics
  if FindWindow('HTML Help') > 0 then do
 	call InfoDialog 'An instance of HTML Help is already open!'
 	return
  end
  self~IndicateBeginProcessing
  rc = RxWinExec('hh ..\Documents\userguide.html')
  if rc < 32 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  call SysSleep 5
  self~IndicateEndProcessing

::method About
  dlg = .About~New
  dlg~Execute('SHOWTOP',15)
  dlg~DeInstall

::method Search
  expose curlist search.text sortby
  sortby = 'doc_id'
  search.text = strip(self~GetValue(201))
  if search.text > '' then do
	if pos('*',search.text) > 0 then do
		call ErrorDialog 'Database does not support the',
				 'use of * as a wildcard',
				 'character! Use % instead. For',
				 'more information, please consult',
				 'the User Guide.'
		return
	end
	if translate(search.text) = 'MINUS' |,
	   translate(search.text) = 'MINUS ' |,
	   translate(search.text) = 'NEAR' |,
	   translate(search.text) = 'NEAR ' |,
 	   translate(search.text) = 'NOT' |,
	   translate(search.text) = 'NOT ' |,
           translate(search.text) = 'AND' |,
	   translate(search.text) = 'AND ' |,
	   translate(search.text) = 'EQUIV' |,
	   translate(search.text) = 'EQUIV ' |,
           translate(search.text) = 'WITHIN' |,
	   translate(search.text) = 'WITHIN ' |,
	   translate(search.text) = 'OR' |,
	   translate(search.text) = 'OR ' |,
	   translate(search.text) = 'ACCUM' |,
	   translate(search.text) = 'ACCUM ' |,
	   translate(search.text) = 'FUZZY' |,
	   translate(search.text) = 'FUZZY ' |,
	   translate(search.text) = 'ABOUT' |,
	   translate(search.text) = 'ABOUT ' |,
	   translate(search.text) = 'ABOUT()' |, 
	    pos('{}',search.text) > 0 |,
	    pos('()',search.text) > 0 |,
	        left(search.text,1) = '=' |,
	        left(search.text,1) = ';' |,
	        left(search.text,1) = '>' |,
	        left(search.text,1) = '-' |,
 	        left(search.text,1) = '~' |,
	        left(search.text,1) = '&' |,
	        left(search.text,1) = '|' |,
	        left(search.text,1) = ',' |,
 	        left(search.text,1) = '!' |,
	        left(search.text,1) = '{' |,
	        left(search.text,1) = '(' |,
	             search.text = '?' |,
		     search.text = '$' then do
		call ErrorDialog 'Syntax Error: Inproper use of',
				 'operators and/or characters.',
				 'For more information, please',
				 'consult the User Guide.'
		return
  	end
	self~SearchDocs
	self~EnableMenuItem(121)
  end
  else
	self~GrayMenuItem(121)

::method OnColumnClick
  expose sortby
  use arg id, column
  if column = 0 then
	sortby = 'doc_id,doc_subject,doc_title,doc_author,doc_added'
  if column = 1 then
	sortby = 'doc_title,doc_subject,doc_author,doc_added,doc_id'
  if column = 2 then
	sortby = 'doc_author,doc_subject,doc_title,doc_added,doc_id'
  if column = 3 then
	sortby = 'doc_subject,doc_title,doc_author,doc_added,doc_id'
  if column = 4 then
	sortby = 'doc_added,doc_subject,doc_title,doc_author,doc_id'
  self~SearchDocs

::method OnClick
  expose curlist docID
  docID = curlist~ItemText(curlist~Selected,0)
  if docID = '' then do
	self~GrayMenuItem(100)
	self~GrayMenuItem(110)
	self~GrayMenuItem(120)
  end
  else do
	self~EnableMenuItem(100)
	self~EnableMenuItem(110)
	self~EnableMenuItem(120)
  end

::method OnCheckboxChanged
  expose checked
  use arg id, itemIndex, state
  if state = 'CHECKED' then
	checked = checked + 1
  if state = 'UNCHECKED' & checked > 0 then
	checked = checked - 1
  if checked > 0 then do
	self~EnableMenuItem(112)
	self~EnableMenuItem(113)
  end
  else do
	self~GrayMenuItem(112)
	self~GrayMenuItem(113)
  end

::method OnDblClk	/* View PDF file */
  expose curlist docID tempdir
  if docID = '' then
	return
  
  self~IndicateBeginProcessing
  pdf.file = SysTempFileName(tempdir'\pdfkeeper???.pdf')
  query = "select doc_pdf from pdfkeeper.docs where",
	  "doc_id ="docID
  ODBCSQL=.ODBCSQL~New
  if ODBCSQL~QueryBLOB("doc_pdf",pdf.file,query) = -1 then do
	self~IndicateEndProcessing
	return
  end

  /* Set the system attribute on the PDF file */
  rc = SysFileTree(pdf.file,'file','F','*****','****+')
  if rc \= 0 then do
	self~IndicateEndProcessing
	call ErrorDialog SysGetErrorText(rc)
	return
  end

  /* View the PDF */
  pdf = .PDF~New
  pdf~View(0,pdf.file)

  self~IndicateEndProcessing

::method SearchDocs
  expose curlist search.text sortby checked
  checked = 0
  self~GrayMenuItem(100)
  self~GrayMenuItem(110)
  self~GrayMenuItem(111)
  self~GrayMenuItem(112)
  self~GrayMenuItem(113)
  self~GrayMenuItem(120)

  self~IndicateBeginProcessing
  curlist~DeleteAll
  
  /* Set default width on list view columns */
  curlist~SetColumnWidth(0,30)
  curlist~SetColumnWidth(1,95)
  curlist~SetColumnWidth(2,95)
  curlist~SetColumnWidth(3,95)
  curlist~SetColumnWidth(4,55)

  /* Query the documents and add to the list */
  query = "select doc_id,doc_title,doc_author,doc_subject,",
	  "doc_added from pdfkeeper.docs where",
 	  "(contains(doc_dummy,q'["search.text"]'))>0",
	  "order by" sortby
  ODBCSQL =.ODBCSQL~New
  c1. = ODBCSQL~Command(query)
  if c1. = -1 then do
	self~IndicateEndProcessing
	return
  end
  do i = 1 to c1.doc_id.0
	curList~AddRow(,,c1.doc_id.i,c1.doc_title.i,,
			 c1.doc_author.i,c1.doc_subject.i,,
			 c1.doc_added.i)
  end
  self~SetValue(204,c1.doc_id.0 'documents found.')

  if c1.doc_id.0 > 0 then do
	self~EnableMenuItem(111)
  
	/* Auto resize list view columns */
  	curlist~SetColumnWidth(0)
	curlist~SetColumnWidth(1)
	curlist~SetColumnWidth(2)
	curlist~SetColumnWidth(3)
	curlist~SetColumnWidth(4)

	/* Make sure the total width of the Title, Author, and */
	/* Subject columns does not exceed 285 */
	if curlist~ColumnWidth(1) + curlist~ColumnWidth(2) +,
	   curlist~ColumnWidth(3) > 285 then do
		do i = 1 to 3
			if curlist~ColumnWidth(i) > 95 then
				curlist~SetColumnWidth(i,95)
		end
	end
  end

  self~IndicateEndProcessing  

::method Ok	/* Link the enter key to the search method */
  self~Search

::method Cancel
  expose tempdir

  /* Remove temporary files */
  self~IndicateBeginProcessing
  rc = SysFileTree(tempdir'\pdfkeeper*.pdf','files','FO')
  if rc \= 0 then do
	self~IndicateEndProcessing
	call ErrorDialog SysGetErrorText(rc)
	return
  end
  do i = 1 to files.0
	rc = SysFileDelete(files.i)
	if rc > 0 then do
		self~IndicateEndProcessing
		call ErrorDialog SysGetErrorText(rc)
		return
	end
  end

  ODBCSQL=.ODBCSQL~New
  ODBCSQL~Disconnect
  self~IndicateEndProcessing

  self~Cancel:super
