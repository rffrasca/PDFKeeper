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

Public Partial Class AboutForm
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will read the Product name, version, and copyright from
	''' the Assembly Information and update the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub AboutFormLoad(sender As Object, e As EventArgs)
		labelProduct.Text = _
				  My.Application.Info.ProductName & " " & _
				  My.Application.Info.Version.Major.ToString( _
										CultureInfo.CurrentCulture) & "." & _
				  My.Application.Info.Version.Minor.ToString( _
										CultureInfo.CurrentCulture) & "." & _
				  My.Application.Info.Version.Build.ToString( _
										CultureInfo.CurrentCulture)
		labelDescription.Text = My.Application.Info.Description
		labelCopyright.Text = My.Application.Info.Copyright
	End Sub
End Class
