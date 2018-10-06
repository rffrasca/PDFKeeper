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
Public NotInheritable Class ApplicationDirectories
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared ReadOnly Property Cache As String
        Get
            Dim folder As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.Cache)
            Directory.CreateDirectory(folder)
            Return folder.ToString
        End Get
    End Property

    Public Shared ReadOnly Property Upload As String
        Get
            Dim folder As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.Upload)
            Directory.CreateDirectory(folder)
            DirectoryHelper.CreateShortcut(UploadShortcut, folder)
            Return folder.ToString
        End Get
    End Property

    Public Shared ReadOnly Property UploadShortcut As String
        Get
            Return Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, _
                                Application.ProductName & " " & My.Resources.Upload & ".lnk")
        End Get
    End Property

    Public Shared ReadOnly Property UploadConfig As String
        Get
            Dim folder As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.UploadConfig)
            Directory.CreateDirectory(folder)
            Return folder.ToString
        End Get
    End Property

    Public Shared ReadOnly Property UploadStaging As String
        Get
            Dim folder As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.UploadStaging)
            Directory.CreateDirectory(folder)
            Return folder.ToString
        End Get
    End Property

    Private Shared ReadOnly Property ApplicationDataRoot As String
        Get
            Dim folder As String = Path.Combine( _
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _
                My.Application.Info.CompanyName, _
                Application.ProductName)
            Directory.CreateDirectory(folder)
            Return folder.ToString
        End Get
    End Property
End Class
