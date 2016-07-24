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

Public Module PdfUtil
	''' <summary>
	''' Creates an image file containing the first page from "pdfFile" and
	''' returns it as an image.  Skip the Writing of the first page from
	''' "pdfFile" to an image file if the image file already exists and is
	''' contained in the file cache and the hash values match.  When the image
	''' file containing the first page from "pdfFile" is created, its name and
	''' hash value will be added to the file cache.  Lastly, Encrypt the image
	''' file, only if file system encryption is supported by the operating
	''' system.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns></returns>
	Public Function PdfFirstPageToImage( _
		ByVal pdfFile As String) As System.Drawing.Image
		
		Dim imageFile As String = _
			Path.ChangeExtension(pdfFile, "png")
		If FileCache.Instance.ContainsItemAndHashValuesMatch( _
			imageFile) = False Then
			
			Dim ghostScript As New Process()
			Try
				ghostScript.StartInfo.FileName = "gswin32c.exe"
				ghostScript.StartInfo.Arguments = _
					"-o " & Chr(34) & imageFile & Chr(34) & _
					" -sDEVICE=pngalpha " & _
					" -r120x120 " & _
					"-dLastPage=1 " & Chr(34) & pdfFile & Chr(34)
				ghostScript.StartInfo.UseShellExecute = False
				ghostScript.StartInfo.CreateNoWindow = True
				ghostScript.StartInfo.RedirectStandardError = True
				ghostScript.Start
				ghostScript.WaitForExit
				If ghostScript.ExitCode <> 0 Then
					Dim ghostScriptErrorOutput As StreamReader = _
						ghostScript.StandardError
					ShowError(ghostScriptErrorOutput.ReadToEnd)
					Return Nothing
				End If
				FileCache.Instance.AddFileToCache(imageFile)
			Catch ex As System.ComponentModel.Win32Exception
				ShowError(ex.Message)
				Return Nothing
			End Try
		End If
		EncryptFile(imageFile)
		Using file As New FileStream(imageFile, FileMode.Open, FileAccess.Read)
			Return System.Drawing.Image.FromStream(file)
		End Using
	End Function
	
	''' <summary>
	''' Returns the number of pages in "pdfFile".
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns></returns>
	Public Function PdfPageCounter(ByVal pdfFile As String) As Integer
		Using reader = New PdfReader(pdfFile)
			Return reader.NumberOfPages
		End Using
	End Function
	
	''' <summary>
	''' Gets the password type set in "pdfFile".
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>Password type enumerator.</returns>
	Public Function GetPdfPasswordType( _
		ByVal pdfFile As String) As Enums.PdfPasswordType

		Try
			Using reader As New PdfReader(pdfFile)
				If reader.IsOpenedWithFullPermissions Then
					Return Enums.PdfPasswordType.None
				Else
					Return Enums.PdfPasswordType.Owner
				End If
			End Using
		Catch ex As DocumentException
			ShowError(ex.Message)
			Return Enums.PdfPasswordType.Unknown
		Catch ex As BadPasswordException
			ShowError( _
				String.Format( _
				CultureInfo.CurrentCulture, _
				PdfKeeper.Strings.PdfContainsUserPassword, _
				pdfFile))
			Return Enums.PdfPasswordType.User
		Catch ex As IOException
			ShowError(ex.Message)
			Return Enums.PdfPasswordType.Unknown
		End Try
	End Function
	
	''' <summary>
	''' Returns a string containing the text extracted from "pdfFile".
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns></returns>
	Public Function PdfTextToString( _
		ByVal pdfFile As String) As String
		
		Using reader = New PdfReader(pdfFile)
			Dim text As New StringBuilder()
			For i As Integer = 1 To reader.NumberOfPages
				Dim strategy As ITextExtractionStrategy = New _
					iTextSharp.text.pdf.parser.LocationTextExtractionStrategy()
				Dim currentPage As String = _
					iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage( _
					reader, _
					i, _
					strategy)
				Dim lines As String() = currentPage.Split(ControlChars.Lf)
				For Each line As String In lines
					text.AppendLine(line)
				Next
			Next
			Return text.ToString()
		End Using
	End Function
End Module
