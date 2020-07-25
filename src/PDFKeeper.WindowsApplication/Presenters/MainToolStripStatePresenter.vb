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
Public Class MainToolStripStatePresenter
    Private toolStrip As IToolStripStateView

    ' List contains ToolStrip short names and Enabled state.
    Dim shortNames As New GenericDictionaryList(Of String, Boolean)

    Public Sub New(toolStrip As IToolStripStateView)
        Me.toolStrip = toolStrip
        SetDefaultState()
    End Sub

    Private Sub SetDefaults()
        shortNames.SetItem("FileOpen", False)
        shortNames.SetItem("FileSave", False)
        shortNames.SetItem("FileSaveAs", False)
        shortNames.SetItem("FilePrint", False)
        shortNames.SetItem("FilePrintPreview", False)
        shortNames.SetItem("FileSelect", False)
        shortNames.SetItem("FileSelectAll", False)
        shortNames.SetItem("FileSelectNone", False)
        shortNames.SetItem("FileSetCategory", False)
        shortNames.SetItem("FileDelete", False)
        shortNames.SetItem("FileExport", False)
        shortNames.SetItem("EditUndo", False)
        shortNames.SetItem("EditCut", False)
        shortNames.SetItem("EditCopy", False)
        shortNames.SetItem("EditPaste", False)
        shortNames.SetItem("EditSelectAll", False)
        shortNames.SetItem("EditRestore", False)
        shortNames.SetItem("EditDateTime", False)
        shortNames.SetItem("EditFlagDocument", False)
        shortNames.SetItem("ViewRefresh", False)
        shortNames.SetItem("ViewSetPreviewImageResolution", False)
    End Sub

    Public Sub SetDefaultState()
        SetDefaults()
        ApplyState()
    End Sub

    Public Sub SetPreSearchState()
        shortNames.SetItem("ViewRefresh", False)
        ApplyState()
    End Sub

    Public Sub SetPostSearchState()
        shortNames.SetItem("ViewRefresh", True)
        ApplyState()
    End Sub

    Public Sub SetSearchResultsRowCountChangedState(ByVal rowCount As Integer)
        Dim itemState As Boolean = False
        If rowCount > 0 Then
            itemState = True
        End If
        shortNames.SetItem("FileSelect", itemState)
        shortNames.SetItem("FileSelectAll", itemState)
        shortNames.SetItem("FileSelectNone", itemState)
        ApplyState()
    End Sub

    Public Sub SetSearchResultsSelectedState(ByVal selectedRows As Integer)
        If selectedRows > 0 Then
            shortNames.SetItem("FileSetCategory", True)
            shortNames.SetItem("FileDelete", True)
            shortNames.SetItem("FileExport", True)
        Else
            shortNames.SetItem("FileSetCategory", False)
            shortNames.SetItem("FileDelete", False)
            shortNames.SetItem("FileExport", False)
        End If
        ApplyState()
    End Sub

    Public Sub SetDocumentSelectedState(ByVal documentSelected As Boolean)
        shortNames.SetItem("FileOpen", documentSelected)
        shortNames.SetItem("FileSaveAs", documentSelected)
        shortNames.SetItem("EditFlagDocument", documentSelected)
        shortNames.SetItem("ViewSetPreviewImageResolution", documentSelected)
        ApplyState()
    End Sub

    Public Sub SetTextBoxEnterState(ByVal isReadOnly As Boolean, _
                                    ByVal textLength As Integer)
        If textLength > 0 Then
            shortNames.SetItem("EditSelectAll", True)
        Else
            shortNames.SetItem("EditSelectAll", False)
        End If
        shortNames.SetItem("EditUndo", False)
        shortNames.SetItem("EditCut", False)
        shortNames.SetItem("EditCopy", False)
        If isReadOnly Then
            shortNames.SetItem("EditDateTime", False)
        Else
            shortNames.SetItem("EditDateTime", True)
        End If
        ApplyState()
    End Sub

    Public Sub SetTextBoxPrintableState(ByVal printable As Boolean)
        If printable Then
            shortNames.SetItem("FilePrint", True)
            shortNames.SetItem("FilePrintPreview", True)
        Else
            shortNames.SetItem("FilePrint", False)
            shortNames.SetItem("FilePrintPreview", False)
        End If
        ApplyState()
    End Sub

    Public Sub SetPasteState(ByVal textBoxSelected As Boolean)
        shortNames.SetItem("EditPaste", textBoxSelected)
        ApplyState()
    End Sub

    Public Sub SetTextBoxTextSelectionState(ByVal isReadOnly As Boolean, _
                                            ByVal textLength As Integer, _
                                            ByVal selectedTextLength As Integer)
        If selectedTextLength > 0 Then
            If isReadOnly = False Then
                shortNames.SetItem("EditCut", True)
            End If
            shortNames.SetItem("EditCopy", True)
            If textLength = selectedTextLength Then
                shortNames.SetItem("EditSelectAll", False)
            Else
                shortNames.SetItem("EditSelectAll", True)
            End If
        Else
            shortNames.SetItem("EditCut", False)
            shortNames.SetItem("EditCopy", False)
            If textLength > 0 Then
                shortNames.SetItem("EditSelectAll", True)
            Else
                shortNames.SetItem("EditSelectAll", False)
            End If
        End If
        ApplyState()
    End Sub

    Public Sub SetNotesTextBoxChangedState(ByVal documentNotesChanged As Boolean, _
                                           ByVal canUndo As Boolean)
        Dim controlEnabled As Boolean = False
        If documentNotesChanged = False Then
            controlEnabled = True
        Else
            shortNames.SetItem("FileSetCategory", controlEnabled)
            shortNames.SetItem("FileDelete", controlEnabled)
            shortNames.SetItem("FileExport", controlEnabled)
        End If
        shortNames.SetItem("FileSave", documentNotesChanged)
        shortNames.SetItem("FileSelect", controlEnabled)
        shortNames.SetItem("FileSelectAll", controlEnabled)
        shortNames.SetItem("FileSelectNone", controlEnabled)
        If canUndo Then
            shortNames.SetItem("EditUndo", True)
        Else
            shortNames.SetItem("EditUndo", False)
        End If
        shortNames.SetItem("EditRestore", documentNotesChanged)
        shortNames.SetItem("ViewRefresh", controlEnabled)
        ApplyState()
    End Sub

    Public Sub SetTextBoxLeaveState()
        shortNames.SetItem("EditUndo", False)
        shortNames.SetItem("EditCut", False)
        shortNames.SetItem("EditCopy", False)
        shortNames.SetItem("EditPaste", False)
        shortNames.SetItem("EditSelectAll", False)
        shortNames.SetItem("EditDateTime", False)
        ApplyState()
    End Sub

    Private Sub ApplyState()
        For Each shortName As Object In shortNames.Keys
            toolStrip.SetToolStripItemsState(CStr(shortName), _
                                             shortNames.GetItem(shortName))
        Next
    End Sub
End Class
