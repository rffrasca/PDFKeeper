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
Public Interface IPrintString
    ''' <summary>
    ''' Shows the Print dialog for printing the String object.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Print()

    ''' <summary>
    ''' Shows the Print Preview dialog for previewing and printing the String
    ''' object.
    ''' </summary>
    ''' <param name="printPreviewFormSize"></param>
    ''' <remarks></remarks>
    Sub PrintPreview(ByVal printPreviewFormSize As System.Drawing.Size)
End Interface
