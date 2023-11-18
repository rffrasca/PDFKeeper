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

Public Class SecureTextBox
    Private _SecureText As SecureString

    ''' <summary>
    ''' Gets the text entered in the text box securely.
    ''' </summary>
    ''' <returns>The SecureString object.</returns>
    Public ReadOnly Property SecureText As SecureString
        Get
            Return _SecureText
        End Get
    End Property

    ''' <summary>
    ''' Constructs the SecureString for the current instance of the SecureTextBox class.
    ''' 
    ''' This method is called by the constructor of this class and is to be called when the
    ''' SecureString needs to be constructed after it has been disposed, allowing for re-entry in
    ''' the same instance of the implementing view.
    ''' </summary>
    Public Sub ConstructSecureString()
        _SecureText = New SecureString
    End Sub

    Private Sub SecureTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Delete Then
            ' Remove the character after the cursor or all selected characters from the SecureString.
            If SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
            ElseIf SelectionStart < Text.Length Then
                SecureText.RemoveAt(SelectionStart)
            End If
            SetTextBoxTextAndCursorPosition(SelectionStart)
            e.Handled = True
        ElseIf e.KeyCode.Equals(Keys.Escape) Or e.KeyCode.Equals(Keys.Enter) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SecureTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = ControlChars.Back Then
            ' Remove the character before the cursor or all selected characters from the
            ' SecureString.
            If Me.SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
                SetTextBoxTextAndCursorPosition(SelectionStart)
            ElseIf SelectionStart > 0 Then
                SecureText.RemoveAt(SelectionStart - 1)
                SetTextBoxTextAndCursorPosition(SelectionStart - 1)
            End If
        Else
            If Me.SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
            End If
            ' Insert printable character into SecureString.
            SecureText.InsertAt(SelectionStart, e.KeyChar)

            SetTextBoxTextAndCursorPosition(SelectionStart + 1)
        End If
        e.Handled = True
    End Sub

    Private Sub RemoveSelectedCharsFromSecureString()
        For i As Integer = 0 To SelectionLength - 1
            SecureText.RemoveAt(SelectionStart)
        Next
    End Sub

    Private Sub SetTextBoxTextAndCursorPosition(cursorPosition As Integer)
        ' Set the Text property of the TextBox to a string of asterisks matching the length of the
        ' SecureString.
        Text = New String("*", SecureText.Length)
        ' This next step sets the cursor position to the specified value. It must be performed
        ' after setting the Text property and cannot be moved out of this method.
        SelectionStart = cursorPosition
    End Sub
End Class
