# PDFKeeper Changelog
All notable changes to PDFKeeper will be documented in this file.

## 0.0.1 - 2014-05-31
### Added

### Changed

### Fixed

### Removed



## 2.1.0 - 2011-03-25 (Retired)
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.1.0).
- Binary release is no longer available for distribution.
### Added
- Document Notes Form: Add Date/Time stamp feature.
- Main Form: Add "Search Text" history feature.
- Add check for update feature.
- Document Notes Form: Add print feature.
- Information Properties Editor: Force close PDF file on save or cancel, if open with bundled viewer.
### Changed
- Third-Party software update in binary release: iTextSharp 5.0.6.
- Third-Party software update in binary release: Sumatra PDF 1.4.
- Information Properties Editor: Replace prompt to view document after saving with a checkbox on form.
### Fixed
- Main Form: Clicking on a list view column after clearing search text triggers an Oracle Text parser error.
- Database Connection Form: Unhandled exception following second failed logon attempt.
- Main Form: When opening a PDF document already open, a second window is opening.

## 2.0.0 - 2010-11-22 (Retired)
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.0.0).
- Binary release is no longer available for distribution.
### Added
- Add document upload capability to the client.
### Changed
- Redesign and rewrite PDFKeeper in VB.NET for .NET Framework 3.5 SP1; Open Object Rexx 4.0.0 is no longer being used.
- Third-Party software update in binary release: Sumatra PDF 1.1.
- The Database Setup will now build the database schema with the BASIC_LEXER, replacing the WORLD_LEXER.
- pdfinfo.exe from Xpdf and pdftk have been replaced by iTextSharp in the binary release.
### Fixed
- Main Form: The window is not re-sizable.
- Main Form: list view can not sort backward based on ID, Title, Author...
- Information Properties Editor Form: can only edit one pdf file at a time.
- Main Form: Disable Search button until valid Search Text is specified.
- Oracle Express Client is no longer required when connecting remotely as the ODP.NET packaged with Oracle Express Client is only certified by the vendor on .NET Framework 1.1. Oracle Data Access Components (ODAC) 11.2.0.1.2 is now required on all systems that will use the PDFKeeper application, offering compatibity with .NET Framework 2.0.
- User Guide: Correction in section 4: Starting and Logging into PDFKeeper.
- User Guide: Corrections in section 5: Document Searching.
- ORA-03113: end-of-file on communication channel.
### Removed
- Remove server-side upload components.
- Rexx/SQL is no longer used and has been removed from the binary release.

## 1.1.0 - 2010-04-29 (Retired)
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.1.0).
- Binary release is no longer available for distribution.
### Added
- Add functionality to "Check" and "Uncheck" all list view items on Search dialog.
- PDFKeeper Summary Editor: Add check for vertical pipe '|' symbol(s).
- Add more examples on the "about" operator to the User Guide.
- Add upload folder integrity check to document loader.
### Changed
- Third-Party software update in binary release: Sumatra PDF 1.0.1.
- Third-Party software update in binary release: pdfinfo.exe version 3.02pl4.
- Prerequisite software change: Open Object Rexx 4.0.0.
### Fixed
- "Error Cannot Load Resource File sqresus.dll" followed by "REXX/SQL-1: Database Error - specified driver could not be loaded due to system error 1114 (Oracle in XEClient)."
- "The system cannot find the file specified."
- "Object Rexx Interface has encountered a problem and needs to close. We are sorry for the inconvenience."
- "Error 40 Incorrect call to routine occurred on line 97 of C:\Program Files\PDFKeeper\Binaries\pdf.cls External routine RXWINEXEC failed".
- Summary Editor: Author and Subject combo boxes are not always being disabled when hourglass is displayed.
- PDF document author was overwritten by document title after being loaded into the database.
- File is still deleted even if it's rejected by the document loader.
- PDFKeeper Summary Editor fails to save keywords correctly if there's carriage return in keywords.
- PDFKeeper Summary Editor: Keywords pane does not have vertical scroll bar.
- PDFKeeper Search: Document note does not have vertical scoll bar if there's a lot of text.
- Database error if user uses * in their search.
- The columns on the main screen are collapsed when trying to search with no result.
- Database error message is displayed if only keywords are entered in the search text.
- Computer needs to be restarted for the dloader to work after everything is installed.
- pdfloader.log does not include the file name of the pdf file.
- PDFKeeper Search: The column is automatically re-sized even if there's a very long subject.
- Error when selecting multiple files for summary edit.
- Win7 - Unable to select upload folder.

## 1.0.0 - 2009-10-24 (Retired)
- First, full release of this product to the public.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.0.0).
- Binary release is no longer available for distribution.
