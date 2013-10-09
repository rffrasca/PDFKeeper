'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Class MessageDialog
	Dim message As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: message"></param>
	Public Sub New(ByVal arg As String)
		message = arg		
	End Sub
	
	''' <summary>
	''' This subroutine will display an Error dialog.
	''' </summary>
	Public Sub DisplayError
		MessageBox.Show(message, MessageDialog_Strings.ErrorTitle, _
								 MessageBoxButtons.OK, _
								 MessageBoxIcon.Error, _
								 MessageBoxDefaultButton.Button1, 0, False)
	End Sub

	''' <summary>
	''' This subroutine will display an Information dialog.
	''' </summary>
	Public Sub DisplayInformation
		MessageBox.Show(message, MessageDialog_Strings.InformationTitle, _
								 MessageBoxButtons.OK, _
							  	 MessageBoxIcon.Information, _
								 MessageBoxDefaultButton.Button1, 0, False)
	End Sub

	''' <summary>
	''' This function will display a Question dialog.
	''' </summary>
	''' <returns>6 for Yes, 7 for No</returns>
	Public Function DisplayQuestion as Byte
		Dim button = MessageBox.Show(message, _
					 			MessageDialog_Strings.QuestionTitle, _
						  		MessageBoxButtons.YesNo, _
								MessageBoxIcon.Question, _
					 			MessageBoxDefaultButton.Button1, 0, False)
		Return button
	End Function
End Class
