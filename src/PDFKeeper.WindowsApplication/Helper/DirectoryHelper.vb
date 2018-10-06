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
Public NotInheritable Class DirectoryHelper
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Sub CreateShortcut(ByVal shortcutFileName As String, _
                                     ByVal directory As String)
        Dim wshShell As New WshShell
        Dim shortcutName As IWshShortcut = _
            DirectCast(wshShell.CreateShortcut(shortcutFileName), IWshShortcut)
        shortcutName.TargetPath = directory
        ' Only create the shortcut if it does not exist.  This is to prevent
        ' the occassional IOException: "The process cannot access the file 
        ' because it is being used by another process" from being thrown.
        If IO.File.Exists(ApplicationDirectories.UploadShortcut) = False Then
            shortcutName.Save()
        End If
    End Sub

    Public Shared Function GetCountOfFiles(ByVal directory As String, _
                                           ByVal searchOption As SearchOption) As Integer
        Dim dirInfo As New DirectoryInfo(directory)
        Return dirInfo.GetFiles("*", searchOption).Count
    End Function
End Class
