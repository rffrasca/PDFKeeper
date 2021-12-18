'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
            ElseIf view.ActiveElement.StartsWith("TaxYearGroup",
                                             StringComparison.Ordinal) Then
                GetTaxYearsByGroup()
            ElseIf view.ActiveElement.StartsWith("AuthorPaired",
                                                 StringComparison.Ordinal) Then
                GetAuthors("AuthorPaired")
            ElseIf view.ActiveElement.StartsWith("SubjectPaired",
                                                 StringComparison.Ordinal) Then
                GetSubjectsByAuthor("SubjectPaired")
            ElseIf view.ActiveElement.StartsWith("Category",
                                                 StringComparison.Ordinal) Then
                GetCategories("Category")
            ElseIf view.ActiveElement.StartsWith("TaxYear",
                                                 StringComparison.Ordinal) Then
                GetTaxYears("TaxYear")
            End If
            view.SetCursor(False)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub FillTaxYears()
        With view
            .TaxYears = TaxYearsGenerator.ToArray
        End With
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
        ElseIf view.ActiveElement.StartsWith("TaxYear",
                                             StringComparison.Ordinal) Then
            view.TaxYear = view.TaxYear.TrimStart
        End If
    End Sub

    Public Sub AllElementsTextTrimEnd()
        If Not view.Category = Nothing Then
            view.Category = view.Category.TrimEnd
        End If
    End Sub

    Private Sub GetAuthorsByGroup()
        If view.SubjectGroup Is Nothing And
            view.CategoryGroup Is Nothing And
            view.TaxYearGroup Is Nothing Then
            GetAuthors("AuthorGroup")
        Else
            GetAuthorsBySubjectCategoryAndTaxYear()
        End If
    End Sub

    Private Sub GetSubjectsByGroup()
        If view.AuthorGroup Is Nothing And
            view.CategoryGroup Is Nothing And
            view.TaxYearGroup Is Nothing Then
            GetSubjects()
        Else
            GetSubjectsByAuthorCategoryAndTaxYear()
        End If
    End Sub

    Private Sub GetCategoriesByGroup()
        If view.AuthorGroup Is Nothing And
            view.SubjectGroup Is Nothing And
            view.TaxYearGroup Is Nothing Then
            GetCategories("CategoryGroup")
        Else
            GetCategoriesByAuthorSubjectAndTaxYear()
        End If
    End Sub

    Private Sub GetTaxYearsByGroup()
        If view.AuthorGroup Is Nothing And
            view.SubjectGroup Is Nothing And
            view.CategoryGroup Is Nothing Then
            GetTaxYears("TaxYearGroup")
        Else
            GetTaxYearsByAuthorSubjectAndCategory()
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

    Private Sub GetAuthorsBySubjectCategoryAndTaxYear()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.AuthorGroup
            view.AuthorsGroup = model.GetAllAuthorsBySubjectCategoryAndTaxYear(view.SubjectGroup,
                                                                               view.CategoryGroup,
                                                                               view.TaxYearGroup)
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

    Private Sub GetSubjectsByAuthorCategoryAndTaxYear()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.SubjectGroup
            view.SubjectsGroup = model.GetAllSubjectsByAuthorCategoryAndTaxYear(view.AuthorGroup,
                                                                                view.CategoryGroup,
                                                                                view.TaxYearGroup)
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

    Private Sub GetCategoriesByAuthorSubjectAndTaxYear()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.CategoryGroup
            view.CategoriesGroup = model.GetAllCategoriesByAuthorSubjectAndTaxYear(view.AuthorGroup,
                                                                                   view.SubjectGroup,
                                                                                   view.TaxYearGroup)
            view.CategoryGroup = currentItem
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetTaxYears(ByVal elementStart As String)
        Try
            Dim currentItem As String = Nothing
            If elementStart = "TaxYearGroup" Then
                currentItem = view.TaxYearGroup
                view.TaxYearsGroup = model.GetAllTaxYears
                view.TaxYearGroup = currentItem
            ElseIf elementStart = "TaxYear" Then
                currentItem = view.TaxYear
                view.TaxYears = model.GetAllTaxYears
                view.TaxYear = currentItem
            End If
        Catch ex As NotImplementedException ' When ComboBox is a DropDownList
        End Try
    End Sub

    Private Sub GetTaxYearsByAuthorSubjectAndCategory()
        Try
            Dim currentItem As String = Nothing
            currentItem = view.TaxYearGroup
            view.TaxYearsGroup = model.GetAllTaxYearsByAuthorSubjectAndCategory(view.AuthorGroup,
                                                                                view.SubjectGroup,
                                                                                view.CategoryGroup)
            view.TaxYearGroup = currentItem
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
