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
Imports PDFKeeper.Domain

Public Interface ILoginService
    ''' <summary>
    ''' Initializes a client server database session and performs a test connection.
    ''' 
    ''' NotSupportedException will be throw when the database platform is SQLite.
    ''' </summary>
    ''' <param name="platform">Database platform</param>
    ''' <param name="model">DbSessionModel object</param>
    Sub InitSession(ByVal platform As String, ByVal model As DbSessionModel)

    ''' <summary>
    ''' Resets the database credential object for the database session.
    ''' </summary>
    Sub ResetCredential()
End Interface
