# *****************************************************************************
# * PDFKeeper -- Open Source PDF Document Management
# * Copyright (C) 2009-2024 Robert F. Frasca
# *
# * This file is part of PDFKeeper.
# *
# * PDFKeeper is free software: you can redistribute it and/or modify it
# * under the terms of the GNU General Public License as published by the
# * Free Software Foundation, either version 3 of the License, or (at your
# * option) any later version.
# *
# * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
# * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
# * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
# * more details.
# *
# * You should have received a copy of the GNU General Public License along
# * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
# *****************************************************************************

$folder = "..\..\..\..\vendor"
$version = "3.42.0"
$archiveName = "sqlite-tools-win32-x86-3420000"
$zipFileUrl = "https://www.sqlite.org/2023/$archiveName.zip"
$zipFile = "$folder\$archiveName.zip"
$exeFile = "$folder\sqlite3.exe"

if (Test-Path $exeFile) {
    $file = Get-Item $exeFile
    if ($file.VersionInfo.ProductVersion -eq $version) {
        Write-Output "SQLite Command Line Shell $version was found in $folder"
        exit 0
    }
}

Invoke-WebRequest -Uri $zipFileUrl -OutFile $zipFile
Expand-Archive -path $zipFile -DestinationPath $folder -Force
Remove-Item -Path $zipFile
Move-Item -Path ($folder + "\" + $archiveName + "\sqlite3.exe") -Destination $folder -Force
Remove-Item -Path ($folder + "\" + $archiveName) -Recurse

exit 0
