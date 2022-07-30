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
Public Class SearchTermHistoryService
    Implements ISearchTermHistoryService
    Private ReadOnly history As New List(Of String)

    Public Sub AddToHistory(value As String) Implements ISearchTermHistoryService.AddToHistory
        If Not history.Contains(value) Then
            history.Add(value)
        End If
    End Sub

    Public Function ListHistory() As Object Implements ISearchTermHistoryService.ListHistory
        Return history.ToArray
    End Function
End Class
