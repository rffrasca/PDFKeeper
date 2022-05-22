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
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure

Public Class DocumentListService
    Implements IDocumentListService
    Private ReadOnly repository As IDocumentRepository

    Public Sub New(ByVal repository As IDocumentRepository)
        Me.repository = repository
    End Sub

    Public Function ListDocumentsByText(text As String) As DataTable Implements IDocumentListService.ListDocumentsByText
        If text Is Nothing Then
            Throw New ArgumentNullException(NameOf(text))
        End If
        text = text.Trim
        If IsQueryOperatorSyntaxCorrect(text) = False Then
            Throw New FormatException
        End If
        Return repository.ListDocuments(IDocumentRepository.DocumentListAction.ByText, text)
    End Function

    Public Function ListDocumentsBySelections(filter As FindSelectionsFilterModel) As DataTable Implements IDocumentListService.ListDocumentsBySelections
        Return repository.ListDocuments(filter)
    End Function

    Public Function ListDocumentsByDateAdded(dateAdded As String) As DataTable Implements IDocumentListService.ListDocumentsByDateAdded
        Return repository.ListDocuments(IDocumentRepository.DocumentListAction.ByDateAdded, dateAdded)
    End Function

    Public Function ListFlaggedDocuments() As DataTable Implements IDocumentListService.ListFlaggedDocuments
        Return repository.ListDocuments(IDocumentRepository.DocumentListAction.Flagged)
    End Function

    Public Function ListAllDocuments() As DataTable Implements IDocumentListService.ListAllDocuments
        Return repository.ListDocuments(IDocumentRepository.DocumentListAction.All)
    End Function

    Private Function IsQueryOperatorSyntaxCorrect(ByVal syntax As String) As Boolean
        If syntax Is Nothing Then
            Throw New ArgumentNullException(NameOf(syntax))
        End If
        Dim result As Boolean
        If DbSession.Platform = DbSession.DbPlatform.Oracle Then
            result = IsQueryOperatorSyntaxCorrectForOracle(syntax)
        ElseIf DbSession.Platform = DbSession.DbPlatform.Sqlite Then
            result = IsQueryOperatorSyntaxCorrectForSqlite(syntax)
        End If
        Return result
    End Function

    Private Function IsQueryOperatorSyntaxCorrectForOracle(ByVal syntax As String) As Boolean
        With syntax
            If syntax = "MINUS" Or syntax = "NEAR" Or syntax = "NOT" Or syntax = "AND" Or syntax = "EQUIV" Or
                syntax = "WITHIN" Or syntax = "OR" Or syntax = "ACCUM" Or syntax = "FUZZY" Or syntax = "ABOUT" Or
                .StartsWith("MINUS ", StringComparison.CurrentCulture) Or
                .StartsWith("NEAR ", StringComparison.CurrentCulture) Or
                .StartsWith("NOT ", StringComparison.CurrentCulture) Or
                .StartsWith("AND ", StringComparison.CurrentCulture) Or
                .StartsWith("EQUIV ", StringComparison.CurrentCulture) Or
                .StartsWith("WITHIN ", StringComparison.CurrentCulture) Or
                .StartsWith("OR ", StringComparison.CurrentCulture) Or
                .StartsWith("ACCUM ", StringComparison.CurrentCulture) Or
                .StartsWith("FUZZY ", StringComparison.CurrentCulture) Or
                .StartsWith("ABOUT ", StringComparison.CurrentCulture) Or
                .IndexOf("{}", StringComparison.Ordinal) <> -1 Or .IndexOf("()", StringComparison.Ordinal) <> -1 Or
                .Substring(0, 1) = "=" Or .Substring(0, 1) = ";" Or .Substring(0, 1) = ">" Or .Substring(0, 1) = "-" Or
                .Substring(0, 1) = "~" Or .Substring(0, 1) = "&" Or .Substring(0, 1) = "|" Or .Substring(0, 1) = "," Or
                .Substring(0, 1) = "!" Or .Substring(0, 1) = "{" Or .Substring(0, 1) = "(" Or syntax = "?" Or
                syntax = "$" Then
                Return False
            Else
                Return True
            End If
        End With
    End Function

    Private Function IsQueryOperatorSyntaxCorrectForSqlite(ByVal syntax As String) As Boolean
        With syntax
            If .Contains("&") Or .Contains("!") Or .Contains("?") Or .Contains("/") Or .Contains("\") Or
                .Contains(",") Or .StartsWith("*", StringComparison.CurrentCulture) Then
                Return False
            Else
                Return True
            End If
        End With
    End Function
End Class
