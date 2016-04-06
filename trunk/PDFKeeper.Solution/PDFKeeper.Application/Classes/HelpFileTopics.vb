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

Public NotInheritable Class HelpFileTopics
	Private Sub New()
		' Class cannot be instantiated as it only contains shared members.
		' Required for FxCop compliance (CA1053).
	End Sub
	
	''' <summary>
	''' Gets the help file name.
	''' </summary>
	Public Shared ReadOnly Property HelpFile As String
		Get
			Return "PDFKeeper.en.chm"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Database Connection Form help topic file name.
	''' </summary>
	Public Shared ReadOnly Property DatabaseConnectionForm As String
		Get
			Return "Database Connection.html"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Direct Upload Configuration Form help topic file name.
	''' </summary>
	Public Shared ReadOnly Property DirectUploadConfigurationForm As String
		Get
			Return "Configuring Direct Upload sub-folders.html"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form Document Search tab help topic file name.
	''' </summary>
	Public Shared ReadOnly Property MainFormDocumentSearchTab As String
		Get
			Return "Document Search.html"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form Document Preview tab help topic file name.
	''' </summary>
	Public Shared ReadOnly Property MainFormDocumentPreviewTab As String
		Get
			Return "Previewing documents returned from a search.html"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form Document Text-Only View tab help topic file name.
	''' </summary>
	Public Shared ReadOnly Property MainFormDocumentTextOnlyViewTab As String
		Get
			Return _
				"Viewing text-only for documents returned from a search.html"
		End Get
	End Property
	
	''' <summary>
	''' Gets the Main Form Document Capture tab help topic file name.
	''' </summary>
	Public Shared ReadOnly Property MainFormDocumentCaptureTab As String
		Get
			Return "Document Capture.html"
		End Get
	End Property	
End Class
