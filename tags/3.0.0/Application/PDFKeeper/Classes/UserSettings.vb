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

Public Class UserSettings
	Dim xmlFile As String = Path.Combine(RootDataDir, "UserSettings.xml")
	Shared m_OpenFileLastFolder As String
	Shared m_SaveFileLastFolder As String
	Shared m_LastUserName As String
	Shared m_LastDataSource As String
	Shared m_FormPositionTop As String
	Shared m_FormPositionLeft As String
	Shared m_FormPositionHeight As String
	Shared m_FormPositionWidth As String
	Shared m_FormPositionWindowState As String
	Shared m_UpdateCheck As String
						
	#Region "Properties"
	
	Shared Property OpenFileLastFolder() As String
		Get
			If m_OpenFileLastFolder = Nothing Then
				m_OpenFileLastFolder = _
						 My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return m_OpenFileLastFolder
		End Get
		Set(ByVal Value As String)
			m_OpenFileLastFolder = Value
		End Set
	End Property
	
	Shared Property SaveFileLastFolder() As String
		Get
			If m_SaveFileLastFolder = Nothing Then
				m_SaveFileLastFolder = _
						 My.Computer.FileSystem.SpecialDirectories.MyDocuments
			End If
			Return m_SaveFileLastFolder
		End Get
		Set(ByVal Value As String)
			m_SaveFileLastFolder = Value
		End Set
	End Property
	
	Shared Property LastUserName() As String
		Get
			Return m_LastUserName
		End Get
		Set(ByVal Value As String)
			m_LastUserName = Value
		End Set
	End Property
	
	Shared Property LastDataSource() As String
		Get
			Return m_LastDataSource
		End Get
		Set(ByVal Value As String)
			m_LastDataSource = Value
		End Set
	End Property

	Shared Property FormPositionTop() As String
		Get
			If m_FormPositionTop = Nothing Then
				m_FormPositionTop = CStr(10)
			End If
			Return m_FormPositionTop
		End Get
		Set(ByVal Value As String)
			m_FormPositionTop = Value
		End Set
	End Property
	
	Shared Property FormPositionLeft() As String
		Get
			If m_FormPositionLeft = Nothing Then
				m_FormPositionLeft = CStr(25)
			End If
			Return m_FormPositionLeft
		End Get
		Set(ByVal Value As String)
			m_FormPositionLeft = Value
		End Set
	End Property
	
	Shared Property FormPositionHeight() As String
		Get
			Return m_FormPositionHeight
		End Get
		Set(ByVal Value As String)
			m_FormPositionHeight = Value
		End Set
	End Property
	
	Shared Property FormPositionWidth() As String
		Get
			Return m_FormPositionWidth
		End Get
		Set(ByVal Value As String)
			m_FormPositionWidth = Value
		End Set
	End Property
	
	Shared Property FormPositionWindowState() As String
		Get
			If m_FormPositionWindowState = Nothing Then
				m_FormPositionWindowState = CStr(0)
			End If
			Return m_FormPositionWindowState
		End Get
		Set(ByVal Value As String)
			m_FormPositionWindowState = Value
		End Set
	End Property
	
	Shared Property UpdateCheck() As String
		Get
			If m_UpdateCheck = Nothing Then
				m_UpdateCheck = CStr(1)
			End If
			Return m_UpdateCheck
		End Get
		Set(ByVal Value As String)
			m_UpdateCheck = Value
		End Set
	End Property
	
	#End Region
	
	''' <summary>
	''' This function will read application user settings from UserSettings.XML
	''' into class properties.  It should get called when the application is
	''' started.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Read() As Integer
		If ImportLegacyProperties() = 1 Then
			Return 1
		End If
		If ImportLegacyRegistryValues() = 1 Then
			Return 1
		End If
		If System.IO.File.Exists(xmlFile) Then
			Try
				Dim xmlConfig As New XmlConfigSource(xmlFile)
				m_LastUserName = xmlConfig.Configs("DatabaseConnectionForm") _
									  	  	.Get("LastUserName")
				m_LastDataSource = xmlConfig.Configs("DatabaseConnectionForm") _
									  		.Get("LastDataSource")
				m_FormPositionTop = xmlConfig.Configs("MainForm") _
									  		.Get("FormPositionTop")
				m_FormPositionLeft = xmlConfig.Configs("MainForm") _
									  		.Get("FormPositionLeft")
				m_FormPositionHeight = xmlConfig.Configs("MainForm") _
									  		.Get("FormPositionHeight")
				m_FormPositionWidth = xmlConfig.Configs("MainForm") _
									  		.Get("FormPositionWidth")
				m_FormPositionWindowState = xmlConfig.Configs("MainForm") _
									  		.Get("FormPositionWindowState")
				m_UpdateCheck = xmlConfig.Configs("MainForm") _
											.Get("UpdateCheck")
				m_OpenFileLastFolder = xmlConfig.Configs("CommonDialogs") _
								   .Get("OpenFileLastFolder")
				m_SaveFileLastFolder = xmlConfig.Configs("CommonDialogs") _
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
	''' This subroutine will write the class properties to UserSettings.XML.
	''' It should get called when the application is closing.
	''' </summary>
	Public Sub Write
		Try
			Dim xmlConfig As New XmlConfigSource
			xmlConfig.AddConfig("DatabaseConnectionForm")
			xmlConfig.Configs("DatabaseConnectionForm").Set("LastUserName", _
														     m_LastUserName)
			xmlConfig.Configs("DatabaseConnectionForm").Set("LastDataSource", _
														 	 m_LastDataSource)
			xmlConfig.AddConfig("MainForm")
			xmlConfig.Configs("MainForm").Set("FormPositionTop", _
											   m_FormPositionTop)
			xmlConfig.Configs("MainForm").Set("FormPositionLeft", _
											   m_FormPositionLeft)
			xmlConfig.Configs("MainForm").Set("FormPositionHeight", _
											   m_FormPositionHeight)
			xmlConfig.Configs("MainForm").Set("FormPositionWidth", _
											   m_FormPositionWidth)
			xmlConfig.Configs("MainForm").Set("FormPositionWindowState", _
											   m_FormPositionWindowState)
			xmlConfig.Configs("MainForm").Set("UpdateCheck", m_UpdateCheck)
			xmlConfig.AddConfig("CommonDialogs")
			If Not m_OpenFileLastFolder = Nothing Then
				xmlConfig.Configs("CommonDialogs").Set("OpenFileLastFolder", _
														m_OpenFileLastFolder)				
			End If
			If Not m_SaveFileLastFolder = Nothing Then
				xmlConfig.Configs("CommonDialogs").Set("SaveFileLastFolder", _
														m_SaveFileLastFolder)
			End If
			xmlConfig.Save(xmlFile)
		Catch ex As UnauthorizedAccessException
			MessageBoxWrapper.ShowError(ex.Message)
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
		End Try
	End Sub
	
	''' <summary>
	''' This function will import a PDFKeeper 1.0.0 or 1.1.0 properties file
	''' into class properties, and then delete the file.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Shared Function ImportLegacyProperties() As Integer
		Dim legacyProperties As String = Path.Combine(AppDataDir, "pdfkeeper.properties")
		If System.IO.File.Exists(legacyProperties) Then
			Try
				Using oFileStream As New FileStream(legacyProperties, _
									 	 FileMode.Open)
					Using oStreamReader As New StreamReader(oFileStream, _
															Encoding.ASCII)
						Dim lastDataSource As String
						Dim lastUserName As String
						lastDataSource = oStreamReader.ReadLine
						lastUserName = oStreamReader.ReadLine
						m_LastDataSource = lastDataSource.Substring(15)
						m_LastUserName = lastUserName.Substring(13)
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
	''' This function will import the PDFKeeper 2.0.0 through 2.4.0 registry
	''' key values into class properties, and then delete the registry key.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Shared Function ImportLegacyRegistryValues() As Integer
		Dim appRoot As String = "HKEY_CURRENT_USER\Software\" & _
								 Application.ProductName
		Dim regKey As Microsoft.Win32.RegistryKey = _
			  		  Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _
			  		 "Software\" & Application.ProductName, True)
		If Not regKey Is Nothing Then
			Try
				m_LastUserName = CStr(Registry.GetValue(appRoot, _
											"LastUserName", Nothing))
				m_LastDataSource = CStr(Registry.GetValue(appRoot, _
											"LastDataSource", Nothing))
				m_FormPositionTop = CStr(Registry.GetValue(appRoot, _
											"FormPositionTop", Nothing))
				m_FormPositionLeft = CStr(Registry.GetValue(appRoot, _
											"FormPositionLeft", Nothing))
				m_FormPositionHeight = CStr(Registry.GetValue(appRoot, _
											"FormPositionHeight", Nothing))
				m_FormPositionWidth = CStr(Registry.GetValue(appRoot, _
											"FormPositionWidth", Nothing))
				m_FormPositionWindowState = CStr(Registry.GetValue(appRoot, _
											"FormPositionWindowState", _
											 Nothing))
				m_UpdateCheck = CStr(Registry.GetValue(appRoot, _
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
