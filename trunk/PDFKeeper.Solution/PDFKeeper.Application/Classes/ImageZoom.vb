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

Public NotInheritable Class ImageZoom
	Const zoomMinValue As Integer = 100
	Const zoomStepValue As Integer = 25
	Shared Private _ZoomLevel As Integer
	
	''' <summary>
	''' Source image to be zoomed.
	''' </summary>
	Shared Property SourceImage As System.Drawing.Image = Nothing
			
	''' <summary>
	''' SourceImage zoomed to ZoomLevel.
	''' </summary>
	Shared ReadOnly Property ZoomedImage As System.Drawing.Image
		Get
			Return ZoomImage
		End Get
	End Property
	
	''' <summary>
	''' Get zoom level.
	''' </summary>
	Shared ReadOnly Property GetZoomLevel As Integer
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
	''' Zoom SourceImage to ZoomLevel.
	''' </summary>
	''' <returns>Source image with ZoomLevel applied</returns>
	Private Shared Function ZoomImage As System.Drawing.Image
		Dim zoomedImg As New Bitmap(SourceImage, CInt( _
			Convert.ToInt32(SourceImage.Width * GetZoomLevel) / 100), ( _
			Convert.ToInt32(SourceImage.Height * GetZoomLevel / 100)))
		Dim resized As Graphics = Graphics.FromImage(zoomedImg)
		resized.InterpolationMode = _
			System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
		Return zoomedImg
	End Function
End Class
