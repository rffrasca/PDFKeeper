PDFKeeper 3.1.0
Copyright (C) 2009-2015 Robert F. Frasca

-------------
 BUILD NOTES
-------------

NOTE: Testing PDFKeeper requires connecting to a database server. Install and
configure Oracle Database Express Edition on any system. Next, configure the
PDFKeeper database schema following the "Database Setup and User Account
Management Guide" located in the project Documentation folder. The Database
Setup script, DatabaseSetup.cmd is located in the project Database folder.

1. Install the following Development Tools:

    a. SharpDevelop (Visual Studio may be used; however, not tested.)
       (http://www.icsharpcode.net/OpenSource/SD/Download/)
    
    b. Wix Toolset (http://wixtoolset.org/)
    
    c. Microsoft HTML Help Workshop
       (http://www.microsoft.com/en-us/download/details.aspx?id=21138)

2. Install the following Library:

    a. ODP.NET, Managed Driver
       (http://www.oracle.com/technetwork/database/windows/downloads/utilsoft-087491.html)

       For installation instructions, refer to the Readme that is included with
       the Xcopy ZIP file. Note, when installing on a UAC (User Access Control)
       enabled Windows system, make sure to perform the installation with a
       Command Prompt session that was opened using Run as administrator.

3. Download Third-Party Programs and Libraries: 

    a. iTextSharp and Trove Unofficial Nini Configuration

       Use Restore Packages feature in SharpDevelop to download NuGet packages.

    b. Sumatra PDF (Portable Version)
       (http://www.sumatrapdfreader.org/free-pdf-reader.html)

       After downloading, extract SumatraPDF.exe into the project Libraries
       folder and unblock.

    c. Ghostscript
       (http://www.ghostscript.com/)

       After downloading, extract gsdll32.dll and gswin32c.exe in the project
       Libraries folder.

       NOTE: To extract from the GhostScript installer, you can use
       7-Zip Portable (http://portableapps.com/apps/utilities/7-zip_portable)

4. Open Help\en\PDFKeeper.hhp with HTML Help Workshop and compile.

5. Open PDFKeeper.sln with SharpDevelop (or Visual Studio) and Build. 
