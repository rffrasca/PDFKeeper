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
	Private cache As New Dictionary(Of String, String)
	
	Public Shared ReadOnly Property Instance As FileCache
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Adds "file" and its calculated hash value to "cache".
	''' </summary>
	''' <param name="file"></param>
	Public Sub AddFileToCache(ByVal file As String)
		cache.Item(file) = ComputeFileHashValue(file)
	End Sub
	
	''' <summary>
	''' Does "file" exist, is contained in "cache", and the hash value of
	''' "file" matches the hash value stored in "cache"?
	''' </summary>
	''' <param name="file"></param>
	''' <returns>True or False</returns>
	Public Function ContainsItemAndHashValuesMatch( _
		ByVal file As String) As Boolean

		If System.IO.File.Exists(file) Then
			If cache.ContainsKey(file) Then
				If ComputeFileHashValue(file) = cache.Item(file) Then
					Return True
				End If
			End If
		End If
		Return False
	End Function
	
	''' <summary>
	''' Deletes all items contained in "cache" from the file system.  This
	''' method should be called when the Main Form is closing.
	''' </summary>
	Public Sub DeleteAllItemsFromFileSystem
		For Each pair As KeyValuePair(Of String, String) In cache
			Dim item As String = pair.Key
			If System.IO.File.Exists(item) Then
				DeleteFile(item, False)
			End If
		Next
	End Sub
End Class
