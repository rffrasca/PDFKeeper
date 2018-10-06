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
Public Class FileDialogDisplayService
    Implements IDisposable, IFileDialogDisplayService
    Private fileDialog As FileDialog
    Private m_FileName As String
    Private m_Filter As String

    Public Sub New(ByVal fileNamePrefill As String, _
                   ByVal filterExtension As String)
        m_FileName = fileNamePrefill
        If filterExtension Is Nothing Then
            Throw New ArgumentNullException(filterExtension)
        End If
        m_Filter = String.Format(CultureInfo.CurrentCulture, _
                                 My.Resources.ResourceManager.GetString("FilterExtension"), _
                                 filterExtension.ToUpper(CultureInfo.CurrentCulture), _
                                 filterExtension)
    End Sub

    Public Function OpenDialog() As String Implements IFileDialogDisplayService.OpenDialog
        fileDialog = New OpenFileDialog
        Return Show()
    End Function

    Public Function SaveDialog() As String Implements IFileDialogDisplayService.SaveDialog
        fileDialog = New SaveFileDialog
        Return Show()
    End Function

    Private Function Show() As String
        fileDialog.FileName = m_FileName
        fileDialog.Filter = m_Filter
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
                fileDialog.Dispose()
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
