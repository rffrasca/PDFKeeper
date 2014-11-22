'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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

Public Class DirectUploadFolderProperties
	Dim xmlFile As String
	Dim m_TitlePreFill As String
	Dim m_AuthorPreFill As String
	Dim m_SubjectPreFill As String
	Dim m_KeywordsPreFill As String
	Dim m_UseExistingTitleChecked As String
	Dim m_UseExistingAuthorChecked As String
	Dim m_UseExistingSubjectChecked As String
	Dim m_UseExistingKeywordsChecked As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="xmlName" name only without the file extension></param>
	Public Sub New(ByVal xmlName as String)
		xmlFile = Path.Combine(UploadXmlDir, xmlName & ".xml")
	End Sub
						
	#Region "Properties"
	
	Public Property TitlePreFill() As String
		Get
			Return m_TitlePreFill
		End Get
		Set(ByVal Value As String)
			m_TitlePreFill = Value
		End Set
	End Property
	
	Public Property AuthorPreFill() As String
		Get
			Return m_AuthorPreFill
		End Get
		Set(ByVal Value As String)
			m_AuthorPreFill = Value
		End Set
	End Property
	
	Public Property SubjectPreFill() As String
		Get
			Return m_SubjectPreFill
		End Get
		Set(ByVal Value As String)
			m_SubjectPreFill = Value
		End Set
	End Property
		
	Public Property KeywordsPreFill() As String
		Get
			If m_KeywordsPreFill = Nothing Then
				m_KeywordsPreFill = ""
			End If
			Return m_KeywordsPreFill
		End Get
		Set(ByVal Value As String)
			m_KeywordsPreFill = Value
		End Set
	End Property
	
	Public Property UseExistingTitleChecked() As String
		Get
			If m_UseExistingTitleChecked = Nothing Then
				m_UseExistingTitleChecked = CStr(0)
			End If
			Return m_UseExistingTitleChecked
		End Get
		Set(ByVal Value As String)
			m_UseExistingTitleChecked = Value
		End Set
	End Property
	
	Public Property UseExistingAuthorChecked() As String
		Get
			If m_UseExistingAuthorChecked = Nothing Then
				m_UseExistingAuthorChecked = CStr(0)
			End If
			Return m_UseExistingAuthorChecked
		End Get
		Set(ByVal Value As String)
			m_UseExistingAuthorChecked = Value
		End Set
	End Property
	
	Public Property UseExistingSubjectChecked() As String
		Get
			If m_UseExistingSubjectChecked = Nothing Then
				m_UseExistingSubjectChecked = CStr(0)
			End If
			Return m_UseExistingSubjectChecked
		End Get
		Set(ByVal Value As String)
			m_UseExistingSubjectChecked = Value
		End Set
	End Property
	
	Public Property UseExistingKeywordsChecked() As String
		Get
			If m_UseExistingKeywordsChecked = Nothing Then
				m_UseExistingKeywordsChecked = CStr(0)
			End If
			Return m_UseExistingKeywordsChecked
		End Get
		Set(ByVal Value As String)
			m_UseExistingKeywordsChecked = Value
		End Set
	End Property
	
	#End Region
	
	''' <summary>
	''' This function will read the folder properties from "xmlFile" into
	''' class properties.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Read() As Integer
		If System.IO.File.Exists(xmlFile) Then
			Try
				Dim xmlConfig As New XmlConfigSource(xmlFile)
				m_TitlePreFill = xmlConfig.Configs("Properties") _
					.Get("TitlePreFill")
				m_AuthorPreFill = xmlConfig.Configs("Properties") _
					.Get("AuthorPreFill")
				m_SubjectPreFill = xmlConfig.Configs("Properties") _
					.Get("SubjectPreFill")
				m_KeywordsPreFill = xmlConfig.Configs("Properties") _
					.Get("KeywordsPreFill")
				m_UseExistingTitleChecked = xmlConfig.Configs("Properties") _
					.Get("UseExistingTitleChecked")
				m_UseExistingAuthorChecked = xmlConfig.Configs("Properties") _
					.Get("UseExistingAuthorChecked")
				m_UseExistingSubjectChecked = xmlConfig.Configs("Properties") _
					.Get("UseExistingSubjectChecked")
				m_UseExistingKeywordsChecked = xmlConfig.Configs( _
					"Properties").Get("UseExistingKeywordsChecked")
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
	''' This function will write the class properties to "xmlFile".
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Write As Integer
		Try
			Dim xmlConfig As New XmlConfigSource
			xmlConfig.AddConfig("Properties")
			If Not m_TitlePreFill = Nothing Then
				xmlConfig.Configs("Properties").Set("TitlePreFill", _
					m_TitlePreFill)
			End If
			If Not m_AuthorPreFill = Nothing Then
				xmlConfig.Configs("Properties").Set("AuthorPreFill", _
					m_AuthorPreFill)
			End If
			If Not m_SubjectPreFill = Nothing Then
				xmlConfig.Configs("Properties").Set("SubjectPreFill", _
					m_SubjectPreFill)
			End If
			If Not m_KeywordsPreFill = Nothing Then
				xmlConfig.Configs("Properties").Set("KeywordsPreFill", _
					m_KeywordsPreFill)
			End If
			If m_UseExistingTitleChecked = Nothing Then
				m_UseExistingTitleChecked = CStr(0)
			End If
			xmlConfig.Configs("Properties").Set("UseExistingTitleChecked", _
					m_UseExistingTitleChecked)
			If m_UseExistingAuthorChecked = Nothing Then
				m_UseExistingAuthorChecked = CStr(0)
			End If
			xmlConfig.Configs("Properties").Set("UseExistingAuthorChecked", _
					m_UseExistingAuthorChecked)
			If m_UseExistingSubjectChecked = Nothing Then
				m_UseExistingSubjectChecked = CStr(0)
			End If
			xmlConfig.Configs("Properties").Set("UseExistingSubjectChecked", _
					m_UseExistingSubjectChecked)
			If m_UseExistingKeywordsChecked = Nothing Then
				m_UseExistingKeywordsChecked = CStr(0)
			End If
			xmlConfig.Configs("Properties").Set("UseExistingKeywordsChecked", _
					m_UseExistingKeywordsChecked)
			xmlConfig.Save(xmlFile)
		Catch ex As UnauthorizedAccessException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
		Return 0
	End Function
End Class
