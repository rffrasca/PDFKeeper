'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
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

Public NotInheritable Class DocumentPreviewer
	Const zoomMinValue As Integer = 100
	Const zoomStepValue As Integer = 25
	Shared Private _ZoomLevel As Integer
	
	''' <summary>
	''' Get and set preview image that was created from PDF.
	''' </summary>
	Shared Property PreviewImage As System.Drawing.Image

	''' <summary>
	''' Get zoom level.
	''' </summary>
	Shared ReadOnly Property ZoomLevel As Integer
		Get
			If _ZoomLevel = 0 Then
				ResetZoomLevel
			End If
			Return _ZoomLevel
		End Get
	End Property
	
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
	Public Shared Function PdfToPreviewImage(ByVal pdfFile As String) As Integer
		Dim imgFile As String = Path.ChangeExtension(pdfFile, "png")
		If FileCache.IsCached(imgFile) = False Then
			Dim ghostScript As New Process()
			Try
				ghostScript.StartInfo.FileName = "gswin32c.exe"
				ghostScript.StartInfo.Arguments = _
					"-o " & Chr(34) & imgFile & Chr(34) & _
					" -sDEVICE=pngalpha " & _
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
		FileTask.Encrypt(imgFile)
		Return 0
	End Function
	
	''' <summary>
	''' Increase ZoomLevel by zoomStepValue.
	''' </summary>
	Public Shared Sub IncreaseZoomLevel
		_ZoomLevel += zoomStepValue
	End Sub
	
	''' <summary>
	''' Decrease ZoomLevel by zoomStepValue.
	''' </summary>
	Public Shared Sub DecreaseZoomLevel
		_ZoomLevel -= zoomStepValue
		If _ZoomLevel < zoomMinValue Then
			ResetZoomLevel
		End If
	End Sub
	
	''' <summary>
	''' Reset ZoomLevel to zoomMinValue.
	''' </summary>
	Public Shared Sub ResetZoomLevel
		_ZoomLevel = zoomMinValue
	End Sub
	
	''' <summary>
	''' Zoom PreviewImage to ZoomLevel.
	''' </summary>
	''' <returns>Preview image with ZoomLevel applied</returns>
	Public Shared Function ZoomPreviewImage As System.Drawing.Image
		Dim zoomImage As New Bitmap(PreviewImage, CInt( _
			Convert.ToInt32(PreviewImage.Width * ZoomLevel) / 100), ( _
			Convert.ToInt32(PreviewImage.Height * ZoomLevel / 100)))
		Dim resized As Graphics = Graphics.FromImage(zoomImage)
		resized.InterpolationMode = _
			System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
		Return zoomImage
	End Function
End Class
