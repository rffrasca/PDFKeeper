'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
'* Copyright (C) 2009-2016 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

''' <summary>
''' Direct Upload folders list created from the names of the configuration
''' files without the XML extension. This class is to be used by Direct Upload
''' and the Direct Upload Folders View and presentation model.
''' </summary>
Public NotInheritable Class DirectUploadFolders
	Private Shared _names As New BindingList(Of String)
	
	Private Sub New()
		' Because type 'DirectUploadFolders' contains only 'Shared' members, a
		' default private constructor was added to prevent the compiler from
		' adding a default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets Direct Upload folder names as a list. In addition to iteration
	''' processing, this property can be bound to the DataSource of a list box. 
	''' </summary>
	Public Shared ReadOnly Property Names() As BindingList(Of String)
		Get
        	GetNames
        	Return _names
        End Get
	End Property
	
	''' <summary>
	''' Gets the Direct Upload folder names and adds them to the Names list.
	''' </summary>
	Private Shared Sub GetNames
		Dim files As String() = Directory.GetFiles( _
			ApplicationProfileFolders.DirectUploadXml, _
			"*.xml", _
			SearchOption.TopDirectoryOnly)
		_names.Clear
		For Each file In files
			_names.Add(Path.GetFileNameWithoutExtension(file))
		Next
	End Sub
	
	''' <summary>
	''' Provided to manually refreshes the Names list when data binding is
	''' used. It is not neccesaey to call this method when performing a get of
	''' the Names property directly.
	''' </summary>
	Public Shared Sub Refresh
		GetNames
	End Sub
End Class
