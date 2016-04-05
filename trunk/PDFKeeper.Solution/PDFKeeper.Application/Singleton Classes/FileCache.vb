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

Public NotInheritable Class FileCache
	Private Shared _instance As FileCache = New FileCache()
	Private fileCacheDict As New Dictionary(Of String, String)
	
	Public Shared ReadOnly Property Instance As FileCache
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Adds "file" and its calculated hash value to fileCacheDict.
	''' </summary>
	''' <param name="file"></param>
	Public Sub Add(ByVal file As String)
		fileCacheDict.Item(file) = ComputeFileHashValue(file)
	End Sub
	
	''' <summary>
	''' Returns True or False if "file" exists, is contained in fileCacheDict,
	''' and the hash value of "file" matches the hash value stored in
	''' fileCacheDict.
	''' </summary>
	''' <param name="file"></param>
	''' <returns></returns>
	Public Function ContainsItemAndHashValuesMatch( _
		ByVal file As String) As Boolean

		If System.IO.File.Exists(file) Then
			If fileCacheDict.ContainsKey(file) Then
				If ComputeFileHashValue(file) = fileCacheDict.Item(file) Then
					Return True
				End If
			End If
		End If
		Return False
	End Function
	
	''' <summary>
	''' Deletes all items contained in fileCacheDict from the file system.
	''' This should be called when the Main Form is closing.
	''' </summary>
	Public Sub DeleteAllItemsFromFileSystem
		For Each pair As KeyValuePair(Of String, String) In fileCacheDict
			Dim item As String = pair.Key
			If System.IO.File.Exists(item) Then
				DeleteFile(item, False)
			End If
		Next
	End Sub
End Class
