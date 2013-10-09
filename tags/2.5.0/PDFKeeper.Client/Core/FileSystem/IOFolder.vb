'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Public Class IOFolder
	Dim folder As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: folder"></param>
	Public Sub New(ByVal arg As String)
		folder = arg
	End Sub
	
	''' <summary>
	''' This function will create the folder object if it doesn't exist.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Create As Integer
		Try
			If Directory.Exists(folder) = False Then
				Directory.CreateDirectory(folder)
			End If
			Return 0
		Catch ex As IOException
			Dim oMessageDialog As New MessageDialog(ex.Message)
			oMessageDialog.DisplayError
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This function will delete the folder object if it does exist.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Delete As Integer
		Try
			If Directory.Exists(folder) Then
				Directory.Delete(folder)
			End If
			Return 0
		Catch ex As IOException
			Dim oMessageDialog As New MessageDialog(ex.Message)
			oMessageDialog.DisplayError
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This subroutine will delete PDF files created by PDFKeeper from the
	''' the folder object.  This subroutine will not display a message when an
	''' IOException has been caught.
	''' </summary>
	Public Sub DeletePdfFiles
		Dim objDirectoryInfo As New DirectoryInfo(folder)
		Dim files As FileInfo() = objDirectoryInfo.GetFiles("pdfkeeper*.pdf")
		For Each file In files
			Try
				file.Delete
			Catch ex as IOException
			End Try
		Next
	End Sub
End Class
