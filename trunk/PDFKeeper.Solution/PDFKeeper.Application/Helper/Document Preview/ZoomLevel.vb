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

Public NotInheritable Class ZoomLevel
	Const minimumValue As Integer = 100
	Const stepValue As Integer = 25
	Shared Private _Level As Integer
	
	''' <summary>
	''' Get zoom level.
	''' </summary>
	Shared ReadOnly Property Level As Integer
		Get
			If _Level = 0 Then
				_Level = minimumValue
			End If
			Return _Level
		End Get
	End Property
		
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New
	End Sub
	
	''' <summary>
	''' Increase zoom level by step value.
	''' </summary>
	Public Shared Sub Increase
		_Level += stepValue
	End Sub
	
	''' <summary>
	''' Decrease zoom level by step value.
	''' </summary>
	Public Shared Sub Decrease
		_Level -= stepValue
		If _Level < minimumValue Then
			Reset
		End If
	End Sub
	
	''' <summary>
	''' Set zoom level to minimum value.
	''' </summary>
	Public Shared Sub Reset
		_Level = minimumValue
	End Sub
End Class
