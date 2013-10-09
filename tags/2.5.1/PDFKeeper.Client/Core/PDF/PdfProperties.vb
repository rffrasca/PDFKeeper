'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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
	Dim oPdfReader As PdfReader
	Dim oPdfStamper As PdfStamper
	Dim pdfFile As String
	Dim m_Title As String
	Dim m_Author As String
	Dim m_Subject As String
	Dim m_Keywords As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: pdfFile"></param>
	Public Sub New(ByVal arg as String)
		pdfFile = arg
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
			
	#Region "Functions and Subroutines"

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
			oPdfReader = New PdfReader(pdfFile)
			inputOpened = True
			If oPdfReader.IsOpenedWithFullPermissions = True Then
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
			Else
				' TODO: When PDFKeeper is localized, the static text in this
				' message will need to be stored in a resource file. 
				Dim oMessageDialog As New _
					 MessageDialog(pdfFile & " is protected by an OWNER " & _
									  		 "password. Password protected" & _
									  		 "PDF documents are not " & _
									  		 "supported by PDFKeeper. " & _
									  		 "Please see the " & _
									  		 "Troubleshooting section in " & _
									  		 "the User FAQ for more "& _
									  		 "information.")
				oMessageDialog.DisplayError
				result = 1
			End If
		Catch ex As DocumentException
			Dim oMessageDialog As New MessageDialog(ex.Message)
			oMessageDialog.DisplayError
			result = 1
		Catch ex as IOException
			Dim oMessageDialog As New MessageDialog(ex.Message)
			oMessageDialog.DisplayError
			result = 1
		Finally
			If inputOpened = True Then
				oPdfReader.Close
			End If
		End Try
		Return result
	End Function
	
	''' <summary>
	''' This function will write the class properties to a new PDF document.
	''' The filename of the new PDF document will include
	''' "modified_by_pdfkeeper" in the filename.  This function will fail if
	''' the Title, Author, or Subject is blank.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Save() As Byte
		Dim result As Byte = 0
		
		' TODO: When PDFKeeper is localized, "modified_by_pdfkeeper" will need
		' to be stored in a resource file.
		Dim modPdfFile As String
		modPdfFile = pdfFile.Substring(0, (pdfFile.Length - 3)) & _
										  "modified_by_pdfkeeper.pdf"

		' Create a dictionary hash and write to new PDF document.
		Dim outputCreated As Boolean = False
		Try
			oPdfReader = New PdfReader(pdfFile)
			oPdfStamper = New PdfStamper(oPdfReader, _
						  New FileStream(modPdfFile, FileMode.Create))
			outputCreated = True
			Dim oDictionary As New Dictionary(Of String, String)
			oDictionary("Title") = m_Title
			oDictionary("Author") = m_Author
			oDictionary("Subject") = m_Subject
			oDictionary("Keywords") = m_Keywords
			oPdfStamper.MoreInfo = oDictionary
		Catch ex As DocumentException
			Dim oMessageDialog1 As New MessageDialog(ex.Message)
			oMessageDialog1.DisplayError
			result = 1
		Catch ex As IOException
			Dim oMessageDialog1 As New MessageDialog(ex.Message)
			oMessageDialog1.DisplayError
			result = 1
		Finally
			oPdfReader.Close
			If outputCreated = True Then
				Dispose
			End If
		End Try
		If result = 1 Then
			Return result
		End If

		' TODO: When PDFKeeper is localized, the static text in this message
		' will need to be stored in a resource file.
		Dim oMessageDialog2 As New MessageDialog("The modified copy of " & _
												 "the PDF document has " & _
												 "been saved as: " & _
												  modPdfFile)
		oMessageDialog2.DisplayInformation
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
				oPdfStamper.Close
				oPdfStamper.Dispose
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
