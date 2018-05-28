# Building PDFKeeper from Source
This document contains instructions for setting up the development environment and building PDFKeeper 4.0.x from source.

##  Install Development Applications and Tools
1. Microsoft Visual Studio 2013 - https://www.visualstudio.com/downloads/
2. Microsoft .NET Framework 4.6.1 Developer Pack - https://www.microsoft.com/en-us/download/details.aspx?id=49978
3. WiX Toolset v3.11 (or newer) - http://wixtoolset.org/releases/
4. WiX Toolset Visual Studio 2013 Extension - http://wixtoolset.org/releases/
5. Wax - https://marketplace.visualstudio.com/items?itemName=TomEnglert.Wax
6. Microsoft HTML Help Workshop - http://www.microsoft.com/en-us/download/details.aspx?id=21138
7. ODP.NET, Managed Driver - http://www.oracle.com/technetwork/database/windows/downloads/utilsoft-087491.html
    Download the latest xcopy version of the ODP.NET, Managed Driver, then unblock and unzip the file.
    For installation instructions, refer to readme.htm that is included with the xcopy ZIP file.
    
    Note, when installing on a UAC (User Access Control) enabled Windows system, make sure to perform the installation with a Command Prompt session that was opened 
    using Run as administrator. In addition, when installing, the machine_wide_configuration argument must be true to install into the Global Assembly Cache (GAC).

## Get the Source
Clone the https://github.com/rffrasca/PDFKeeper repository to your development system using Git or download the source code for a release from [here](https://github.com/rffrasca/PDFKeeper/releases).

## Download and Extract Third-Party Components
1. Sumatra PDF 3.1.2 (32-bit Portable Version) - http://www.sumatrapdfreader.org/download-free-pdf-viewer.html
    
    Extract into the "vendor" folder.
2. Xpdf tools (Windows 32/64-bit) - http://www.xpdfreader.com/download.html

    Extract the entire archive into into the "vendor" folder, maintaining the folder structure in the archive.

## Build PDFKeeper
1. Open "PDFKeeper.sln" with Visual Studio.
2. Use "Restore" in "NuGet Package Manager" to download NuGet packages.
3. Set configuration to Release, and then Build the Solution.

    After a successful build, "PDFKeeper-4.0.x.msi" will exist in "PDFKeeper\src\PDFKeeper.Setup\bin\Release".    
