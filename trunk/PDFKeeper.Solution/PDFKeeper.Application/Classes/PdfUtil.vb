'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Upload, and Search
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

Public NotInheritable Class PdfUtil
	
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New()
	End Sub
		
	''' <summary>
	''' Create image file that contains the first page from "pdfFile" in the
	''' same folder as "pdfFile", and then register in the file cache.  Skip
	''' creating the image file if it's already registered in the file cache.
	''' If file encryption is supported by the operating system, encrypt the
	''' image file.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function CreatePreviewImage(ByVal pdfFile As String) _
															As Integer
		Dim imgFile As String = Path.ChangeExtension(pdfFile, "png")
		If FileCache.IsCached(imgFile) = False Then
			Dim ghostScript As New Process()
			Try
				ghostScript.StartInfo.FileName = "gswin32c.exe"
				ghostScript.StartInfo.Arguments = _
					"-o " & Chr(34) & imgFile & Chr(34) & _
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
					MessageBoxWrapper.ShowError( _
						ghostScriptErrorOutput.ReadToEnd)
					Return 1
				End If
				FileCache.Add(imgFile)
			Catch ex As System.ComponentModel.Win32Exception
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			End Try
		End If
		FileUtil.Encrypt(imgFile)
		Return 0
	End Function
		
	''' <summary>
	''' Extract the text contained within "pdfFile" and return to the caller.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>Text extracted from pdfFile</returns>
	Public Shared Function ExtractText(ByVal pdfFile As String) As String
		Using reader = New PdfReader(pdfFile)
			Dim text As New StringBuilder()
			For i As Integer = 1 To reader.NumberOfPages
				Dim strategy As ITextExtractionStrategy = _
					New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy()
				Dim currentPage As String = PdfTextExtractor.GetTextFromPage( _
					reader, i, strategy)
				Dim lines As String() = currentPage.Split(ControlChars.Lf)
				For Each line As String In lines
					text.AppendLine(line)
				Next
			Next
			Return text.ToString()
		End Using
	End Function
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="sourcePdfFile"></param>
	''' <param name="targetPdfFile"></param>
	''' <param name="fromPage"></param>
	''' <param name="toPage"></param>
	''' <param name="rotation"></param>
	''' <returns></returns>
	Public Shared Function PageRotation(ByVal sourcePdfFile As String, _
										ByVal targetPdfFile As String, _
										ByVal fromPage As Integer, _
										ByVal toPage As Integer, _
										ByVal rotation As Integer) As Integer
		Dim reader As PdfReader = Nothing
		Dim stamper As PdfStamper = Nothing
		Try
			reader = New PdfReader(sourcePdfFile)
			Dim pages As Integer = reader.NumberOfPages
			Dim page As PdfDictionary
			Dim rotate As PdfNumber
			For pageCounter As Integer = 1 To pages
				page = reader.GetPageN(pageCounter)
				rotate = page.GetAsNumber(PdfName.ROTATE)
				If rotate Is Nothing Then
					page.Put(PdfName.ROTATE, New PdfNumber(rotation))
				Else
					page.Put(PdfName.ROTATE, New _
						PdfNumber((rotate.IntValue() + rotation) Mod 360))
				End If
			Next
			stamper = New PdfStamper(reader, New FileStream(targetPdfFile, _
					FileMode.Create, FileAccess.Write, FileShare.None))
			Return 0
		Catch ex As DocumentException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Finally
			reader.Close
			stamper.Close
		End Try
	End Function
	
	''' <summary>
	''' Check security level for "pdfFile" and return the result code to the
	'''	caller.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>
	''' 0 = Can be opened with full permissions.
	''' 1 = OWNER password required.
	''' 2 = Contains a USER password or unable to check document.
	''' </returns>
	Public Shared Function SecurityLevelCheck(ByVal pdfFile As String) As Integer
		Dim reader As PdfReader = Nothing
		Dim inputOpened As Boolean = False
		Try
			reader = New PdfReader(pdfFile)
			inputOpened = True
			If reader.IsOpenedWithFullPermissions Then
				Return 0
			Else
				Return 1
			End If
		Catch ex As DocumentException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 3
		Catch ex As IOException
			If ex.Message = "Bad user password" Then
				MessageBoxWrapper.ShowError(String.Format( _
							CultureInfo.CurrentCulture, _
							PdfKeeper.Strings.PdfContainsUserPassword, pdfFile))
			Else
				MessageBoxWrapper.ShowError(ex.Message)
			End If
			Return 2
		Finally
			If inputOpened = True Then
				reader.Close
			End If
		End Try
	End Function
	
	''' <summary>
	''' Return the total number of pages contained in "pdfFile" to the caller.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>Total number of pages in "pdfFile"</returns>
	Public Shared Function TotalPages(ByVal pdfFile As String) As Integer
		Using reader = New PdfReader(pdfFile)
			Return reader.NumberOfPages
		End Using
	End Function
End Class
