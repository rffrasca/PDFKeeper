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
Public Interface IFileSelect
    Inherits IDisposable

    ''' <summary>
    ''' Gets/Sets the file name to prefill in the SaveFileDialog.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FileName As String

    ''' <summary>
    ''' Gets/Sets the file extension to filter in the FileDialog.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Filter As String

    ''' <summary>
    ''' Shows OpenFileDialog for selecting an existing file.
    ''' </summary>
    ''' <returns>
    ''' File path of the selected file or Nothing if cancel was selected.
    ''' </returns>
    ''' <remarks></remarks>
    Function ShowOpen() As String

    ''' <summary>
    ''' Shows SaveFileDialog for specifying a file.
    ''' </summary>
    ''' <returns>
    ''' File path of the specified file or Nothing if cancel was selected.
    ''' </returns>
    ''' <remarks></remarks>
    Function ShowSave() As String
End Interface
