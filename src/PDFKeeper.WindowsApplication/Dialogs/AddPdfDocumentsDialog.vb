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
Imports System.Windows.Forms

Public Class AddPdfDocumentsDialog
    Implements IAddPdfDocumentsView
    Private ReadOnly commonPresenter As CommonPresenter
    Private ReadOnly presenter As AddPdfDocumentsPresenter
    Private ReadOnly help As IHelpDisplayService = New HelpDisplayService

    Public Sub New()
        InitializeComponent()
        commonPresenter = New CommonPresenter(Me)
        presenter = New AddPdfDocumentsPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
    End Sub

#Region "Interface Members"
    Public Property SelectedPdfPath As String Implements IAddPdfDocumentsView.SelectedPdfPath
        Get
            Return SelectedPdfDocumentTextBox.Text
        End Get
        Set(value As String)
            SelectedPdfDocumentTextBox.Text = value
        End Set
    End Property

    Public Property SelectEnabled As Boolean Implements IAddPdfDocumentsView.SelectEnabled
        Get
            Return SelectButton.Enabled
        End Get
        Set(value As Boolean)
            SelectButton.Enabled = value
        End Set
    End Property

    Public Property ViewEnabled As Boolean Implements IAddPdfDocumentsView.ViewEnabled
        Get
            Return ViewButton.Enabled
        End Get
        Set(value As Boolean)
            ViewButton.Enabled = value
        End Set
    End Property

    Public Property Title As String Implements IAddPdfDocumentsView.Title
        Get
            Return TitleTextBox.Text
        End Get
        Set(value As String)
            TitleTextBox.Text = value
        End Set
    End Property

    Public Property TitleEnabled As Boolean Implements IAddPdfDocumentsView.TitleEnabled
        Get
            Return TitleTextBox.Enabled
        End Get
        Set(value As Boolean)
            TitleTextBox.Enabled = value
        End Set
    End Property

    Public Property SetTitleToFileNameEnabled As Boolean Implements IAddPdfDocumentsView.SetTitleToFileNameEnabled
        Get
            Return SetTitleToFileNameButton.Enabled
        End Get
        Set(value As Boolean)
            SetTitleToFileNameButton.Enabled = value
        End Set
    End Property

    Public Property AuthorsPaired As DataTable Implements ICommonView.AuthorsPaired
        Get
            Return AuthorPairedComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorPairedComboBox.DataSource = value
            AuthorPairedComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property AuthorPaired As String Implements ICommonView.AuthorPaired
        Get
            Return AuthorPairedComboBox.Text
        End Get
        Set(value As String)
            AuthorPairedComboBox.Text = value
        End Set
    End Property

    Public Property AuthorPairedEnabled As Boolean Implements IAddPdfDocumentsView.AuthorPairedEnabled
        Get
            Return AuthorPairedComboBox.Enabled
        End Get
        Set(value As Boolean)
            AuthorPairedComboBox.Enabled = value
        End Set
    End Property

    Public Property SubjectsPaired As DataTable Implements ICommonView.SubjectsPaired
        Get
            Return SubjectPairedComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectPairedComboBox.DataSource = value
            SubjectPairedComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property SubjectPaired As String Implements ICommonView.SubjectPaired
        Get
            Return SubjectPairedComboBox.Text
        End Get
        Set(value As String)
            SubjectPairedComboBox.Text = value
        End Set
    End Property

    Public Property SubjectPairedEnabled As Boolean Implements IAddPdfDocumentsView.SubjectPairedEnabled
        Get
            Return SubjectPairedComboBox.Enabled
        End Get
        Set(value As Boolean)
            SubjectPairedComboBox.Enabled = value
        End Set
    End Property

    Public Property Keywords As String Implements IAddPdfDocumentsView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property KeywordsEnabled As Boolean Implements IAddPdfDocumentsView.KeywordsEnabled
        Get
            Return KeywordsTextBox.Enabled
        End Get
        Set(value As Boolean)
            KeywordsTextBox.Enabled = value
        End Set
    End Property

    Public Property Categories As DataTable Implements ICommonView.Categories
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = "doc_category"
        End Set
    End Property

    Public Property Category As String Implements ICommonView.Category
        Get
            Return CategoryComboBox.Text
        End Get
        Set(value As String)
            CategoryComboBox.Text = value
        End Set
    End Property

    Public Property CategoryEnabled As Boolean Implements IAddPdfDocumentsView.CategoryEnabled
        Get
            Return CategoryComboBox.Enabled
        End Get
        Set(value As Boolean)
            CategoryComboBox.Enabled = value
        End Set
    End Property

    Public Property FlagDocumentEnabled As Boolean Implements IAddPdfDocumentsView.FlagDocumentEnabled
        Get
            Return FlagDocumentCheckBox.Enabled
        End Get
        Set(value As Boolean)
            FlagDocumentCheckBox.Enabled = value
        End Set
    End Property

    Public Property DeleteSelectedPdfOnOkEnabled As Boolean Implements IAddPdfDocumentsView.DeleteSelectedPdfOnOkEnabled
        Get
            Return DeleteSelectedPdfOnOKCheckBox.Enabled
        End Get
        Set(value As Boolean)
            DeleteSelectedPdfOnOKCheckBox.Enabled = value
        End Set
    End Property

    Public Property SaveEnabled As Boolean Implements IAddPdfDocumentsView.SaveEnabled
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
        End Set
    End Property

    Public Property PreviewEnabled As Boolean Implements IAddPdfDocumentsView.PreviewEnabled
        Get
            Return PreviewButton.Enabled
        End Get
        Set(value As Boolean)
            PreviewButton.Enabled = value
        End Set
    End Property

    Public Property AddEnabled As Boolean Implements IAddPdfDocumentsView.AddEnabled
        Get
            Return AddButton.Enabled
        End Get
        Set(value As Boolean)
            AddButton.Enabled = value
        End Set
    End Property

    Public Property DiscardEnabled As Boolean Implements IAddPdfDocumentsView.DiscardEnabled
        Get
            Return DiscardButton.Enabled
        End Get
        Set(value As Boolean)
            DiscardButton.Enabled = value
        End Set
    End Property

    Public ReadOnly Property ActiveElement As String Implements ICommonView.ActiveElement
        Get
            Return Me.ActiveControl.Name
        End Get
    End Property

    Public Property AuthorsGroup As DataTable Implements ICommonView.AuthorsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AuthorGroup As String Implements ICommonView.AuthorGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectsGroup As DataTable Implements ICommonView.SubjectsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectGroup As String Implements ICommonView.SubjectGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CategoriesGroup As DataTable Implements ICommonView.CategoriesGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CategoryGroup As String Implements ICommonView.CategoryGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub SetCursor(wait As Boolean) Implements ICommonView.SetCursor
        If wait Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

    Private Sub AddPdfDocumentsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter.ResetView()
    End Sub

    Private Sub SelectButton_Click(sender As Object, e As EventArgs) Handles SelectButton.Click
        presenter.SelectPdf()
        Me.Focus()
        ViewButton.Select()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click
        presenter.ViewSelectedPdf()
    End Sub

    Private Sub TitleTextBox_TextChanged(sender As Object, e As EventArgs) Handles TitleTextBox.TextChanged
        presenter.RequiredTextElementChanged()
    End Sub

    Private Sub SetTitleToFileNameButton_Click(sender As Object, e As EventArgs) Handles SetTitleToFileNameButton.Click
        presenter.SetTitleToFileName()
    End Sub

    Private Sub AuthorPairedComboBox_Enter(sender As Object, e As EventArgs) Handles AuthorPairedComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub AuthorPairedComboBox_TextChanged(sender As Object, e As EventArgs) Handles AuthorPairedComboBox.TextChanged
        presenter.RequiredTextElementChanged()
        commonPresenter.ActiveElementTextTrimStart()
    End Sub

    Private Sub SubjectPairedComboBox_Enter(sender As Object, e As EventArgs) Handles SubjectPairedComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub SubjectPairedComboBox_TextChanged(sender As Object, e As EventArgs) Handles SubjectPairedComboBox.TextChanged
        presenter.RequiredTextElementChanged()
        commonPresenter.ActiveElementTextTrimStart()
    End Sub

    Private Sub CategoryComboBox_Enter(sender As Object, e As EventArgs) Handles CategoryComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub CategoryComboBox_TextChanged(sender As Object, e As EventArgs) Handles CategoryComboBox.TextChanged
        commonPresenter.ActiveElementTextTrimStart()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        presenter.SaveStagedPdf()
        PreviewButton.Select()
    End Sub

    Private Sub PreviewButton_Click(sender As Object, e As EventArgs) Handles PreviewButton.Click
        presenter.PreviewStagedPdf()
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        presenter.AddStagedPdf()
    End Sub

    Private Sub DiscardButton_Click(sender As Object, e As EventArgs) Handles DiscardButton.Click
        presenter.Discard()
    End Sub

    Private Sub AddPdfDocumentsDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If DiscardButton.Enabled Then
            If presenter.ViewClosingPrompt = False Then
                e.Cancel = True
            End If
        End If
    End Sub
End Class
