'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2015 Robert F. Frasca
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

Public Class PdfPreviewCreator
	Private m_pdfPathName As String
	
	''' <summary>
	''' Preview image created from PdfPathName.
	''' </summary>
	ReadOnly Property PreviewImage As System.Drawing.Image
		Get
			Return CreatePreviewImage
		End Get
	End Property
		
	''' <summary>
	''' PdfPreviewCreator constructor.
	''' </summary>
	''' <param name="pdfPathName"></param>
	Public Sub New(ByVal pdfPathName As String)
		m_pdfPathName = pdfPathName
	End Sub
	
	''' <summary>
	''' Create an image containing the first page from the PdfPathName.
	''' </summary>
	''' <returns>Image of first page from PdfPathName</returns>
	Private Function CreatePreviewImage As System.Drawing.Image
		Dim imageFile As String = _
			Path.ChangeExtension(m_pdfPathName, "png")
		If FileCache.IsCached(imageFile) = False Then
			Dim ghostScript As New Process()
			Try
				ghostScript.StartInfo.FileName = "gswin32c.exe"
				ghostScript.StartInfo.Arguments = _
					"-o " & Chr(34) & imageFile & Chr(34) & _
					" -sDEVICE=pngalpha " & _
					" -r120x120 " & _
					"-dLastPage=1 " & Chr(34) & m_pdfPathName & Chr(34)
				ghostScript.StartInfo.UseShellExecute = False
				ghostScript.StartInfo.CreateNoWindow = True
				ghostScript.StartInfo.RedirectStandardError = True
				ghostScript.Start
				ghostScript.WaitForExit
				If ghostScript.ExitCode <> 0 Then
					Dim ghostScriptErrorOutput As StreamReader = _
						ghostScript.StandardError
					MessageBoxWrapper.ShowError( _
						ghostScriptErrorOutput.ReadToEnd)
					Return Nothing
				End If
				FileCache.Add(imageFile)
			Catch ex As System.ComponentModel.Win32Exception
				MessageBoxWrapper.ShowError(ex.Message)
				Return Nothing
			End Try
		End If
		FileUtil.Encrypt(imageFile)
		Using file As New FileStream(imageFile, FileMode.Open, FileAccess.Read)
			Return System.Drawing.Image.FromStream(file)
		End Using
	End Function
End Class
