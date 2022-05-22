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
Imports PDFKeeper.Common
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class AddPdfDialog
    Implements IAddPdfView
    Private ReadOnly presenter As AddPdfPresenter
    Private ReadOnly help As New HelpProvider

    Public Sub New()
        InitializeComponent()
        Dim repository = DocumentRepositoryFactory.Repository
        presenter = New AddPdfPresenter(Me, New AuthorListService(repository), New SubjectListService(repository),
                                        New CategoryListService(repository), New TaxYearListService, New PdfService)
        HelpProvider.HelpNamespace = help.HelpFileName
        AddHandlers()
    End Sub

    Public ReadOnly Property SelectedPdf As String Implements IAddPdfView.SelectedPdf
        Get
            Return SelectedPdfTextBox.Text
        End Get
    End Property

    Public Property Title As String Implements IAddPdfView.Title
        Get
            Return TitleTextBox.Text
        End Get
        Set(value As String)
            TitleTextBox.Text = value
        End Set
    End Property

    Public Property Author As String Implements IViewCommon.Author
        Get
            Return AuthorComboBox.Text
        End Get
        Set(value As String)
            AuthorComboBox.Text = value
        End Set
    End Property

    Public Property AuthorItems As DataTable Implements IViewCommon.AuthorItems
        Get
            Return AuthorComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorComboBox.DataSource = value
            AuthorComboBox.DisplayMember = AuthorComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property Subject As String Implements IViewCommon.Subject
        Get
            Return SubjectComboBox.Text
        End Get
        Set(value As String)
            SubjectComboBox.Text = value
        End Set
    End Property

    Public Property SubjectItems As DataTable Implements IViewCommon.SubjectItems
        Get
            Return SubjectComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectComboBox.DataSource = value
            SubjectComboBox.DisplayMember = SubjectComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property Keywords As String Implements IAddPdfView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property Category As String Implements IViewCommon.Category
        Get
            Return CategoryComboBox.Text
        End Get
        Set(value As String)
            CategoryComboBox.Text = value
        End Set
    End Property

    Public Property CategoryItems As DataTable Implements IViewCommon.CategoryItems
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = CategoryComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property TaxYear As String Implements IViewCommon.TaxYear
        Get
            Return TaxYearComboBox.Text
        End Get
        Set(value As String)
            TaxYearComboBox.Text = value
        End Set
    End Property

    Public Property TaxYearItems As DataTable Implements IViewCommon.TaxYearItems
        Get
            Return TaxYearComboBox.DataSource
        End Get
        Set(value As DataTable)
            TaxYearComboBox.DataSource = value
            TaxYearComboBox.DisplayMember = TaxYearComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property FlagDocument As Boolean Implements IAddPdfView.FlagDocument
        Get
            Return FlagDocumentCheckBox.Checked
        End Get
        Set(value As Boolean)
            FlagDocumentCheckBox.Checked = value
        End Set
    End Property

    Public Property SaveEnabled As Boolean Implements IAddPdfView.SaveEnabled
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
        End Set
    End Property

    Public Property PreviewEnabled As Boolean Implements IAddPdfView.PreviewEnabled
        Get
            Return PreviewButton.Enabled
        End Get
        Set(value As Boolean)
            PreviewButton.Enabled = value
        End Set
    End Property

    Public Property AddEnabled As Boolean Implements IAddPdfView.AddEnabled
        Get
            Return AddButton.Enabled
        End Get
        Set(value As Boolean)
            AddButton.Enabled = value
        End Set
    End Property

    Public Sub ShowOpenPdfFileDialog() Implements IAddPdfView.ShowOpenPdfFileDialog
        OpenFileDialog.ShowDialog()
        SelectedPdfTextBox.Text = OpenFileDialog.FileName
    End Sub

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.AddPdfDialog_Load
        AddHandler ViewButton.Click, AddressOf presenter.ViewButton_Click
        AddHandler SetTitleToFileNameButton.Click, AddressOf presenter.SetTitleToFileNameButton_Click
        AddHandler TitleTextBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler AuthorComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler AuthorComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler SubjectComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler SubjectComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler KeywordsTextBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler CategoryComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler CategoryComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler TaxYearComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler SaveButton.Click, AddressOf presenter.SaveButton_Click
        AddHandler PreviewButton.Click, AddressOf presenter.PreviewButton_Click
        AddHandler AddButton.Click, AddressOf presenter.AddButton_Click
        AddHandler MyBase.FormClosing, AddressOf presenter.AddPdfDialog_FormClosing
    End Sub
End Class
