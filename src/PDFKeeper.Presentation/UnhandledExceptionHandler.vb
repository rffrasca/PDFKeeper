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
Imports PDFKeeper.Common

Public Class UnhandledExceptionHandler
    Private ReadOnly eventArgs As ApplicationServices.UnhandledExceptionEventArgs
    Private ReadOnly commonDialogs As New CommonDialogs

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="eventArgs">Unhandled exception event data</param>
    Public Sub New(ByVal eventArgs As ApplicationServices.UnhandledExceptionEventArgs)
        Me.eventArgs = eventArgs
    End Sub

    ''' <summary>
    ''' Logs the Unhandled Exception with stack trace to %APPDATA%\Robert F. Frasca\PDFKeeper\[version]\PDFKeeper.log.
    ''' </summary>
    Public Sub Log()
        My.Application.Log.WriteException(eventArgs.Exception, TraceEventType.Error,
                                          String.Concat(vbCr, DateTime.Now, vbCr, eventArgs.Exception.ToString))
    End Sub

    ''' <summary>
    ''' Shows a basic unhandled exception error message.
    ''' </summary>
    Public Sub Show()
        commonDialogs.ShowMessageBox(String.Concat(My.Resources.UnhandledException, vbCr, vbCr,
                                                   eventArgs.Exception.GetType.Name, vbCr,
                                                   eventArgs.Exception.Message, vbCr, vbCr,
                                                   String.Format(
                                                   CultureInfo.CurrentCulture,
                                                   My.Resources.ResourceManager.GetString(
                                                   "UnhandledExceptionLog", CultureInfo.CurrentCulture),
                                                   My.Application.Log.DefaultFileLogWriter.FullLogFileName)), True)
    End Sub
End Class
