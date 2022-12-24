'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Public Interface IUploadService
    ''' <summary>
    ''' Do PDF files to upload exist?
    ''' </summary>
    ''' <returns>True or False</returns>
    ReadOnly Property PdfFilesToUploadExist As Boolean

    ''' <summary>
    ''' Were documents uploaded?
    ''' </summary>
    ''' <returns>True or False</returns>
    ReadOnly Property DocumentsUploaded As Boolean

    ''' <summary>
    ''' Do any rejected PDF files exist?
    ''' </summary>
    ''' <returns>True or False</returns>
    ReadOnly Property RejectedPdfFilesExist As Boolean

    ''' <summary>
    ''' Executes upload folder maintenance tasks:
    ''' 
    ''' 1. Creates missing upload profile folders.
    ''' 2. Deletes dormant upload folders that are either empty (not profile folder) or any empty sub-folder under each
    '''    upload profile folder.
    ''' </summary>
    Sub ExecuteUploadFolderMaintenance()

    ''' <summary>
    ''' Executes PDF document upload tasks.
    ''' 
    ''' 1. Stages PDF documents in UploadFolders for uploading.
    ''' 2. Uploads all PDF documents in UploadStaging to the repository.
    ''' </summary>
    Sub ExecuteUpload()
End Interface
