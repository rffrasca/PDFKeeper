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

Public NotInheritable Class ImageZoom
	
	''' <summary>
	''' Original, preview image from picture box.
	''' </summary>
	Shared Property OriginalImage As System.Drawing.Image = Nothing
	
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New
	End Sub
	
	''' <summary>
	''' Apply ZoomLevel.Level to OriginalImage.
	''' </summary>
	''' <returns>Image with zoom level applied</returns>
	Public Shared Function Zoom As System.Drawing.Image
		Dim zoomImage As New Bitmap(OriginalImage, CInt( _
			Convert.ToInt32(OriginalImage.Width * ZoomLevel.Level) / 100), ( _
			Convert.ToInt32(OriginalImage.Height * ZoomLevel.Level / 100)))
		Dim resized As Graphics = Graphics.FromImage(zoomImage)
		resized.InterpolationMode = _
			System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
		Return zoomImage
	End Function
End Class
