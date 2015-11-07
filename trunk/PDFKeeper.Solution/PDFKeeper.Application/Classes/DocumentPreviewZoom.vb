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

Public NotInheritable Class DocumentPreviewZoom
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
