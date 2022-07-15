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
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Data
Imports iText.Kernel.Pdf.Canvas.Parser.Listener

Friend Class PdfImageDetector
    Implements IEventListener
    Private _Detected As Boolean = False

    ''' <summary>
    ''' Were images detected in PDF page source?
    ''' </summary>
    ''' <returns>True or False</returns>
    Friend ReadOnly Property Detected As Boolean
        Get
            Return _Detected
        End Get
    End Property

    Public Sub EventOccurred(data As IEventData, type As EventType) Implements IEventListener.EventOccurred
        If type = EventType.RENDER_IMAGE Then
            _Detected = True
        End If
    End Sub

    Public Function GetSupportedEvents() As ICollection(Of EventType) Implements IEventListener.GetSupportedEvents
        Return Nothing
    End Function
End Class
