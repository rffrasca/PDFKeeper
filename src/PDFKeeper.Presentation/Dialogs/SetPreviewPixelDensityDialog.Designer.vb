'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Partial Class SetPreviewPixelDensityDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetPreviewPixelDensityDialog))
        Me.PixelsPerInchLabel = New System.Windows.Forms.Label()
        Me.PixelsPerInchNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.OK_Button = New System.Windows.Forms.Button()
        CType(Me.PixelsPerInchNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PixelsPerInchLabel
        '
        resources.ApplyResources(Me.PixelsPerInchLabel, "PixelsPerInchLabel")
        Me.PixelsPerInchLabel.Name = "PixelsPerInchLabel"
        '
        'PixelsPerInchNumericUpDown
        '
        Me.PixelsPerInchNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Global.PDFKeeper.Presentation.My.MySettings.Default, "PreviewPixelDensity", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.PixelsPerInchNumericUpDown.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        resources.ApplyResources(Me.PixelsPerInchNumericUpDown, "PixelsPerInchNumericUpDown")
        Me.PixelsPerInchNumericUpDown.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.PixelsPerInchNumericUpDown.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.PixelsPerInchNumericUpDown.Name = "PixelsPerInchNumericUpDown"
        Me.PixelsPerInchNumericUpDown.ReadOnly = True
        Me.PixelsPerInchNumericUpDown.Value = Global.PDFKeeper.Presentation.My.MySettings.Default.PreviewPixelDensity
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'SetPreviewPixelDensityDialog
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.PixelsPerInchNumericUpDown)
        Me.Controls.Add(Me.PixelsPerInchLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetPreviewPixelDensityDialog"
        Me.ShowInTaskbar = False
        CType(Me.PixelsPerInchNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PixelsPerInchLabel As System.Windows.Forms.Label
    Friend WithEvents PixelsPerInchNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents OK_Button As System.Windows.Forms.Button

End Class
