# Building PDFKeeper 5.x.x from Source

##  Install Development Applications and Tools
1. Microsoft Visual Studio 2013 - https://www.visualstudio.com/downloads/
2. Microsoft .NET Framework 4.6.1 Developer Pack - https://www.microsoft.com/en-us/download/details.aspx?id=49978
3. WiX Toolset v3.11.2 - http://wixtoolset.org/releases/
4. WiX Toolset Visual Studio 2013 Extension 1.0.0.4 - https://github.com/wixtoolset/VisualStudioExtension/releases/tag/v1.0.0.4
5. Wax - https://github.com/tom-englert/Wax/releases. Version must be earlier than 1.3, but a binary release of 1.2 is not available from the author.
6. Microsoft HTML Help Workshop - http://www.microsoft.com/en-us/download/details.aspx?id=21138
7. Oracle Data Provider for .NET - https://www.oracle.com/database/technologies/dotnet-odacdeploy-downloads.html
    Download 32-bit ODAC 19.3 (must be 32-bit) from the ODAC Xcopy section to a folder for staging, then unblock and unzip the file.
    For installation instructions, refer to readme.htm that is included in the zip file.
    
    Note, make sure to perform the installation using a Command Prompt session that was opened using Run as administrator. In addition, it is not necessary to install the full ODAC product, only ODP.NET4 is required.

## Get the Source
Clone the https://github.com/rffrasca/PDFKeeper repository to your development system using Git or download the source code for a 5.x.x release from [here](https://github.com/rffrasca/PDFKeeper/releases).

## Download and Extract Third-Party Components
1. Sumatra PDF 3.1.2 (32-bit Portable Version) - http://www.sumatrapdfreader.org/download-free-pdf-viewer.html
    
    Extract into the "vendor" folder in the PDFKeeper Solution.
2. Xpdf command line tools (Windows 32/64-bit) - http://www.xpdfreader.com/download.html

    Extract the entire archive into the "vendor" folder in the PDFKeeper Solution, maintaining the folder structure. Next, rename the "xpdf-tools-win-x.xx.xx" folder in the "vendor" folder to "xpdf-tools-win".

## Build PDFKeeper
1. Open "PDFKeeper.sln" with Visual Studio.
2. Use "Restore" in "NuGet Package Manager" to download NuGet packages.
3. Set configuration to Release, and then Build the Solution.

    After a successful build, "PDFKeeper-5.x.x.msi" will exist in "PDFKeeper\src\PDFKeeper.Setup\bin\Release".    
