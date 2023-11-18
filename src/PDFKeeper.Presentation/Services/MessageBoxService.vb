' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Imports PDFKeeper.Core.Services

Public Class MessageBoxService
    Implements IMessageBoxService
    Private ReadOnly options As MessageBoxOptions
    Private buttons As MessageBoxButtons

    Public Sub New()
        If CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft Then
            options = MessageBoxOptions.RtlReading
        Else
            options = 0
        End If
    End Sub

    Public Sub ShowMessage(message As String, isError As Boolean) Implements IMessageBoxService.ShowMessage
        Dim icon = MessageBoxIcon.Information
        If isError Then
            icon = MessageBoxIcon.Error
        End If
        MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, icon,
                        MessageBoxDefaultButton.Button1, options)
    End Sub

    Public Function ShowQuestion(message As String, showCancel As Boolean) As Integer Implements IMessageBoxService.ShowQuestion
        If showCancel Then
            buttons = MessageBoxButtons.YesNoCancel
        Else
            buttons = MessageBoxButtons.YesNo
        End If
        Return Convert.ToInt32(MessageBox.Show(message, Application.ProductName, buttons,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1, options))
    End Function
End Class
