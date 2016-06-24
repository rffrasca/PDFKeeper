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

Public NotInheritable Class ApplicationUpdate
	Private Shared _instance As ApplicationUpdate = New ApplicationUpdate()
	Private _isUpdateAvailable As Nullable(Of Boolean) = Nothing
	
	Public Shared ReadOnly Property Instance As ApplicationUpdate
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Is an update available for the application?
	''' 
	''' The CheckForUpdate method must be called for this property to be set;
	''' otheriwse, null will be returned.
	''' </summary>
	Public ReadOnly Property IsUpdateAvailable As Nullable(Of Boolean)
		Get
			Return _isUpdateAvailable
		End Get
	End Property
	
	''' <summary>
	''' Checks for an update and sets the IsUpdateAvailable property to True or
	''' False.  This method is intended to be executed in its own thread when
	''' the Main Form opens.
	''' </summary>
	Public Sub CheckForUpdate
		Dim installerVersion As String
		Dim maxVersion As String = "0.0.0"
		Using oWebClient As New WebClient
			Dim pageContents As String = oWebClient.DownloadString( _
				ConfigurationManager.AppSettings("UpdateCheckUrl"))
			Dim matches As MatchCollection = Regex.Matches(pageContents, _
				"\b(" & Application.ProductName & ")\b\s\d.\d.\d")
			For Each expMatch As Match In matches
				installerVersion = expMatch.ToString.Substring(10)
				If installerVersion > maxVersion Then
					maxVersion = installerVersion
				End If
			Next
		End Using
		If maxVersion > Application.ProductVersion Then
			_isUpdateAvailable = True
		Else
			_isUpdateAvailable = False
		End If
	End Sub
End Class
