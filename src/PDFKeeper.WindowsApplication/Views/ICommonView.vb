'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Interface ICommonView
    ' Implementation:
    '
    ' Column Datatable:
    '
    '   Public Property <Column_plural> As DataTable Implements ICommonView.<Column_plural>
    '       Get
    '           Return <Column>ComboBox.DataSource
    '       End Get
    '       Set(value As DataTable)
    '           <Column>ComboBox.DataSource = value
    '           <Column>ComboBox.DisplayMember = "doc_<column>"
    '       End Set
    '   End Property
    '
    ' Column Property:
    '
    '   Public Property <Column> As String Implements ICommonView.<Column>
    '       Get
    '           Return <Column>ComboBox.Text
    '       End Get
    '       Set(value As String)
    '           <Column>ComboBox.Text = value
    '       End Set
    '   End Property
    '
    ' Throw New NotImplementedException for any method or property setter not
    ' implemented and return Nothing from any property getter not implemented.
    '
    ' ActiveControlName: Return Me.ActiveControl.Name
    '                    Return <Container>.ActiveControl.Name
    '
    ' OnLongRunningOperationStarted: Me.Cursor = Cursors.WaitCursor
    ' OnLongRunningOperationCompleted: Me.Cursor = Cursors.Default
    Property Authors As DataTable
    Property Author As String
    Property Subjects As DataTable
    Property Subject As String
    Property AuthorsPaired As DataTable
    Property AuthorPaired As String
    Property SubjectsPaired As DataTable
    Property SubjectPaired As String
    Property Categories As DataTable
    Property Category As String
    ReadOnly Property ActiveElement As String
    Sub SetCursor(ByVal wait As Boolean)
End Interface
