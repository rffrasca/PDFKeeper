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
Public NotInheritable Class UserProfileFolders
    ''' <summary>
    ''' Gets the Documents folder path.
    ''' </summary>
    ''' <returns>Folder path name</returns>
    Public Shared ReadOnly Property Documents As String
        Get
            Return My.Computer.FileSystem.SpecialDirectories.MyDocuments
        End Get
    End Property

    ''' <summary>
    ''' Gets the Downloads folder path.
    ''' </summary>
    ''' <returns>Folder path name</returns>
    Public Shared ReadOnly Property Downloads As String
        Get
            Return NativeMethods.SHGetKnownFolderPath(New Guid("374DE290-123F-4565-9164-39C4925E467B"), 0, Nothing)
        End Get
    End Property
End Class
