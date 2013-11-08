'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will check if an update is available.
	''' </summary>
	''' <returns>True or False</returns>
	Public Shared Function UpdateAvailable As Boolean
		Dim installerVersion As String
		Dim maxVersion As String = "0.0.0"
		Using oWebClient As New WebClient
			Dim pageContents As String = oWebClient.DownloadString( _
				ConfigurationManager.AppSettings("ProjectSiteUrl"))
			Dim matches As MatchCollection = Regex.Matches(pageContents, _
				"\bPDFKeeper\b\s\d.\d.\d")
			For Each expMatch As Match In matches
				installerVersion = expMatch.ToString.Substring(10)
				If installerVersion > maxVersion Then
					maxVersion = installerVersion
				End If
			Next
		End Using
		If maxVersion > MainForm.appVersion Then
			Return True
		Else
			Return False
		End If
	End Function
End Class
