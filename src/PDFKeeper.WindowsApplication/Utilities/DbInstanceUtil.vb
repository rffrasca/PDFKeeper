'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Public NotInheritable Class DbInstanceUtil
    Private Sub New()
        ' All methods are shared.
    End Sub

    ''' <summary>
    ''' Performs a test connection to the database.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub TestConnection()
        If DbInstanceProperties.Platform =
            DatabasePlatform.Oracle.ToString Then
            Using provider As New OracleDbDataProvider
                Try
                    provider.TestConnection()
                Catch ex As OracleException
                    Throw New CustomDbException(ex.Message)
                End Try
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Resets the credential object that contains the database user name and
    ''' encrypted password.  This should be called when a test connection to
    ''' the database fails.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ResetCredential()
        If DbInstanceProperties.Platform =
            DatabasePlatform.Oracle.ToString Then
            Using provider As New OracleDbDataProvider
                provider.ResetCredential()
            End Using
        End If
    End Sub
End Class
