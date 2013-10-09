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
::requires winsystm.cls

::class Property public

::method Init
  expose reg propfile
  reg = .WindowsRegistry~new

  .local~appl.title = 'PDFKeeper'
  .local~appl.version = '1.0.0'
  .local~appl.description = 'PDF Document Storage for Small or',
			    'Home Office'  
  .local~appl.copyright = 'Copyright © 2009 Robert F. Frasca'
  .local~gpl.1 = 'This program comes with ABSOLUTELY NO WARRANTY.'
  .local~gpl.2 = 'This is free software, and you are welcome to',
	         'redistribute it under certain conditions. See',
	         'COPYING.txt for details.'
  .local~3rdparty = 'This software includes third-party',
		    'components. See THIRDPARTYLICENSEREADME.txt',
		    'for details.'

  propfile = value('APPDATA',,'ENVIRONMENT')'\pdfkeeper.properties'

::method SetFontSize	/* Set dialog font size */
  size = ScreenSize()
  height = format(size[4] / 100,,0) + 2
  return height

::method ODBCDriver	/* Get the Oracle XE ODBC driver to use */
  expose reg
  drivers.0 = 2
  drivers.1 = 'Oracle in XE'
  drivers.2 = 'Oracle in XEClient'
  .local~odbc.driver = ''
  do i = 1 to drivers.0
	if reg~open(reg~local_machine,'SOFTWARE\ODBC\ODBCINST.INI',,
				      'QUERY') \= 0 then do
		reg~List(,keys.)
		do j over keys.
			if keys.j = drivers.i then
				.local~odbc.driver = drivers.i
		end
		reg~Close
	end
	else do
		call ErrorDialog 'Unable to open registry key:',
				 'HKLM\SOFTWARE\ODBC\ODBCINST.INI'
		return -1
	end
  end
  if .local~odbc.driver = '' then do
	call ErrorDialog 'Unable to locate an Oracle XE ODBC',
			 'driver!'
	return -1
  end

  return 0

::method Get
  expose propfile
  prop = .properties~load(propfile)

  .local~font.size = self~SetFontSize
  .local~username = prop~getProperty('LastUsername','')
  .local~datasource = prop~getProperty('LastDataSource','')

  if .local~odbc.driver = 'Oracle in XE' then
	.local~datasource = 'XE'

::method Set
  expose propfile
  prop = .properties~load(propfile)

  if .local~odbc.driver = 'Oracle in XE' then
	.local~datasource = ''

  prop~setProperty('LastUsername',.local~username)
  prop~setProperty('LastDataSource',.local~datasource)
  prop~save(propfile)
