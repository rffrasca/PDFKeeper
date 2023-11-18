' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsDialog))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.FindFlaggedDocumentsOnStartupCheckBox = New System.Windows.Forms.CheckBox()
        Me.ShowPdfWithDefaultApplicationCheckBox = New System.Windows.Forms.CheckBox()
        Me.SelectLastRowWhenListingDocumentsCheckBox = New System.Windows.Forms.CheckBox()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'FindFlaggedDocumentsOnStartupCheckBox
        '
        resources.ApplyResources(Me.FindFlaggedDocumentsOnStartupCheckBox, "FindFlaggedDocumentsOnStartupCheckBox")
        Me.FindFlaggedDocumentsOnStartupCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.FindFlaggedDocumentsOnStartup
        Me.FindFlaggedDocumentsOnStartupCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "FindFlaggedDocumentsOnStartup", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FindFlaggedDocumentsOnStartupCheckBox.Name = "FindFlaggedDocumentsOnStartupCheckBox"
        Me.HelpProvider.SetShowHelp(Me.FindFlaggedDocumentsOnStartupCheckBox, CType(resources.GetObject("FindFlaggedDocumentsOnStartupCheckBox.ShowHelp"), Boolean))
        Me.FindFlaggedDocumentsOnStartupCheckBox.UseVisualStyleBackColor = True
        '
        'ShowPdfWithDefaultApplicationCheckBox
        '
        resources.ApplyResources(Me.ShowPdfWithDefaultApplicationCheckBox, "ShowPdfWithDefaultApplicationCheckBox")
        Me.ShowPdfWithDefaultApplicationCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.ShowPdfWithDefaultApplication
        Me.ShowPdfWithDefaultApplicationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "ShowPdfWithDefaultApplication", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ShowPdfWithDefaultApplicationCheckBox.Name = "ShowPdfWithDefaultApplicationCheckBox"
        Me.ShowPdfWithDefaultApplicationCheckBox.UseVisualStyleBackColor = True
        '
        'SelectLastRowWhenListingDocumentsCheckBox
        '
        resources.ApplyResources(Me.SelectLastRowWhenListingDocumentsCheckBox, "SelectLastRowWhenListingDocumentsCheckBox")
        Me.SelectLastRowWhenListingDocumentsCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.SelectLastDocumentRow
        Me.SelectLastRowWhenListingDocumentsCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "SelectLastDocumentRow", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SelectLastRowWhenListingDocumentsCheckBox.Name = "SelectLastRowWhenListingDocumentsCheckBox"
        Me.SelectLastRowWhenListingDocumentsCheckBox.UseVisualStyleBackColor = True
        '
        'OptionsDialog
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.FindFlaggedDocumentsOnStartupCheckBox)
        Me.Controls.Add(Me.ShowPdfWithDefaultApplicationCheckBox)
        Me.Controls.Add(Me.SelectLastRowWhenListingDocumentsCheckBox)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents SelectLastRowWhenListingDocumentsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowPdfWithDefaultApplicationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FindFlaggedDocumentsOnStartupCheckBox As CheckBox
    Friend WithEvents HelpProvider As HelpProvider
End Class
