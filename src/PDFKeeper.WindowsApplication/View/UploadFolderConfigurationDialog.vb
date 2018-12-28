'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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

Public Class UploadFolderConfigurationDialog
    Implements IUploadFolderConfigurationView
    Private presenter As UploadFolderConfigurationViewPresenter
    Private m_EditUploadFolderConfig As String

    Public Sub New(ByVal editUploadFolderConfig As String)
        InitializeComponent()
        presenter = New UploadFolderConfigurationViewPresenter(Me)
        m_EditUploadFolderConfig = editUploadFolderConfig
        HelpProvider.HelpNamespace = HelpProviderHelper.HelpFile
        FolderNameTextBox.Select()
    End Sub

    Private Sub UploadFolderConfigurationDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not m_EditUploadFolderConfig Is Nothing Then
            presenter.ReadFolderConfiguration()
        End If
    End Sub

    Private Sub UploadFolderConfigurationDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            If GenericFormClosingPrompt.IsOkayToCancel = False Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub TitleComboBox_Enter(sender As Object, e As EventArgs) Handles TitleComboBox.Enter
        presenter.GetTitleTokens()
    End Sub

    Private Sub AuthorComboBox_Enter(sender As Object, e As EventArgs) Handles AuthorComboBox.Enter
        Me.Cursor = Cursors.WaitCursor
        presenter.GetAuthors()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SubjectComboBox_Enter(sender As Object, e As EventArgs) Handles SubjectComboBox.Enter
        Me.Cursor = Cursors.WaitCursor
        presenter.GetSubjectsByAuthor()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles FolderNameTextBox.TextChanged, _
                                                                              TitleComboBox.TextChanged, _
                                                                              AuthorComboBox.TextChanged, _
                                                                              SubjectComboBox.TextChanged, _
                                                                              KeywordsTextBox.TextChanged
        presenter.TextChanged()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        presenter.SaveFolderConfiguration()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "IUploadFolderConfigurationView members get/set by UploadFolderConfigurationPresenter"
    Public ReadOnly Property EditFolderName As String Implements IUploadFolderConfigurationView.EditFolderName
        Get
            Return m_EditUploadFolderConfig
        End Get
    End Property

    Public Property FolderName As String Implements IUploadFolderConfigurationView.FolderName
        Get
            Return FolderNameTextBox.Text
        End Get
        Set(value As String)
            FolderNameTextBox.Text = value
        End Set
    End Property

    Public Property FolderNameErrorProviderMessage As String Implements IUploadFolderConfigurationView.FolderNameErrorProviderMessage
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

    Public Property Titles As Object Implements IUploadFolderConfigurationView.Titles
        Get
            Return TitleComboBox.Items
        End Get
        Set(value As Object)
            TitleComboBox.Items.Clear()
            TitleComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property Title As String Implements IUploadFolderConfigurationView.Title
        Get
            Return TitleComboBox.Text
        End Get
        Set(value As String)
            TitleComboBox.Text = value
        End Set
    End Property

    Public Property Authors As DataTable Implements IUploadFolderConfigurationView.Authors
        Get
            Return AuthorComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorComboBox.DataSource = value
            AuthorComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property Author As String Implements IUploadFolderConfigurationView.Author
        Get
            Return AuthorComboBox.Text
        End Get
        Set(value As String)
            AuthorComboBox.Text = value
        End Set
    End Property

    Public Property Subjects As DataTable Implements IUploadFolderConfigurationView.Subjects
        Get
            Return SubjectComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectComboBox.DataSource = value
            SubjectComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property Subject As String Implements IUploadFolderConfigurationView.Subject
        Get
            Return SubjectComboBox.Text
        End Get
        Set(value As String)
            SubjectComboBox.Text = value
        End Set
    End Property

    Public Property Keywords As String Implements IUploadFolderConfigurationView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property OkEnabled As Boolean Implements IUploadFolderConfigurationView.OkEnabled
        Get
            Return OK_Button.Enabled
        End Get
        Set(value As Boolean)
            OK_Button.Enabled = value
        End Set
    End Property
#End Region

End Class
