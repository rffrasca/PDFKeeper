'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Public Partial Class RotatePagesForm
	
	''' <summary>
	''' Class constructor.
	''' </summary>
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub RotatePagesFormLoad(sender As Object, e As EventArgs)
		comboBoxRotation.SelectedIndex = 0
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		MsgBox(maskedTextBoxFromPage.Text)
	End Sub
End Class
