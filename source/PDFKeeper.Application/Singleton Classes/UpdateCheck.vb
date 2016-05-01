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

Public NotInheritable Class UpdateCheck
	Private Shared _instance As UpdateCheck = New UpdateCheck()
	Private _isUpdateAvailable As Boolean = Nothing
	
	Public Shared ReadOnly Property Instance As UpdateCheck
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Returns True or False if an update is available for the application.
	''' </summary>
	Public ReadOnly Property IsUpdateAvailable As Boolean
		Get
			Return _isUpdateAvailable
		End Get
	End Property
	
	''' <summary>
	''' Sets the IsUpdateAvailable property to True or False.  This method must
	''' be called when the Main Form opens.
	''' </summary>
	Public Sub SetIsUpdateAvailable
		Dim installerVersion As String
		Dim maxVersion As String = "0.0.0"
		Using oWebClient As New WebClient
			Dim pageContents As String = oWebClient.DownloadString( _
				ConfigurationManager.AppSettings("UpdateCheckUrl"))
			Dim matches As MatchCollection = Regex.Matches(pageContents, _
				"\b(" & ProductDetails.Name & ")\b\s\d.\d.\d")
			For Each expMatch As Match In matches
				installerVersion = expMatch.ToString.Substring(10)
				If installerVersion > maxVersion Then
					maxVersion = installerVersion
				End If
			Next
		End Using
		If maxVersion > ProductDetails.Version Then
			_isUpdateAvailable = True
		Else
			_isUpdateAvailable = False
		End If
	End Sub
End Class
