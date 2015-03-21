'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2015 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public NotInheritable Class HelpWrapper
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub	
	
	''' <summary>
	''' This subroutine will display the help file, selecting the specified
	''' topic file contained in the help file.
	''' </summary>
	''' <param name="parentForm"></param>
	''' <param name="topicFile"></param>
	Public Shared Sub ShowHelp(ByVal parentForm As Windows.Forms.Control, _
							   ByVal topicFile As String)
		Help.ShowHelp(parentForm, "PDFKeeper.en.chm", topicFile)
	End Sub
End Class
