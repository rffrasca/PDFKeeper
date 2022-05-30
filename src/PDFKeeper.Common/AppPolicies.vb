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
Public NotInheritable Class AppPolicies
    ''' <summary>
    ''' Application policy name type.
    ''' </summary>
    Public Enum AppPolicy
        RemoveListAllDocuments
    End Enum

    ''' <summary>
    ''' Gets the application policy value.
    ''' </summary>
    ''' <param name="policy">Policy name</param>
    ''' <returns>0 or 1</returns>
    Public Shared Function GetValue(ByVal policy As AppPolicy) As Integer
        If My.Computer.Registry.GetValue(String.Concat("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\",
                                                       My.Application.Info.CompanyName, "\",
                                                       My.Application.Info.ProductName), policy.ToString,
                                                                                         Nothing) = 1 Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class
