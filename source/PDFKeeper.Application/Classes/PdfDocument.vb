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

'Public Class PdfDocument
'	Private _sourcePdf As String
'	Private _targetPdf As String
'	Private _ownerPassword As SecureString
'	Private _title As String
'	Private _author As String
'	Private _subject As String
'	Private _keywords As String
'	Private reader As PdfReader
'	
'	''' <summary>
'	''' Constructor for only reading PDF without OWNER password.
'	''' </summary>
'	''' <param name="sourcePdf"></param>
'	Public Sub New(ByVal sourcePdf As String)
'		_sourcePdf = sourcePdf
'	End Sub
'	
'	''' <summary>
'	''' Constructor for reading and writing PDF without OWNER password.
'	''' </summary>
'	''' <param name="sourcePdf"></param>
'	''' <param name="targetPdf"></param>
'	Public Sub New(ByVal sourcePdf As String, ByVal targetPdf As String)
'		_sourcePdf = sourcePdf
'		_targetPdf = targetPdf
'	End Sub
'	
'	''' <summary>
'	''' Constructor for only reading PDF with OWNER password.
'	''' </summary>
'	''' <param name="sourcePdf"></param>
'	''' <param name="ownerPassword"></param>
'	Public Sub New( _
'		ByVal sourcePdf As String, _
'		ByVal ownerPassword As SecureString)
'		
'		_sourcePdf = sourcePdf
'		_ownerPassword = ownerPassword
'	End Sub
'	
'	''' <summary>
'	''' Constructor for reading and writing PDF with OWNER password.
'	''' </summary>
'	''' <param name="sourcePdf"></param>
'	''' <param name="targetPdf"></param>
'	''' <param name="ownerPassword"></param>
'	Public Sub New( _
'		ByVal sourcePdf As String, _
'		ByVal targetPdf As String, _
'		ByVal ownerPassword As SecureString)
'		
'		_sourcePdf = sourcePdf
'		_targetPdf = targetPdf
'		_ownerPassword = ownerPassword
'	End Sub
'	
'	''' <summary>
'	''' Gets or sets the PDF Title.
'	''' </summary>
'	Public Property Title As String
'		Get
'			Return _title
'		End Get
'		Set(ByVal value As String)
'			_title = value
'		End Set
'	End Property
'	
'	''' <summary>
'	''' Gets or sets the PDF Author.
'	''' </summary>
'	Public Property Author As String
'		Get
'			Return _author
'		End Get
'		Set(ByVal value As String)
'			_author = value
'		End Set
'	End Property
'	
'	''' <summary>
'	''' Gets or sets the PDF Subject.
'	''' </summary>
'	Public Property Subject As String
'		Get
'			Return _subject
'		End Get
'		Set(ByVal value As String)
'			_subject = value
'		End Set
'	End Property
'	
'	''' <summary>
'	''' Gets or sets the PDF Keywords.
'	''' </summary>
'	Public Property Keywords As String
'		Get
'			Return _keywords
'		End Get
'		Set(ByVal value As String)
'			_keywords = value
'		End Set
'	End Property
'	
'	
'	
'	''' <summary>
'	''' 
'	''' </summary>
'	Private Sub Read
'		Try
'			If _ownerPassword IsNot Nothing Then
'				Dim password As SecureString = _ownerPassword
'				reader = New PdfReader( _
'					_sourcePdf, _
'					System.Text.Encoding.ASCII.GetBytes(password.GetString))
'			Else
'				reader = New PdfReader(_sourcePdf)
'			End If
'			If reader.Info.ContainsKey(PdfInformationPropertiesEnum.Name.Title) Then
'				_title = reader.Info("Title")
'			End If
'			
'			
'
'			
'			
'		End Try
'		
'		
'		
'		
'
'	End Sub
'End Class
