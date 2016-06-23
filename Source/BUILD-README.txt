===============================================================================
===                       BUILD-README for PDFKeeper                        ===
===============================================================================

-------------------------------------------------------------------------------
 Prerequisites
-------------------------------------------------------------------------------

NOTE: Testing PDFKeeper requires connecting to a database server. Install and
configure Oracle Database Express Edition on any system. Next, configure the
PDFKeeper database schema following the "Database Setup and User Account
Management Guide" located in the "Documentation" folder. The Database Setup
Setup script, "DatabaseSetup.cmd" is located in the "Scripts" folder.

1. The following Development Tools must be installed:

    a. SharpDevelop
	http://www.icsharpcode.net/OpenSource/SD/Download/

	Note: Visual Studio may be used; however, not tested.
    
    b. Wix Toolset
	http://wixtoolset.org/
    
    c. Microsoft HTML Help Workshop
	http://www.microsoft.com/en-us/download/details.aspx?id=21138

2. The following Library must be installed:

    a. ODP.NET, Managed Driver
	http://www.oracle.com/technetwork/database/windows/downloads/utilsoft-087491.html

       	For installation instructions, refer to the Readme that is included
	with the Xcopy ZIP file. Note, when installing on a UAC (User Access
	Control) enabled Windows system, make sure to perform the installation
	with a Command Prompt session that was opened using "Run as
	administrator".

-------------------------------------------------------------------------------
 Download External Applications
-------------------------------------------------------------------------------

1. Create an "Externals" folder in the same folder where "PDFKeeper.sln" is
   located.

2. Download Sumatra PDF (Portable Version)
	http://www.sumatrapdfreader.org/free-pdf-reader.html

	After downloading, extract "SumatraPDF.exe" into the "Externals"
	folder, and then unblock.

3. Download Ghostscript
	http://www.ghostscript.com/

	After downloading, extract "gsdll32.dll" and "gswin32c.exe" into the
       	"Externals" folder.

       	NOTE: To extract from the GhostScript installer, you can use
       	7-Zip Portable (http://portableapps.com/apps/utilities/7-zip_portable)

4. iTextSharp and Trove Unofficial Nini Configuration

	Use Restore Packages feature in SharpDevelop to download NuGet
	packages.

-------------------------------------------------------------------------------
 Building PDFKeeper
-------------------------------------------------------------------------------

1. Open "Help\EN\PDFKeeper.hhp" with HTML Help Workshop and compile.

2. Open "PDFKeeper.sln" with SharpDevelop (or Visual Studio), set configuration
   to Release, and then Build.
