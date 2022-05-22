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
Imports System.Security
Imports PDFKeeper.Domain
Imports PDFKeeper.FileIO
Imports PDFKeeper.FileIO.PdfPasswordTypes

Public Interface IPdfService
    ''' <summary>
    ''' Gets the password type set in the PDF.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <returns>None, Owner, User, or Unknown</returns>
    Function GetPdfPasswordType(ByVal pdfFile As String) As PdfPasswordType

    ''' <summary>
    ''' Is the PDF Owner password valid?
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="Password">SecureString object</param>
    ''' <returns>True or False</returns>
    Function IsPdfOwnerPasswordValid(ByVal pdfFile As String, ByVal password As SecureString) As Boolean

    ''' <summary>
    ''' Shows PDF with bundled viewer or default PDF application.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="useDefaultApp">Use default application for showing PDF files (True or False)</param>
    Sub ShowPdf(ByVal pdfFile As String, ByVal useDefaultApp As Boolean)

    ''' <summary>
    ''' Shows PDF with restricted viewer.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Sub ShowPdfWithRestrictedViewer(ByVal pdfFile As String)

    ''' <summary>
    ''' Closes the restricted PDF viewer.
    ''' </summary>
    Sub CloseRestrictedViewer()

    ''' <summary>
    ''' Reads the information properties from the PDF.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="password">PDF password secure string or Nothing</param>
    ''' <returns>PdfInfoModel object</returns>
    Function ReadPdfInfo(ByVal pdfFile As String, ByVal password As SecureString) As PdfInfoModel

    ''' <summary>
    ''' Writes target PDF with contents from source PDF with information properties applied.
    ''' </summary>
    ''' <param name="sourcePdfFile">Source PDF file name</param>
    ''' <param name="sourcePdfPassword">Source PDF password secure string or Nothing</param>
    ''' <param name="targetPdfFile">Target PDF file name</param>
    ''' <param name="model">PdfInfoModel object</param>
    Sub WritePdfWithInfo(ByVal sourcePdfFile As String, ByVal sourcePdfPassword As SecureString,
                         ByVal targetPdfFile As String, ByVal model As PdfInfoModel)

    ''' <summary>
    ''' Reads the information properties extensions for the PDF.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <returns>PdfInfoExtModel object</returns>
    Function ReadPdfInfoExt(ByVal pdfFile As String) As PdfInfoExtModel

    ''' <summary>
    ''' Writes the information properties extensions for the PDF to an XML file with the same name and in the same
    ''' folder as the PDF.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="model">PdfInfoExtModel object</param>
    Sub WritePdfInfoExt(ByVal pdfFile As String, ByVal model As PdfInfoExtModel)

    ''' <summary>
    ''' Stages the PDF and corresponding information properties extensions XML file for upload.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Sub StagePdfForUpload(ByVal pdfFile As String)
End Interface
