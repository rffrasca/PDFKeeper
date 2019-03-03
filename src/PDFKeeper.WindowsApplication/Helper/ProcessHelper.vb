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
Public NotInheritable Class ProcessHelper
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Function IsProcessExpired(ByVal processId As Integer) As Boolean
        Try
            Dim process As System.Diagnostics.Process
            process = System.Diagnostics.Process.GetProcessById(processId)
            process.Dispose()
            Return False
        Catch ex As ArgumentException
            Return True
        End Try
    End Function

    Public Shared Sub Close(ByVal processId As Integer)
        Try
            Dim process As System.Diagnostics.Process
            process = System.Diagnostics.Process.GetProcessById(processId)
            process.CloseMainWindow()
            process.Close()
        Catch ex As ArgumentException
        End Try
    End Sub
End Class
