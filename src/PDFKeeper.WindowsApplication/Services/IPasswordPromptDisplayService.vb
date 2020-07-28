'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Interface IPasswordPromptDisplayService
    ''' <summary>
    ''' Gets/Sets title to be displayed on the Password Prompt view.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Title As String

    ''' <summary>
    ''' Gets/Sets text to be displayed above the text box on the Password
    ''' Prompt view.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TextLabel As String

    ''' <summary>
    ''' Shows the Password Prompt view.
    ''' </summary>
    ''' <returns>SecureString object containing the password entered.</returns>
    ''' <remarks></remarks>
    Function Show() As SecureString
End Interface
