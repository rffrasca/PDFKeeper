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
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class UploadProfilesDialog
    Implements IUploadProfilesView
    Private ReadOnly presenter As UploadProfilesPresenter
    Private ReadOnly help As New HelpFile

    Public Sub New()
        InitializeComponent()
        Dim repository = DocumentRepositoryFactory.Repository
        presenter = New UploadProfilesPresenter(Me, New AuthorListService(repository),
                                                New SubjectListService(repository),
                                                New CategoryListService(repository),
                                                New TaxYearListService, New UploadProfileService(
                                                New XmlRepository(Of UploadProfileModel)(AppFolders.GetPath(
                                                AppFolders.AppFolder.UploadProfiles))))
        HelpProvider.HelpNamespace = help.FileName
        AddHandlers()
    End Sub

    Public Property ProfilesGroupEnabled As Boolean Implements IUploadProfilesView.ProfilesGroupEnabled
        Get
            Return ProfilesGroupBox.Enabled
        End Get
        Set(value As Boolean)
            ProfilesGroupBox.Enabled = value
        End Set
    End Property

    Public Property Profile As String Implements IUploadProfilesView.Profile
        Get
            Return ProfileComboBox.SelectedItem
        End Get
        Set(value As String)
            ProfileComboBox.SelectedItem = value
        End Set
    End Property

    Public Property ProfileItems As Object Implements IUploadProfilesView.ProfileItems
        Get
            Return ProfileComboBox.Items
        End Get
        Set(value As Object)
            ProfileComboBox.Items.Clear()
            ProfileComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property EditEnabled As Boolean Implements IUploadProfilesView.EditEnabled
        Get
            Return EditButton.Enabled
        End Get
        Set(value As Boolean)
            EditButton.Enabled = value
        End Set
    End Property

    Public Property DeleteEnabled As Boolean Implements IUploadProfilesView.DeleteEnabled
        Get
            Return DeleteButton.Enabled
        End Get
        Set(value As Boolean)
            DeleteButton.Enabled = value
        End Set
    End Property

    Public Property ProfileGroupEnabled As Boolean Implements IUploadProfilesView.ProfileGroupEnabled
        Get
            Return ProfileGroupBox.Enabled
        End Get
        Set(value As Boolean)
            ProfileGroupBox.Enabled = value
        End Set
    End Property

    Public Property ProfileName As String Implements IUploadProfilesView.ProfileName
        Get
            Return ProfileNameTextBox.Text
        End Get
        Set(value As String)
            ProfileNameTextBox.Text = value
        End Set
    End Property

    Public Property Title As String Implements IUploadProfilesView.Title
        Get
            Return TitleComboBox.Text
        End Get
        Set(value As String)
            TitleComboBox.Text = value
        End Set
    End Property

    Public Property TitleItems As Object Implements IUploadProfilesView.TitleItems
        Get
            Return TitleComboBox.Items
        End Get
        Set(value As Object)
            TitleComboBox.Items.Clear()
            TitleComboBox.Items.AddRange(value)
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

    Public Property SetProfileNameToAuthorSubjectLinkEnabled As Boolean Implements IUploadProfilesView.SetProfileNameToAuthorSubjectLinkEnabled
        Get
            Return SetProfileNameToAuthorSubjectLinkLabel.Enabled
        End Get
        Set(value As Boolean)
            SetProfileNameToAuthorSubjectLinkLabel.Enabled = value
        End Set
    End Property

    Public Property Keywords As String Implements IUploadProfilesView.Keywords
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

    Public Property FlagDocument As Boolean Implements IUploadProfilesView.FlagDocument
        Get
            Return FlagDocumentCheckBox.Checked
        End Get
        Set(value As Boolean)
            FlagDocumentCheckBox.Checked = value
        End Set
    End Property

    Public Property OcrPdfTextAndImageDataPages As Boolean Implements IUploadProfilesView.OcrPdfTextAndImageDataPages
        Get
            Return OcrPdfTextAndImageDataPagesCheckBox.Checked
        End Get
        Set(value As Boolean)
            OcrPdfTextAndImageDataPagesCheckBox.Checked = value
        End Set
    End Property

    Public Property SaveEnabled As Boolean Implements IUploadProfilesView.SaveEnabled
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DiscardEnabled As Boolean Implements IUploadProfilesView.DiscardEnabled
        Get
            Return DiscardButton.Enabled
        End Get
    End Property

    Public Sub SetFocusOnProfileNameTextBox() Implements IUploadProfilesView.SetFocusOnProfileNameTextBox
        ProfileNameTextBox.Focus()
    End Sub

    Public Sub SetErrorProviderMessage(message As String) Implements IUploadProfilesView.SetErrorProviderMessage
        If message Is Nothing Then
            ErrorProvider.Clear()
        Else
            ErrorProvider.SetError(ProfileNameTextBox, message)
        End If
    End Sub

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.UploadProfilesDialog_Load
        AddHandler ProfileComboBox.DropDown, AddressOf presenter.ProfileComboBox_DropDown
        AddHandler ProfileComboBox.SelectedIndexChanged, AddressOf presenter.ProfileComboBox_SelectedIndexChanged
        AddHandler NewButton.Click, AddressOf presenter.NewButton_Click
        AddHandler EditButton.Click, AddressOf presenter.EditButton_Click
        AddHandler DeleteButton.Click, AddressOf presenter.DeleteButton_Click
        AddHandler ProfileNameTextBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler TitleComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler AuthorComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler AuthorComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler SubjectComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler SubjectComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler SetProfileNameToAuthorSubjectLinkLabel.LinkClicked,
            AddressOf presenter.SetProfileNameToAuthorSubjectLinkLabel_LinkClicked
        AddHandler KeywordsTextBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler CategoryComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler CategoryComboBox.TextChanged, AddressOf presenter.Common_TextChanged
        AddHandler TaxYearComboBox.Enter, AddressOf presenter.CommonComboBox_Enter
        AddHandler SaveButton.Click, AddressOf presenter.SaveButton_Click
        AddHandler DiscardButton.Click, AddressOf presenter.DiscardButton_Click
        AddHandler MyBase.FormClosing, AddressOf presenter.UploadProfilesDialog_FormClosing
    End Sub
End Class
