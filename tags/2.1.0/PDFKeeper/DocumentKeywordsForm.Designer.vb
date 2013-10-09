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

Partial Class DocumentKeywordsForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DocumentKeywordsForm))
		Me.textBoxKeywords = New System.Windows.Forms.TextBox
		Me.buttonClose = New System.Windows.Forms.Button
		Me.SuspendLayout
		'
		'textBoxKeywords
		'
		Me.textBoxKeywords.Location = New System.Drawing.Point(12, 12)
		Me.textBoxKeywords.MaxLength = 4000
		Me.textBoxKeywords.Multiline = true
		Me.textBoxKeywords.Name = "textBoxKeywords"
		Me.textBoxKeywords.ReadOnly = true
		Me.textBoxKeywords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.textBoxKeywords.Size = New System.Drawing.Size(620, 350)
		Me.textBoxKeywords.TabIndex = 0
		'
		'buttonClose
		'
		Me.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonClose.Location = New System.Drawing.Point(557, 383)
		Me.buttonClose.Name = "buttonClose"
		Me.buttonClose.Size = New System.Drawing.Size(75, 23)
		Me.buttonClose.TabIndex = 1
		Me.buttonClose.Text = "&Close"
		Me.buttonClose.UseVisualStyleBackColor = true
		'
		'DocumentKeywordsForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonClose
		Me.ClientSize = New System.Drawing.Size(644, 418)
		Me.Controls.Add(Me.buttonClose)
		Me.Controls.Add(Me.textBoxKeywords)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DocumentKeywordsForm"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Document Keywords"
		AddHandler Load, AddressOf Me.DocumentKeywordsFormLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private buttonClose As System.Windows.Forms.Button
	Private textBoxKeywords As System.Windows.Forms.TextBox
End Class
