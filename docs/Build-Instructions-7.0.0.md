# Building PDFKeeper 7.0.0 from Source

##  Install Development Applications and Tools
1. Microsoft Visual Studio 2019 - https://www.visualstudio.com/downloads/
2. WiX Toolset Build Tools - Download and install v3.11.2 using "Manage Extensions" in Visual Studio.
3. Wix Toolset Visual Studio 2019 Extension - Download and install using "Manage Extensions" in Visual Studio.
4. Wax - Download and install using "Manage Extensions" in Visual Studio.
5. Microsoft HTML Help Workshop - http://www.microsoft.com/en-us/download/details.aspx?id=21138
6. Oracle Data Provider for .NET - https://www.oracle.com/database/technologies/dotnet-odacdeploy-downloads.html
    Download 64-bit ODAC 19.3 from the ODAC Xcopy section to a folder for staging, then unblock and unzip the file.
    For installation instructions, refer to readme.htm that is included in the zip file.
    
    Note, make sure to perform the installation using a Command Prompt session that was opened using "Run as administrator". In addition, it is not necessary to install the full ODAC product, only ODP.NET4 is required.

## Get the Source
Clone the https://github.com/rffrasca/PDFKeeper repository to your development system using Git.

## Download and Extract Third-Party Components
1. Sumatra PDF 3.1.2 (64-bit Portable Version) - http://www.sumatrapdfreader.org/download-free-pdf-viewer.html
    
    Extract into the "vendor" folder in the PDFKeeper Solution.
2. Xpdf command line tools (Windows 32/64-bit) - http://www.xpdfreader.com/download.html

    Extract the entire archive into the "vendor" folder in the PDFKeeper Solution, maintaining the folder structure. Next, rename the "xpdf-tools-win-x.xx.xx" folder in the "vendor" folder to "xpdf-tools-win".

## Build PDFKeeper
1. Open "PDFKeeper.sln" with Visual Studio.
2. Set configuration to Release, and then Build the Solution.

    After a successful build, "PDFKeeper-7.0.0.msi" will exist in "PDFKeeper\src\PDFKeeper.Setup\bin\Release".
