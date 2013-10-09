'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
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

Friend Class MessageDialog
	
	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will display a message box; supported dialog types are
	''' "Error", "Information", and "Question".  messageText is the text to
	''' display in the message area of the dialog.
	''' </summary>
	''' <param name="dialogType"></param>
	''' <param name="messageText"></param>
	''' <returns>1 for OK, 6 for Yes, 7 for No</returns>
	Public Shared Function Display(ByVal dialogType As String, _
								   ByVal messageText As String) As Byte
		Dim icon As MessageBoxIcon
		Dim buttons As MessageBoxButtons
		Select Case dialogType
			Case = "Error"
				buttons = MessageBoxButtons.OK
				icon = MessageBoxIcon.Error
			Case = "Information"
				buttons = MessageBoxButtons.OK
				icon = MessageBoxIcon.Information
			Case = "Question"
				buttons = MessageBoxButtons.YesNo
				icon = MessageBoxIcon.Question
			Case Else
				buttons = MessageBoxButtons.OK
				icon = MessageBoxIcon.None
		End Select
		Dim button = MessageBox.Show(messageText, dialogType, _
									 buttons, icon, _
					 				 MessageBoxDefaultButton.Button1, _
   									 0, False)
 		Return button
	End Function
End Class
