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
Imports System.Runtime.Remoting.Messaging

Public Class MainSelectedSearchResultsProcessPresenter
    Private ReadOnly m_View As IMainView
    Private ReadOnly m_ActionToPerform As String
    Private ReadOnly m_ActionParam As String
    Private m_ExportFolderPath As String
    Private m_IdBeingProcessed As Integer

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="view">IMainView object of the view.</param>
    ''' <param name="actionToPerform">
    ''' SelectedDocumentsAction.SetCategory
    ''' SelectedDocumentsAction.SetTaxYear
    ''' SelectedDocumentsAction.Delete
    ''' SelectedDocumentsAction.Export
    ''' SelectedDocumentsAction.Populate
    ''' </param>
    ''' <param name="actionParam">
    ''' Can be either Nothing or the category name when "actionToPerform" is
    ''' SelectedDocumentsAction.SetCategory or the tax year when
    ''' "actionToPerform" is SelectedDocumentsAction.SetTaxYear or the folder
    ''' path to use for the export when "actionToPerform" is
    ''' SelectedDocumentsAction.Export.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As IMainView,
                   ByVal actionToPerform As SelectedDocumentsAction,
                   ByVal actionParam As String)
        m_View = view
        m_ActionToPerform = actionToPerform
        m_ActionParam = actionParam
    End Sub

    ''' <summary>
    ''' Returns the target folder path of the export.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' Path or Nothing if "actionToPerform" is SelectedDocumentsAction.Export.
    ''' </returns>
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
        If m_ActionToPerform = SelectedDocumentsAction.Export Then
            m_ExportFolderPath = IO.Path.Combine(m_ActionParam,
                                                 My.Application.Info.ProductName & "-" &
                                                 My.Resources.Export & "_" &
                                                 DateTime.Now.ToString("yyyy-MM-dd_HH.mm",
                                                                       CultureInfo.CurrentCulture))
            Directory.CreateDirectory(m_ExportFolderPath)
        End If
        m_View.SelectedDocumentsProcessProgressVisible = True
        m_View.SelectedDocumentsProcessProgressMaximum = m_View.SelectedSearchResultsIdsCount
        For Each id As Object In m_View.SelectedSearchResultsIds
            m_IdBeingProcessed = CInt(id)
            If m_ActionToPerform = SelectedDocumentsAction.SetCategory Then
                SetCategoryOnDocument(m_IdBeingProcessed, m_ActionParam)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.SetTaxYear Then
                SetTaxYearOnDocument(m_IdBeingProcessed, m_ActionParam)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.Delete Then
                DeleteDocument(m_IdBeingProcessed)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.Export Then
                ExportDocument(m_IdBeingProcessed, m_ExportFolderPath)
            ElseIf m_ActionToPerform = SelectedDocumentsAction.Populate Then
                PopulateDocumentNewColumns(m_IdBeingProcessed)
            End If
            m_View.SelectedDocumentsProcessProgressPerformStep()
            Application.DoEvents()
        Next
        m_View.SelectedDocumentsProcessProgressVisible = False
        If m_ActionToPerform = SelectedDocumentsAction.SetCategory Or
            SelectedDocumentsAction.Delete Then
            m_View.RefreshSearchResults()
        Else
            m_View.SelectDeselectAllSearchResults(SelectionState.DeselectAll)
        End If
        If m_ActionToPerform = SelectedDocumentsAction.Populate Then
            ProductUpdate.DeleteNewDbTableColumnsTempFile()
            m_View.SetPopulateNewDatabaseTableColumnsVisibleState(False)
        End If
    End Sub

    Private Shared Sub SetCategoryOnDocument(ByVal id As Integer,
                                             ByVal newCategory As String)
        Using model As IDocumentRepository = New DocumentRepository
            model.UpdateCategoryById(id, newCategory)
        End Using
    End Sub

    Private Shared Sub SetTaxYearOnDocument(ByVal id As Integer,
                                            ByVal newTaxYear As String)
        Using model As IDocumentRepository = New DocumentRepository
            model.UpdateTaxYearById(id, newTaxYear)
        End Using
    End Sub

    Private Shared Sub DeleteDocument(ByVal id As Integer)
        Using model As IDocumentRepository = New DocumentRepository
            model.DeleteRecordById(id)
        End Using
    End Sub

    Private Shared Sub ExportDocument(ByVal id As Integer,
                                      ByVal exportFolder As String)
        Using model As IDocumentRepository = New DocumentRepository
            Dim authorFolderInfo As New DirectoryInfo(IO.Path.Combine(exportFolder,
                                                                      model.GetAuthorById(id)))
            Dim subjectFolderInfo As New DirectoryInfo(IO.Path.Combine(authorFolderInfo.FullName,
                                                                       model.GetSubjectById(id)))
            authorFolderInfo.Create()
            subjectFolderInfo.Create()
            Dim pdfFile As New PdfFileType(IO.Path.Combine(subjectFolderInfo.FullName,
                                                           "[" & id & "]" & model.GetTitleById(id) & ".pdf"))
            model.GetPdfById(id, pdfFile.FullName)
            Dim helper As New PdfMetadataHelper(pdfFile.FullName, Nothing)
            Dim metadata As PdfMetadataReader = helper.Read
            If metadata.Title <> model.GetTitleById(id) Or
                    metadata.Author <> model.GetAuthorById(id) Or
                    metadata.Subject <> model.GetSubjectById(id) Or
                    metadata.Keywords <> model.GetKeywordsById(id) Then
                Dim tempPdfFile As String = IO.Path.Combine(IO.Path.GetTempPath,
                                                            IO.Path.GetFileName(pdfFile.FullName))
                helper.Write(tempPdfFile,
                             model.GetTitleById(id),
                             model.GetAuthorById(id),
                             model.GetSubjectById(id),
                             model.GetKeywordsById(id))
                'TODO: Overwrite parameter was added to File.Move in .NET5
                IO.File.Delete(pdfFile.FullName)
                IO.File.Move(tempPdfFile, pdfFile.FullName)
            End If
            Dim notes As String = model.GetNotesById(id)
            Dim category As String = model.GetCategoryById(id)
            Dim taxYear As String = model.GetTaxYearById(id)
            Dim flagState As String = model.GetFlagStateById(id)
            Dim suppDataHelper As New PdfSupplementalDataHelper(pdfFile.FullName)
            suppDataHelper.Write(notes, category, taxYear, flagState)
        End Using
    End Sub

    Private Shared Sub PopulateDocumentNewColumns(ByVal id As Integer)
        Dim cachePathName As New CacheFilePathName(id)
        Dim pdfFile As New PdfFileType(cachePathName.Pdf)
        Using model As IDocumentRepository = New DocumentRepository
            model.GetPdfById(id, pdfFile.FullName)
            model.UpdateTextAnnotationsById(id, pdfFile.GetTextAnnotations)
            model.UpdateTextById(id, pdfFile.GetText)
        End Using
        IO.File.Delete(pdfFile.FullName)
    End Sub
End Class
