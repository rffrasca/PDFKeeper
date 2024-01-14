' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Imports PDFKeeper.Core.Services

Public Class SaveFileDialogService
    Implements IFileDialogService

    Public Function ShowDialog(filter As String, fileName As String) As String Implements IFileDialogService.ShowDialog
        Using dialog = New SaveFileDialog
            dialog.Filter = filter
            dialog.FileName = fileName
            If dialog.ShowDialog() = DialogResult.OK Then
                Return dialog.FileName
            Else
                Return Nothing
            End If
        End Using
    End Function
End Class
