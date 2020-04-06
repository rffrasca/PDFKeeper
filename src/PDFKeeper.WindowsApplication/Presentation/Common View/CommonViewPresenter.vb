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
Public Class CommonViewPresenter
    Implements IDisposable
    Private view As ICommonView
    Private model As IDocumentRepository
    Private messageDisplay As IMessageDisplay = New MessageDisplay

    Public Sub New(view As ICommonView)
        Me.view = view
    End Sub

    Public Sub GetColumnItemsByGroup()
        'Called by <Name>ComboBox.Enter
        Try
            model = New DocumentRepository
            view.OnLongRunningOperationStarted()
            If view.ActiveElement.StartsWith("AuthorPaired", _
                                             StringComparison.Ordinal) Then
                GetAuthors(True)
            ElseIf view.ActiveElement.StartsWith("SubjectPaired", _
                                                 StringComparison.Ordinal) Then
                GetSubjectsByAuthor()
            ElseIf view.ActiveElement.StartsWith("Author", _
                                                 StringComparison.Ordinal) Then
                GetAuthors(False)
            ElseIf view.ActiveElement.StartsWith("Subject", _
                                                 StringComparison.Ordinal) Then
                GetSubjects()
            ElseIf view.ActiveElement.StartsWith("Category", _
                                                 StringComparison.Ordinal) Then
                GetCategories()
            End If
            view.OnLongRunningOperationFinished()
        Catch ex As OracleException
            view.OnLongRunningOperationFinished()
            messageDisplay.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub ActiveElementTextTrimStart()
        ' Called by <Name>ComboBox.TextChanged and <Name>ComboBox.TextUpdate
        If view.ActiveElement.StartsWith("AuthorPaired", _
                                         StringComparison.Ordinal) Then
            view.AuthorPaired = view.AuthorPaired.TrimStart
        ElseIf view.ActiveElement.StartsWith("SubjectPaired", _
                                             StringComparison.Ordinal) Then
            view.SubjectPaired = view.SubjectPaired.TrimStart
        ElseIf view.ActiveElement.StartsWith("Author", _
                                             StringComparison.Ordinal) Then
            view.Author = view.Author.TrimStart
        ElseIf view.ActiveElement.StartsWith("Subject", _
                                             StringComparison.Ordinal) Then
            view.Subject = view.Subject.TrimStart
        ElseIf view.ActiveElement.StartsWith("Category", _
                                             StringComparison.Ordinal) Then
            view.Category = view.Category.TrimStart
        End If
    End Sub

    Public Sub AllElementsTextTrimEnd()
        AuthorPairedTextTrimEnd()
        SubjectPairedTextTrimEnd()
        AuthorTextTrimEnd()
        SubjectTextTrimEnd()
        CategoryTextTrimEnd()
    End Sub

    Private Sub GetAuthors(ByVal paired As Boolean)
        Try
            Dim currentItem As String = Nothing
            If paired Then
                currentItem = view.AuthorPaired
                view.AuthorsPaired = model.GetAllAuthors
                view.AuthorPaired = currentItem
            Else
                currentItem = view.Author
                view.Authors = model.GetAllAuthors
                view.Author = currentItem
            End If
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjects()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.Subject
            view.Subjects = model.GetAllSubjects
            view.Subject = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjectsByAuthor()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.SubjectPaired
            view.SubjectsPaired = model.GetAllSubjectsByAuthor(view.AuthorPaired)
            view.SubjectPaired = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetCategories()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.Category
            view.Categories = model.GetAllCategories
            view.Category = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub AuthorPairedTextTrimEnd()
        If Not view.AuthorPaired = Nothing Then
            view.AuthorPaired = view.AuthorPaired.TrimEnd
        End If
    End Sub

    Private Sub SubjectPairedTextTrimEnd()
        If Not view.SubjectPaired = Nothing Then
            view.SubjectPaired = view.SubjectPaired.TrimEnd
        End If
    End Sub

    Private Sub AuthorTextTrimEnd()
        If Not view.Author = Nothing Then
            view.Author = view.Author.TrimEnd
        End If
    End Sub

    Private Sub SubjectTextTrimEnd()
        If Not view.Subject = Nothing Then
            view.Subject = view.Subject.TrimEnd
        End If
    End Sub

    Private Sub CategoryTextTrimEnd()
        If Not view.Category = Nothing Then
            view.Category = view.Category.TrimEnd
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                model.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
