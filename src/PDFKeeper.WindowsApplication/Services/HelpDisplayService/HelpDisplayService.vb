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
Public Class HelpDisplayService
    Implements IHelpDisplayService
    Private ReadOnly chmFile As String

    Public Sub New()
        chmFile = "PDFKeeper." & CultureInfo.CurrentCulture.ToString & ".chm"
        If IO.File.Exists(chmFile) = False Then
            chmFile = "PDFKeeper.en-US.chm"
        End If
    End Sub

    Public ReadOnly Property Name As String Implements IHelpDisplayService.Name
        Get
            Return chmFile
        End Get
    End Property

    Public Sub ShowAndWait(helpTopic As String) Implements IHelpDisplayService.ShowAndWait
        Using htmlHelp As New Process
            htmlHelp.StartInfo.FileName = IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "hh.exe")
            htmlHelp.StartInfo.Arguments = "ms-its:" & chmFile & "::" & helpTopic
            htmlHelp.Start()
            htmlHelp.WaitForExit()
        End Using
    End Sub

    Public Sub Show(parentControl As Control, helpTopic As String) Implements IHelpDisplayService.Show
        Help.ShowHelp(parentControl, chmFile, helpTopic)
    End Sub
End Class
