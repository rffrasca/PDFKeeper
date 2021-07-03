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
Public NotInheritable Class TaxYearsGenerator
    ''' <summary>
    ''' Generates a range of tax years that includes the last 10 years and 1
    ''' year into the future.
    ''' </summary>
    ''' <returns>Array of tax years sorted in descending order.</returns>
    Public Shared Function ToArray() As Object
        Dim taxYears As New ArrayList From {
            String.Empty
        }
        Dim taxYearsTemp As New ArrayList
        Dim lastYear As Integer = DateTime.Now.Year
        Dim j As Integer = lastYear - 10
        Do While j <= lastYear
            j += 1
            taxYearsTemp.Add(j)
        Loop
        taxYearsTemp.Reverse()
        taxYears.AddRange(taxYearsTemp)
        Return taxYears.ToArray
    End Function
End Class
