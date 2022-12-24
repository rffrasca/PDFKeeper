'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Imports System.Globalization
Imports System.Windows.Forms

Public Class CommonDialogs
    Private ReadOnly options As MessageBoxOptions
    Private buttons As MessageBoxButtons

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    Public Sub New()
        If CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft Then
            options = MessageBoxOptions.RtlReading
        Else
            options = 0
        End If
    End Sub

    ''' <summary>
    ''' Shows a message box.
    ''' </summary>
    ''' <param name="message">Message</param>
    ''' <param name="isError">Is message an error? True or False</param>
    Public Sub ShowMessageBox(ByVal message As String, ByVal isError As Boolean)
        Dim icon = MessageBoxIcon.Information
        If isError Then
            icon = MessageBoxIcon.Error
        End If
        MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, icon, MessageBoxDefaultButton.Button1,
                        options)
    End Sub

    ''' <summary>
    ''' Shows a message box that asks a question.
    ''' </summary>
    ''' <param name="question">Question</param>
    ''' <param name="cancelVisible">Cancel visible? True or False</param>
    ''' <returns>Yes, No, or Cancel</returns>
    Public Function ShowQuestionMessageBox(ByVal question As String, ByVal cancelVisible As Boolean) As DialogResult
        If cancelVisible Then
            buttons = MessageBoxButtons.YesNoCancel
        Else
            buttons = MessageBoxButtons.YesNo
        End If
        Return MessageBox.Show(question, Application.ProductName, buttons, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button1, options)
    End Function
End Class
