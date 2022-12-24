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
Imports PDFKeeper.Domain

Public Interface IUploadProfileService
    ''' <summary>
    ''' Lists profile names in the repository.
    ''' </summary>
    ''' <returns>Collection object</returns>
    Function ListProfileNames() As Object

    ''' <summary>
    ''' Does profile name exist in the repository.
    ''' </summary>
    ''' <param name="name">Profile name</param>
    ''' <returns>True or False</returns>
    Function ProfileExists(ByVal name As String) As Boolean

    ''' <summary>
    ''' Creates an upload profile in the repository.
    ''' </summary>
    ''' <param name="name">Profile name</param>
    ''' <param name="model">UploadProfileModel object</param>
    Sub CreateProfile(ByVal name As String, ByVal model As UploadProfileModel)

    ''' <summary>
    ''' Reads an upload profile from the repository.
    ''' </summary>
    ''' <param name="name">Profile name</param>
    ''' <returns>UploadProfileModel object</returns>
    Function ReadProfile(ByVal name As String) As UploadProfileModel

    ''' <summary>
    ''' Updates an upload profile in the repository.
    ''' </summary>
    ''' <param name="name">Profile name</param>
    ''' <param name="previousName">Previous profile name</param>
    ''' <param name="model">UploadProfileModel object</param>
    Sub UpdateProfile(ByVal name As String, ByVal previousName As String, ByVal model As UploadProfileModel)

    ''' <summary>
    ''' Deletes an upload profile from the repository.
    ''' </summary>
    ''' <param name="name">Profile name</param>
    Sub DeleteProfile(ByVal name As String)

    ''' <summary>
    ''' Creates missing upload profile folders.
    ''' </summary>
    Sub CreateMissingUploadFolders()

    ''' <summary>
    ''' Deletes dormant upload folders that are either empty without a profile or any empty sub-folders under each
    ''' upload folder with a profile.
    ''' </summary>
    Sub DeleteDormantUploadFolders()
End Interface
