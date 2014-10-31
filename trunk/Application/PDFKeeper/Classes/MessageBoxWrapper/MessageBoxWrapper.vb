'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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

Public NotInheritable Class MessageBoxWrapper
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will display an Error message box.
	''' </summary>
	''' <param name="message"></param>
	Public Shared Sub ShowError(ByVal message As String)
		MessageBox.Show(message, MessageBoxWrapper_Strings.ErrorTitle, _
								 MessageBoxButtons.OK, _
								 MessageBoxIcon.Error, _
								 MessageBoxDefaultButton.Button1, 0, False)
	End Sub
		
	''' <summary>
	''' This subroutine will display an Information message box.
	''' </summary>
	''' <param name="message"></param>
	Public Shared Sub ShowInformation(ByVal message As String)
		MessageBox.Show(message, MessageBoxWrapper_Strings.InformationTitle, _
								 MessageBoxButtons.OK, _
							  	 MessageBoxIcon.Information, _
								 MessageBoxDefaultButton.Button1, 0, False)
	End Sub
	
	''' <summary>
	''' This subroutine will display a Question message box.
	''' </summary>
	''' <param name="message"></param>
	''' <returns>6 for Yes, 7 for No</returns>
	Public Shared Function ShowQuestion(ByVal message As String) As Integer
		Dim button = MessageBox.Show(message, _
					 			MessageBoxWrapper_Strings.QuestionTitle, _
						  		MessageBoxButtons.YesNo, _
								MessageBoxIcon.Question, _
					 			MessageBoxDefaultButton.Button1, 0, False)
		Return button
	End Function
End Class
