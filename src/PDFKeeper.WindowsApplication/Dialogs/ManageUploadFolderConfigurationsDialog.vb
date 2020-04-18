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
Public Class ManageUploadFolderConfigurationsDialog
    Implements IManageUploadFolderConfigurationsView
    Private commonPresenter As CommonPresenter
    Private presenter As ManageUploadFolderConfigurationsPresenter
    Private help As IHelpDisplayService = New HelpDisplayService

    Public Sub New()
        InitializeComponent()
        commonPresenter = New CommonPresenter(Me)
        presenter = New ManageUploadFolderConfigurationsPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
    End Sub

#Region "Interface Members"
    Public Property UploadFolderConfigurationsElementsEnabled As Boolean Implements IManageUploadFolderConfigurationsView.UploadFolderConfigurationsElementsEnabled
        Get
            Return UploadFolderConfigurationsGroupBox.Enabled
        End Get
        Set(value As Boolean)
            UploadFolderConfigurationsGroupBox.Enabled = value
        End Set
    End Property

    Public Property UploadFolderConfigurations As Object Implements IManageUploadFolderConfigurationsView.UploadFolderConfigurations
        Get
            Return UploadFolderConfigurationsComboBox.Items
        End Get
        Set(value As Object)
            UploadFolderConfigurationsComboBox.Items.Clear()
            UploadFolderConfigurationsComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property UploadFolderConfiguration As String Implements IManageUploadFolderConfigurationsView.UploadFolderConfiguration
        Get
            Return UploadFolderConfigurationsComboBox.SelectedItem
        End Get
        Set(value As String)
            UploadFolderConfigurationsComboBox.SelectedItem = value
        End Set
    End Property

    Public Property AddEnabled As Boolean Implements IManageUploadFolderConfigurationsView.AddEnabled
        Get
            Return NewButton.Enabled
        End Get
        Set(value As Boolean)
            NewButton.Enabled = value
        End Set
    End Property

    Public Property EditEnabled As Boolean Implements IManageUploadFolderConfigurationsView.EditEnabled
        Get
            Return EditButton.Enabled
        End Get
        Set(value As Boolean)
            EditButton.Enabled = value
        End Set
    End Property

    Public Property DeleteEnabled As Boolean Implements IManageUploadFolderConfigurationsView.DeleteEnabled
        Get
            Return DeleteButton.Enabled
        End Get
        Set(value As Boolean)
            DeleteButton.Enabled = value
        End Set
    End Property

    Public Property UploadFolderConfigurationElementsEnabled As Boolean Implements IManageUploadFolderConfigurationsView.UploadFolderConfigurationElementsEnabled
        Get
            Return UploadFolderConfigurationGroupBox.Enabled
        End Get
        Set(value As Boolean)
            UploadFolderConfigurationGroupBox.Enabled = value
            FolderNameTextBox.Focus()
        End Set
    End Property

    Public Property FolderName As String Implements IManageUploadFolderConfigurationsView.FolderName
        Get
            Return FolderNameTextBox.Text
        End Get
        Set(value As String)
            FolderNameTextBox.Text = value
        End Set
    End Property

    Public Property FolderNameError As String Implements IManageUploadFolderConfigurationsView.FolderNameError
        Get
            Return FolderNameErrorProvider.GetError(FolderNameTextBox)
        End Get
        Set(value As String)
            If value Is Nothing Then
                FolderNameErrorProvider.Clear()
            Else
                FolderNameErrorProvider.SetError(FolderNameTextBox, value)
            End If
        End Set
    End Property

    Public Property Titles As Object Implements IManageUploadFolderConfigurationsView.Titles
        Get
            Return TitleComboBox.Items
        End Get
        Set(value As Object)
            TitleComboBox.Items.Clear()
            ' Second clear is needed to prevent duplicate items from appearing in the drop
            ' down list when drop down arrow is clicked without Enter event being triggered.
            TitleComboBox.Items.Clear()
            TitleComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property Title As String Implements IManageUploadFolderConfigurationsView.Title
        Get
            Return TitleComboBox.Text
        End Get
        Set(value As String)
            TitleComboBox.Text = value
        End Set
    End Property

    Public Property Authors As DataTable Implements ICommonView.Authors
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Author As String Implements ICommonView.Author
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Subjects As DataTable Implements ICommonView.Subjects
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Subject As String Implements ICommonView.Subject
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
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

    Public Property Keywords As String Implements IManageUploadFolderConfigurationsView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
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

    Public Property FlagDocument As Boolean Implements IManageUploadFolderConfigurationsView.FlagDocument
        Get
            Return FlagDocumentCheckBox.Checked
        End Get
        Set(value As Boolean)
            FlagDocumentCheckBox.Checked = value
        End Set
    End Property

    Public Property SaveEnabled As Boolean Implements IManageUploadFolderConfigurationsView.SaveEnabled
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
        End Set
    End Property

    Public ReadOnly Property ActiveElement As String Implements ICommonView.ActiveElement
        Get
            Return Me.ActiveControl.Name
        End Get
    End Property

    Public Sub SetCursor(wait As Boolean) Implements ICommonView.SetCursor
        If wait Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

    Private Sub ManageUploadFolderConfigurationsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter.ResetView()
    End Sub

    Private Sub UploadFolderConfigurationsComboBox_Enter(sender As Object, e As EventArgs) Handles UploadFolderConfigurationsComboBox.Enter
        presenter.GetConfigurations()
    End Sub

    Private Sub UploadFolderConfigurationsComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UploadFolderConfigurationsComboBox.SelectedIndexChanged
        presenter.ConfigurationSelected()
    End Sub

    Private Sub NewButton_Click(sender As Object, e As EventArgs) Handles NewButton.Click
        presenter.NewConfiguration()
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        presenter.EditConfiguration()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        presenter.DeleteConfiguration()
    End Sub

    Private Sub FolderNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles FolderNameTextBox.TextChanged
        presenter.RequiredTextElementChanged()
    End Sub

    Private Sub TitleComboBox_Enter(sender As Object, e As EventArgs) Handles TitleComboBox.Enter
        presenter.GetTokens()
    End Sub

    Private Sub TitleComboBox_TextChanged(sender As Object, e As EventArgs) Handles TitleComboBox.TextChanged
        presenter.RequiredTextElementChanged()
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
        presenter.SaveConfiguration()
    End Sub

    Private Sub DiscardButton_Click(sender As Object, e As EventArgs) Handles DiscardButton.Click
        presenter.Discard()
    End Sub

    Private Sub ManageUploadFolderConfigurationsDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If DiscardButton.Enabled Then
            If presenter.ViewClosingPrompt = False Then
                e.Cancel = True
            End If
        End If
    End Sub
End Class
