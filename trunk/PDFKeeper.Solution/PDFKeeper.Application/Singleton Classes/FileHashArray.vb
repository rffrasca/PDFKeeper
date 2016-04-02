'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Public NotInheritable Class FileHashArray
	Private Shared _instance As FileHashArray = New FileHashArray()
	Private fileHashDict As New Dictionary(Of String, String)
	
	Public Shared ReadOnly Property Instance As FileHashArray
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Adds the specified file and its calculated hash value to fileHashDict.
	''' </summary>
	''' <param name="file">Path name of file.</param>
	Public Sub Add(ByVal file As String)
		fileHashDict.Item(file) = ComputeFileHashValue(file)
	End Sub
	
	''' <summary>
	''' Checks that the specified file exists, is contained in fileHashDict,
	''' and the hash value of the file matches the hash value in fileHashDict.
	''' </summary>
	''' <param name="file">Path name of file.</param>
	''' <returns>True or False.</returns>
	Public Function ContainsItemAndHashValuesMatch( _
		ByVal file As String) As Boolean

		If System.IO.File.Exists(file) Then
			If fileHashDict.ContainsKey(file) Then
				If ComputeFileHashValue(file) = fileHashDict.Item(file) Then
					Return True
				End If
			End If
		End If
		Return False
	End Function
	
	''' <summary>
	''' Deletes all items in the list from the file system.
	''' </summary>
	Public Sub DeleteAllItemsFromFileSystem
		For Each pair As KeyValuePair(Of String, String) In fileHashDict
			Dim item As String = pair.Key
			If System.IO.File.Exists(item) Then
				DeleteFile(item, False)
			End If
		Next
	End Sub
End Class
