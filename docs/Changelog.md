# PDFKeeper Changelog
All notable changes to PDFKeeper will be documented in this file.

## 0.0.1 - 2014-05-31
### Added

### Changed

### Fixed

### Removed



## [3.1.2] - 2015-07-11
- This is a bug fix release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.1).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Changed
- Third-Party software update in binary release: Ghostscript 9.16.
### Fixed
- Document Search: listview not scrolling when selected item is off the screen.
- Document Search: selected listview item becomes deselected after refresh.

## [3.1.1] - 2015-03-21
- This is a bug fix release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.1).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Fixed
- Losing focus from Document Search list view when arrowing up or down.
- Document Capture: Unhandled Exception.

## [3.1.0] - 2015-02-07
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Added
- Document Preview to Main Form.
- Document Text View to Main Form.
### Changed
- Third-Party software update in binary release: iTextSharp 5.5.2.
- Third-Party software update in binary release: Sumatra PDF 3.0.
### Removed
- XPS and HTML converters.

## [3.0.1] - 2014-11-22
- This is a bug fix release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.1).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Known Issues
- HTML Converter menu item does not enable on 64-bit Windows even though the correct version of wkhtmltopdf is installed.
### Fixed
- Unhandled Exception when renaming a PDF that is selected in Document Capture.
- Simplify HTML Converter setup by requiring wkhtmltopdf 0.12.1 or higher.
- Document Search: number of checked list view items should be displayed on status bar.
- Document Capture: if selected document does not contain a Title in Information Properties, the filename should be displayed in Title text box.
- Direct Upload Configuration: unable to scroll horizontally when folder name exceeds width of list box.
- Document Search: Search Text history not sorted.
- Unhandled Exception when saving PDF document to disk in Document Search.
- Unhandled Exception when opening PDF document for viewing in Document Search.

## [3.0.0] - 2013-12-21
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Added
- Prompt for password when PDF document contains an OWNER password.
- Window-level help system for application forms replacing the User Guide.
- Seamless XPS to PDF document conversion to Document Capture using GhostXPS.
- Seamed HTML to PDF document conversion to Document Capture using wkhtmltopdf.
- Create "Document Capture"; a centralized, document intake process.
- Menu item for reporting new issue.
- Created "Direct Upload" that will replace the "PDF Document Upload folder watcher.
### Changed
- Trap all unhandled exceptions and display to user.
- Modify update check to use new project site for verification.
- Target .NET Framework 4 Client Profile when building application.
- Migrate installation from InnoSetup to Windows Installer.
- Oracle Data Provider for .NET, Managed Driver is now required in place of Oracle Data Access Components.
- Enable database connection pooling to improve performance.
- Third-Party software update in binary release: Sumatra PDF 2.4.
- Third-Party software update in binary release: iTextSharp 5.4.5.
- Redesign About Form.
- Set form font to SystemFonts.MessageBoxFont to improve appearance on Windows Vista and later.
### Fixed
- Update links in source and documentation to new project site.
- Using mouse scroll wheel following search results in scrolling in Search Text combo box instead of list view.
- Database Setup: 'sqlplus' is not recognized as an internal or external command, operable program or batch file.
- Improve Database Connection form password handling.
- Main Form status bar getting cut off when vertical screen resolution value is 800.

## [2.7.0] - 2012-09-29
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.7.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Added
- Main Form: Add a folder watcher that will directly upload PDF documents.
### Changed
- Set InitialDirectory property to last folder selected in Open and Save dialogs.
- Third-Party software update in binary release: Sumatra PDF 2.1.1.
- Third-Party software update in binary release: iTextSharp 5.3.0.
- Prompt to process existing PDF's when folder watchers are enabled.
- Installer: Move uninstall of existing version to just before start of installing files.
- Information Properties Editor and folder watcher; upload folder watcher file saving modifications.
### Fixed
- Main Form: ListView sort order being reset on new search.
- Main Form: Form flicker during a lengthy operation.
- Information Properties Editor Form: Form flicker when selecting Author or Subject combo box.
- Information Properties Editor Folder Watcher: PDF not found as file or resource.

