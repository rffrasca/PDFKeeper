'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Public Class UpdateCheck
	Dim updateURL As String
	Dim appVersion As String
		
	''' <summary>
	''' This constructor will read the Update URL and application version
	''' from the registry.
	''' </summary>
	Public Sub New()
		Dim softwareRoot As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\" & _
									 "Windows\CurrentVersion\Uninstall\" & _
									 "{4F0E9E20-AB83-4AB1-9B05-D77BDED27ED4}_is1"
		Try
			updateURL = Registry.GetValue(softwareRoot, "URLUpdateInfo", _
										  Nothing)
			appVersion = Registry.GetValue(softwareRoot, "DisplayVersion", _
										   Nothing)
		Catch ex as IOException
		End Try
	End Sub
	
	''' <summary>
	''' This subroutine will check if an update is available.
	''' </summary>
	''' <returns>True or False</returns>
	Public Function IsUpdateAvailable As Boolean
		Dim result as Boolean = False
		Dim installerVersion As String
		Dim maxVersion As String = "0.0.0"
		Using oWebClient As New WebClient
			Dim pageContents As String = oWebClient.DownloadString(updateURL)
			Dim matches As MatchCollection = Regex.Matches(pageContents, _
										"//\S+[^-,;:?]\.exe")
			For Each expMatch As Match In matches
				For Each oGroup As group In expMatch.Groups
					installerVersion = oGroup.Value.Substring( _
									   oGroup.Value.LastIndexOf("/", _
								 	   StringComparison.Ordinal) + 11,5)
					If installerVersion > maxVersion Then
						maxVersion = installerVersion
					End If
				Next
			Next
		End Using
		If maxVersion > appVersion Then
			result = True
		End If
		Return result
	End Function
	
	''' <summary>
	''' This subroutine will open the update URL using the default web
	''' browser.
	''' </summary>
	Public Sub OpenUpdateUrl
		If Not updateURL = Nothing Then
			Process.Start(updateURL)
		Else
			Dim oMessageDialog As New MessageDialog("Unable to read " & _
												    "update URL from the " & _
								  					"registry.")
			oMessageDialog.DisplayError
		End If
	End Sub
End Class
