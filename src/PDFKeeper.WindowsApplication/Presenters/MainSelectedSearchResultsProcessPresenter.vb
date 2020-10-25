'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
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
Imports System.Runtime.Remoting.Messaging

Public Class MainSelectedSearchResultsProcessPresenter
    Private ReadOnly m_View As IMainView
    Private ReadOnly m_ActionToPerform As String
    Private ReadOnly m_CategoryExportParam As String
    Private m_ExportFolderPath As String
    Private m_IdBeingProcessed As Integer

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="view">IMainView object of the view.</param>
    ''' <param name="actionToPerform">
    ''' SelectedDocumentsAction.SetClearCategory
    ''' SelectedDocumentsAction.Delete
    ''' SelectedDocumentsAction.Export
    ''' </param>
    ''' <param name="categoryOrExportParam">
    ''' Can be either the new category name when "actionToPerform" is
    ''' SelectedDocumentsAction.SetClearCategory or the folder path to use for
    ''' the export when "actionToPerform" is SelectedDocumentsAction.Export.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As IMainView,
                   ByVal actionToPerform As SelectedDocumentsAction,
                   ByVal categoryOrExportParam As String)
        m_View = view
        m_ActionToPerform = actionToPerform
        m_CategoryExportParam = categoryOrExportParam
    End Sub

    ''' <summary>
    ''' Returns the target folder path of the export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExportFolderPath As String
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
    Public ReadOnly Property IdBeingProcessed As Integer
        Get
            Return m_IdBeingProcessed
        End Get
    End Property

    Public Sub ProcessSelectedSearchResults()
        If m_ActionToPerform = SelectedDocumentsAction.ExportFull Or
            SelectedDocumentsAction.ExportPdf Then
            m_ExportFolderPath = Path.Combine(m_CategoryExportParam,
                                              My.Application.Info.ProductName & "-" &
                                              My.Resources.Export & "_" &
                                              DateTime.Now.ToString("yyyy-MM-dd_HH.mm",
                                                                    CultureInfo.CurrentCulture))
            Directory.CreateDirectory(m_ExportFolderPath)
        End If
        m_View.DeleteExportProgressVisible = True
        m_View.DeleteExportProgressMaximum = m_View.SelectedSearchResultsIdsCount
        For Each id As Object In m_View.SelectedSearchResultsIds
            m_IdBeingProcessed = CInt(id)
            If m_ActionToPerform = SelectedDocumentsAction.SetClearCategory Then
                SetCategoryOnDocument(m_IdBeingProcessed, m_CategoryExportParam)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.Delete Then
                DeleteDocument(m_IdBeingProcessed)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.ExportFull Or
                SelectedDocumentsAction.ExportPdf Then
                ExportDocument(m_IdBeingProcessed, m_ExportFolderPath, m_ActionToPerform)
            End If
            m_View.DeleteExportProgressPerformStep()
            Application.DoEvents()
        Next
        m_View.DeleteExportProgressVisible = False
        If m_ActionToPerform = SelectedDocumentsAction.SetClearCategory Or
            SelectedDocumentsAction.Delete Then
            m_View.RefreshSearchResults()
        Else
            m_View.SelectDeselectAllSearchResults(SelectionState.DeselectAll)
        End If
    End Sub

    Private Shared Sub SetCategoryOnDocument(ByVal id As Integer,
                                             ByVal newCategory As String)
        Using model As IDocumentRepository = New DocumentRepository
            model.UpdateCategoryById(id, newCategory)
        End Using
    End Sub

    Private Shared Sub DeleteDocument(ByVal id As Integer)
        Using model As IDocumentRepository = New DocumentRepository
            model.DeleteRecordById(id)
        End Using
    End Sub

    Private Shared Sub ExportDocument(ByVal id As Integer,
                                      ByVal exportFolder As String,
                                      ByVal exportAction As SelectedDocumentsAction)
        Using model As IDocumentRepository = New DocumentRepository
            Dim authorFolderInfo As New DirectoryInfo(Path.Combine(exportFolder,
                                                                   model.GetAuthorById(id)))
            Dim subjectFolderInfo As New DirectoryInfo(Path.Combine(authorFolderInfo.FullName,
                                                                    model.GetSubjectById(id)))
            authorFolderInfo.Create()
            subjectFolderInfo.Create()
            Dim pdfInfo As New PdfFileInfo(Path.Combine(subjectFolderInfo.FullName,
                                                        "[" & id & "]" & model.GetTitleById(id) & ".pdf"))
            model.GetPdfById(id, pdfInfo.FullName)
            If exportAction = SelectedDocumentsAction.ExportFull Then
                Dim notes As String = model.GetNotesById(id)
                Dim category As String = model.GetCategoryById(id)
                Dim flagState As String = model.GetFlagStateById(id)
                Dim suppDataHelper As New PdfSupplementalDataHelper(pdfInfo.FullName)
                suppDataHelper.Write(notes, category, flagState)
            End If
        End Using
    End Sub
End Class
