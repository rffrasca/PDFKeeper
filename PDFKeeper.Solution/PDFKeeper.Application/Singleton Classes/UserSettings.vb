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

Public NotInheritable Class UserSettings
	Private Shared _instance As UserSettings = New UserSettings()
	Private ReadOnly settingsFile As String = Path.Combine( _
		ApplicationProfileFolders.RoamingParent, _
		"UserSettings.xml")
	Private ReadOnly formDefaultPositionTop As Integer = 10
	Private ReadOnly formDefaultPositionLeft As Integer = 25
	Private ReadOnly formDefaultHeight As Integer = 714
	Private ReadOnly formDefaultWidth As Integer = 714
	Private ReadOnly formDefaultWindowState As Integer = 0	' 0 = Normal
															' 2 = Maximized
	Private readonly defaultUpdateCheck As Integer = 1	' 1 = Enabled
														' 0 = Disabled
	Private ReadOnly defaultDoNotResetZoomLevel As Integer = 0	' 1 = Enabled
																' 0 = Disabled
	Private _lastUserName As String
	Private _lastDataSource As String
	Private _formPositionTop As String
	Private _formPositionLeft As String
	Private _formPositionHeight As String
	Private _formPositionWidth As String
	Private _formPositionWindowState As String
	Private _updateCheck As String
	Private _doNotResetZoomLevel As String
	Private _openFileLastFolder As String = _
		My.Computer.FileSystem.SpecialDirectories.MyDocuments
	Private _saveFileLastFolder As String = _
		My.Computer.FileSystem.SpecialDirectories.MyDocuments
		
	Public Shared ReadOnly Property Instance As UserSettings
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form default height.
	''' </summary>
	Public ReadOnly Property FormPositionDefaultHeight As Integer
		Get
			Return formDefaultHeight		
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form default width.
	''' </summary>
	Public ReadOnly Property FormPositionDefaultWidth As Integer
		Get
			Return formDefaultWidth		
		End Get
	End Property
		
	''' <summary>
	''' Gets or sets the Database Connection Form last database User Name.
	''' </summary>
	Public Property LastUserName As String
		Get
			_lastUserName = ReadSetting( _
				UserSettingsEnums.SectionName.DatabaseConnectionForm.ToString, _
				UserSettingsEnums.KeyName.LastUserName.ToString)
			Return _lastUserName
		End Get
		Set(ByVal value As String)
			_lastUserName = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Database Connection Form last Data Source.
	''' </summary>
	Public Property LastDataSource As String
		Get
			_lastDataSource = ReadSetting( _
				UserSettingsEnums.SectionName.DatabaseConnectionForm.ToString, _
				UserSettingsEnums.KeyName.LastDataSource.ToString)
			Return _lastDataSource
		End Get
		Set(ByVal value As String)
			_lastDataSource = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form top position.
	''' </summary>
	Public Property FormPositionTop As String
		Get
			_formPositionTop = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.FormPositionTop.ToString)
			If _formPositionTop Is Nothing Then
				_formPositionTop = CStr(formDefaultPositionTop)
			End If
			Return _formPositionTop
		End Get
		Set(ByVal value As String)
			_formPositionTop = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form left position.
	''' </summary>
	Public Property FormPositionLeft As String
		Get
			_formPositionLeft = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.FormPositionLeft.ToString)
			If _formPositionLeft Is Nothing Then
				_formPositionLeft = CStr(formDefaultPositionLeft)
			End If
			Return _formPositionLeft
		End Get
		Set(ByVal value As String)
			_formPositionLeft = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form height.
	''' </summary>
	Public Property FormPositionHeight As String
		Get
			_formPositionHeight = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.FormPositionHeight.ToString)
			If _formPositionHeight Is Nothing Then
				_formPositionHeight = CStr(formDefaultHeight)
			End If
			Return _formPositionHeight
		End Get
		Set(ByVal value As String)
			_formPositionHeight = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form width.
	''' </summary>
	Public Property FormPositionWidth As String
		Get
			_formPositionWidth = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.FormPositionWidth.ToString)
			If _formPositionWidth Is Nothing Then
				_formPositionWidth = CStr(formDefaultWidth)
			End If
			Return _formPositionWidth
		End Get
		Set(ByVal value As String)
			_formPositionWidth = value
			SaveAllSettings
		End Set
	End Property
		
	''' <summary>
	''' Gets or sets the Main Form window state.
	''' 0 = Normal, 2 = Maximized
	''' </summary>
	Public Property FormPositionWindowState As String
		Get
			_formPositionWindowState = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.FormPositionWindowState.ToString)
			If _formPositionWindowState Is Nothing Then
				_formPositionWindowState = CStr(formDefaultWindowState)
			End If
			Return _formPositionWindowState
		End Get
		Set(ByVal value As String)
			_formPositionWindowState = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form "Automatically Check for Newer Version"
	''' setting.
	''' 1 = Enabled, 0 = Disabled
	''' </summary>
	Public Property UpdateCheck As String
		Get
			_updateCheck = ReadSetting( _
				UserSettingsEnums.SectionName.MainForm.ToString, _
				UserSettingsEnums.KeyName.UpdateCheck.ToString)
			If _updateCheck Is Nothing Then
				_updateCheck = CStr(defaultUpdateCheck)
			End If
			Return _updateCheck
		End Get
		Set(ByVal value As String)
			_updateCheck = value
			SaveAllSettings
		End Set
	End Property
	
	''' <summary>
	''' Gets or sets the Main Form, Document Preview tab "Do not reset Zoom
	''' Level during this preview session" setting.
	''' 1 = Enabled, 0 = Disabled
	''' </summary>
	Public Property DoNotResetZoomLevel As String
		Get
			_doNotResetZoomLevel = ReadSetting( _
				UserSettingsEnums.SectionName.MainFormDocumentPreviewTab.ToString, _
				UserSettingsEnums.KeyName.DoNotResetZoomLevel.ToString)
			If _doNotResetZoomLevel Is Nothing Then
				_doNotResetZoomLevel = CStr(defaultDoNotResetZoomLevel)
			End If
			Return _doNotResetZoomLevel
		End Get
		Set(ByVal value As String)
			_doNotResetZoomLevel = value
			SaveAllSettings
		End Set
	End Property
		
	''' <summary>
	''' Gets or sets the Common Open file dialog last selected folder.
	''' </summary>
	Public Property OpenFileLastFolder As String
		Get
			_openFileLastFolder = ReadSetting( _
				UserSettingsEnums.SectionName.CommonDialogs.ToString, _
				UserSettingsEnums.KeyName.OpenFileLastFolder.ToString)
			If _openFileLastFolder Is Nothing Then
				_openFileLastFolder = _
					My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return _openFileLastFolder
		End Get
		Set(ByVal value As String)
			_openFileLastFolder = value
			SaveAllSettings
		End Set
	End Property
		
	''' <summary>
	''' Gets or sets the Common Save file dialog last selected folder.
	''' </summary>
	Public Property SaveFileLastFolder As String
		Get
			_saveFileLastFolder = ReadSetting( _
				UserSettingsEnums.SectionName.CommonDialogs.ToString, _
				UserSettingsEnums.KeyName.SaveFileLastFolder.ToString)
			If _saveFileLastFolder Is Nothing Then
				_saveFileLastFolder = _
					My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return _saveFileLastFolder
		End Get
		Set(ByVal value As String)
			_saveFileLastFolder = value
			SaveAllSettings
		End Set
	End Property
		
	''' <summary>
	''' Reads the specified User Setting from UserSettings.XML.
	''' </summary>
	''' <param name="sectionName">Section Name</param>
	''' <param name="keyName">Key Name</param>
	''' <returns>Value</returns>
	Private Function ReadSetting( _
		ByVal sectionName As String, _
		ByVal keyName As String) As String
				
		If System.IO.File.Exists(settingsFile) Then
			Try
				Dim xmlConfig As New XmlConfigSource(settingsFile)
				Return xmlConfig.Configs(sectionName).Get(keyName)
			Catch ex As System.NullReferenceException
				Return Nothing	
			Catch ex As UnauthorizedAccessException
				ShowError(ex.Message)
				Return Nothing
			Catch ex As IOException
				ShowError(ex.Message)
				Return Nothing
			End Try
		End If
		Return Nothing	
	End Function
	
	''' <summary>
	''' Saves all User Settings to UserSettings.XML.
	''' </summary>
	Private Sub SaveAllSettings
		Try
			Dim xmlConfig As New XmlConfigSource
			xmlConfig.AddConfig( _
				UserSettingsEnums.SectionName.DatabaseConnectionForm.ToString)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.DatabaseConnectionForm.ToString).Set( _
				UserSettingsEnums.KeyName.LastUserName.ToString, _
				_lastUserName)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.DatabaseConnectionForm.ToString).Set( _
				UserSettingsEnums.KeyName.LastDataSource.ToString, _
				_lastDataSource)
			xmlConfig.AddConfig( _
				UserSettingsEnums.SectionName.MainForm.ToString)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.FormPositionTop.ToString, _
				_formPositionTop)	
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.FormPositionLeft.ToString, _
				_formPositionLeft)	
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.FormPositionHeight.ToString, _
				_formPositionHeight)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.FormPositionWidth.ToString, _
				_formPositionWidth)			
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.FormPositionWindowState.ToString, _
				_formPositionWindowState)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainForm.ToString).Set( _
				UserSettingsEnums.KeyName.UpdateCheck.ToString, _
				_updateCheck)
			xmlConfig.AddConfig( _
				UserSettingsEnums.SectionName.MainFormDocumentPreviewTab.ToString)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.MainFormDocumentPreviewTab.ToString).Set( _
				UserSettingsEnums.KeyName.DoNotResetZoomLevel.ToString, _
				_doNotResetZoomLevel)
			xmlConfig.AddConfig( _
				UserSettingsEnums.SectionName.CommonDialogs.ToString)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.CommonDialogs.ToString).Set( _
				UserSettingsEnums.KeyName.OpenFileLastFolder.ToString, _
				_openFileLastFolder)
			xmlConfig.Configs( _
				UserSettingsEnums.SectionName.CommonDialogs.ToString).Set( _
				UserSettingsEnums.KeyName.SaveFileLastFolder.ToString, _
				_saveFileLastFolder)
			xmlConfig.Save(settingsFile)
		Catch ex As System.ArgumentNullException
		Catch ex As UnauthorizedAccessException
			ShowError(ex.Message)
		Catch ex As IOException
			ShowError(ex.Message)
		End Try
	End Sub
End Class
