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
Public NotInheritable Class ApplicationPolicy
    Private Sub New()
        ' All members are shared.
    End Sub

    Public Shared ReadOnly Property DisableQueryAllDocuments As Boolean
        Get
            Return GetPolicyValue("DisableQueryAllDocuments")
        End Get
    End Property

    Private Shared Function GetPolicyValue(ByVal policy As String) As Boolean
        If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\" & _
                                         My.Application.Info.CompanyName & "\" & _
                                         My.Application.Info.ProductName, _
                                         policy, _
                                         Nothing) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
