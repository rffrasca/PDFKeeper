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
	''' Creates an image file containing the first page from the specified
	''' PDF and returns it as an image.  Writing the first page from the PDF to
	''' an image file will be skipped if the image file exists in the File Hash
	''' Array and the hashes match.  When the image file containing the first
	''' page from the PDF is created, it will be added to the File Hash Array.
	''' Lastly, Encrypt the image file, only if file system encryption is
	''' supported by the operating system. 
	''' </summary>
	''' <param name="pdfFile">PDF file pathname.</param>
	''' <returns>Image of first page from PDF or nothing on error.</returns>
	Public Function PdfFirstPageToImage( _
		ByVal pdfFile As String) As System.Drawing.Image
		
		Dim imageFile As String = _
			Path.ChangeExtension(pdfFile, "png")
		If FileHashArray.Instance.ContainsItemAndHashValuesMatch( _
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
					MessageBoxError(ghostScriptErrorOutput.ReadToEnd)
					Return Nothing
				End If
				FileHashArray.Instance.Add(imageFile)
			Catch ex As System.ComponentModel.Win32Exception
				MessageBoxError(ex.Message)
				Return Nothing
			End Try
		End If
		EncryptFile(imageFile)
		Using file As New FileStream(imageFile, FileMode.Open, FileAccess.Read)
			Return System.Drawing.Image.FromStream(file)
		End Using
	End Function
	
	''' <summary>
	''' Returns the number of pages in the specified PDF.
	''' </summary>
	''' <param name="pdfFile">PDF file pathname.</param>
	''' <returns>Page count.</returns>
	Public Function PdfPageCounter(ByVal pdfFile As String) As Integer
		Using reader = New PdfReader(pdfFile)
			Return reader.NumberOfPages
		End Using
	End Function
		
	''' <summary>
	''' Returns a string containing text from the specified PDF.
	''' </summary>
	''' <param name="pdfFile">PDF file pathname.</param>
	''' <returns>String containing text from PDF.</returns>
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
