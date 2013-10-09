'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2010 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Friend Class PdfProcess
	
	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will open pdfFile for viewing with SumatraPDF.  To
	''' open SumatraPDF in restricted mode, set restrict to True.  To open
	''' SumatraPDF in normal mode, set restrict to False.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <param name="restrict"></param>
	''' <returns>window handle</returns>
	Public Shared Sub Start(ByVal pdfFile As String, _
						    ByVal restrict As Boolean)
		Dim processArgs As String
		If restrict Then
			processArgs = "-restrict " & chr(34) & pdfFile & chr(34)
		Else
			processArgs = chr(34) & pdfFile & chr(34)
		End If
		WinProcess.Start("SumatraPDF.exe", processArgs, _
						  Path.GetFileName(pdfFile))
	End Sub
End Class
