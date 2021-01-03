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
Public NotInheritable Class ProductUpdate
    Private Shared ReadOnly newDbTableColumnsTempFile As String =
        Path.Combine(Path.GetTempPath,
                     "PDFKeeper.docs.tmp")

    Private Sub New()
        ' All members are shared.
    End Sub

    ''' <summary>
    ''' Check that new database table columns can be populated.
    ''' 
    ''' This property should be checked when setting initial state on all
    ''' toolstrip items.
    ''' </summary>
    ''' <returns>True or False</returns>
    Public Shared ReadOnly Property NewDbTableColumnsCanBePopulated As Boolean
        Get
            If IO.File.Exists(newDbTableColumnsTempFile) Then
                Dim text As String = IO.File.ReadAllText(newDbTableColumnsTempFile)
                If text.Contains("DOC_TEXT_ANNOTATIONS") And
                    text.Contains("DOC_TEXT") Then
                    Return True
                Else
                    DeleteNewDbTableColumnsTempFile()
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' This method is called during the load of the MainForm and from a Timer
    ''' on the same form to check for a product update.
    ''' </summary>
    Public Shared Sub Start()
        Dim configUrl As String =
            "https://raw.githubusercontent.com/rffrasca/PDFKeeper/master/config/PDFKeeper.AutoUpdater.config.xml"
        AutoUpdater.RunUpdateAsAdmin = False
        AutoUpdater.Start(configUrl)
    End Sub

    ''' <summary>
    ''' Deletes the new database table columns temp file that is created by the
    ''' database schema upgrade.
    ''' 
    ''' This method should be called after the new database table columns have
    ''' been successfully populated.
    ''' </summary>
    Public Shared Sub DeleteNewDbTableColumnsTempFile()
        If IO.File.Exists(newDbTableColumnsTempFile) Then
            IO.File.Delete(newDbTableColumnsTempFile)
        End If
    End Sub
End Class
