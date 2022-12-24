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
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure

Public Class LoginService
    Implements ILoginService
    Private repository As IDocumentRepository

    Public Sub InitSession(platform As String, model As DbSessionModel) Implements ILoginService.InitSession
        If model Is Nothing Then
            Throw New NullReferenceException(NameOf(model))
        End If
        Dim list = DirectCast(System.Enum.GetValues(GetType(DbSession.DbPlatform)), IList)
        For i = 0 To list.Count - 1
            Dim item = list(i)
            If item.ToString = platform Then
                DbSession.Platform = item
            End If
        Next
        If DbSession.Platform <> DbSession.DbPlatform.Sqlite Then
            With model
                DbSession.UserName = .UserName.Trim
                DbSession.Password = .Password
                DbSession.DataSource = .DataSource.Trim
            End With
            repository = DocumentRepositoryFactory.Repository
            repository.TestConnection()
        Else
            Throw New NotSupportedException
        End If
    End Sub

    Public Sub ResetCredential() Implements ILoginService.ResetCredential
        Try
            repository.ResetCredential()
        Catch ex As NotSupportedException
        End Try
    End Sub
End Class
