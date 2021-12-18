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
Public Class MessageDisplayService
    Implements IMessageDisplayService
    Private options As MessageBoxOptions

    Public Sub Show(message As String, isError As Boolean) Implements IMessageDisplayService.Show
        If CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft Then
            options = MessageBoxOptions.RtlReading
        Else
            options = 0
        End If
        Dim icon As MessageBoxIcon = MessageBoxIcon.Information
        If isError Then
            icon = MessageBoxIcon.Error
        End If
        MessageBox.Show(message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        icon,
                        MessageBoxDefaultButton.Button1,
                        options)
    End Sub
End Class
