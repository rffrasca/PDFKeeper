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
Public Class MainToolStripStatePresenter
    Private ReadOnly toolStrip As IToolStripStateView

    ' List contains ToolStrip short names and Enabled state.
    ReadOnly shortNamesEnabled As New GenericDictionaryList(Of String, Boolean)

    ' List contains ToolStrip short names and Visible state.
    ReadOnly shortNamesVisible As New GenericDictionaryList(Of String, Boolean)

    Public Sub New(toolStrip As IToolStripStateView)
        Me.toolStrip = toolStrip
        SetEnabledStateDefaults()
        SetVisibleStateDefaults()
        ApplyState()
    End Sub

    Public Sub SetPreSearchState()
        shortNamesEnabled.SetItem("ViewRefresh", False)
        ApplyState()
    End Sub

    Public Sub SetPostSearchState()
        shortNamesEnabled.SetItem("ViewRefresh", True)
        ApplyState()
    End Sub

    Public Sub SetPopulateNewDatabaseTableColumnsVisibleState(ByVal allDocumentsSelected As Boolean)
        Dim visible As Boolean = False
        If allDocumentsSelected Then
            visible = ProductUpdate.NewDbTableColumnsCanBePopulated
        End If
        shortNamesVisible.SetItem("FilePopulateNewDatabaseTableColumns",
                                  visible)
        ApplyState()
    End Sub

    Public Sub SetSearchResultsRowCountChangedState(ByVal rowCount As Integer)
        Dim itemState As Boolean = False
        If rowCount > 0 Then
            itemState = True
        End If
        With shortNamesEnabled
            .SetItem("FileSelect", itemState)
            .SetItem("FileSelectAll", itemState)
            .SetItem("FileSelectNone", itemState)
        End With
        ApplyState()
    End Sub

    Public Sub SetSearchResultsSelectedState(ByVal selectedRows As Integer)
        With shortNamesEnabled
            If selectedRows > 0 Then
                .SetItem("FileSetCategory", True)
                .SetItem("FileSetTaxYear", True)
                .SetItem("FileDelete", True)
                .SetItem("FileExport", True)
            Else
                .SetItem("FileSetCategory", False)
                .SetItem("FileSetTaxYear", False)
                .SetItem("FileDelete", False)
                .SetItem("FileExport", False)
            End If
        End With
        ApplyState()
    End Sub

    Public Sub SetDocumentSelectedState(ByVal documentSelected As Boolean)
        With shortNamesEnabled
            .SetItem("FileOpen", documentSelected)
            .SetItem("FileSaveAs", documentSelected)
            .SetItem("EditFlagDocument", documentSelected)
            .SetItem("ViewSetPreviewImageResolution", documentSelected)
        End With
        ApplyState()
    End Sub

    Public Sub SetTextBoxEnterState(ByVal isReadOnly As Boolean,
                                    ByVal textLength As Integer)
        With shortNamesEnabled
            If textLength > 0 Then
                .SetItem("EditSelectAll", True)
            Else
                .SetItem("EditSelectAll", False)
            End If
            .SetItem("EditUndo", False)
            .SetItem("EditCut", False)
            .SetItem("EditCopy", False)
            If isReadOnly Then
                .SetItem("EditDateTime", False)
                .SetItem("InsertText", False)
            Else
                .SetItem("EditDateTime", True)
                .SetItem("InsertText", True)
            End If
        End With
        ApplyState()
    End Sub

    Public Sub SetTextBoxPrintableState(ByVal printable As Boolean)
        With shortNamesEnabled
            If printable Then
                .SetItem("FilePrint", True)
                .SetItem("FilePrintPreview", True)
            Else
                .SetItem("FilePrint", False)
                .SetItem("FilePrintPreview", False)
            End If
        End With
        ApplyState()
    End Sub

    Public Sub SetPasteState(ByVal textBoxSelected As Boolean)
        shortNamesEnabled.SetItem("EditPaste", textBoxSelected)
        ApplyState()
    End Sub

    Public Sub SetTextBoxTextSelectionState(ByVal isReadOnly As Boolean,
                                            ByVal textLength As Integer,
                                            ByVal selectedTextLength As Integer)
        With shortNamesEnabled
            If selectedTextLength > 0 Then
                If isReadOnly = False Then
                    .SetItem("EditCut", True)
                End If
                .SetItem("EditCopy", True)
                If textLength = selectedTextLength Then
                    .SetItem("EditSelectAll", False)
                Else
                    .SetItem("EditSelectAll", True)
                End If
            Else
                .SetItem("EditCut", False)
                .SetItem("EditCopy", False)
                If textLength > 0 Then
                    .SetItem("EditSelectAll", True)
                Else
                    .SetItem("EditSelectAll", False)
                End If
            End If
        End With
        ApplyState()
    End Sub

    Public Sub SetNotesTextBoxChangedState(ByVal documentNotesChanged As Boolean,
                                           ByVal canUndo As Boolean)
        Dim controlEnabled As Boolean = False
        With shortNamesEnabled
            If documentNotesChanged = False Then
                controlEnabled = True
            Else
                .SetItem("FileSetCategory", controlEnabled)
                .SetItem("FileSetTaxYear", controlEnabled)
                .SetItem("FileDelete", controlEnabled)
                .SetItem("FileExport", controlEnabled)
            End If
            .SetItem("FileSave", documentNotesChanged)
            .SetItem("FileSelect", controlEnabled)
            .SetItem("FileSelectAll", controlEnabled)
            .SetItem("FileSelectNone", controlEnabled)
            If canUndo Then
                .SetItem("EditUndo", True)
            Else
                .SetItem("EditUndo", False)
            End If
            .SetItem("EditRestore", documentNotesChanged)
            .SetItem("ViewRefresh", controlEnabled)
        End With
        ApplyState()
    End Sub

    Public Sub SetTextBoxLeaveState()
        With shortNamesEnabled
            .SetItem("EditUndo", False)
            .SetItem("EditCut", False)
            .SetItem("EditCopy", False)
            .SetItem("EditPaste", False)
            .SetItem("EditSelectAll", False)
            .SetItem("EditDateTime", False)
            .SetItem("InsertText", False)
        End With
        ApplyState()
    End Sub

    Private Sub SetEnabledStateDefaults()
        With shortNamesEnabled
            .SetItem("FileOpen", False)
            .SetItem("FileSave", False)
            .SetItem("FileSaveAs", False)
            .SetItem("FilePrint", False)
            .SetItem("FilePrintPreview", False)
            .SetItem("FileSelect", False)
            .SetItem("FileSelectAll", False)
            .SetItem("FileSelectNone", False)
            .SetItem("FileSetCategory", False)
            .SetItem("FileSetTaxYear", False)
            .SetItem("FileDelete", False)
            .SetItem("FileExport", False)
            .SetItem("EditUndo", False)
            .SetItem("EditCut", False)
            .SetItem("EditCopy", False)
            .SetItem("EditPaste", False)
            .SetItem("EditSelectAll", False)
            .SetItem("EditRestore", False)
            .SetItem("EditDateTime", False)
            .SetItem("EditFlagDocument", False)
            .SetItem("ViewRefresh", False)
            .SetItem("ViewSetPreviewImageResolution", False)
            .SetItem("InsertText", False)
        End With
    End Sub

    Private Sub SetVisibleStateDefaults()
        shortNamesVisible.SetItem("FilePopulateNewDatabaseTableColumns",
                                  False)
    End Sub

    Private Sub ApplyState()
        For Each shortName As Object In shortNamesEnabled.Keys
            toolStrip.SetToolStripItemsEnabledState(CStr(shortName),
                                                    shortNamesEnabled.GetItem(shortName))
        Next
        For Each shortName As Object In shortNamesVisible.Keys
            toolStrip.SetToolStripItemsVisibleState(CStr(shortName),
                                                    shortNamesVisible.GetItem(shortName))
        Next
    End Sub
End Class
