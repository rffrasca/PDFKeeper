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
Public NotInheritable Class UploadFolderConfigurationTokens
    Private Sub New()
        ' All members are shared.
    End Sub

    Public Shared ReadOnly Property DateToken As String
        Get
            Return My.Resources.DateToken   'yyyy-MM-dd
        End Get
    End Property

    Public Shared ReadOnly Property DateTimeToken As String
        Get
            Return My.Resources.DateTimeToken   'yyyy-MM-dd HH:mm:ss
        End Get
    End Property

    Public Shared ReadOnly Property FileNameToken As String
        Get
            Return My.Resources.FileNameToken
        End Get
    End Property

    ''' <summary>
    ''' Returns an object containing all Upload Folder Configuration Tokens as
    ''' an array.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ToArray() As Object
        Dim tokens As New GenericList(Of String)
        tokens.Add(DateToken)
        tokens.Add(DateTimeToken)
        tokens.Add(FileNameToken)
        Return tokens.ToArray(False)
    End Function
End Class
