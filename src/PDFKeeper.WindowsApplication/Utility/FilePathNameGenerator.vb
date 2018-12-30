'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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
Public NotInheritable Class FilePathNameGenerator
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Function GenerateCachePdfFilePathName(ByVal id As Integer) As String
        Return Path.Combine(ApplicationDirectories.Cache, _
                            My.Application.Info.ProductName & id & ".pdf")
    End Function

    Public Shared Function GenerateCachePdfPreviewFilePathName(ByVal id As Integer) As String
        Return Path.Combine(Path.GetDirectoryName(FilePathNameGenerator.GenerateCachePdfFilePathName(id)), _
                            Path.GetFileNameWithoutExtension(FilePathNameGenerator.GenerateCachePdfFilePathName(id)) & "-" & _
                            My.Settings.PreviewImageResolution & "-000001.png")
    End Function
End Class
