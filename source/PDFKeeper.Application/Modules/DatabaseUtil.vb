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

Public Module DatabaseUtil
	''' <summary>
	''' Checks if can connect to the database.
	''' </summary>
	''' <returns>True or False</returns>
	Public Function CanConnectToDatabase As Boolean
		Using oraConnection As New OracleConnection
			Try
				oraConnection.ConnectionString = _
					DatabaseConnectionString.Instance.ConnectionString
				oraConnection.Open
				Return True
			Catch ex As OracleException
				ShowError(ex.Message.ToString())
				Return False
			End Try
		End Using
	End Function
End Module
