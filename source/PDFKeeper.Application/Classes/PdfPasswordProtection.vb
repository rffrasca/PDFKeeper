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

Public NotInheritable Class PdfPasswordProtection
	''' <summary>
	''' PDF Password types.
	''' </summary>
	Public Enum Type
		None
		Owner
		User
		Unknown
	End Enum
	
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns></returns>
	Public Shared Function GetPType( _
		ByVal pdfFile As String) As Type
		Dim reader As PdfReader = Nothing
		Dim inputOpened As Boolean = False
		Try
			reader = New PdfReader(pdfFile)
			inputOpened = True
			If reader.IsOpenedWithFullPermissions Then
				Return PdfPasswordProtection.Type.None
			Else
				Return PdfPasswordProtection.Type.Owner
			End If
		Catch ex As DocumentException
			ShowError(ex.Message)
			Return PdfPasswordProtection.Type.Unknown
		Catch ex As IOException
			If ex.Message = "Bad user password" Then
				ShowError(String.Format( _
					CultureInfo.CurrentCulture, _
					PdfKeeper.Strings.PdfContainsUserPassword, _
					pdfFile))
				Return PdfPasswordProtection.Type.User
			Else
				ShowError(ex.Message)
				Return PdfPasswordProtection.Type.Unknown
			End If
		Finally
			If inputOpened = True Then
				reader.Close
			End If
		End Try
	End Function
End Class
