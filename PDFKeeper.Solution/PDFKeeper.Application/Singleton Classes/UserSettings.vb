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

'Public NotInheritable Class UserSettings
'	Private Shared _instance As UserSettings = New UserSettings()
''	Private ReadOnly formDefaultPositionTop As Integer = 10
''	Private ReadOnly formDefaultPositionLeft As Integer = 25
''	Private ReadOnly formDefaultHeight As Integer = 714
''	Private ReadOnly formDefaultWidth As Integer = 714
''	Private ReadOnly formDefaultWindowState As Integer = 0	' 0 = Normal
''															' 2 = Maximized
''	Private ReadOnly defaultUpdateCheck As Integer = 1	' 1 = Enabled
''														' 0 = Disabled
''	Private ReadOnly defaultDoNotResetZoomLevel As Integer = 0	' 1 = Enabled
''																' 0 = Disabled
''	Private ReadOnly settingsFile As String = Path.Combine( _
''		ApplicationProfileFolders.RoamingParent, _
''		"UserSettings.xml")
'	Private _lastUserName As String = Nothing
'	Private _lastDataSource As String = Nothing
''	Private _formPositionTop As String = 
'		
'	Public Shared ReadOnly Property Instance As UserSettings
'		Get
'			Return _instance
'		End Get
'	End Property
'	
'	''' <summary>
'	''' Gets or Sets Database Connection Form last User Name.
'	''' </summary>
'	Public Property LastUserName As String
'		Get
'			If _lastUserName = Nothing Then
'				GetSettings
'			End If
'			Return _lastUserName
'		End Get
'		Set(ByVal value As String)
'			_lastUserName = value
'			SetSettings
'		End Set
'	End Property
'	
'	''' <summary>
'	''' Gets or Sets Database Connection Form last Data Source.
'	''' </summary>
'	Public Property LastDataSource As String
'		Get
'			If _lastDataSource = Nothing Then
'				GetSettings
'			End If
'			Return _lastDataSource
'		End Get
'		Set(ByVal value As String)
'			_lastDataSource = value
'			SetSettings
'		End Set
'	End Property
'		
'	''' <summary>
'	''' Gets or sets the Main Form top position.
'	''' </summary>
'	'Public Property FormPositionTop As String = CStr(formDefaultPositionTop)
''	Public Property FormPositionTop As String
''		Get
''			If _formPositionTop = Nothing Then
''				
''				If _formPositionTop = Nothing Then
''					
''				End If
''			End If
''		End Get
''		Set(ByVal value As String)
''			
''		End Set
''	End Property
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	''' <summary>
'	''' Gets all user settings from UserSettings.xml.  This is called
'	''' internally during a property get when the property value = nothing.
'	''' </summary>
'	Public Sub GetSettings
'		ImportDeprecatedProperties
'		ImportDeprecatedRegistryValues
'		If System.IO.File.Exists(settingsFile) Then
'			Try
'				Dim xmlConfig As New XmlConfigSource(settingsFile)
'				LastUserName = xmlConfig.Configs("DatabaseConnectionForm") _
'					.Get("LastUserName")					
'				LastDataSource = xmlConfig.Configs("DatabaseConnectionForm") _
'					.Get("LastDataSource")					
'				FormPositionTop = xmlConfig.Configs("MainForm") _
'					.Get("FormPositionTop")					
'				FormPositionLeft = xmlConfig.Configs("MainForm") _
'					.Get("FormPositionLeft")					
'				FormPositionHeight = xmlConfig.Configs("MainForm") _
'					.Get("FormPositionHeight")
'				FormPositionWidth = xmlConfig.Configs("MainForm") _
'					.Get("FormPositionWidth")
'				FormPositionWindowState = xmlConfig.Configs("MainForm") _
'					.Get("FormPositionWindowState")
'				UpdateCheck = xmlConfig.Configs("MainForm") _
'					.Get("UpdateCheck")
'				DoNotResetZoomLevel = _
'					xmlConfig.Configs("MainForm.DocumentPreview") _
'					.Get("DoNotResetZoomLevel")
'				OpenFileLastFolder = xmlConfig.Configs("CommonDialogs") _
'					.Get("OpenFileLastFolder")
'				SaveFileLastFolder = xmlConfig.Configs("CommonDialogs") _
'					.Get("SaveFileLastFolder")
'			Catch ex As System.NullReferenceException
'			Catch ex As UnauthorizedAccessException
'				Throw New UnauthorizedAccessException(ex.Message.ToString())
'			Catch ex As IOException
'				Throw New IOException(ex.Message.ToString())
'			End Try
'		End If
'	End Sub
'		
'	''' <summary>
'	''' Sets all user settings into UserSettings.xml.  This is called
'	''' internally when a property is set.
'	''' </summary>
'	Public Sub SetSettings
'		Try
'			Dim xmlConfig As New XmlConfigSource
'			xmlConfig.AddConfig("DatabaseConnectionForm")
'			xmlConfig.Configs("DatabaseConnectionForm").Set("LastUserName", _
'														     LastUserName)
'			xmlConfig.Configs("DatabaseConnectionForm").Set("LastDataSource", _
'														 	 LastDataSource)
'			xmlConfig.AddConfig("MainForm")
'			xmlConfig.Configs("MainForm").Set("FormPositionTop", _
'											   FormPositionTop)
'			xmlConfig.Configs("MainForm").Set("FormPositionLeft", _
'											   FormPositionLeft)
'			xmlConfig.Configs("MainForm").Set("FormPositionHeight", _
'											   FormPositionHeight)
'			xmlConfig.Configs("MainForm").Set("FormPositionWidth", _
'											   FormPositionWidth)
'			xmlConfig.Configs("MainForm").Set("FormPositionWindowState", _
'											   FormPositionWindowState)
'			xmlConfig.Configs("MainForm").Set("UpdateCheck", UpdateCheck)
'			xmlConfig.AddConfig("MainForm.DocumentPreview")		
'			xmlConfig.Configs( _
'				"MainForm.DocumentPreview").Set("DoNotResetZoomLevel", _
'				DoNotResetZoomLevel)
'			xmlConfig.AddConfig("CommonDialogs")
'			xmlConfig.Configs("CommonDialogs").Set("OpenFileLastFolder", _
'				OpenFileLastFolder)
'			xmlConfig.Configs("CommonDialogs").Set("SaveFileLastFolder", _
'				SaveFileLastFolder)
'			xmlConfig.Save(settingsFile)
'		Catch ex As System.ArgumentNullException
'		Catch ex As UnauthorizedAccessException
'			Throw New UnauthorizedAccessException(ex.Message.ToString())
'		Catch ex As IOException
'			Throw New IOException(ex.Message.ToString())
'		End Try
'	End Sub
'		
'	''' <summary>
'	''' Imports deprecated PDFKeeper 1.0.0 or 1.1.0 user settings.
'	''' </summary>
'	Public Sub ImportDeprecatedProperties
'		Dim legacyProperties As String = Path.Combine( _
'			WindowsProfileFolders.AppData, "pdfkeeper.properties")
'		If System.IO.File.Exists(legacyProperties) Then
'			Try
'				Using fileStream As New FileStream( _
'					legacyProperties, _
'					FileMode.Open)
'					
'					Using streamReader As New StreamReader( _
'						fileStream, _
'						Encoding.ASCII)
'						
'						LastUserName = streamReader.ReadLine.Substring(13)
'						LastDataSource = streamReader.ReadLine.Substring(15)
'					End Using
'					fileStream.Close
'				End Using
'				System.IO.File.Delete(legacyProperties)
'			Catch ex As IOException
'				Throw New IOException(ex.Message.ToString())
'			End Try
'		End If
'	End Sub
'	
'	''' <summary>
'	''' Imports deprecated PDFKeeper 2.0.0 through 2.4.0 user settings.
'	''' </summary>
'	Private Sub ImportDeprecatedRegistryValues
'		Dim appRoot As String = "HKEY_CURRENT_USER\Software\" & _
'								 Application.ProductName
'		Dim regKey As Microsoft.Win32.RegistryKey = _
'			  		  Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _
'			  		 "Software\" & Application.ProductName, True)
'		If IsNothing(regKey) = False Then
'			Try
'				LastUserName = CStr(Registry.GetValue(appRoot, _
'					"LastUserName", Nothing))
'				LastDataSource = CStr(Registry.GetValue(appRoot, _
'					"LastDataSource", Nothing))
'				FormPositionTop = CStr(Registry.GetValue(appRoot, _
'					"FormPositionTop", Nothing))
'				FormPositionLeft = CStr(Registry.GetValue(appRoot, _
'					"FormPositionLeft", Nothing))
'				FormPositionHeight = CStr(Registry.GetValue(appRoot, _
'					"FormPositionHeight", Nothing))
'				FormPositionWidth = CStr(Registry.GetValue(appRoot, _
'					"FormPositionWidth", Nothing))
'				FormPositionWindowState = CStr(Registry.GetValue(appRoot, _
'					"FormPositionWindowState", Nothing))
'				UpdateCheck = CStr(Registry.GetValue(appRoot, _
'					"UpdateCheck", Nothing))
'				registry.CurrentUser.DeleteSubKey("Software\" & _
'					Application.ProductName)
'			Catch ex as IOException
'				Throw New IOException(ex.Message.ToString())
'			End Try
'		End If
'	End Sub
'End Class
