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
Imports System.Windows

Public Class CommonPresenter
    Implements IDisposable
    Private ReadOnly view As ICommonView
    Private model As IDocumentRepository
    Private ReadOnly message As IMessageDisplayService =
        New MessageDisplayService

    Public Sub New(view As ICommonView)
        Me.view = view
    End Sub

    Public Sub GetColumnItemsByGroup()
        'Called by <Name>ComboBox.Enter and <Name>ComboBox.DropDown
        Try
            model = New DocumentRepository
            view.SetCursor(True)
            If view.ActiveElement.StartsWith("AuthorGroup",
                                             StringComparison.Ordinal) Then
                GetAuthorsByGroup()
            ElseIf view.ActiveElement.StartsWith("SubjectGroup",
                                                 StringComparison.Ordinal) Then
                GetSubjectsByGroup()
            ElseIf view.ActiveElement.StartsWith("CategoryGroup",
                                                 StringComparison.Ordinal) Then
                GetCategoriesByGroup()
            ElseIf view.ActiveElement.StartsWith("AuthorPaired",
                                                 StringComparison.Ordinal) Then
                GetAuthors("AuthorPaired")
            ElseIf view.ActiveElement.StartsWith("SubjectPaired",
                                                 StringComparison.Ordinal) Then
                GetSubjectsByAuthor("SubjectPaired")
            ElseIf view.ActiveElement.StartsWith("Category",
                                                 StringComparison.Ordinal) Then
                GetCategories("Category")
            End If
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub ActiveElementTextTrimStart()
        ' Called by <Name>ComboBox.TextChanged and <Name>ComboBox.TextUpdate
        If view.ActiveElement.StartsWith("AuthorPaired",
                                         StringComparison.Ordinal) Then
            view.AuthorPaired = view.AuthorPaired.TrimStart
        ElseIf view.ActiveElement.StartsWith("SubjectPaired",
                                             StringComparison.Ordinal) Then
            view.SubjectPaired = view.SubjectPaired.TrimStart
        ElseIf view.ActiveElement.StartsWith("Category",
                                             StringComparison.Ordinal) Then
            view.Category = view.Category.TrimStart
        End If
    End Sub

    Public Sub AllElementsTextTrimEnd()
        If Not view.Category = Nothing Then
            view.Category = view.Category.TrimEnd
        End If
    End Sub

    Private Sub GetAuthorsByGroup()
        If view.SubjectGroup Is Nothing And view.CategoryGroup Is Nothing Then
            GetAuthors("AuthorGroup")
        ElseIf view.SubjectGroup IsNot Nothing And view.CategoryGroup Is Nothing Then
            GetAuthorsBySubject()
        ElseIf view.SubjectGroup Is Nothing And view.CategoryGroup IsNot Nothing Then
            GetAuthorsByCategory()
        ElseIf view.SubjectGroup IsNot Nothing And view.CategoryGroup IsNot Nothing Then
            GetAuthorsBySubjectAndCategory()
        End If
    End Sub

    Private Sub GetSubjectsByGroup()
        If view.AuthorGroup Is Nothing And view.CategoryGroup Is Nothing Then
            GetSubjects()
        ElseIf view.AuthorGroup IsNot Nothing And view.CategoryGroup Is Nothing Then
            GetSubjectsByAuthor("SubjectGroup")
        ElseIf view.AuthorGroup Is Nothing And view.CategoryGroup IsNot Nothing Then
            GetSubjectsByCategory()
        ElseIf view.AuthorGroup IsNot Nothing And view.CategoryGroup IsNot Nothing Then
            GetSubjectsByAuthorAndCategory()
        End If
    End Sub

    Private Sub GetCategoriesByGroup()
        If view.AuthorGroup Is Nothing And view.SubjectGroup Is Nothing Then
            GetCategories("CategoryGroup")
        ElseIf view.AuthorGroup IsNot Nothing And view.SubjectGroup Is Nothing Then
            GetCategoriesByAuthor()
        ElseIf view.AuthorGroup Is Nothing And view.SubjectGroup IsNot Nothing Then
            GetCategoriesBySubject()
        ElseIf view.AuthorGroup IsNot Nothing And view.SubjectGroup IsNot Nothing Then
            GetCategoriesByAuthorAndSubject()
        End If
    End Sub

    Private Sub GetAuthors(ByVal elementStart As String)
        Try
            Dim currentItem As String = Nothing
            If elementStart = "AuthorGroup" Then
                currentItem = view.AuthorGroup
                view.AuthorsGroup = model.GetAllAuthors
                view.AuthorGroup = currentItem
            ElseIf elementStart = "AuthorPaired" Then
                currentItem = view.AuthorPaired
                view.AuthorsPaired = model.GetAllAuthors
                view.AuthorPaired = currentItem
            End If
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetAuthorsBySubject()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.AuthorGroup
            view.AuthorsGroup = model.GetAllAuthorsBySubject(view.SubjectGroup)
            view.AuthorGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetAuthorsByCategory()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.AuthorGroup
            view.AuthorsGroup = model.GetAllAuthorsByCategory(view.CategoryGroup)
            view.AuthorGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetAuthorsBySubjectAndCategory()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.AuthorGroup
            view.AuthorsGroup = model.GetAllAuthorsBySubjectAndCategory(view.SubjectGroup,
                                                                        view.CategoryGroup)
            view.AuthorGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjects()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.SubjectGroup
            view.SubjectsGroup = model.GetAllSubjects
            view.SubjectGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjectsByAuthor(ByVal elementStart As String)
        Try
            Dim currentItem As String = Nothing
            If elementStart = "SubjectGroup" Then
                currentItem = view.SubjectGroup
                view.SubjectsGroup = model.GetAllSubjectsByAuthor(view.AuthorGroup)
                view.SubjectGroup = currentItem
            ElseIf elementStart = "SubjectPaired" Then
                currentItem = view.SubjectPaired
                view.SubjectsPaired = model.GetAllSubjectsByAuthor(view.AuthorPaired)
                view.SubjectPaired = currentItem
            End If
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjectsByCategory()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.SubjectGroup
            view.SubjectsGroup = model.GetAllSubjectsByCategory(view.CategoryGroup)
            view.SubjectGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetSubjectsByAuthorAndCategory()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.SubjectGroup
            view.SubjectsGroup = model.GetAllSubjectsByAuthorAndCategory(view.AuthorGroup,
                                                                         view.CategoryGroup)
            view.SubjectGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetCategories(ByVal elementStart As String)
        Try
            Dim currentItem As String = Nothing
            If elementStart = "CategoryGroup" Then
                currentItem = view.CategoryGroup
                view.CategoriesGroup = model.GetAllCategories
                view.CategoryGroup = currentItem
            ElseIf elementStart = "Category" Then
                currentItem = view.Category
                view.Categories = model.GetAllCategories
                view.Category = currentItem
            End If
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetCategoriesByAuthor()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.CategoryGroup
            view.CategoriesGroup = model.GetAllCategoriesByAuthor(view.AuthorGroup)
            view.CategoryGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetCategoriesBySubject()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.CategoryGroup
            view.CategoriesGroup = model.GetAllCategoriesBySubject(view.SubjectGroup)
            view.CategoryGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetCategoriesByAuthorAndSubject()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.CategoryGroup
            view.CategoriesGroup = model.GetAllCategoriesByAuthorAndSubject(view.AuthorGroup,
                                                                            view.SubjectGroup)
            view.CategoryGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Try
                    model.Dispose()
                Catch ex As NullReferenceException
                End Try
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
