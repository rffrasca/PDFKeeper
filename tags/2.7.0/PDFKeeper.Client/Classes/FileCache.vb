'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Class FileCache
	Friend Shared cache As New Dictionary(Of String, String)
	Dim file As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: file"></param>
	Public Sub New(ByVal arg As String)
		file = arg
	End Sub

	''' <summary>
	''' This subroutine will add file object to the cache collection.
	''' </summary>
	Public Sub Add
		Dim oFileIO As New FileIO(file)
		cache.Add(file, oFileIO.CalcHashCode)
	End Sub
	
	''' <summary>
	''' This function will check if file object is cached.  For the file to be
	''' cached, it must exist on disk and its hash code must match the hash
	''' code stored in the cache collection.
	''' </summary>
	''' <returns>True or False</returns>
	Public Function IsFileCached As Boolean
		If IO.File.Exists(file) Then
			If cache.ContainsKey(file) Then
				Dim oFileIO As New FileIO(file)
				If oFileIO.CalcHashCode = cache(file) Then
					Return True
				End If
			End If
		End If
		Return False
	End Function
End Class
