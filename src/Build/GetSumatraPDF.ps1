#******************************************************************************
#* PDFKeeper -- Open Source PDF Document Management System
#* Copyright (C) 2009-2022  Robert F. Frasca
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

$folder = "..\..\..\..\vendor"
$version = "3.4.6"
$zipFileUrl = "https://www.sumatrapdfreader.org/dl/rel/$version/SumatraPDF-$version-64.zip"
$zipFile = "$folder\SumatraPDF-$version-64.zip"
$oldExeFile = "$folder\sumatraPDF.exe"
$exeFile = "$folder\sumatraPDF-$version-64.exe"

if (Test-Path $exeFile) {
    $file = Get-Item $exeFile
    if ($file.VersionInfo.ProductVersion -eq $version) {
        Write-Output "SumatraPDF $version was found in $folder"
        exit 0
    }
}

if (Test-Path $oldExeFile) {
    Remove-Item -Path $oldExeFile
}

Invoke-WebRequest -Uri $zipFileUrl -OutFile $zipFile
Expand-Archive -path $zipFile -DestinationPath $folder -Force
Remove-Item -Path $zipFile

exit 0
