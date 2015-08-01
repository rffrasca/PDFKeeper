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
''' PDFKeeper application user settings.
''' </summary>
Public NotInheritable Class UserSettings
	Shared settingsFile As String = Path.Combine(RootDataDir, _
		"UserSettings.xml")
	Shared _OpenFileLastFolder As String
	Shared _SaveFileLastFolder As String
	Shared _FormPositionHeight As String
	Shared _FormPositionWidth As String
	
	''' <summary>
	''' Database Connection form last database username.
	''' </summary>
	Shared Property LastUserName As String = Nothing
	
	''' <summary>
	''' Database Connection form last data source.
	''' </summary>
	Shared Property LastDataSource As String = Nothing
	
	''' <summary>
	''' Main form top position.
	''' </summary>
	Shared Property FormPositionTop As String = "10"
	
	''' <summary>
	''' Main form left position.
	''' </summary>
	Shared Property FormPositionLeft As String = "25"
	
	''' <summary>
	''' Main form default height.
	''' </summary>
	Shared ReadOnly Property FormPositionDefaultHeight As Integer
		Get
			Return 714 
		End Get
	End Property
	
	''' <summary>
	''' Main form height.
	''' </summary>
	Shared Property FormPositionHeight As String
		Get
			Return _FormPositionHeight
		End Get
		Set(ByVal Value As String)
			_FormPositionHeight = Value
		End Set
	End Property
	
	''' <summary>
	''' Main form default width.
	''' </summary>
	Shared ReadOnly Property FormPositionDefaultWidth As Integer
		Get
			Return 714 
		End Get
	End Property
	
	''' <summary>
	''' Main form width.
	''' </summary>
	Shared Property FormPositionWidth As String
		Get
			Return _FormPositionWidth
		End Get
		Set(ByVal Value As String)
			_FormPositionWidth = Value
		End Set
	End Property
	
	''' <summary>
	''' Main form window state.
	''' 0 = Normal, 2 = Maximized
	''' </summary>
	Shared Property FormPositionWindowState As String = "0"
	
	''' <summary>
	''' Main form "Automatically Check for Newer Version" setting.
	''' 1 = Enabled, 0 = Disabled
	''' </summary>	
	Shared Property UpdateCheck As String = "1"
	
	''' <summary>
	''' Common Open file dialog last folder.
	''' </summary>
	Shared Property OpenFileLastFolder As String
		Get
			If _OpenFileLastFolder = Nothing Then
				_OpenFileLastFolder = _
					My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return _OpenFileLastFolder
		End Get
		Set(ByVal Value As String)
			_OpenFileLastFolder = Value
		End Set
	End Property
	
	''' <summary>
	''' Common Save file dialog last folder.
	''' </summary>
	Shared Property SaveFileLastFolder As String
		Get
			If _SaveFileLastFolder = Nothing Then
				_SaveFileLastFolder = _
					My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return _SaveFileLastFolder
		End Get
		Set(ByVal Value As String)
			_SaveFileLastFolder = Value
		End Set
	End Property
	
	''' <summary>
	''' Private constructor required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' Read user settings from UserSettings.xml.  This needs to be performed
	''' during application startup.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function Read As Integer
		If ImportDeprecatedProperties = 1 Then
			Return 1
		End If
		If ImportDeprecatedRegistryValues = 1 Then
			Return 1
		End If
		If System.IO.File.Exists(settingsFile) Then
			Try
				Dim xmlConfig As New XmlConfigSource(settingsFile)
				LastUserName = xmlConfig.Configs("DatabaseConnectionForm") _
					.Get("LastUserName")					
				LastDataSource = xmlConfig.Configs("DatabaseConnectionForm") _
					.Get("LastDataSource")					
				FormPositionTop = xmlConfig.Configs("MainForm") _
					.Get("FormPositionTop")					
				FormPositionLeft = xmlConfig.Configs("MainForm") _
					.Get("FormPositionLeft")					
				FormPositionHeight = xmlConfig.Configs("MainForm") _
					.Get("FormPositionHeight")
				FormPositionWidth = xmlConfig.Configs("MainForm") _
					.Get("FormPositionWidth")
				FormPositionWindowState = xmlConfig.Configs("MainForm") _
					.Get("FormPositionWindowState")
				UpdateCheck = xmlConfig.Configs("MainForm") _
					.Get("UpdateCheck")
				OpenFileLastFolder = xmlConfig.Configs("CommonDialogs") _
					.Get("OpenFileLastFolder")
				SaveFileLastFolder = xmlConfig.Configs("CommonDialogs") _
					.Get("SaveFileLastFolder")
			Catch ex As System.NullReferenceException
			Catch ex As UnauthorizedAccessException
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			Catch ex As IOException
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			End Try
		End If
		Return 0
	End Function

	''' <summary>
	''' Save user settings to UserSettings.xml.  This needs to be performed
	''' while the Main form is closing.
	''' </summary>
	Public Shared Sub Save
		Try
			Dim xmlConfig As New XmlConfigSource
			xmlConfig.AddConfig("DatabaseConnectionForm")
			xmlConfig.Configs("DatabaseConnectionForm").Set("LastUserName", _
														     LastUserName)
			xmlConfig.Configs("DatabaseConnectionForm").Set("LastDataSource", _
														 	 LastDataSource)
			xmlConfig.AddConfig("MainForm")
			xmlConfig.Configs("MainForm").Set("FormPositionTop", _
											   FormPositionTop)
			xmlConfig.Configs("MainForm").Set("FormPositionLeft", _
											   FormPositionLeft)
			xmlConfig.Configs("MainForm").Set("FormPositionHeight", _
											   FormPositionHeight)
			xmlConfig.Configs("MainForm").Set("FormPositionWidth", _
											   FormPositionWidth)
			xmlConfig.Configs("MainForm").Set("FormPositionWindowState", _
											   FormPositionWindowState)
			xmlConfig.Configs("MainForm").Set("UpdateCheck", UpdateCheck)
			xmlConfig.AddConfig("CommonDialogs")
			If Not OpenFileLastFolder = Nothing Then
				xmlConfig.Configs("CommonDialogs").Set("OpenFileLastFolder", _
														OpenFileLastFolder)				
			End If
			If Not SaveFileLastFolder = Nothing Then
				xmlConfig.Configs("CommonDialogs").Set("SaveFileLastFolder", _
														SaveFileLastFolder)
			End If
			xmlConfig.Save(settingsFile)
		Catch ex As UnauthorizedAccessException
			MessageBoxWrapper.ShowError(ex.Message)
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
		End Try
	End Sub
	
	''' <summary>
	''' Import deprecated PDFKeeper 1.0.0 or 1.1.0 user settings.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Shared Function ImportDeprecatedProperties As Integer
		Dim legacyProperties As String = Path.Combine(AppDataDir, "pdfkeeper.properties")
		If System.IO.File.Exists(legacyProperties) Then
			Try
				Using oFileStream As New FileStream(legacyProperties, _
									 	 FileMode.Open)
					Using oStreamReader As New StreamReader(oFileStream, _
															Encoding.ASCII)
						LastUserName = oStreamReader.ReadLine.Substring(13)
						LastDataSource = oStreamReader.ReadLine.Substring(15)
					End Using
					oFileStream.Close
				End Using
				System.IO.File.Delete(legacyProperties)
			Catch ex As IOException
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			End Try
		End If
		Return 0
	End Function
	
	''' <summary>
	''' Import deprecated PDFKeeper 2.0.0 through 2.4.0 user settings.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Shared Function ImportDeprecatedRegistryValues As Integer
		Dim appRoot As String = "HKEY_CURRENT_USER\Software\" & _
								 Application.ProductName
		Dim regKey As Microsoft.Win32.RegistryKey = _
			  		  Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _
			  		 "Software\" & Application.ProductName, True)
		If Not regKey Is Nothing Then
			Try
				LastUserName = CStr(Registry.GetValue(appRoot, _
					"LastUserName", Nothing))
				LastDataSource = CStr(Registry.GetValue(appRoot, _
					"LastDataSource", Nothing))
				FormPositionTop = CStr(Registry.GetValue(appRoot, _
					"FormPositionTop", Nothing))
				FormPositionLeft = CStr(Registry.GetValue(appRoot, _
					"FormPositionLeft", Nothing))
				FormPositionHeight = CStr(Registry.GetValue(appRoot, _
					"FormPositionHeight", Nothing))
				FormPositionWidth = CStr(Registry.GetValue(appRoot, _
					"FormPositionWidth", Nothing))
				FormPositionWindowState = CStr(Registry.GetValue(appRoot, _
					"FormPositionWindowState", Nothing))
				UpdateCheck = CStr(Registry.GetValue(appRoot, _
					"UpdateCheck", Nothing))
				registry.CurrentUser.DeleteSubKey("Software\" & _
					Application.ProductName)
			Catch ex as IOException
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			End Try
		End If
		Return 0
	End Function
End Class
