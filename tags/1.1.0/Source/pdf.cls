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

::class PDF public

::method Init
  expose tempdir
  tempdir = value('TMP',,'ENVIRONMENT')

::method SelectOpen
  curdir = directory()
  file = FileNameDialog('*.pdf',,"Adobe PDF Files"'0'x"*.pdf"'0'x,,,
			'Select PDF Document')
  call directory curdir
  if file = 0 then
	file = -1
  return file

::method SelectSave
  parse arg doc.title
  curdir = directory()
  file = FileNameDialog(doc.title'.pdf',,,
		       "Adobe PDF Files"'0'x"*.pdf"'0'x,'SAVE',,
		       'Save PDF Document')
  call directory curdir
  if file = 0 then
	file = -1
  return file

::method View
  expose pdf.file
  parse arg restrict,pdf.file
  if restrict = 1 then
	cmdLine = '..\Binaries\SumatraPDF -restrict "'pdf.file'"'
  else
	cmdLine = '..\Binaries\SumatraPDF "'pdf.file'"'
  rc = RxWinExec(cmdLine)
  if rc < 32 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  return 0

::method CheckDictionary
  expose pdf.file tempdir
  parse arg pdf.file

  /* Verify the PDF contains an Info Dictionary */
  out.file = SysTempFileName(tempdir'\pdftk???.txt')
  '..\Binaries\pdftk "'pdf.file'" dump_data 1>nul 2>"'out.file'"'
  if rc \= 0 then do
	call ErrorDialog 'Unable to dump the dictionary',
			 'of' pdf.file 'to' out.file
	return -1
  end
  warning = 'Warning: no info dictionary found'
  if stream(out.file,'c','query size') > 0 then do
	rc = SysFileSearch(warning,out.file,'text')
	if rc \= 0 then do
		call ErrorDialog SysGetErrorText(rc)
		return -1
	end
  end
  rc = SysFileDelete(out.file)
  if rc > 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  if text.0 = 0 then
	return 0
  if translate(strip(text.1)) \= translate(warning) then
	return 0

  /* Create an Info Dictionary in the PDF file */
  out.file = SysTempFileName(tempdir'\pdftk???.pdf')
  '..\Binaries\pdftk "'pdf.file'" cat output "'out.file'">nul'
  if rc \= 0 then do
	call ErrorDialog 'Unable to catenate' pdf.file 'to',
			  out.file
	return -1
  end
  rc = SysFileCopy(out.file,pdf.file)
  if rc \= 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  rc = SysFileDelete(out.file)
  if rc > 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end

  return 0

::method Repair
  expose pdf.file tempdir
  parse arg pdf.file
  out.file = SysTempFileName(tempdir'\pdftk???.pdf')
  '..\Binaries\pdftk "'pdf.file'" output "'out.file'" 1>nul 2>nul'
  if rc \= 0 then do
	call ErrorDialog 'Unable to repair:' pdf.file
	return -1
  end
  rc = SysFileCopy(out.file,pdf.file)
  if rc \= 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  rc = SysFileDelete(out.file)
  if rc > 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end

  return 0

::method Title
  expose pdf.file
  parse arg pdf.file
  return self~GetInfo('Title:')

::method Author
  expose pdf.file
  parse arg pdf.file
  return self~GetInfo('Author:')

::method Subject
  expose pdf.file
  parse arg pdf.file
  return self~GetInfo('Subject:')

::method Keywords
  expose pdf.file
  parse arg pdf.file
  return self~GetInfo('Keywords:')

::method Encrypted
  expose pdf.file
  parse arg pdf.file
  return self~GetInfo('Encrypted:')

::method GetInfo private
  expose pdf.file tempdir
  use arg info.type

  /* Extract the PDF file InfoDictionary */
  info.file = SysTempFileName(tempdir'\pdfinfo???.txt')
  '..\Binaries\pdfinfo "'pdf.file'" > "'info.file'"'
  if rc = 1 then
	message = 'Unable to open file:' pdf.file
  if rc = 2 then
	message = 'Unable to open file:' info.file
  if rc = 3 then
	message = 'Unable to access file:' pdf.file
  if rc = 99 then
	message = 'Unable to extract InfoDictionary from',
		  'file:' pdf.file
  if rc \= 0 then do
  	call ErrorDialog message
	return -1
  end

  /* Search the extracted data for the requested text */
  rc = SysFileSearch(info.type,info.file,'text')
  if rc \= 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end
  rc = SysFileDelete(info.file)
  if rc > 0 then do
	call ErrorDialog SysGetErrorText(rc)
	return -1
  end

  /* Return the results back to the caller */
  if text.0 = 0 then
	return ''
  string = strip(substr(text.1,16))
  return string
