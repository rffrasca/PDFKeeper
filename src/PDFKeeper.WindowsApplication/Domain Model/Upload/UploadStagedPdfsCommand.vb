'******************************************************************************
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
Public Class UploadStagedPdfsCommand
    Implements ICommand

    ''' <summary>
    ''' Uploads all PDF files and supplemental data in the UploadStaging
    ''' folder. 
    ''' </summary>
    ''' <remarks>
    ''' Password protected PDF files will not be uploaded.
    ''' </remarks>
    Public Sub Execute() Implements ICommand.Execute
        Dim pdfs = Directory.GetFiles(UserProfile.UploadStagingPath, _
                                          "*.pdf", _
                                          SearchOption.TopDirectoryOnly).OrderBy( _
                                          Function(f) New FileInfo(f).LastWriteTime)
        For Each pdf In pdfs
            UploadPdfAndSupplementalData(pdf)
        Next
    End Sub

    Private Shared Sub UploadPdfAndSupplementalData(ByVal pdfPath As String)
        Dim fileInfo As New FileInfo(pdfPath)
        fileInfo.WaitWhileIsInUse()
        Dim pdfInfo As New PdfFileInfo(pdfPath)
        If pdfInfo.ContainsOwnerPassword = False Then
            Try
                Dim pdfReader As New PdfInformationPropertiesReader(pdfPath)
                If pdfReader.Title IsNot Nothing And _
                    pdfReader.Author IsNot Nothing And _
                    pdfReader.Subject IsNot Nothing Then
                    Dim notes As String = Nothing
                    Dim category As String = Nothing
                    Dim flag As Integer = 0
                    Dim suppDataHelper As New PdfSupplementalDataHelper(pdfPath)
                    Dim suppData As PdfSupplementalData = suppDataHelper.Read
                    If suppData IsNot Nothing Then
                        notes = suppData.Notes
                        category = suppData.Category
                        flag = suppData.FlagState
                    End If
                    Dim dataClient As IDataClient = New DataClient
                    dataClient.CreateRecord(pdfReader.Title, _
                                            pdfReader.Author, _
                                            pdfReader.Subject, _
                                            pdfReader.Keywords, _
                                            notes, _
                                            pdfPath, _
                                            category, _
                                            flag)
                    fileInfo.DeleteToRecycleBin()
                    Dim suppDataXmlPath As String = _
                        Path.ChangeExtension(pdfPath, "xml")
                    If suppData IsNot Nothing Then
                        Dim xmlFileInfo As New FileInfo(suppDataXmlPath)
                        xmlFileInfo.DeleteToRecycleBin()
                    Else
                        IO.File.Delete(suppDataXmlPath)
                    End If
                End If
            Catch ex As BadPasswordException    ' Ignore the file.            
            End Try
        End If
    End Sub
End Class
