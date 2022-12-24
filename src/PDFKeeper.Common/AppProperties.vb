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
Public NotInheritable Class AppProperties
    ''' <summary>
    ''' Gets the application registry top level full key name.
    ''' </summary>
    ''' <returns>Registry key full name</returns>
    Public Shared ReadOnly Property RegistryTopLevelKeyFullName As String
        Get
            Return String.Concat("HKEY_CURRENT_USER\SOFTWARE\", My.Application.Info.CompanyName, "\",
                                 My.Application.Info.ProductName)
        End Get
    End Property

    ''' <summary>
    ''' Gets the PDFKeeper home page URI.
    ''' </summary>
    ''' <returns>URI</returns>
    Public Shared ReadOnly Property HomePageUrl As Uri
        Get
            Return New Uri("https://www.pdfkeeper.org/")
        End Get
    End Property

    ''' <summary>
    ''' Gets the PDFKeeper AutoUpdater configuration file URI.
    ''' </summary>
    ''' <returns>URI</returns>
    Public Shared ReadOnly Property AutoUpdaterConfigUri As Uri
        Get
            Return New Uri("https://raw.githubusercontent.com/rffrasca/PDFKeeper/master/config/PDFKeeper.AutoUpdater.config.xml")
        End Get
    End Property
End Class
