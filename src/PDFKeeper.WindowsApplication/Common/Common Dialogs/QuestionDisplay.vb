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
Public Class QuestionDisplay
    Implements IQuestionDisplay
    Private options As MessageBoxOptions
    Private buttons As MessageBoxButtons

    Public Function Show(question As String, cancelVisible As Boolean) As DialogResult Implements IQuestionDisplay.Show
        SetOptions()
        If cancelVisible Then
            buttons = MessageBoxButtons.YesNoCancel
        Else
            buttons = MessageBoxButtons.YesNo
        End If
        Return MessageBox.Show(question, _
                               Application.ProductName, _
                               buttons, _
                               MessageBoxIcon.Question, _
                               MessageBoxDefaultButton.Button1, _
                               options)
    End Function

    Public Function ShowFormClosingPrompt() As Boolean Implements IQuestionDisplay.ShowFormClosingPrompt
        If Show(My.Resources.GenericClosePrompt, _
                False) = Windows.Forms.DialogResult.Yes Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SetOptions()
        If CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft Then
            options = MessageBoxOptions.RtlReading
        Else
            options = 0
        End If
    End Sub
End Class
