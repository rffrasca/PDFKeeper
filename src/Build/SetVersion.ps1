#******************************************************************************
#* PDFKeeper -- Open Source PDF Document Management System
#* Copyright (C) 2009-2023 Robert F. Frasca
#*
#* This file is part of PDFKeeper.
#*
#* PDFKeeper is free software: you can redistribute it and/or modify
#* it under the terms of the GNU General Public License as published by
#* the Free Software Foundation, either version 3 of the License, or
#* (at your option) any later version.
#*
#* PDFKeeper is distributed in the hope that it will be useful,
#* but WITHOUT ANY WARRANTY; without even the implied warranty of
#* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#* GNU General Public License for more details.
#*
#* You should have received a copy of the GNU General Public License
#* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
#******************************************************************************

$previousVersion="8.2.0"
$currentVersion="8.1.2"

(Get-ChildItem -Include GlobalAssemblyInfo.vb,PDFKeeper.Setup.wixproj,Product.wxs,'THIRD-PARTY-NOTICES.*' -Recurse ) |
Foreach-Object {
    Set-Content $_ ((Get-content $_) -replace $previousVersion, $currentVersion)
}

exit 0
