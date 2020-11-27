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
Public Class MainWindowStatePresenter
    Private ReadOnly view As IMainWindowStateView

    Public Sub New(view As IMainWindowStateView)
        Me.view = view
    End Sub

    Public Sub GetState()
        If Not IsNothing(My.Settings.MainLocation) Then
            ' Workaround added for an occasional bug that can cause the Main Form to
            ' be positioned off the screen.
            If My.Settings.MainLocation = New System.Drawing.Point(-32000, -32000) Then
                view.ViewLocation = New System.Drawing.Point(0, 0)
            Else
                view.ViewLocation = My.Settings.MainLocation
            End If
        End If
        If Not IsNothing(My.Settings.MainSize) Then
            view.ViewSize = My.Settings.MainSize
        End If
        view.ViewWindowState = My.Settings.MainWindowState
        view.ViewSplitterDistance = My.Settings.MainSplitterDistance
    End Sub

    Public Sub SetState()
        My.Settings.MainSplitterDistance = view.ViewSplitterDistance
        My.Settings.MainLocation = view.ViewLocation
        If view.ViewWindowState = FormWindowState.Normal Then
            My.Settings.MainSize = view.ViewSize
        Else
            My.Settings.MainSize = view.ViewRestoreBoundsSize
        End If
        My.Settings.MainWindowState = view.ViewWindowState
    End Sub
End Class
