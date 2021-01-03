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
Public NotInheritable Class QueryOperatorChecker
    ''' <summary>
    ''' Is query operator syntax correct in string.
    ''' </summary>
    ''' <param name="dbPlatform">Database platform used.</param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function IsSyntaxCorrect(ByVal dbPlatform As String,
                                           ByVal value As String) As Boolean
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
        Dim result As Boolean
        If dbPlatform = DatabasePlatform.Sqlite.ToString Then
            result = IsSyntaxCorrectForSqlite(value)
        ElseIf dbPlatform = DatabasePlatform.Oracle.ToString Then
            result = IsSyntaxCorrectForOracle(value)
        End If
        Return result
    End Function

    Private Shared Function IsSyntaxCorrectForSqlite(ByVal value As String) As Boolean
        If value.Contains("&") Or
            value.Contains("!") Or
            value.Contains("?") Or
            value.Contains("/") Or
            value.Contains("\") Or
            value.Contains(",") Or
            value.StartsWith("*", StringComparison.CurrentCulture) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Shared Function IsSyntaxCorrectForOracle(ByVal value As String) As Boolean
        If value = "MINUS" Or
                value = "NEAR" Or
                value = "NOT" Or
                value = "AND" Or
                value = "EQUIV" Or
                value = "WITHIN" Or
                value = "OR" Or
                value = "ACCUM" Or
                value = "FUZZY" Or
                value = "ABOUT" Or
                value.StartsWith("MINUS ", StringComparison.CurrentCulture) Or
                value.StartsWith("NEAR ", StringComparison.CurrentCulture) Or
                value.StartsWith("NOT ", StringComparison.CurrentCulture) Or
                value.StartsWith("AND ", StringComparison.CurrentCulture) Or
                value.StartsWith("EQUIV ", StringComparison.CurrentCulture) Or
                value.StartsWith("WITHIN ", StringComparison.CurrentCulture) Or
                value.StartsWith("OR ", StringComparison.CurrentCulture) Or
                value.StartsWith("ACCUM ", StringComparison.CurrentCulture) Or
                value.StartsWith("FUZZY ", StringComparison.CurrentCulture) Or
                value.StartsWith("ABOUT ", StringComparison.CurrentCulture) Or
                value.IndexOf("{}", StringComparison.Ordinal) <> -1 Or
                value.IndexOf("()", StringComparison.Ordinal) <> -1 Or
                value.Substring(0, 1) = "=" Or
                value.Substring(0, 1) = ";" Or
                value.Substring(0, 1) = ">" Or
                value.Substring(0, 1) = "-" Or
                value.Substring(0, 1) = "~" Or
                value.Substring(0, 1) = "&" Or
                value.Substring(0, 1) = "|" Or
                value.Substring(0, 1) = "," Or
                value.Substring(0, 1) = "!" Or
                value.Substring(0, 1) = "{" Or
                value.Substring(0, 1) = "(" Or
                value = "?" Or
                value = "$" Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
