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
Public Class FileSelectDisplayService
    Implements IFileSelectDisplayService, IDisposable
    Private fileDialog As FileDialog
    Private m_FileName As String
    Private m_Filter As String

    Public Property FileName As String Implements IFileSelectDisplayService.FileName
        Get
            Return m_FileName
        End Get
        Set(value As String)
            m_FileName = value
        End Set
    End Property

    Public Property Filter As String Implements IFileSelectDisplayService.Filter
        Get
            Return m_Filter
        End Get
        Set(value As String)
            m_Filter = value
        End Set
    End Property

    Public Function ShowOpen() As String Implements IFileSelectDisplayService.ShowOpen
        fileDialog = New OpenFileDialog
        Return ShowDialog()
    End Function

    Public Function ShowSave() As String Implements IFileSelectDisplayService.ShowSave
        fileDialog = New SaveFileDialog
        Return ShowDialog()
    End Function

    Private Function ShowDialog() As String
        fileDialog.FileName = m_FileName
        fileDialog.Filter = String.Format(CultureInfo.CurrentCulture,
                                          My.Resources.ResourceManager.GetString("FilterExtension",
                                                                                 CultureInfo.CurrentCulture),
                                          m_Filter.ToUpper(CultureInfo.CurrentCulture), m_Filter)
        If fileDialog.ShowDialog() = DialogResult.Cancel Then
            fileDialog.FileName = Nothing
        End If
        Return fileDialog.FileName
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Try
                    fileDialog.Dispose()
                Catch ex As NullReferenceException
                End Try
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
