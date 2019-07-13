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
Public Class UploadFacade
    Private Shared s_Instance As UploadFacade
    Private executing As Boolean
    Private paused As Boolean

    ''' <summary>
    ''' Prevents multiple instances of this class and allows this class to be
    ''' subclassed. 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub New()
    End Sub

    ''' <summary>
    ''' Allows single instance access to the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance As UploadFacade
        Get
            If s_Instance Is Nothing Then
                s_Instance = New UploadFacade
            End If
            Return s_Instance
        End Get
    End Property

    ReadOnly Property CanUploadBeExecuted As Boolean
        Get
            If executing Or paused Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Sub PauseUpload(ByVal value As Boolean)
        WaitForUploadToFinish()
        If value Then
            paused = True
        Else
            paused = False
        End If
    End Sub

    Public Sub ExecuteUpload()
        If CanUploadBeExecuted = False Then
            Throw New InvalidOperationException( _
                My.Resources.UploadCannotBeStarted)
        End If
        executing = True
        Dim upload As ICommand = New UploadCommand
        upload.Execute()
        executing = False
    End Sub

    Public Sub WaitForUploadToFinish()
        Do While executing
            Threading.Thread.Sleep(1000)
        Loop
    End Sub
End Class
