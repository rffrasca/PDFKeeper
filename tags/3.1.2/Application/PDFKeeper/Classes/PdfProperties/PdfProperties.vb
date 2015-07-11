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

Public Class PdfProperties
	Implements IDisposable
	Private isDisposed As Boolean
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
		"CA1823:AvoidUnusedPrivateFields")> _
	Dim ownerPasswordLocal As SecureString
	Dim oPdfReader As PdfReader
	Dim oPdfStamper As PdfStamper
	Dim sourcePdf As String
	Dim targetPdf As String
	Dim containsOwnerPassword As Boolean
	Dim m_Title As String
	Dim m_Author As String
	Dim m_Subject As String
	Dim m_Keywords As String

	''' <summary>
	''' This subroutine is the class constructor for reading only.
	''' </summary>
	''' <param name="sourcePdf"></param>
	Public Sub New(ByVal sourcePdfArg as String)
		sourcePdf = sourcePdfArg
	End Sub
	
	''' <summary>
	''' This subroutine is the class constructor for reading only with OWNER
	''' password.
	''' </summary>
	''' <param name="sourcePdf"></param>
	''' <param name="ownerPassword"></param>
	Public Sub New(ByVal sourcePdfArg As String, _
				   ByVal ownerPasswordArg As SecureString)
		sourcePdf = sourcePdfArg
		ownerPasswordLocal = ownerPasswordArg
		containsOwnerPassword = True
	End Sub
	
	''' <summary>
	''' This subroutine is the class constructor overload for reading and
	''' writing.
	''' </summary>
	''' <param name="sourcePdf"></param>
	''' <param name="targetPdf"></param>
	Public Sub New(ByVal sourcePdfArg As String, ByVal targetPdfArg As String)
		sourcePdf = sourcePdfArg
		targetPdf = targetPdfArg
	End Sub
	
	''' <summary>
	''' This subroutine is the class constructor overload for reading and
	''' writing with OWNER password.
	''' </summary>
	''' <param name="sourcePdf"></param>
	''' <param name="targetPdf"></param>
	''' <param name="ownerPassword"></param>
	Public Sub New(ByVal sourcePdfArg As String, _
				   ByVal targetPdfArg As String, _
				   ByVal ownerPasswordArg As SecureString)
		sourcePdf = sourcePdfArg
		targetPdf = targetPdfArg
		ownerPasswordLocal = ownerPasswordArg
		containsOwnerPassword = True
	End Sub
	
	#Region "Properties"
	
	Property Title() As String
		Get
			Return Me.m_Title
		End Get
		Set(ByVal Value As String)
			m_Title = Value
		End Set
	End Property
	
	Property Author() As String
		Get
			Return Me.m_Author
		End Get
		Set(ByVal Value As String)
			m_Author = Value
		End Set
	End Property
	
	Property Subject() As String
		Get
			Return Me.m_Subject
		End Get
		Set(ByVal Value As String)
			m_Subject = Value
		End Set
	End Property
	
	Property Keywords() As String
		Get
			Return Me.m_Keywords
		End Get
		Set(ByVal Value As String)
			m_Keywords = Value
		End Set
	End Property
	
	#End Region
			
	''' <summary>
	''' This function will read the information properties for the PDF object
	''' into class properties.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Read As Integer
		Dim inputOpened As Boolean = False
		Try
			If containsOwnerPassword Then
				oPdfReader = New PdfReader(sourcePdf, _
					System.Text.Encoding.ASCII.GetBytes( _
					StringDecoder.SecureStringToString(ownerPasswordLocal)))
			Else
				oPdfReader = New PdfReader(sourcePdf)
			End If
			inputOpened = True
			If oPdfReader.Info.ContainsKey("Title") Then
				m_Title = oPdfReader.Info("Title")
			Else
				m_Title = Nothing
			End If
			If oPdfReader.Info.ContainsKey("Author") Then
				m_Author = oPdfReader.Info("Author")
			Else
				m_Author = Nothing
			End If
			If oPdfReader.Info.ContainsKey("Subject") Then
				m_Subject = oPdfReader.Info("Subject")
			Else
				m_Subject = Nothing
			End If
			If oPdfReader.Info.ContainsKey("Keywords") Then
				m_Keywords = oPdfReader.Info("Keywords")
			Else
				m_Keywords = Nothing
			End If
			Return 0
		Catch ex As DocumentException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Catch ex As IOException
			If ex.Message = "Bad user password" Then
				MessageBoxWrapper.ShowError(PdfProperties_Strings.BadPassword)
			Else
				MessageBoxWrapper.ShowError(ex.Message)
			End If
			Return 1
		Finally
			If inputOpened = True Then
				oPdfReader.Close
			End If
		End Try
	End Function
	
	''' <summary>
	''' This function will write the class properties to the target PDF
	''' document object.  This function will fail if the Title, Author, or
	''' Subject is blank.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Write As Integer
		Dim outputCreated As Boolean = False
		Try
			If containsOwnerPassword Then
				oPdfReader = New PdfReader(sourcePdf, _
					System.Text.Encoding.ASCII.GetBytes( _
					StringDecoder.SecureStringToString(ownerPasswordLocal)))
			Else
				oPdfReader = New PdfReader(sourcePdf)
			End If
			oPdfStamper = New PdfStamper(oPdfReader, _
						  New FileStream(targetPdf, FileMode.Create))
			outputCreated = True
			Dim oDictionary As New Dictionary(Of String, String)
			oDictionary("Title") = m_Title
			oDictionary("Author") = m_Author
			oDictionary("Subject") = m_Subject
			oDictionary("Keywords") = m_Keywords
			oPdfStamper.MoreInfo = oDictionary
			Return 0
		Catch ex As DocumentException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		Finally
			If outputCreated = True Then
				Dispose
			End If
		End Try
	End Function
	
	''' <summary>
	''' This subroutine will close the PdfStamper file object and dispose it
	''' and clear the owner password if one was specified.
	''' </summary>
	''' <param name="disposing"></param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.isDisposed Then
			If disposing Then
				If containsOwnerPassword Then
					ownerPasswordLocal.Clear
				End If
				oPdfStamper.Close
				oPdfStamper.Dispose
				oPdfReader.Dispose
			End If
		End If
		Me.isDisposed = True
	End Sub
	
	''' <summary>
	''' This subroutine will call the protected Dispose subroutine.  It is
	''' called by the consumer of the object after the Information Properties
	''' have been saved to the PDF file.
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub
	
	''' <summary>
	''' This subroutine will call the protected Dispose subroutine.  It is
	''' called by the garbage collector, only if the consumer of the object
	''' does not call Dispose as it should.
	''' </summary>
	Protected Overrides Sub Finalize()
		Dispose(False)
	End Sub
End Class
