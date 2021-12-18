'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
Public NotInheritable Class UserIdentification
    ''' <summary>
    ''' Returns the name of the user logged into the operating system when the
    ''' database platform is SQLite or the database user account name.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property AccountName As String
        Get
            If DbInstanceProperties.Platform =
                DatabasePlatform.Sqlite.ToString Then
                Return Environment.UserName
            Else
                Return DbInstanceProperties.UserName
            End If
        End Get
    End Property
End Class
