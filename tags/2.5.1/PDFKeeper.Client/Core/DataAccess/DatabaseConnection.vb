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

Public Class DatabaseConnection
	Implements IDisposable
	Private isDisposed As Boolean
	Friend oraConnection As New OracleConnection
	
	''' <summary>
	''' This function will open a database connection.  If the connection fails to
	''' open, the connection object is closed and disposed.
	''' </summary>
	''' <returns>0 = Successful, 1 = Failed</returns>
	Public Function Open() As Byte
		Dim result As Byte = 0
		Dim connectionStringPtr As IntPtr
		connectionStringPtr = Marshal.SecureStringToBSTR( _
							  DatabaseConnectionString.oraConnectionString)
		Try
			oraConnection.ConnectionString = Marshal.PtrToStringBSTR(connectionStringPtr)
			oraConnection.Open
			DatabaseConnectionString.oraConnectionString.MakeReadOnly()
  		Catch ex As OracleException
			Dispose
			Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
			oMessageDialog.DisplayError
			result = 1
  		Finally
  			Marshal.ZeroFreeBSTR(connectionStringPtr)
		End Try
		Return result
	End Function
	
	''' <summary>
	''' This subroutine will close the active database connection and will
	''' dispose the database connection object.
	''' </summary>
	''' <param name="disposing"></param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.isDisposed Then
			If disposing Then
				oraConnection.Close
				oraConnection.Dispose
				oraConnection = Nothing
			End If
		End If
		Me.isDisposed = True
	End Sub
	
	''' <summary>
	''' This subroutine will call the protected Dispose subroutine.  It is
	''' called by the consumer of the object when a database operation has
	''' completed.
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
