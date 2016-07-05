'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
'* Copyright (C) 2009-2016 Robert F. Frasca
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
'*
'* Created by SharpDevelop.
'* User: Robert
'* Date: 7/4/2016
'* Time: 10:08 AM
'*
'******************************************************************************

Public NotInheritable Class HelpUtil
	Private Sub New()
		' Because type 'HelpUtil' contains only 'Shared' members, add a
		' default private constructor to prevent the compiler from adding a
		' default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Shows the specified topic file in the Help dialog box.
	''' </summary>
	''' <param name="parent">Parent control of the help dialog box.</param>
	''' <param name="helpTopic">Topic file in help file to display.</param>
	Public Shared Sub ShowHelp( _
		ByVal parent As System.Windows.Forms.Control, _
		helpTopic As String)
			
		Help.ShowHelp(parent, "PDFKeeper.en.chm", helpTopic & ".html")
	End Sub
End Class
