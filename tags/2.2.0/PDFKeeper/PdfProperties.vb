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

Public Class PdfProperties
	Implements IDisposable
	Private isDisposed As Boolean
	Dim objPdfReader As PdfReader
	Dim objPdfStamper As PdfStamper
	Dim m_pdfFile As String
	Dim m_Title As String
	Dim m_Author As String
	Dim m_Subject As String
	Dim m_Keywords As String
	
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
			
	#Region "Functions and Subroutines"
	
	Public Sub New(ByVal pdfFile as String)
		m_pdfFile = pdfFile
	End Sub
	
	''' <summary>
	''' This function will read the information properties for the PDF object
	''' into class properties, provided the PDF document is not password
	''' protected.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Read() As Byte
		Dim result As Byte = 0
		Dim inputOpened As Boolean = False
		Try
			objPdfReader = New PdfReader(m_pdfFile)
			inputOpened = True
			If objPdfReader.IsOpenedWithFullPermissions = True Then
				If objPdfReader.Info.ContainsKey("Title") Then
					m_Title = objPdfReader.Info("Title")
				Else
					m_Title = Nothing
				End If
				If objPdfReader.Info.ContainsKey("Author") Then
					m_Author = objPdfReader.Info("Author")
				Else
					m_Author = Nothing
				End If
				If objPdfReader.Info.ContainsKey("Subject") Then
					m_Subject = objPdfReader.Info("Subject")
				Else
					m_Subject = Nothing
				End If
				If objPdfReader.Info.ContainsKey("Keywords") Then
					m_Keywords = objPdfReader.Info("Keywords")
				Else
					m_Keywords = Nothing
				End If
			Else
				MessageDialog.Display("Error", m_pdfFile & " is protected " & _
									  "by an OWNER password. Password " & _
									  "protected PDF documents are not " & _
									  "supported by PDFKeeper. Please see " & _
									  "the Troubleshooting section in the " & _
									  "User FAQ for more information.")
				result = 1
			End If
		Catch ex As DocumentException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		Catch ex as IOException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		Finally
			If inputOpened = True Then
				objPdfReader.Close
			End If
		End Try
		Return result
	End Function
	
	''' <summary>
	''' This function will write the class properties to a new PDF document,
	''' and then save the original PDF document as a backup file with a random
	''' 3 digit number as the file extension.  This function will fail if the
	''' Title, Author, or Subject is blank.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Save() As Byte
		Dim result As Byte = 0
		Dim modPdfFile As String
		modPdfFile = Path.Combine(TempDir, _
					"pdfkeeper" & Path.GetRandomFileName & ".pdf")
			
		' Create a dictionary hash and write to new PDF document.
		Dim outputCreated As Boolean = False
		Try
			objPdfReader = New PdfReader(m_pdfFile)
			objPdfStamper = New PdfStamper(objPdfReader, _
							New FileStream(modPdfFile, FileMode.Create))
			outputCreated = True
			Dim objDictionary As New Dictionary(Of String, String)
			objDictionary("Title") = m_Title
			objDictionary("Author") = m_Author
			objDictionary("Subject") = m_Subject
			objDictionary("Keywords") = m_Keywords
			objPdfStamper.MoreInfo = objDictionary
		Catch ex As DocumentException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		Catch ex As IOException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		Finally
			objPdfReader.Close
			If outputCreated = True Then
				Dispose
			End If
		End Try
		If result = 1 Then
			Return result
		End If

		' Backup the source PDF document and rename the new PDF with the
		' original name as the source.
		Dim objRandom As New Random
		Dim randNumber As String = objRandom.Next
		Dim bakPdfFile As String = m_pdfFile & "." & randNumber.Substring(0,3)
		Try
			File.Move(m_pdfFile, bakPdfFile)
			File.Move(modPdfFile, m_pdfFile)
			MessageDialog.Display("Information", "The original version " & _
								  "of " & m_pdfFile & " has been saved " & _
								  "as " & bakPdfFile)
		Catch ex as IOException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		Catch ex As UnauthorizedAccessException
			MessageDialog.Display("Error", ex.Message)
			result = 1
		End Try

		Return result
	End Function
		
	#End Region
	
	#Region "Dispose Subroutines"
	
	''' <summary>
	''' This subroutine will close the PdfStamper file object and dispose it.
	''' </summary>
	''' <param name="disposing"></param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.isDisposed Then
			If disposing Then
				objPdfStamper.Close
				objPdfStamper.Dispose
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
	
	#End Region
End Class
