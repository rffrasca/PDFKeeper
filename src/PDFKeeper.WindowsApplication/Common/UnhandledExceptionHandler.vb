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
Public Class UnhandledExceptionHandler
    Private m_ExceptionEventArgs As ApplicationServices.UnhandledExceptionEventArgs
    Private messageDisplay As IMessageDisplay = New MessageDisplay

    Public Sub New(ByVal exceptionEventArgs As ApplicationServices.UnhandledExceptionEventArgs)
        m_ExceptionEventArgs = exceptionEventArgs
    End Sub

    ''' <summary>
    ''' Writes the UnhandledException object to:
    ''' [SystemDrive]\Users\[User]\AppData\Roaming\Robert F. Frasca\PDFKeeper\[version]\PDFKeeper.log
    ''' </summary>
    ''' <remarks>Called during an application UnhandledException event.</remarks>
    Public Sub WriteToLog()
        Dim message As String = "(" & DateTime.Now & ") " & _
            m_ExceptionEventArgs.Exception.GetType.Name & _
            m_ExceptionEventArgs.Exception.StackTrace
        My.Application.Log.WriteException(m_ExceptionEventArgs.Exception, _
                                          TraceEventType.Critical, _
                                          message)
    End Sub

    ''' <summary>
    ''' Shows the UnhandledException in an error message box.
    ''' </summary>
    ''' <remarks>Called during an application UnhandledException event.</remarks>
    Public Sub Show()
        Dim message As String = My.Resources.UnhandledException & vbCr & _
            m_ExceptionEventArgs.Exception.GetType.Name & vbCr & _
            m_ExceptionEventArgs.Exception.Message & vbCr & _
            m_ExceptionEventArgs.Exception.StackTrace
        messageDisplay.Show(message, True)
    End Sub
End Class
