'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public Interface IQuestionDisplay
    ''' <summary>
    ''' Shows a question message box.
    ''' </summary>
    ''' <param name="question">Question to display.</param>
    ''' <param name="cancelVisible">
    ''' Cancel button visible (True or False)
    ''' </param>
    ''' <returns>DialogResult = Yes, No, or Cancel</returns>
    ''' <remarks></remarks>
    Function Show(ByVal question As String, _
                  ByVal cancelVisible As Boolean) As DialogResult

    ''' <summary>
    ''' Shows a generic form cancelling question in a MessageBox with Yes and
    ''' No buttons.
    ''' </summary>
    ''' <returns>
    ''' True when user selects Yes or False when user selects No.
    ''' </returns>
    ''' <remarks></remarks>
    Function ShowFormClosingPrompt() As Boolean
End Interface
