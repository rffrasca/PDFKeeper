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

PDF=.PDF~New
property=.Property~New

/* Get input PDF file */
parse arg inputpdf
if inputpdf > '' then do
	.local~pdf.file = strip(inputpdf)
	if translate(right(.local~pdf.file,3)) \= 'PDF' then do
		.local~pdf.file = PDF~SelectOpen
	  	if .local~pdf.file = -1 then
			exit
	end
	if stream(.local~pdf.file,'c','query exists') = '' then do
		call ErrorDialog 'File does not',
				 'exist:' .local~pdf.file
		exit
	end
end
else do
	.local~pdf.file = PDF~SelectOpen
  	if .local~pdf.file = -1 then
		exit
end

/* Get the Oracle XE ODBC driver to use */
if property~ODBCDriver = -1 then
	exit

/* Check if the PDF file is encrypted. Do not continue if the */
/* PDF file is protected with a user password. */
encrypted = PDF~Encrypted(.local~pdf.file)
if encrypted = -1 then do
	call ErrorDialog .local~pdf.file 'is encrypted and',
			 'protected by a USER password.',
			 'USER password protected PDF documents',
			 'are not supported by PDFKeeper. Please',
			 'see the Troubleshooting section in the',
			 'User FAQ for more information.'
	exit
end
if left(encrypted,3) = 'yes' then do
	call ErrorDialog .local~pdf.file 'is encrypted and maybe',
			 'protected by an OWNER password.',
			 'PDFKeeper Summary Editor does not',
			 'support encrypted PDF documents. Please',
			 'see the Troubleshooting section in the',
			 'User FAQ for more information.'
	exit
end

/* Read in PDF file summary */
.local~pdf.title = PDF~Title(.local~pdf.file)
if .local~pdf.title = -1 then
	exit
.local~pdf.author = PDF~Author(.local~pdf.file)
if .local~pdf.author = -1 then
	exit
.local~pdf.subject = PDF~Subject(.local~pdf.file)
if .local~pdf.subject = -1 then
	exit
.local~pdf.keywords = PDF~Keywords(.local~pdf.file)
if .local~pdf.keywords = -1 then
	exit

property~Get	/* Get application properties */

/* Show GUI */
dlg = .DBLogon~New
dlg~Execute('SHOWTOP',15)
res = result
dlg~DeInstall
if res = 1 then do
	dlg = .SummaryEditor~New
	dlg~Execute('SHOWTOP',15)
	dlg~DeInstall
	ODBCSQL=.ODBCSQL~New
	ODBCSQL~Disconnect
end

property~Set	/* Set application properties */

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
::requires pdf.cls
::requires dblogon.cls
::requires pdfsedit.cls
