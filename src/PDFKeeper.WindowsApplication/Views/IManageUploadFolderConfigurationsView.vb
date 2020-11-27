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
Public Interface IManageUploadFolderConfigurationsView
    Inherits ICommonView
    Property UploadFolderConfigurations As Object
    Property UploadFolderConfigurationsElementsEnabled As Boolean
    Property UploadFolderConfiguration As String
    Property AddEnabled As Boolean
    Property EditEnabled As Boolean
    Property DeleteEnabled As Boolean
    Property UploadFolderConfigurationElementsEnabled As Boolean
    Property FolderName As String
    Property FolderNameError As String
    Property Titles As Object
    Property Title As String
    Property Keywords As String
    Property FlagDocument As Boolean
    Property SaveEnabled As Boolean
End Interface
