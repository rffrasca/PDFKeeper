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
Public MustInherit Class FileBase
    Private m_FullName As String

    Public Property FullName As String
        Get
            Return m_FullName
        End Get
        Protected Set(value As String)
            m_FullName = value
        End Set
    End Property

    Public ReadOnly Property Exists As Boolean
        Get
            Return IO.File.Exists(m_FullName)
        End Get
    End Property

    Public Function ComputeHash() As String
        Using algorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
            Using inputStream As New FileStream(m_FullName, _
                                                FileMode.Open, _
                                                FileAccess.Read)
                Dim hash As Byte() = algorithm.ComputeHash(inputStream)
                Return BitConverter.ToString(hash)
            End Using
        End Using
    End Function

    Public Sub CopyTo(ByVal targetFile As String)
        IO.File.Copy(m_FullName, targetFile, True)
    End Sub
End Class
