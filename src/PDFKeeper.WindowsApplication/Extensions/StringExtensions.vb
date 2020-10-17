'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Module StringExtensions
    ''' <summary>
    ''' Returns a new String containing a carriage return, followed by the
    ''' date, time, and specified text appended to the String object.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <param name="valueToAppend"></param>
    ''' <returns>Appended string</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function AppendDateTimeAndText(ByVal valueParam As String,
                                          ByVal valueToAppend As String) As String
        If valueParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(valueParam))
        End If
        If valueParam.Length > 0 Then
            valueParam = valueParam & vbCrLf & vbCrLf
        End If
        Return valueParam & "--- " & Date.Now & " (" &
            valueToAppend & ") ---" & vbCrLf
    End Function

    ''' <summary>
    ''' Returns a new String containing a carriage return (only if valueParam
    ''' doesn't end in a line feed character), followed by the specified text
    ''' appended to the String object.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <param name="valueToAppend"></param>
    ''' <returns>Appended string</returns>
    <Extension()>
    Public Function AppendText(ByVal valueParam As String,
                               ByVal valueToAppend As String) As String
        If valueParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(valueParam))
        End If
        If valueParam.Length > 0 Then
            If valueParam.Substring(valueParam.Length - 1) <> vbLf Then
                valueParam &= vbCrLf
            End If
        End If
        Return valueParam & valueToAppend & vbCrLf
    End Function

    ''' <summary>
    ''' Verifies String object contains characters not allowed in file names.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ContainsInvalidFileNameChars(ByVal valueParam As String) As Boolean
        If valueParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(valueParam))
        End If
        For Each invalidChar In Path.GetInvalidFileNameChars()
            If valueParam.Contains(invalidChar) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Prints the string object to the selected printer.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub Print(ByVal valueParam As String)
        Dim print As IStringPrintService = New StringPrintService(valueParam)
        print.Print()
    End Sub

    ''' <summary>
    ''' Previews the string object for printing using the default printer.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <param name="printPreviewFormSize"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub PrintPreview(ByVal valueParam As String,
                            ByVal printPreviewFormSize As System.Drawing.Size)
        Dim printPreview As IStringPrintService = New StringPrintService(valueParam)
        printPreview.PrintPreview(printPreviewFormSize)
    End Sub

    ''' <summary>
    ''' Writes the String object to the specified file path.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <param name="filePath"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub WriteToFile(ByVal valueParam As String,
                           ByVal filePath As String)
        IO.File.WriteAllText(filePath, valueParam)
    End Sub

    ''' <summary>
    ''' Verifies proper usage of query operators in String object.
    ''' </summary>
    ''' <param name="valueParam"></param>
    ''' <returns>True or False</returns>
    ''' <remarks>
    ''' These query operators are specific to the Oracle Database.
    ''' </remarks>
    <Extension()>
    Public Function VerifyProperUsageOfQueryOperators(ByVal valueParam As String) As Boolean
        If valueParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(valueParam))
        End If
        valueParam = valueParam.ToUpper(CultureInfo.CurrentCulture)
        If valueParam = "MINUS" Or
            valueParam = "NEAR" Or
            valueParam = "NOT" Or
            valueParam = "AND" Or
            valueParam = "EQUIV" Or
            valueParam = "WITHIN" Or
            valueParam = "OR" Or
            valueParam = "ACCUM" Or
            valueParam = "FUZZY" Or
            valueParam = "ABOUT" Or
            valueParam.StartsWith("MINUS ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("NEAR ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("NOT ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("AND ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("EQUIV ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("WITHIN ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("OR ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("ACCUM ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("FUZZY ", StringComparison.CurrentCulture) Or
            valueParam.StartsWith("ABOUT ", StringComparison.CurrentCulture) Or
            valueParam.IndexOf("{}", StringComparison.Ordinal) <> -1 Or
            valueParam.IndexOf("()", StringComparison.Ordinal) <> -1 Or
            valueParam.Substring(0, 1) = "=" Or
            valueParam.Substring(0, 1) = ";" Or
            valueParam.Substring(0, 1) = ">" Or
            valueParam.Substring(0, 1) = "-" Or
            valueParam.Substring(0, 1) = "~" Or
            valueParam.Substring(0, 1) = "&" Or
            valueParam.Substring(0, 1) = "|" Or
            valueParam.Substring(0, 1) = "," Or
            valueParam.Substring(0, 1) = "!" Or
            valueParam.Substring(0, 1) = "{" Or
            valueParam.Substring(0, 1) = "(" Or
            valueParam = "?" Or
            valueParam = "$" Then
            Return False
        Else
            Return True
        End If
    End Function
End Module
