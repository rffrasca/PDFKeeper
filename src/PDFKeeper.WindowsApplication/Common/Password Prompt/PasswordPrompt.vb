'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
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
Public Class PasswordPrompt
    Implements IPasswordPrompt

    Public Property Title As String Implements IPasswordPrompt.Title
    Public Property TextLabel As String Implements IPasswordPrompt.TextLabel

    Public Function Show() As SecureString Implements IPasswordPrompt.Show
        Using dialog As New PasswordDialog(Title, TextLabel)
            dialog.ShowDialog()
            If dialog.DialogResult = DialogResult.OK Then
                Return dialog.Password
            Else
                Return Nothing
            End If
        End Using
    End Function
End Class
