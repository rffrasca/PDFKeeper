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

Imports PDFKeeper.Core.ViewModels

Friend Class TextBoxHelper
    ''' <summary>
    ''' Gets the text box with focus.
    ''' </summary>
    ''' <returns>The TextBox object.</returns>
    Friend Shared Function GetFocusedTextBox(view As MainView) As TextBox
        Dim result = Nothing
        If view.NotesTextBox.Focused Then
            result = view.NotesTextBox
        ElseIf view.KeywordsTextBox.Focused Then
            result = view.KeywordsTextBox
        ElseIf view.TextTextBox.Focused Then
            result = view.TextTextBox
        ElseIf view.SearchTermSnippetsTextBox.Focused Then
            result = view.SearchTermSnippetsTextBox
        End If
        Return result
    End Function

    ''' <summary>
    ''' Syncs the ViewModel SelectedText property for a text box.
    ''' </summary>
    ''' <param name="textBox">The TextBox object.</param>
    ''' <param name="view">The MainView instance.</param>
    ''' <param name="viewModel">The MainViewModel instance.</param>
    Friend Shared Sub SyncSelectedTextWithViewModel(textBox As TextBox, view As MainView,
                                                    viewModel As MainViewModel)
        If textBox.Equals(view.NotesTextBox) Then
            viewModel.SelectedNotes = view.NotesTextBox.SelectedText
        ElseIf textBox.Equals(view.KeywordsTextBox) Then
            viewModel.SelectedKeywords = view.KeywordsTextBox.SelectedText
        ElseIf textBox.Equals(view.TextTextBox) Then
            viewModel.SelectedText = view.TextTextBox.SelectedText
        ElseIf textBox.Equals(view.SearchTermSnippetsTextBox) Then
            viewModel.SelectedSearchTermSnippets = view.SearchTermSnippetsTextBox.SelectedText
        End If
    End Sub
End Class
