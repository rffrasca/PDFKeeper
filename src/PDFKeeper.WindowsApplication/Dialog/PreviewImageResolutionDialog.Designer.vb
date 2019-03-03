'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
'* Copyright (C) 2009-2019 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PreviewImageResolutionDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PreviewImageResolutionDialog))
        Me.PreviewImageResolutionLabel = New System.Windows.Forms.Label()
        Me.PreviewImageResolutionNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.OK_Button = New System.Windows.Forms.Button()
        CType(Me.PreviewImageResolutionNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PreviewImageResolutionLabel
        '
        resources.ApplyResources(Me.PreviewImageResolutionLabel, "PreviewImageResolutionLabel")
        Me.PreviewImageResolutionLabel.Name = "PreviewImageResolutionLabel"
        '
        'PreviewImageResolutionNumericUpDown
        '
        Me.PreviewImageResolutionNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "PreviewImageResolution", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.PreviewImageResolutionNumericUpDown.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        resources.ApplyResources(Me.PreviewImageResolutionNumericUpDown, "PreviewImageResolutionNumericUpDown")
        Me.PreviewImageResolutionNumericUpDown.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.PreviewImageResolutionNumericUpDown.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.PreviewImageResolutionNumericUpDown.Name = "PreviewImageResolutionNumericUpDown"
        Me.PreviewImageResolutionNumericUpDown.ReadOnly = True
        Me.PreviewImageResolutionNumericUpDown.Value = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.PreviewImageResolution
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'PreviewImageResolutionDialog
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.PreviewImageResolutionNumericUpDown)
        Me.Controls.Add(Me.PreviewImageResolutionLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PreviewImageResolutionDialog"
        Me.ShowInTaskbar = False
        CType(Me.PreviewImageResolutionNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PreviewImageResolutionLabel As System.Windows.Forms.Label
    Friend WithEvents PreviewImageResolutionNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents OK_Button As System.Windows.Forms.Button

End Class
