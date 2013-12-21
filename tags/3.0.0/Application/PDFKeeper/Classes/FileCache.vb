'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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
	Friend Shared cache As New Dictionary(Of String, String)
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will add "file" to the cache collection with its
	''' hashcode.
	''' </summary>
	''' <param name="file"></param>
	Public Shared Sub Add(ByVal file As String)
		cache.Add(file, FileTask.CalcHashCode(file))
	End Sub
	
	''' <summary>
	''' This function will check if "file" is cached.  For the file to be
	''' cached, it must exist on disk and its hash code must match the hash
	''' code stored in the cache collection. 
	''' </summary>
	''' <param name="file"></param>
	''' <returns>True or False</returns>
	Public Shared Function IsCached(ByVal file As String) As Boolean
		If System.IO.File.Exists(file) Then
			If cache.ContainsKey(file) Then
				If FileTask.CalcHashCode(file) = cache(file) Then
					Return True
				End If
			End If
		End If
		Return False
	End Function
End Class
