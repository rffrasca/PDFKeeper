'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Public NotInheritable Class PdfPasswordTypes
    ''' <summary>
    ''' PDF Password type.
    ''' 
    ''' None: PDF is not password protected.
    ''' Owner: Owner password will be required to read PDF.
    ''' User: Prevents opening the PDF and is not supported.
    ''' Unknown: Unable to determine password type because PDF is invalid.
    ''' </summary>
    Public Enum PdfPasswordType
        None
        Owner
        User
        Unknown
    End Enum
End Class
