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

Public NotInheritable Class WindowsProfileFolders
	Private Sub New()
		' Class cannot be instantiated as it only contains shared members.
		' Required for FxCop compliance (CA1053).
	End Sub
	
	''' <summary>
	''' Gets the absoluate pathname to the Links folder in the user's profile.
	''' </summary>
	Public Shared ReadOnly Property Links As String
		Get
			Return Path.Combine( _
				Environment.GetFolderPath( _
				Environment.SpecialFolder.UserProfile), _
				"Links")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the AppData folder in the user's profile.
	''' </summary>
	Public Shared ReadOnly Property AppData As String
		Get
			Return Environment.GetFolderPath( _
				Environment.SpecialFolder.ApplicationData)
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the LocalAppData folder in the user's
	''' profile.
	''' </summary>
	Public Shared ReadOnly Property LocalAppData As String
		Get
			Return Environment.GetFolderPath( _
				Environment.SpecialFolder.LocalApplicationData)
		End Get
	End Property
End Class
