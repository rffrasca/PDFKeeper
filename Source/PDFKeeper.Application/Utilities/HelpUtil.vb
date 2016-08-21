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

Public NotInheritable Class HelpUtil
	Private Sub New()
		' Because type 'HelpWrapper' contains only 'Shared' members, a default
		' private constructor was added to prevent the compiler from adding a
		' default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Shows the specified topic file in the Help dialog box. Topic files are
	''' named to match the parent form or control.
	''' </summary>
	''' <param name="parent">
	''' Parent form or control of the help dialog box.
	''' Example: ShowHelp(Me, "topic")
	''' </param>
	''' <param name="helpTopic">
	''' Topic file (without extension) in help file to display.
	''' Use ActiveForm.Name to specify the name of the active form.
	''' </param>
	Public Shared Sub ShowHelp( _
		ByVal parent As System.Windows.Forms.Control, _
		helpTopic As String)
		Help.ShowHelp(parent, "PDFKeeper.en.chm", helpTopic & ".html")
	End Sub
End Class
