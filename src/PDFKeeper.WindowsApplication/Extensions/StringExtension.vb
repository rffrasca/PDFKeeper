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
Module StringExtension
    <Extension()> _
    Public Function ContainsInvalidFileNameChars(ByVal text As String) As Boolean
        For Each invalidChar In Path.GetInvalidFileNameChars()
            If text.Contains(invalidChar) Then
                Return True
            End If
        Next
        Return False
    End Function

    <Extension()> _
    Public Function InsertDateTimeAndText(ByVal text As String, _
                                          ByVal textToInsert As String) As String
        If text.Length > 0 Then
            text = text & vbCrLf & vbCrLf
        End If
        Return text & "--- " & Date.Now & " (" & _
            textToInsert & ") ---" & vbCrLf
    End Function

    <Extension()> _
    Public Sub SaveToFile(ByVal text As String, ByVal targetFile As String)
        IO.File.WriteAllText(targetFile, text)
    End Sub

    <Extension()> _
    Public Function ValidateProperUsageOfQueryOperators(ByVal text As String) As Boolean
        text = text.ToUpper(CultureInfo.CurrentCulture)
        If text = "MINUS" Or _
            text = "NEAR" Or _
            text = "NOT" Or _
            text = "AND" Or _
            text = "EQUIV" Or _
            text = "WITHIN" Or _
            text = "OR" Or _
            text = "ACCUM" Or _
            text = "FUZZY" Or _
            text = "ABOUT" Or _
            text.StartsWith("MINUS ", StringComparison.CurrentCulture) Or _
            text.StartsWith("NEAR ", StringComparison.CurrentCulture) Or _
            text.StartsWith("NOT ", StringComparison.CurrentCulture) Or _
            text.StartsWith("AND ", StringComparison.CurrentCulture) Or _
            text.StartsWith("EQUIV ", StringComparison.CurrentCulture) Or _
            text.StartsWith("WITHIN ", StringComparison.CurrentCulture) Or _
            text.StartsWith("OR ", StringComparison.CurrentCulture) Or _
            text.StartsWith("ACCUM ", StringComparison.CurrentCulture) Or _
            text.StartsWith("FUZZY ", StringComparison.CurrentCulture) Or _
            text.StartsWith("ABOUT ", StringComparison.CurrentCulture) Or _
            text.EndsWith("MINUS", StringComparison.CurrentCulture) Or _
            text.EndsWith("NEAR", StringComparison.CurrentCulture) Or _
            text.EndsWith("NOT", StringComparison.CurrentCulture) Or _
            text.EndsWith("AND", StringComparison.CurrentCulture) Or _
            text.EndsWith("EQUIV", StringComparison.CurrentCulture) Or _
            text.EndsWith("WITHIN", StringComparison.CurrentCulture) Or _
            text.EndsWith("OR", StringComparison.CurrentCulture) Or _
            text.EndsWith("ACCUM", StringComparison.CurrentCulture) Or _
            text.EndsWith("FUZZY", StringComparison.CurrentCulture) Or _
            text.EndsWith("ABOUT", StringComparison.CurrentCulture) Or _
            text.IndexOf("{}", StringComparison.Ordinal) <> -1 Or _
            text.IndexOf("()", StringComparison.Ordinal) <> -1 Or _
            text.Substring(0, 1) = "=" Or _
            text.Substring(0, 1) = ";" Or _
            text.Substring(0, 1) = ">" Or _
            text.Substring(0, 1) = "-" Or _
            text.Substring(0, 1) = "~" Or _
            text.Substring(0, 1) = "&" Or _
            text.Substring(0, 1) = "|" Or _
            text.Substring(0, 1) = "," Or _
            text.Substring(0, 1) = "!" Or _
            text.Substring(0, 1) = "{" Or _
            text.Substring(0, 1) = "(" Or _
            text = "?" Or _
            text = "$" Then
            Return False
        Else
            Return True
        End If
    End Function
End Module