## [2.6.0] - 2012-06-30
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.6.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Added
- Information Properties Editor Form: Add checkbox that when checked will upload PDF document to the database after saving.
- Implement logging for the PDF upload process.
- Main Form and Information Properties Editor Form: Add option to delete PDF document after being uploaded.
- Information Properties Editor Form: Add checkbox that will delete the source file after saving the target file.
- Main Form: Information Properties Editor Folder Watcher: Add event to status bar icon to open watched folder.
- Main Form: Implement a centralized document upload process.
### Changed
- Third-Party software update in binary release: Sumatra PDF 2.0.1.
- Third-Party software update in binary release: iTextSharp 5.2.1.
- Installer to prompt user to close applications in use during installation on Windows Vista and newer.
### Fixed
- PDFKeeper not responsive after opening PDF document.
- PDFKeeper not responsive after opening Help or Windows Explorer on non-English version of Windows.

## [2.5.1] - 2012-04-14
- This is a bug fix release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.1).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Fixed
- Information Properties Editor Folder Watcher detecting duplicate events.
- Information Properties Editor Folder Watcher launching Information Properties Editor while PDF is being written.
- Folder Watcher log file only contains the last error logged.
- Folder Watcher Unhandled Exception: System.ArgumentOutOfRangeException: startIndex can not be less than zero.

## [2.5.0] - 2012-03-24
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Added
- Encrypt PDF document when retrieved from the database during an "open" operation on EFS supported operating systems.
- Main Form: Add a folder watcher that will call the Information Properties Editor.
### Changed
- Store user settings in an XML file instead of the registry.
- Information Properties Editor: remember the last state of the "After saving, open PDF document in viewer" check box.
- Third-Party software update in binary release: iTextSharp 5.2.0.
### Fixed
- Disable some of Sumatra PDF's functionality.
- PdfKeeper.exe is not removed if in use during uninstall.
- "DeleteFile failed; code 5. Access is denied." during upgrade if PDFKeeper is in use.
- Main Form: (Not Responding) after opening PDF document.
- Move PDF document cache to location in user profile that remains local.
- Information Properties Editor not waiting for user to close PDF viewer when "After saving, open PDF document in viewer." is checked.
- Information Properties Editing: Modify to not rename original file and to append "modified_by_pdfkeeper" to the filename of the new copy.

## [2.4.0] - 2011-12-24
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.4.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Changed
- Information Properties Editor: Make form modeless.
- Use Cache folder instead of TEMP folder for opened PDF documents.
- Enable caching of opened PDF documents.
- Update documentation for ODAC 11.2 Release 3 (11.2.0.2.1).
- Update documentation for Oracle Database Express Edition 11g Release 2.
- Third-Party software update in binary release: Sumatra PDF 1.9.
- Third-Party software update in binary release: iTextSharp 5.1.3.
### Fixed
- Main Form: Display Document Upload results on status bar, not in message box.
- About Form: Graphic, text, and OK button not centered.

## [2.3.0] - 2011-09-24
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.3.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Changed
- Main Form: Enhance the Upload process to execute on a separate thread.
- Third-Party software update in binary release: Sumatra PDF 1.7.
- Third-Party software update in binary release: iTextSharp 5.1.2.
### Fixed
- Main Form: Document Notes text is highlighted after update.
- Main Form: Check for update not detecting newer version on project site.

## [2.2.0] - 2011-06-25
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.2.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
### Changed
- Third-Party software update in binary release: Sumatra PDF 1.6.
- Consolidate Document Notes and Document Keywords functionality into Main Form.
- Third-Party software update in binary release: iTextSharp 5.1.1.
### Fixed
- Information Properties Editor: Unhandled Exception: Access to the path is denied.
- Information Properties Editor: On occasion, the Author has to be chosen twice.
- Main Form: Search Text combo box does not resize.
- Database Setup: Only works if either Oracle XE server or client is installed.

## [2.1.0] - 2011-03-25
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.1.0).
- This version is no longer supported.
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

## [2.0.0] - 2010-11-22
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.0.0).
- This version is no longer supported.
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

## [1.1.0] - 2010-04-29
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.1.0).
- This version is no longer supported.
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

## [1.0.0] - 2009-10-24
- First, full release of this product to the public.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.0.0).
- This version is no longer supported.
- Binary release is no longer available for distribution.
