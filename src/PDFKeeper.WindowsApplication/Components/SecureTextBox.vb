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
Public Class SecureTextBox
    Inherits System.Windows.Forms.TextBox
    Private m_SecureText As SecureString

    Public Sub New()
        Me.ShortcutsEnabled = False
        ConstructSecureString()
    End Sub

    Public ReadOnly Property SecureText As SecureString
        Get
            Return m_SecureText
        End Get
    End Property

    ''' <summary>
    ''' Constructs the SecureString for the instance of this class.
    ''' 
    ''' This method is called by the constructor of this class and is to be
    ''' called when the SecureString needs to be constructed after it has been
    ''' disposed, allowing for re-entry in the same instance of the
    ''' implementing form.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConstructSecureString()
        m_SecureText = New SecureString
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException(NameOf(e))
        End If
        If e.KeyCode = Keys.Delete Then
            ' Remove the character after the cursor or all selected characters from
            ' the SecureString.
            If Me.SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
            ElseIf Me.SelectionStart < Me.Text.Length Then
                m_SecureText.RemoveAt(Me.SelectionStart)
            End If
            SetTextBoxTextAndCursorPosition(Me.SelectionStart)
            e.Handled = True
        ElseIf e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException(NameOf(e))
        End If
        If e.KeyChar = ControlChars.Back Then
            ' Remove the character before the cursor or all selected characters from the
            ' SecureString.
            If Me.SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
                SetTextBoxTextAndCursorPosition(Me.SelectionStart)
            ElseIf Me.SelectionStart > 0 Then
                m_SecureText.RemoveAt(Me.SelectionStart - 1)
                SetTextBoxTextAndCursorPosition(Me.SelectionStart - 1)
            End If
        Else
            If Me.SelectionLength > 0 Then
                RemoveSelectedCharsFromSecureString()
            End If
            ' Insert printable character into SecureString.
            m_SecureText.InsertAt(Me.SelectionStart, e.KeyChar)

            SetTextBoxTextAndCursorPosition(Me.SelectionStart + 1)
        End If
        e.Handled = True
    End Sub

    Private Sub RemoveSelectedCharsFromSecureString()
        For i As Integer = 0 To Me.SelectionLength - 1
            m_SecureText.RemoveAt(Me.SelectionStart)
        Next
    End Sub

    Private Sub SetTextBoxTextAndCursorPosition(ByVal cursorPosition As Integer)
        ' Set the Text property of the TextBox to a string of asterisks matching
        ' the length of the SecureString.
        Me.Text = New String(CChar("*"), m_SecureText.Length)
        ' This next step sets the cursor position to the specified value.  It
        ' must be performed after setting Text property and cannot be moved out
        ' of this method.
        Me.SelectionStart = cursorPosition
    End Sub
End Class
