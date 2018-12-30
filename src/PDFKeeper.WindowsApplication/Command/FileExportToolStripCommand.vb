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
Public Class FileExportToolStripCommand
    Implements ICommand
    Private m_parentForm As Control
    Private m_presenter As MainViewSearchPresenter
    Private Shared s_ExportFolder As String

    Public Sub New(ByVal parentForm As Control, _
                   ByVal presenter As MainViewSearchPresenter)
        m_parentForm = parentForm
        m_presenter = presenter
    End Sub

    Public Shared ReadOnly Property ExportFolder As String
        Get
            Dim folder As String = Path.Combine(s_ExportFolder, _
                                                My.Application.Info.ProductName & "-" & _
                                                My.Resources.Export & "-" & _
                                                Guid.NewGuid.ToString)
            Directory.CreateDirectory(folder)
            Return folder
        End Get
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Public Sub Execute() Implements ICommand.Execute
        Dim folderService As IFolderBrowserDialogDisplayService = _
            New FolderBrowserDialogDisplayService(My.Resources.SelectExportFolder)
        Dim selectedFolder As String = folderService.Show
        s_ExportFolder = selectedFolder
        If Not s_ExportFolder Is Nothing Then
            m_parentForm.Cursor = Cursors.WaitCursor
            m_presenter.FileExportToolStripCommandExecute()
            m_parentForm.Cursor = Cursors.Default
        End If
    End Sub
End Class
