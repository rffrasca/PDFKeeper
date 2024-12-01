# Building PDFKeeper v11 from Source

##  Install Development Applications and Tools
1. Microsoft Visual Studio 2019 - https://www.visualstudio.com/downloads/
2. WiX Toolset Build Tools - Download and install v3.14.1 using "Manage Extensions" in Visual Studio.
3. WiX v3 - Visual Studio 2019 Extension - Download and install using "Manage Extensions" in Visual Studio.
4. Wax - Download and install using "Manage Extensions" in Visual Studio.
5. Microsoft HTML Help Workshop - https://web.archive.org/web/20201201163924/http://www.microsoft.com/en-us/download/details.aspx?id=21138

## Get the Source
Download the source code for a v11 release (when available) from [here](https://github.com/rffrasca/PDFKeeper/releases). For the latest version in development, clone the https://github.com/rffrasca/PDFKeeper repository to your development system using Git.

## Download and Extract Third-Party Components
- Ghostscript for Windows (64 bit) - https://www.ghostscript.com/download/gsdnld.html
    
    After downloading, extract gswin64c.exe and gsdll64.dll from the Ghostscript installer into the vendor folder in the PDFKeeper solution using the following instructions:
    
    1. Download and install 7-Zip Portable - https://portableapps.com/apps/utilities/7-zip_portable
    2. Open 7-Zip Portable.
    3. Select gsxxxxw64.exe in folder where downloaded.
    4. Select the File menu, and then Open Inside.
    5. Open the bin folder.
    6. Select gswin64c.exe and gsdll64.dll.
    7. Select Extract.
    8. Select the vendor folder in the PDFKeeper solution.
    9. Select OK.

- Oracle ODP.NET 23.5.1 - https://www.nuget.org/packages/Oracle.ManagedDataAccess/23.5.1
    1. Open in NuGet Package Explorer.
    2. Under Contents expand lib, and then expand net472.
    3. Double click Oracle.ManagedDataAccess.dll.
    4. Download Oracle.ManagedDataAccess.dll to the vendor folder in the PDFKeeper solution and Unblock.

## Build PDFKeeper
1. Open src\Help\en-US\PDFKeeper.hhp with HTML Help Workshop and compile.
2. If src\Help\PDFKeeper.en-US.chm resides on a network/remote drive, copy to a local drive.
3. Open PDFKeeper.en-US.chm. If the help file fails to open, as an Administrator, execute *regsvr32 C:\Windows\System32\hhctrl.ocx* and then repeat the compile and test again.
4. Open "PDFKeeper.sln" with Visual Studio.
5. Set configuration to Release, and then Build the Solution.

    After a successful build, "PDFKeeper-11.0.0.msi" will exist in "PDFKeeper\src\PDFKeeper.Setup\bin\Release".
