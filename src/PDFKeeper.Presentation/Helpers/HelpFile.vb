' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Public Class HelpFile
    Private ReadOnly _fileName As String

    Public Sub New()
        Dim product = My.Application.Info.ProductName
        _fileName = String.Concat(product, CultureInfo.CurrentCulture.ToString, ".chm")
        If File.Exists(_fileName) = False Then
            _fileName = String.Concat(product, ".en-US.chm")
        End If
    End Sub

    ''' <summary>
    ''' Gets the help file name based on the current culture.
    ''' </summary>
    ''' <returns>
    ''' If available, file name for current culture. Otherwise, file name for en-US.
    ''' </returns>
    Public ReadOnly Property FileName As String
        Get
            Return _fileName
        End Get
    End Property

    ''' <summary>
    ''' Gets the license topic file name.
    ''' </summary>
    ''' <returns>The topic file name.</returns>
    Public Shared ReadOnly Property LicenseTopicFileName As String
        Get
            Return "COPYING.html"
        End Get
    End Property

    ''' <summary>
    ''' Gets the Third-Party Notices topic file name.
    ''' </summary>
    ''' <returns>The topic file name.</returns>
    Public Shared ReadOnly Property ThirdPartyNoticesTopicFileName As String
        Get
            Return "THIRD-PARTY-NOTICES.html"
        End Get
    End Property

    ''' <summary>
    ''' Gets the Donate topic file name.
    ''' </summary>
    ''' <returns>The topic file name.</returns>
    Public Shared ReadOnly Property DonateTopicFileName As String
        Get
            Return "Donate.html"
        End Get
    End Property

    ''' <summary>
    ''' Shows a topic page in the help file and waits for the help dialog to be closed by the user.
    ''' </summary>
    ''' <param name="topic">The topic file name that is contained in the help file.</param>
    Public Sub Show(topic As String)
        Using process = New Process
            With process
                .StartInfo.FileName = Path.Combine(Environment.GetFolderPath(
                                                   Environment.SpecialFolder.Windows), "hh.exe")
                .StartInfo.Arguments = String.Concat("ms-its:", _fileName, "::", topic)
                .Start()
                .WaitForExit()
            End With
        End Using
    End Sub

    ''' <summary>
    ''' Shows a topic page in the help file modelessly.
    ''' </summary>
    ''' <param name="topic">The topic file name that is contained in the help file.</param>
    ''' <param name="parent">The parent dialog, form, or control object of the Help dialog.</param>
    Public Sub Show(topic As String, parent As Control)
        Help.ShowHelp(parent, _fileName, topic)
    End Sub
End Class
