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

Public NotInheritable Class ImageZoom
	Private Shared _instance As ImageZoom = New ImageZoom()
	Private Const ActualSize As Integer = 100	' percentage
	Private Const ZoomStepValue As Integer = 10	' percentage
	Private _zoomLevel As Integer

	Public Shared ReadOnly Property Instance As ImageZoom
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets or sets the source image.
	''' </summary>
	Public Property SourceImage As System.Drawing.Image = Nothing
	
	''' <summary>
	''' Gets SourceImage with ZoomLevel applied.
	''' </summary>
	Public ReadOnly Property ZoomedImage As System.Drawing.Image
		Get
			If IsNothing(SourceImage) Then
				Return Nothing
			End If
			Return ZoomSourceImage
		End Get
	End Property
		
	''' <summary>
	''' Gets zoom level.
	''' </summary>
	Public ReadOnly Property ZoomLevel As Integer
		Get
			If _zoomLevel = 0 Then
				ResetZoomLevel
			End If
			Return _zoomLevel
		End Get
	End Property
	
	''' <summary>
	''' Increases ZoomLevel by ZoomStepValue.
	''' </summary>
	Public Sub IncreaseZoomLevel
		_zoomLevel += ZoomStepValue
	End Sub
	
	''' <summary>
	''' Decreases ZoomLevel by ZoomStepValue but not below ActualSize.
	''' </summary>
	Public Sub DecreaseZoomLevel
		_zoomLevel -= ZoomStepValue
		If _zoomLevel < ActualSize Then
			ResetZoomLevel
		End If
	End Sub
	
	''' <summary>
	''' Resets ZoomLevel to ActualSize.
	''' </summary>
	Public Sub ResetZoomLevel
		_zoomLevel = ActualSize
	End Sub
	
	''' <summary>
	''' Zooms SourceImage to the value set in ZoomLevel and returns as an
	''' image, leaving SourceImage unchanged.
	''' </summary>
	''' <returns>SourceImage with ZoomLevel applied.</returns>
	Private Function ZoomSourceImage As System.Drawing.Image
		Dim zoomedImage As New Bitmap( _
			SourceImage, _
			CInt(Convert.ToInt32(SourceImage.Width * ZoomLevel) / 100), _
			(Convert.ToInt32(SourceImage.Height * ZoomLevel / 100)))
		Dim resized As Graphics = Graphics.FromImage(zoomedImage)
		resized.InterpolationMode = _
			System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
		Return zoomedImage
	End Function
End Class
