' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetTaxYearView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetTaxYearView))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TaxYearDropDownListUserControl = New PDFKeeper.Presentation.TaxYearDropDownListUserControl()
        Me.StringEnumerableViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.StringEnumerableViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'TaxYearDropDownListUserControl
        '
        resources.ApplyResources(Me.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl")
        Me.TaxYearDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("TaxYears", Me.StringEnumerableViewModelBindingSource, "Items", True))
        Me.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl"
        Me.TaxYearDropDownListUserControl.TaxYear = ""
        Me.TaxYearDropDownListUserControl.TaxYears = Nothing
        '
        'StringEnumerableViewModelBindingSource
        '
        Me.StringEnumerableViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.StringEnumerableViewModel)
        '
        'SetTaxYearView
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.TaxYearDropDownListUserControl)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetTaxYearView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.StringEnumerableViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TaxYearDropDownListUserControl As TaxYearDropDownListUserControl
    Friend WithEvents StringEnumerableViewModelBindingSource As BindingSource
    Friend WithEvents HelpProvider As HelpProvider
End Class
