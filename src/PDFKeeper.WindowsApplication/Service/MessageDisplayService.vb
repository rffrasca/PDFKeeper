'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
'* Copyright (C) 2009-2019 Robert F. Frasca
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
Public Class MessageDisplayService
    Implements IMessageDisplayService
    Private options As MessageBoxOptions
    Private icon As MessageBoxIcon
    Private buttons As MessageBoxButtons

    Public Sub New()
        If CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft Then
            options = MessageBoxOptions.RtlReading
        Else
            options = 0
        End If
    End Sub

    Public Sub ShowError(message As String) Implements IMessageDisplayService.ShowError
        buttons = MessageBoxButtons.OK
        icon = MessageBoxIcon.Error
        ShowMessageBox(message)
    End Sub

    Public Sub ShowInformation(message As String) Implements IMessageDisplayService.ShowInformation
        buttons = MessageBoxButtons.OK
        icon = MessageBoxIcon.Information
        ShowMessageBox(message)
    End Sub

    Public Function ShowQuestion(message As String, cancelButton As Boolean) As DialogResult Implements IMessageDisplayService.ShowQuestion
        If cancelButton Then
            buttons = MessageBoxButtons.YesNoCancel
        Else
            buttons = MessageBoxButtons.YesNo
        End If
        icon = MessageBoxIcon.Question
        Return ShowMessageBox(message)
    End Function

    Private Function ShowMessageBox(ByVal message As String) As DialogResult
        Return MessageBox.Show(message, _
                               Application.ProductName, _
                               buttons, _
                               icon, _
                               MessageBoxDefaultButton.Button1, _
                               options)
    End Function
End Class
