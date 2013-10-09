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

Public Class UserSettings
	Dim appRoot As String = "HKEY_CURRENT_USER\Software\" & _
							 Application.ProductName
	Shared m_LastUserName As String
	Shared m_LastDataSource As String
	Shared m_FormPositionTop As String
	Shared m_FormPositionLeft As String
	Shared m_FormPositionHeight As String
	Shared m_FormPositionWidth As String
	Shared m_FormPositionWindowState As String
	Shared m_UpdateCheck As String
			
	#Region "Properties"
	
	Shared Property LastUserName() As String
		Get
			return m_LastUserName
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
				m_FormPositionTop = 10	
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
				m_FormPositionLeft = 25	
			End If
			Return m_FormPositionLeft
		End Get
		Set(ByVal Value As String)
			m_FormPositionLeft = Value
		End Set
	End Property
	
	Shared Property FormPositionHeight() As String
		Get
			If m_FormPositionHeight = Nothing Then
				m_FormPositionHeight = 550	
			End If
			Return m_FormPositionHeight
		End Get
		Set(ByVal Value As String)
			m_FormPositionHeight = Value
		End Set
	End Property
	
	Shared Property FormPositionWidth() As String
		Get
			If m_FormPositionWidth = Nothing Then
				m_FormPositionWidth = 750	
			End If
			Return m_FormPositionWidth
		End Get
		Set(ByVal Value As String)
			m_FormPositionWidth = Value
		End Set
	End Property
	
	Shared Property FormPositionWindowState() As String
		Get
			If m_FormPositionWindowState = Nothing Then
				m_FormPositionWindowState = 0	
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
				m_UpdateCheck = 1	
			End If
			Return m_UpdateCheck
		End Get
		Set(ByVal Value As String)
			m_UpdateCheck = Value
		End Set
	End Property
	
	#End Region
	
	#Region "Functions and Subroutines"

	''' <summary>
	''' This function will read application user settings from the registry
	''' into class properties.  This function will get called when the
	''' application is started.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Read() As Byte
		Try
			m_LastUserName = Registry.GetValue(appRoot, "LastUserName", _
											   Nothing)
			m_LastDataSource = Registry.GetValue(appRoot, "LastDataSource", _
												 Nothing)
			m_FormPositionTop = Registry.GetValue(appRoot, "FormPositionTop", _
												  Nothing)
			m_FormPositionLeft = Registry.GetValue(appRoot, "FormPositionLeft", _
												   Nothing)
			m_FormPositionHeight = Registry.GetValue(appRoot, "FormPositionHeight", _
													 Nothing)
			m_FormPositionWidth = Registry.GetValue(appRoot, "FormPositionWidth", _
													Nothing)
			m_FormPositionWindowState = Registry.GetValue(appRoot, _
												 "FormPositionWindowState", _
												  Nothing)
			m_UpdateCheck = Registry.GetValue(appRoot, "UpdateCheck", Nothing)
		Catch ex as IOException
			MessageDialog.Display("Error", ex.Message)
			Return 1
		End Try
				
		' If a PDFKeeper 1.0.0 and 1.1.0 properties file does exist, import and
		' delete it.
		Dim legacyProperties As String = Path.Combine(AppDataDir, "pdfkeeper.properties")
		if File.Exists(legacyProperties) Then
			Try
				Using objFileStream As New FileStream(legacyProperties, _
									 	   FileMode.Open)
					Using objStreamReader As New StreamReader(objFileStream, _
															  Encoding.ASCII)
				
						' First line is LastDataSource.
						Dim lastDataSource As String
						lastDataSource = objStreamReader.ReadLine
						m_LastDataSource = lastDataSource.Substring(15)
				
						' Second line is LastUsername.
						Dim lastUserName As String
						lastUserName = objStreamReader.ReadLine
						m_LastUserName = lastUserName.Substring(13)
					End Using
					objFileStream.Close
				End Using	
				File.Delete(legacyProperties)
			Catch ex as IOException
				MessageDialog.Display("Error", ex.Message)
				Return 1
			End Try
		End If
		
		Return 0
	End Function

	''' <summary>
	''' This subroutine will write the class properties to the registry.  This
	''' subroutine gets called when the application is closing.
	''' </summary>
	Public Sub Write
		Try
			Registry.SetValue(appRoot, "LastUserName", m_LastUserName)
			Registry.SetValue(appRoot, "LastDataSource", _
							  m_LastDataSource)
			Registry.SetValue(appRoot, "FormPositionTop", _
							  m_FormPositionTop)
			Registry.SetValue(appRoot, "FormPositionLeft", _
							  m_FormPositionLeft)
			Registry.SetValue(appRoot, "FormPositionHeight", _
							  m_FormPositionHeight)
			Registry.SetValue(appRoot, "FormPositionWidth", _
							  m_FormPositionWidth)
			Registry.SetValue(appRoot, "FormPositionWindowState", _
							  m_FormPositionWindowState)
			Registry.SetValue(appRoot, "UpdateCheck", m_UpdateCheck)
		Catch ex as IOException
			MessageDialog.Display("Error", ex.Message)
		End Try
	End Sub
	
	#End Region
End Class
