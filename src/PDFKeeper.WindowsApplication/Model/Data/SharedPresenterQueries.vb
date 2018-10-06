'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public NotInheritable Class SharedPresenterQueries
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="0#")> _
    Public Shared Sub GetAuthors(ByRef authors As DataTable)
        authors = DoQueryAuthors()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="0#")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="1#")> _
    Public Shared Sub GetAuthors(ByRef author As String, ByRef authors As DataTable)
        Dim currentAuthor As String = author
        authors = DoQueryAuthors()
        author = currentAuthor
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="1#")> _
    Public Shared Sub GetSubjectsByAuthor(ByVal author As String, _
                                          ByRef subjects As DataTable)
        subjects = DoQuerySubjectsByAuthor(author)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="1#")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", MessageId:="2#")> _
    Public Shared Sub GetSubjectsByAuthor(ByVal author As String, _
                                          ByRef subject As String, _
                                          ByRef subjects As DataTable)
        Dim currentSubject As String = subject
        subjects = DoQuerySubjectsByAuthor(author)
        subject = currentSubject
    End Sub

    Private Shared Function DoQueryAuthors() As DataTable
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            Return queryService.GetAuthors
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Shared Function DoQuerySubjectsByAuthor(ByVal author As String) As DataTable
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            Return queryService.GetSubjectsByAuthor(author)
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
            Return Nothing
        End Try
    End Function
End Class
