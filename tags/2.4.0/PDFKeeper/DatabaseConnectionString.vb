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

Public Class DatabaseConnectionString
	Friend Shared oraConnectionString As New SecureString
	Dim userName As String
	Dim password As String
	Dim dataSource As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg1: userName"></param>
	''' <param name="arg2: password"></param>
	''' <param name="arg3: dataSource"></param>
	Public Sub New(ByVal arg1 As String, ByVal arg2 As String, _
				   ByVal arg3 As String)
		userName = arg1
		password = arg2
		datasource = arg3
	End Sub
	
	''' <summary>
	''' This subroutine will create the database connection string that is
	''' used whenever a database connection is opened.
	''' </summary>
	Public Sub Create
		Dim tempConnectionString As String = Nothing
		Dim oGCHandle1 As GCHandle = GCHandle.Alloc(password, _
									 GCHandleType.Pinned)
		Dim oGCHandle2 As GCHandle = GCHandle.Alloc(tempConnectionString, _
									 GCHandleType.Pinned)
		tempConnectionString = "User Id=" + userName + ";" & _
							   "Password=" + password + ";" & _
							   "Data Source=" + dataSource + ";" & _
							   "Persist Security Info=False;Pooling=False"
		For i = 0 To tempConnectionString.Length - 1
			oraConnectionString.AppendChar(tempConnectionString.Chars(i))
		Next i
		NativeMethods.ZeroMemory(password, password.Length * 2)
		NativeMethods.ZeroMemory(tempConnectionString, _
								 tempConnectionString.Length * 2)
		oGCHandle1.Free
		oGCHandle2.Free
	End Sub
	
	''' <summary>
	''' This subroutine will dispose the database connection string.  It should
	''' be called when the application is closing.
	''' </summary>
	Public Shared Sub Dispose
		oraConnectionString.Dispose
	End Sub
End Class
