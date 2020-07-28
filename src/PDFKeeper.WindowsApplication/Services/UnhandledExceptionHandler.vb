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
Public Class UnhandledExceptionHandler
    Private ReadOnly m_ExceptionEventArgs As _
        ApplicationServices.UnhandledExceptionEventArgs
    Private ReadOnly message As _
        IMessageDisplayService = New MessageDisplayService

    Public Sub New(ByVal exceptionEventArgs As ApplicationServices.UnhandledExceptionEventArgs)
        m_ExceptionEventArgs = exceptionEventArgs
    End Sub

    ''' <summary>
    ''' Writes the UnhandledException object to:
    ''' [SystemDrive]\Users\[User]\AppData\Roaming\Robert F. Frasca\PDFKeeper\[version]\PDFKeeper.log
    ''' </summary>
    ''' <remarks>Called during an application UnhandledException event.</remarks>
    Public Sub WriteToLog()
        Dim message As String = "(" & DateTime.Now & ") " &
            m_ExceptionEventArgs.Exception.GetType.Name &
            m_ExceptionEventArgs.Exception.StackTrace
        My.Application.Log.WriteException(m_ExceptionEventArgs.Exception,
                                          TraceEventType.Critical,
                                          message)
    End Sub

    ''' <summary>
    ''' Shows the UnhandledException in an error message box.
    ''' </summary>
    ''' <remarks>Called during an application UnhandledException event.</remarks>
    Public Sub Show()
        Dim message As String = My.Resources.UnhandledException & vbCr &
            m_ExceptionEventArgs.Exception.GetType.Name & vbCr &
            m_ExceptionEventArgs.Exception.Message & vbCr &
            m_ExceptionEventArgs.Exception.StackTrace
        Me.message.Show(message, True)
    End Sub
End Class
