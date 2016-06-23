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
'	Implements IDisposable
'	Private isDisposed As Boolean
'	Private _sourcePdf As String
'	Private _targetPdf As String
'	Private _ownerPassword As SecureString
'	Private _title As String
'	Private _author As String
'	Private _subject As String
'	Private _keywords As String
'	Private reader As PdfReader
'	Private stamper As PdfStamper
'	
'	''' <summary>
'	''' Constructor for only reading PDF without OWNER password.
'	''' </summary>
'	''' <param name="sourcePdf"></param>
'	Public Sub New(ByVal sourcePdf As String)
'		_sourcePdf = sourcePdf
'		Read
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
'		Read
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
'		Read
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
'		Read
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
'	''' <summary>
'	''' Reads the information properties from _sourcePdf into class objects.
'	''' </summary>
'	Private Sub Read
'		Dim sourceOpened As Boolean
'		Try
'			If _ownerPassword IsNot Nothing Then
'				Dim password As SecureString = _ownerPassword
'				reader = New PdfReader( _
'					_sourcePdf, _
'					System.Text.Encoding.ASCII.GetBytes(password.GetString))
'			Else
'				reader = New PdfReader(_sourcePdf)
'			End If
'			sourceOpened = True
'			If reader.Info.ContainsKey( _
'				Enums.PdfInformationProperty.Title.ToString) Then
'				
'				_title = reader.Info( _
'					Enums.PdfInformationProperty.Title.ToString)
'			End If
'			If reader.Info.ContainsKey( _
'				Enums.PdfInformationProperty.Author.ToString) Then
'				
'				_author = reader.Info( _
'					Enums.PdfInformationProperty.Author.ToString)
'			End If
'			If reader.Info.ContainsKey( _
'				Enums.PdfInformationProperty.Subject.ToString) Then
'				
'				_subject = reader.Info( _
'					Enums.PdfInformationProperty.Subject.ToString)
'			End If
'			If reader.Info.ContainsKey( _
'				Enums.PdfInformationProperty.Keywords.ToString) Then
'				
'				_keywords = reader.Info( _
'					Enums.PdfInformationProperty.Keywords.ToString)
'			End If
'		Catch ex As DocumentException
'			Throw New BadPdfFormatException(ex.Message)
'		Catch ex As IOException
''			If ex.Message = "Bad user password" Then
'				Throw New BadPasswordException(PdfKeeper.Strings.IncorrectOwnerPassword)
''			Else
''				Throw New IOException(ex.Message)
''			End If
'		Finally
'			If sourceOpened Then
'				reader.Close
'			End If
'		End Try
'	End Sub
'	
'	''' <summary>
'	''' 
'	''' </summary>
'	''' <returns></returns>
'	Public Function Stamp As Integer
'		Try
'			If _ownerPassword IsNot Nothing Then
'				Dim password As SecureString = _ownerPassword
'				reader = New PdfReader( _
'					_sourcePdf, _
'					System.Text.Encoding.ASCII.GetBytes(password.GetString))
'			Else
'				reader = New PdfReader(_sourcePdf)
'			End If
'			stamper = New PdfStamper( _
'				reader, _
'				New FileStream(_targetPdf, FileMode.Create))
'			
'			Dim dict As New Dictionary(Of String, String)
'			dict(Enums.PdfInformationProperty.Title.ToString) = _title
'			dict(Enums.PdfInformationProperty.Author.ToString) = _author
'			dict(Enums.PdfInformationProperty.Subject.ToString) = _subject
'			dict(Enums.PdfInformationProperty.Keywords.ToString) = _keywords
'			stamper.MoreInfo = dict
'			Return 0
'		Catch ex As DocumentException
'			ShowError(ex.Message)
'			Return 1
'		Catch ex As IOException
'			ShowError(ex.Message)
'			Return 1
'		Finally
'			Dispose
'		End Try
'	End Function
'	
'	''' <summary>
'	''' Disposes the PdfStamper file object and clears the owner password if
'	''' one was specified.
'	''' </summary>
'	''' <param name="disposing"></param>
'	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'		If Not Me.isDisposed Then
'			If disposing Then
'				If _ownerPassword IsNot Nothing Then
'					_ownerPassword.Clear
'				End If
'				stamper.Dispose
'				reader.Dispose
'			End If
'		End If
'		Me.isDisposed = True
'	End Sub
'	
'	''' <summary>
'	''' Calls the protected Dispose subroutine.  It is called by the consumer
'	''' of the object after the information properties have been saved to
'	''' _targetPdf.
'	''' </summary>
'	Public Sub Dispose() Implements IDisposable.Dispose
'		Dispose(True)
'		GC.SuppressFinalize(Me)
'	End Sub
'	
'	''' <summary>
'	''' Calls the protected Dispose subroutine.  It is called by the Garbage
'	''' Collector, only if the consumer of the object does not call Dispose as
'	''' it should.
'	''' </summary>
'	Protected Overrides Sub Finalize()
'		Dispose(False)
'	End Sub
'End Class
