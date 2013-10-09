*******************************************************************************
*
* PDFKeeper 2.7.0
* Copyright (C) 2009-2012 Robert F. Frasca
*
* Build Document Readme
*
*******************************************************************************

1. Install neccessary development applications and tools:

	SharpDevelop 3.2.1
	WWW: http://www.icsharpcode.net/OpenSource/SD/Default.aspx
	Purpose: Application development

	Microsoft FxCop 1.36
	WWW: http://www.microsoft.com/downloads/en/details.aspx?displaylang=en&FamilyID=917023f6-d5b7-41bb-bbc0-411a7d66cf3c
	Purpose: Code validation
	
	Amaya 11.4.4
	WWW: http://www.w3.org/Amaya/
	Purpose: Documentation 

	Inno Setup 5.5.1 (unicode)
	WWW: http://www.jrsoftware.org
	Purpose: Installer
	
	Inno Script Studio 1.0.0.24
	WWW: https://www.kymoto.org/inno-script-studio
	Purpose: Installer

2. Install Oracle Data Access Components (ODAC) 11.2.0.2.1 or
   Oracle Data Access Components (ODAC) 11.2.0.1.2
   See the "Getting Started Guide" for instructions.

3. Install and configure Oracle Database 11g Express Edition (11.2) Server
   or Oracle Database 10g Express Edition (10.2.0.1) Server (Universal) on
   any system.  See the "Getting Started Guide" for instructions.

   Note: Step 3 is only required for testing PDFKeeper.

4. Download SumatraPDF-2.1.1.zip from
   http://blog.kowalczyk.info/software/sumatrapdf/download-free-pdf-viewer.html
   and extract into "3rdParty\SumatraPDF"; unblock SumatraPDF.exe.

5. Download itextsharp-all-5.3.0.zip from
   http://sourceforge.net/projects/itextsharp/files/itextsharp/iTextSharp-5.3.0/
   and extract into "3rdParty\iTextSharp".  Extract
   itextsharp-dll-core-5.3.0.zip into "3rdParty\iTextSharp" and unblock
   itextsharp.dll.

6. Download Nini-1.1.0.zip from http://nini.sourceforge.net/download.php and
   extract into "3rdParty"; unblock
   "3rdParty\Nini\Bin\DotNet\2.0\Release\Nini.dll".

7. Download ckill_1.0.zip "Program" from
   http://colinfinck.de/en/progs/ckill/ and extract into "3rdParty\CKill";
   unblock ckill.exe and gpl.txt.

8. Open PDFKeeper.sln with SharpDevelop to build the solution.

9. In the Installer folder, open PDFKeeper.iss with Inno Script Studio to
   compile the installer.

[END OF DOCUMENT]
