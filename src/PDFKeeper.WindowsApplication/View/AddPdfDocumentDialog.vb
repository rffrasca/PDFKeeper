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

Public Class AddPdfDocumentDialog
    Implements IAddPdfDocumentView
    Private presenter As AddPdfDocumentViewPresenter
    Private m_OriginalPdfFile As String
    Private m_OriginalPdfFilePassword As New SecureString

    Public Sub New(ByVal originalPdfFile As String, _
                   ByVal originalPdfFilePassword As SecureString)
        InitializeComponent()
        presenter = New AddPdfDocumentViewPresenter(Me)
        m_OriginalPdfFile = originalPdfFile
        m_OriginalPdfFilePassword = originalPdfFilePassword
        HelpProvider.HelpNamespace = HelpProviderHelper.HelpFile
        ViewOriginalButton.Select()
    End Sub

    Private Sub AddPdfDocumentDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        presenter.ViewLoad()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AddPdfDocumentDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            If FormClosingPromptService.IsOkayToCancel = False Then
                e.Cancel = True
            Else
                presenter.DeleteOutputPdf()
            End If
        End If
    End Sub

    Private Sub ViewOriginalButton_Click(sender As Object, e As EventArgs) Handles ViewOriginalButton.Click
        Me.Cursor = Cursors.WaitCursor
        presenter.ViewOriginalPdf()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TextBoxes_TextChanged(sender As Object, e As EventArgs) Handles TitleTextBox.TextChanged, _
                                                                                AuthorComboBox.TextChanged, _
                                                                                SubjectComboBox.TextChanged, _
                                                                                KeywordsTextBox.TextChanged
        presenter.TextChanged()
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

    Private Sub SetToFileNameButton_Click(sender As Object, e As EventArgs) Handles SetToFileNameButton.Click
        presenter.SetTitleToPdfFileName()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Me.Cursor = Cursors.WaitCursor
        presenter.SaveOutputPdf()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PreviewButton_Click(sender As Object, e As EventArgs) Handles PreviewButton.Click
        Me.Cursor = Cursors.WaitCursor
        presenter.ViewOutputPdf()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        presenter.DeleteOriginalPdf()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "IAddPdfDocumentView members get/set by AddPdfDocumentViewPresenter"
    Public ReadOnly Property OriginalPdfFile As String Implements IAddPdfDocumentView.OriginalPdfFile
        Get
            Return m_OriginalPdfFile
        End Get
    End Property

    Public ReadOnly Property OriginalPdfFilePassword As SecureString Implements IAddPdfDocumentView.OriginalPdfFilePassword
        Get
            Return m_OriginalPdfFilePassword
        End Get
    End Property

    Public Property OriginalPdfPathName As String Implements IAddPdfDocumentView.OriginalPdfPathName
        Get
            Return OriginalPdfPathNameTextBox.Text
        End Get
        Set(value As String)
            OriginalPdfPathNameTextBox.Text = value
        End Set
    End Property

    Public Property Title As String Implements IAddPdfDocumentView.Title
        Get
            Return TitleTextBox.Text
        End Get
        Set(value As String)
            TitleTextBox.Text = value
        End Set
    End Property

    Public Property Authors As DataTable Implements IAddPdfDocumentView.Authors
        Get
            Return AuthorComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorComboBox.DataSource = value
            AuthorComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property Author As String Implements IAddPdfDocumentView.Author
        Get
            Return AuthorComboBox.Text
        End Get
        Set(value As String)
            AuthorComboBox.Text = value
        End Set
    End Property

    Public Property Subjects As DataTable Implements IAddPdfDocumentView.Subjects
        Get
            Return SubjectComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectComboBox.DataSource = value
            SubjectComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property Subject As String Implements IAddPdfDocumentView.Subject
        Get
            Return SubjectComboBox.Text
        End Get
        Set(value As String)
            SubjectComboBox.Text = value
        End Set
    End Property

    Public Property Keywords As String Implements IAddPdfDocumentView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property SaveEnabled As Boolean Implements IAddPdfDocumentView.SaveEnabled
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
        End Set
    End Property

    Public Property PreviewEnabled As Boolean Implements IAddPdfDocumentView.PreviewEnabled
        Get
            Return PreviewButton.Enabled
        End Get
        Set(value As Boolean)
            PreviewButton.Enabled = value
        End Set
    End Property

    Public Property OkEnabled As Boolean Implements IAddPdfDocumentView.OkEnabled
        Get
            Return OK_Button.Enabled
        End Get
        Set(value As Boolean)
            OK_Button.Enabled = value
        End Set
    End Property
#End Region

End Class
