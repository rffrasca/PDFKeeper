'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public NotInheritable Class AboutBox
    Private ReadOnly help As IHelpDisplayService = New HelpDisplayService

    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format(CultureInfo.CurrentCulture,
                                My.Resources.ResourceManager.GetString("About",
                                                                       CultureInfo.CurrentCulture),
                                ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        ' Changed version being displayed from
        ' My.Application.Info.Version.ToString (x.x.x.x) to
        ' Application.ProductVersion (x.x.x).
        Me.LabelVersion.Text = String.Format(CultureInfo.CurrentCulture,
                                             My.Resources.ResourceManager.GetString("Version",
                                                                                    CultureInfo.CurrentCulture),
                                             Application.ProductVersion)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Resources.DescriptionDetail
        'Position License and Third-Party Components link labels.
        LicenseLinkLabel.Parent = LogoPictureBox
        LicenseLinkLabel.BackColor = Color.Transparent
        LicenseLinkLabel.Location = New Point(0, 257)
        ThirdPartyLinkLabel.Parent = LogoPictureBox
        ThirdPartyLinkLabel.BackColor = Color.Transparent
        ThirdPartyLinkLabel.Location = New Point(0, 273)
    End Sub

    Private Sub LicenseLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LicenseLinkLabel.LinkClicked
        help.Show(Me, "GNU General Public License v3_0 - GNU Project - Free Software Foundation (FSF).html")
    End Sub

    Private Sub ThirdPartyLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ThirdPartyLinkLabel.LinkClicked
        help.Show(Me, "Third-Party Attribution.html")
    End Sub

    Private Sub WebsiteLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebsiteLinkLabel.LinkClicked
        Process.Start("https://www.pdfkeeper.org/")
    End Sub

    Private Sub DonateLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles DonateLinkLabel.LinkClicked
        help.Show(Me, "Donate.html")
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class
