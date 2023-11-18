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

Imports PDFKeeper.Core.Application

Public NotInheritable Class AboutBox
    Private ReadOnly help As New HelpFile

    Private Sub AboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim appTitle As String
        If My.Application.Info.Title <> "" Then
            appTitle = My.Application.Info.Title
        Else
            appTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Text = String.Format(CultureInfo.CurrentCulture,
                             My.Resources.ResourceManager.GetString(
                             "About", CultureInfo.CurrentCulture), appTitle)
        ' Initialize all of the text displayed on the About Box.
        LabelProductName.Text = My.Application.Info.ProductName
        ' Changed version being displayed from My.Application.Info.Version.ToString (x.x.x.x) to
        ' Application.ProductVersion (x.x.x).
        LabelVersion.Text = String.Format(CultureInfo.CurrentCulture,
                                          My.Resources.ResourceManager.GetString(
                                          "Version", CultureInfo.CurrentCulture),
                                          Application.ProductVersion)
        LabelCopyright.Text = My.Application.Info.Copyright
        LabelCompanyName.Text = My.Application.Info.CompanyName
        TextBoxDescription.Text = My.Resources.DescriptionDetail
        'Position License and Third-Party Components link labels.
        LicenseLinkLabel.Parent = LogoPictureBox
        LicenseLinkLabel.BackColor = Color.Transparent
        LicenseLinkLabel.Location = New Point(0, 257)
        ThirdPartyNoticesLinkLabel.Parent = LogoPictureBox
        ThirdPartyNoticesLinkLabel.BackColor = Color.Transparent
        ThirdPartyNoticesLinkLabel.Location = New Point(0, 273)
    End Sub

    Private Sub LicenseLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LicenseLinkLabel.LinkClicked
        help.Show(HelpFile.LicenseTopicFileName, Me)
    End Sub

    Private Sub ThirdPartyNoticesLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ThirdPartyNoticesLinkLabel.LinkClicked
        help.Show(HelpFile.ThirdPartyNoticesTopicFileName, Me)
    End Sub

    Private Sub WebsiteLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebsiteLinkLabel.LinkClicked
        Process.Start(ApplicationUri.HomePage.AbsoluteUri)
    End Sub

    Private Sub DonateLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles DonateLinkLabel.LinkClicked
        help.Show(HelpFile.DonateTopicFileName, Me)
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Close()
    End Sub
End Class
