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
Imports System.IO
Imports PDFKeeper.Common

Public Class MoveSqliteDatabaseCommand
    Implements ICommand
    Private ReadOnly destFolderPath As String

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="destFolderPath">Destination folder path</param>
    Public Sub New(ByVal destFolderPath As String)
        Me.destFolderPath = destFolderPath
    End Sub

    ''' <summary>
    ''' Moves the local SQLite database to a new folder.
    ''' </summary>
    Public Sub Execute() Implements ICommand.Execute
        Dim dbFilePath = Path.Combine(destFolderPath, String.Concat(My.Application.Info.ProductName, ".sqlite"))
        File.Move(DbSession.LocalDatabasePath, dbFilePath)
        DbSession.LocalDatabasePath = dbFilePath
    End Sub
End Class
