﻿'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
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
Public Class ProcessSelectedSearchResultsCommand
    Implements IProcessSelectedSearchResultsCommand
    Private m_SourceView As IMainView
    Private m_FunctionToPerform As String
    Private m_CategoryExportParam As String
    Private m_ExportFolderPath As String
    Private m_IdBeingProcessed As Integer

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="sourceView">IMainView object of the source view.</param>
    ''' <param name="functionToPerform">
    ''' Enums.SelectedDocumentsFunction.SetClearCategory
    ''' Enums.SelectedDocumentsFunction.Delete
    ''' Enums.SelectedDocumentsFunction.Export
    ''' </param>
    ''' <param name="categoryOrExportParam">
    ''' Can be either the new category name when "functionToPerform" is
    ''' Enums.SelectedDocumentsFunction.SetClearCategory or the folder path to
    ''' use for the export when "functionToPerform" is
    ''' Enums.SelectedDocumentsFunction.Export.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal sourceView As IMainView, _
                   ByVal functionToPerform As Enums.SelectedDocumentsFunction, _
                   ByVal categoryOrExportParam As String)
        m_SourceView = sourceView
        m_FunctionToPerform = functionToPerform
        m_CategoryExportParam = categoryOrExportParam
    End Sub

    ''' <summary>
    ''' Returns the target folder path of the export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExportFolderPath As String Implements IProcessSelectedSearchResultsCommand.ExportFolderPath
        Get
            Return m_ExportFolderPath
        End Get
    End Property

    ''' <summary>
    ''' Returns the ID of the document record being processed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IdBeingProcessed As Integer Implements IProcessSelectedSearchResultsCommand.IdBeingProcessed
        Get
            Return m_IdBeingProcessed
        End Get
    End Property

    Public Sub Execute() Implements IProcessSelectedSearchResultsCommand.Execute
        If m_FunctionToPerform = Enums.SelectedDocumentsFunction.Export Then
            m_ExportFolderPath = Path.Combine(m_CategoryExportParam, _
                                              My.Application.Info.ProductName & "-" & _
                                              My.Resources.Export & "-" & _
                                              Guid.NewGuid.ToString)
            Directory.CreateDirectory(m_ExportFolderPath)
        End If
        m_SourceView.DeleteExportProgressVisible = True
        m_SourceView.DeleteExportProgressMaximum = m_SourceView.SelectedSearchResultsIdsCount
        For Each id As Object In m_SourceView.SelectedSearchResultsIds
            m_IdBeingProcessed = CInt(id)
            If m_FunctionToPerform = Enums.SelectedDocumentsFunction.SetClearCategory Then
                SetCategoryOnDocument(m_IdBeingProcessed, m_CategoryExportParam)
            ElseIf m_FunctionToPerform = Enums.SelectedDocumentsFunction.Delete Then
                DeleteDocument(m_IdBeingProcessed)
            ElseIf m_FunctionToPerform = Enums.SelectedDocumentsFunction.Export Then
                ExportDocument(m_IdBeingProcessed, m_ExportFolderPath)
            End If
            m_SourceView.DeleteExportProgressPerformStep()
        Next
        m_SourceView.DeleteExportProgressVisible = False
        If m_FunctionToPerform = Enums.SelectedDocumentsFunction.SetClearCategory Or _
            Enums.SelectedDocumentsFunction.Delete Then
            m_SourceView.RefreshSearchResults()
        Else
            m_SourceView.SelectDeselectAllSearchResults(SelectionState.DeselectAll)
        End If
    End Sub

    Private Shared Sub SetCategoryOnDocument(ByVal id As Integer, _
                                             ByVal newCategory As String)
        Dim dataClient As IDataClient = New DataClient
        dataClient.UpdateCategoryById(id, newCategory)
    End Sub

    Private Shared Sub DeleteDocument(ByVal id As Integer)
        Dim dataClient As IDataClient = New DataClient
        dataClient.DeleteRecordById(id)
    End Sub

    Private Shared Sub ExportDocument(ByVal id As Integer, ByVal exportFolder As String)
        Dim pdfInfo As New PdfFileInfo(Path.Combine(exportFolder, _
                                                    My.Application.Info.ProductName & id & ".pdf"))
        Dim dataClientPdf As IDataClient = New DataClient
        dataClientPdf.GetPdfById(id, pdfInfo.FullName)
        Dim dataClientNotes As IDataClient = New DataClient
        Dim dataTableNotes As DataTable = dataClientNotes.GetNotesById(id)
        Dim notes As String = Convert.ToString(dataTableNotes.Rows(0)("doc_notes"), _
                                               CultureInfo.CurrentCulture)
        Dim dataClientCategory As IDataClient = New DataClient
        Dim dataTableCategory As DataTable = dataClientCategory.GetCategoryById(id)
        Dim category As String = Convert.ToString(dataTableCategory.Rows(0)("doc_category"), _
                                                  CultureInfo.CurrentCulture)
        Dim dataClientFlagState As IDataClient = New DataClient
        Dim dataTableFlagState As DataTable = dataClientFlagState.GetFlagStateById(id)
        Dim flagState As String = Convert.ToInt32(dataTableFlagState.Rows(0)("doc_flag"), _
                                                  CultureInfo.CurrentCulture)
        Dim suppDataHelper As New PdfSupplementalDataHelper(pdfInfo.FullName)
        suppDataHelper.Write(notes, category, flagState)
    End Sub
End Class