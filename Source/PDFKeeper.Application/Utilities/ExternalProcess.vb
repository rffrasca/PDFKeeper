'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
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

Public Class ExternalProcess
	Private openParam As String
	
	''' <summary>
	''' Initializes a new instance of this class with the specified System.Uri
	''' object.
	''' </summary>
	''' <param name="arg">System.Uri object.</param>
	Public Sub New(ByVal arg As System.Uri)
		openParam = arg.ToString
	End Sub
	
	''' <summary>
	''' Initializes a new instance of this class with the specified string
	''' object.
	''' </summary>
	''' <param name="arg"></param>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Design", _
		"CA1057:StringUriOverloadsCallSystemUriOverloads")> _
	Public Sub New(ByVal arg As String)
		openParam = arg
	End Sub
	
	''' <summary>
	''' Opens the object using the default application.    
	''' </summary>
	Public Sub Open
		Process.Start(openParam)
	End Sub
End Class
