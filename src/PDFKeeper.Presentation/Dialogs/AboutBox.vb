'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
Imports PDFKeeper.Common

Public NotInheritable Class AboutBox
    Private ReadOnly help As New HelpProvider

    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim appTitle As String
        If My.Application.Info.Title <> "" Then
            appTitle = My.Application.Info.Title
        Else
            appTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format(CultureInfo.CurrentCulture,
                                My.Resources.ResourceManager.GetString("About", CultureInfo.CurrentCulture), appTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        ' Changed version being displayed from My.Application.Info.Version.ToString (x.x.x.x) to
        ' Application.ProductVersion (x.x.x).
        Me.LabelVersion.Text = String.Format(CultureInfo.CurrentCulture,
                                             My.Resources.ResourceManager.GetString("Version", CultureInfo.CurrentCulture),
                                             Application.ProductVersion)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Resources.DescriptionDetail
        'Position License and Third-Party Components link labels.
        LicenseLinkLabel.Parent = LogoPictureBox
        LicenseLinkLabel.BackColor = Color.Transparent
        LicenseLinkLabel.Location = New Point(0, 257)
        ThirdPartyNoticesLinkLabel.Parent = LogoPictureBox
        ThirdPartyNoticesLinkLabel.BackColor = Color.Transparent
        ThirdPartyNoticesLinkLabel.Location = New Point(0, 273)
    End Sub

    Private Sub LicenseLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LicenseLinkLabel.LinkClicked
        help.Show(HelpProvider.LicenseTopicFileName, Me)
    End Sub

    Private Sub ThirdPartyNoticesLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ThirdPartyNoticesLinkLabel.LinkClicked
        help.Show(HelpProvider.ThirdPartyNoticesTopicFileName, Me)
    End Sub

    Private Sub WebsiteLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebsiteLinkLabel.LinkClicked
        Process.Start(AppProperties.HomePageUrl.AbsoluteUri)
    End Sub

    Private Sub DonateLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles DonateLinkLabel.LinkClicked
        help.Show(HelpProvider.DonateTopicFileName, Me)
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class
