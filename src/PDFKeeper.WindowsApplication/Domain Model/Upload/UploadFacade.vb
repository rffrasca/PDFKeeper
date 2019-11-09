'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public NotInheritable Class UploadFacade
    Private Shared executing As Boolean
    Private Shared paused As Boolean

    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared ReadOnly Property CanUploadBeExecuted As Boolean
        Get
            If executing Or paused Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Shared Sub PauseUpload(ByVal value As Boolean)
        WaitForUploadToFinish()
        paused = value
    End Sub

    Public Shared Sub ExecuteUpload()
        If CanUploadBeExecuted = False Then
            Throw New InvalidOperationException( _
                My.Resources.UploadCannotBeStarted)
        End If
        executing = True
        Upload.Execute()
        executing = False
    End Sub

    Public Shared Sub WaitForUploadToFinish()
        Do While executing
            Threading.Thread.Sleep(1000)
        Loop
    End Sub
End Class
