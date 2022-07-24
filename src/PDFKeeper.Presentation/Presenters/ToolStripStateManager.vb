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
Imports PDFKeeper.Infrastructure

Public Class ToolStripStateManager
    Implements IDisposable
    Private ReadOnly view As MainForm
    Private ReadOnly actionsTable As DataTable
    Private disposedValue As Boolean

    Public Sub New(ByVal view As MainForm)
        Me.view = view
        actionsTable = New DataTable("ToolStripActions")
        AddColumns()
        SetInitialState()
        CommitState()
    End Sub

    Public Sub EnableEditPaste(ByVal enable As Boolean)
        For Each row In actionsTable.Rows
            If row("Action") = "EditPaste" Then
                row("StateValue") = enable
            End If
        Next
        CommitState()
    End Sub

    Public Sub EnableViewRefresh(ByVal enable As Boolean)
        For Each row In actionsTable.Rows
            If row("Action") = "ViewRefresh" Then
                row("StateValue") = enable
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForViewRowCount(ByVal rows As Integer)
        Dim enable = False
        If rows > 0 Then
            enable = True
        End If
        For Each row In actionsTable.Rows
            If row("Action") = "FileSelect" Then
                row("StateValue") = enable
            End If
            If row("Action") = "FileSelectAll" Then
                row("StateValue") = enable
            End If
            If row("Action") = "FileSelectNone" Then
                row("StateValue") = enable
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForViewCheckedRowCount(ByVal rows As Integer)
        Dim enable = False
        If rows > 0 Then
            enable = True
        End If
        For Each row In actionsTable.Rows
            If row("Action") = "FileSetCategory" Then
                row("StateValue") = enable
            End If
            If row("Action") = "FileSetTaxYear" Then
                row("StateValue") = enable
            End If
            If row("Action") = "FileDelete" Then
                row("StateValue") = enable
            End If
            If row("Action") = "FileExport" Then
                row("StateValue") = enable
            End If
            If row("Action") = "ToolsUpdatePdfTextColumns" Then
                row("StateValue") = enable
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForDocumentSelected(ByVal selected As Boolean)
        For Each row In actionsTable.Rows
            If row("Action") = "FileOpen" Then
                row("StateValue") = selected
            End If
            If row("Action") = "FileSaveAs" Then
                row("StateValue") = selected
            End If
            If row("Action") = "FileBurst" Then
                row("StateValue") = selected
            End If
            If row("Action") = "EditFlagDocument" Then
                row("StateValue") = selected
            End If
            If row("Action") = "ViewSetPreviewPixelDensity" Then
                row("StateValue") = selected
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForTextBoxEnter(ByVal textBox As TextBox, ByVal printable As Boolean)
        If textBox Is Nothing Then
            Throw New ArgumentNullException(NameOf(textBox))
        End If
        For Each row In actionsTable.Rows
            If row("Action") = "FilePrint" Then
                row("StateValue") = printable
            End If
            If row("Action") = "FilePrintPreview" Then
                row("StateValue") = printable
            End If
            If row("Action") = "EditUndo" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditCut" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditCopy" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditSelectAll" Then
                If textBox.Text.Length > 0 Then
                    row("StateValue") = True
                Else
                    row("StateValue") = False
                End If
            End If
            If row("Action") = "EditDateTime" Then
                If textBox.ReadOnly Then
                    row("StateValue") = False
                Else
                    row("StateValue") = True
                End If
            End If
            If row("Action") = "InsertText" Then
                If textBox.ReadOnly Then
                    row("StateValue") = False
                Else
                    row("StateValue") = True
                End If
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForTextBoxSelectedText(ByVal isReadOnly As Boolean, ByVal textLength As Integer,
                                              ByVal selectedTextLength As Integer)
        For Each row In actionsTable.Rows
            If row("Action") = "EditCut" Then
                If selectedTextLength > 0 Then
                    If isReadOnly = False Then
                        row("StateValue") = True
                    End If
                Else
                    row("StateValue") = False
                End If
            End If
            If row("Action") = "EditCopy" Then
                If selectedTextLength > 0 Then
                    row("StateValue") = True
                Else
                    row("StateValue") = False
                End If
            End If
            If row("Action") = "EditSelectAll" Then
                If textLength > 0 Then
                    If textLength = selectedTextLength Then
                        row("StateValue") = False
                    Else
                        row("StateValue") = True
                    End If
                Else
                    row("StateValue") = False
                End If
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForNotesChanged(ByVal changed As Boolean, ByVal canUndo As Boolean)
        Dim actionEnabled = False
        If changed = False Then
            actionEnabled = True
        End If
        For Each row In actionsTable.Rows
            If row("Action") = "FileSave" Then
                row("StateValue") = changed
            End If
            If row("Action") = "FileSelect" Then
                row("StateValue") = actionEnabled
            End If
            If row("Action") = "FileSelectAll" Then
                row("StateValue") = actionEnabled
            End If
            If row("Action") = "FileSelectNone" Then
                row("StateValue") = actionEnabled
            End If
            If row("Action") = "FileSetCategory" And changed = True Then
                row("StateValue") = False
            End If
            If row("Action") = "FileSetTaxYear" And changed = True Then
                row("StateValue") = False
            End If
            If row("Action") = "FileDelete" And changed = True Then
                row("StateValue") = False
            End If
            If row("Action") = "FileExport" And changed = True Then
                row("StateValue") = False
            End If
            If row("Action") = "ToolsUpdatePdfTextColumns" And changed = True Then
                row("StateValue") = False
            End If
            If row("Action") = "EditUndo" Then
                If canUndo Then
                    row("StateValue") = True
                Else
                    row("StateValue") = False
                End If
            End If
            If row("Action") = "EditRestore" Then
                row("StateValue") = changed
            End If
            If row("Action") = "ViewRefresh" Then
                row("StateValue") = actionEnabled
            End If
        Next
        CommitState()
    End Sub

    Public Sub SetStateForTextBoxLeave()
        For Each row In actionsTable.Rows
            If row("Action") = "FilePrint" Then
                row("StateValue") = False
            End If
            If row("Action") = "FilePrintPreview" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditUndo" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditCut" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditCopy" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditPaste" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditSelectAll" Then
                row("StateValue") = False
            End If
            If row("Action") = "EditDateTime" Then
                row("StateValue") = False
            End If
            If row("Action") = "InsertText" Then
                row("StateValue") = False
            End If
        Next
        CommitState()
    End Sub

    Private Sub AddColumns()
        Dim actionColumn = actionsTable.Columns.Add("Action", GetType(String))
        actionColumn.AllowDBNull = False
        actionColumn.Unique = True
        actionsTable.Columns.Add("SetState", GetType(String))
        actionsTable.Columns.Add("StateValue", GetType(Boolean))
    End Sub

    Private Sub SetInitialState()
        With actionsTable
            For Each item In GetEnabledActions()
                Dim row = actionsTable.NewRow
                row("Action") = item
                row("SetState") = "Enabled"
                row("StateValue") = False
                actionsTable.Rows.Add(row)
            Next
            For Each item In GetVisibleActions()
                Dim row = actionsTable.NewRow
                row("Action") = item
                row("SetState") = "Visible"
                If DbSession.Platform = DbSession.DbPlatform.Sqlite Then
                    row("StateValue") = True
                Else
                    row("StateValue") = False
                End If
                actionsTable.Rows.Add(row)
            Next
        End With
    End Sub

    Private Function GetEnabledActions() As List(Of String)
        Dim actions = New List(Of String)
        With actions
            .Add("FileOpen")
            .Add("FileSave")
            .Add("FileSaveAs")
            .Add("FileBurst")
            .Add("FilePrint")
            .Add("FilePrintPreview")
            .Add("FileSelect")
            .Add("FileSelectAll")
            .Add("FileSelectNone")
            .Add("FileSetCategory")
            .Add("FileSetTaxYear")
            .Add("FileDelete")
            .Add("FileExport")
            .Add("EditUndo")
            .Add("EditCut")
            .Add("EditCopy")
            .Add("EditPaste")
            .Add("EditSelectAll")
            .Add("EditRestore")
            .Add("EditDateTime")
            .Add("EditFlagDocument")
            .Add("ViewRefresh")
            .Add("ViewSetPreviewPixelDensity")
            .Add("InsertText")
            .Add("ToolsUpdatePdfTextColumns")
        End With
        Return actions
    End Function

    Private Function GetVisibleActions() As List(Of String)
        Dim actions = New List(Of String)
        With actions
            .Add("ToolsMoveDatabase")
            .Add("ToolsRebuildFullTextSearchIndex")
        End With
        Return actions
    End Function

    Private Sub CommitState()
        actionsTable.AcceptChanges()
        For Each row In actionsTable.Rows
            For Each menuItem In view.MenuStrip.Items.Find(String.Concat(row("Action"), "ToolStripMenuItem"), True).ToList
                If row("SetState") = "Enabled" Then
                    menuItem.Enabled = row("StateValue")
                ElseIf row("SetState") = "Visible" Then
                    menuItem.Visible = row("StateValue")
                End If
            Next
            For Each button In view.ToolStrip.Items.Find(String.Concat(row("Action"), "ToolStripButton"), True).ToList
                If row("SetState") = "Enabled" Then
                    button.Enabled = row("StateValue")
                ElseIf row("SetState") = "Visible" Then
                    button.Visible = row("StateValue")
                End If
            Next
        Next
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                actionsTable.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
