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

''' <summary>
''' Database Connection properties are set by the presentation model and are
''' used by the Utility class to construct the connection string.
''' </summary>
''' <remarks>
''' The UserName and DataSource properties are serializable.
''' </remarks>
<SerializableAttribute>Public Class DBConnection
	' String members must be set to an empty string to prevent a
	' NullReferenceException from being thrown during instantiation.
	Private Shared _userName As String = String.Empty
	Private Shared _password As SecureString
	Private Shared _dataSource As String = String.Empty
	
	' CA1822 warnings from FxCop have to be suppressed as shared properties
	' cannot be serialized. The Password property is marked shared to satisfy
	' the FxCop warning CA1822 as it is not serializable.
	
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public Property UserName As String
		Get
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value
		End Set
	End Property
	
	Friend Shared Property Password As SecureString
		Get
			Return _password
		End Get
		Set(ByVal value As SecureString)
			_password = value
		End Set
	End Property
	
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public Property DataSource As String
		Get
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value
		End Set
	End Property
End Class
