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

''' <summary>
''' Document Preview image resizer. 
''' </summary>
Public NotInheritable Class ImageResizer
	Shared _Image As System.Drawing.Image
	Shared _ResizedImage As System.Drawing.Image
		
	''' <summary>
	''' Source image. 
	''' </summary>
	Shared Property Image As System.Drawing.Image
		Get
			Return _Image
		End Get
		Set(ByVal value As System.Drawing.Image)
			_Image = value
		End Set
	End Property
	
	''' <summary>
	''' Resized image with zoom value applied.
	''' </summary>
	Shared Property ResizedImage As System.Drawing.Image
		Get
			Return _ResizedImage
		End Get
		Set(ByVal value As System.Drawing.Image)
			_ResizedImage = value
		End Set
	End Property
	
	''' <summary>
	''' Zoom value, default is 100%.
	''' </summary>
	Shared Property Zoom As Int32 = 100
	
	''' <summary>
	''' Private constructor required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' Increase zoom by 25% and resize image.
	''' </summary>
	Public Shared Sub IncreaseZoom
		Zoom += 25
		Resize
	End Sub
	
	''' <summary>
	''' Decrease zoom by 25% and resize image.
	''' </summary>
	Public Shared Sub DecreaseZoom
		Zoom -= 25
		Resize
	End Sub
	
	''' <summary>
	''' Reset zoom to 100% and resize image.
	''' </summary>
	Public Shared Sub ResetZoom
		Zoom = 100
		Resize
	End Sub
	
	''' <summary>
	''' Resize image with zoom value applied.
	''' </summary>
	Private Shared Sub Resize
		Dim zoomImage As New Bitmap(Image, _
			CInt(Convert.ToInt32(Image.Width * Zoom) / 100), _
			(Convert.ToInt32(Image.Height * Zoom / 100)))
		Dim resized As Graphics = Graphics.FromImage(zoomImage)
		resized.InterpolationMode = _
			System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
		ResizedImage = zoomImage
	End Sub
End Class
