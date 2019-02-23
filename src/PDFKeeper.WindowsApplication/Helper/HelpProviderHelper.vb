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
Public NotInheritable Class HelpProviderHelper
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    ''' <summary>
    ''' Gets the help file based on the current culture. If a help file does
    ''' not exist for the current culture, then default to the help file for
    ''' en-US. 
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' Help file name with extension. The current working directory is
    ''' assumed.
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property HelpFile As String
        Get
            Dim chmFile As String = _
                "PDFKeeper." & CultureInfo.CurrentCulture.ToString & ".chm"
            If IO.File.Exists(chmFile) = False Then
                chmFile = "PDFKeeper.en-US.chm"
            End If
            Return chmFile
        End Get
    End Property

    ''' <summary>
    ''' Opens the help file at the specified topic page and waits until closed.
    ''' </summary>
    ''' <param name="helpTopic">
    ''' Topic file with extension contained in help file to display.
    ''' </param>
    ''' <remarks></remarks>
    Public Shared Sub OpenHelpFileAndWait(ByVal helpTopic As String)
        Using htmlHelp As New Process
            htmlHelp.StartInfo.FileName = Path.Combine( _
                Environment.GetFolderPath(Environment.SpecialFolder.Windows), _
                "hh.exe")
            htmlHelp.StartInfo.Arguments = "ms-its:" & HelpFile & "::" & helpTopic
            htmlHelp.Start()
            htmlHelp.WaitForExit()
        End Using
    End Sub

    ''' <summary>
    ''' Shows the help file at the specified topic in the Help dialog box.
    ''' </summary>
    ''' <param name="parentControl">
    ''' Parent form or control of the help dialog box. The Me object can be
    ''' specified as the parentControl.
    ''' </param>
    ''' <param name="helpTopic">
    ''' Topic file with extension contained in help file to display.
    ''' </param>
    ''' <remarks></remarks>
    Public Shared Sub ShowHelp(ByVal parentControl As System.Windows.Forms.Control, _
                               ByVal helpTopic As String)
        Help.ShowHelp(parentControl, HelpFile, helpTopic)
    End Sub
End Class
