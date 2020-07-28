'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
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
Public Interface IHelpDisplayService
    ''' <summary>
    ''' Returns the name of the help file based on the current culture.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' If a help file does not exist for the current culture, then the help
    ''' file for en-US will be used.
    ''' </remarks>
    ReadOnly Property Name As String

    ''' <summary>
    ''' Shows the specified topic page in the help file and waits until closed.
    ''' </summary>
    ''' <param name="helpTopic">
    ''' Topic file with extension contained in help file to display.
    ''' </param>
    ''' <remarks></remarks>
    Sub ShowAndWait(ByVal helpTopic As String)

    ''' <summary>
    ''' Shows the specified topic in the help file using the Help dialog box.
    ''' </summary>
    ''' <param name="parentControl">
    ''' Parent form or control of the help dialog box. The Me object can be
    ''' specified as the parent control.
    ''' </param>
    ''' <param name="helpTopic">
    ''' Topic file with extension contained in help file to display.
    ''' </param>
    ''' <remarks></remarks>
    Sub Show(ByVal parentControl As System.Windows.Forms.Control,
             ByVal helpTopic As String)
End Interface
