# PDFKeeper Changelog
All notable changes to PDFKeeper will be documented in this file.

## v11.3.0 - 2025-06-16
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.3.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.3.0).
### Added
* Added Custom Combo Box with enhanced AutoComplete that provides Sub-string Search for improved Author, Subject, and Category Searching. [#59](https://github.com/rffrasca/PDFKeeper/issues/59)
* Added drag and drop of PDF associated with a document from Documents DataGridView to other applications. [#64](https://github.com/rffrasca/PDFKeeper/issues/64)
* Added Clear Selections button to Find Documents form that will clear the selected Author, Subject, Category, and Tax Year. [#77](https://github.com/rffrasca/PDFKeeper/issues/77)
### Changed
* Updated Ghostscript to 10.05.1.

## v11.2.2 - 2025-04-26
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.2).
### Changed
* Documents DataGridView will only be refreshed when contents have changed. [#76](https://github.com/rffrasca/PDFKeeper/issues/76)
* Updated Magick.NET to 14.6.0.
### Fixed
* Fixed issue when pressing F1 key while Main Form has focus was not opening correct help topic. [#74](https://github.com/rffrasca/PDFKeeper/issues/74)
* Fixed System.InvalidOperationException: Collection was modified; enumeration operation may not execute. [#75](https://github.com/rffrasca/PDFKeeper/issues/75)

## v11.2.1 - 2025-04-05
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.1).
### Changed
* Updated SQL Server section in Logging into PDFKeeper help topic.
### Fixed
* Fixed issue with ALL PRIVILEGES and * not being recognized when connecting to MySQL. [#72](https://github.com/rffrasca/PDFKeeper/issues/72)

## v11.2.0 - 2025-03-30
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.2.0).
### Added
* Added Flag column to Documents DataGridView. [#58](https://github.com/rffrasca/PDFKeeper/issues/58)
* Added policy to execute the PDF Upload as a blocking operation in a multi-user database environment. [#60](https://github.com/rffrasca/PDFKeeper/issues/60)
### Changed
* In a multi-user database environment, all functionality that the user does not have access to perform will be disabled or blocked. [#55](https://github.com/rffrasca/PDFKeeper/issues/55)
* Updated Ghostscript to 10.05.0.

## v11.1.1 - 2025-02-16
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.1.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.1.1).
### Changed
* Tax Year drop down list in the Set Tax Year, Add PDF, and Upload Profile Editor dialogs will include the last 25 years instead of 10. [#66](https://github.com/rffrasca/PDFKeeper/issues/66)
### Fixed
* Fixed in Export, System.NotSupportedException: The given path's format is not supported. [#67](https://github.com/rffrasca/PDFKeeper/issues/67)

## v11.1.0 - 2025-02-03
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.1.0).
### Added
* Added after delete trigger to existing SQLite database that is using AUTOINCREMENT that sets the docs table sequence to the largest doc_id in use. [#63](https://github.com/rffrasca/PDFKeeper/issues/63)
* Added delete of all files associated with a document ID from the file cache when document record is deleted from the database. [#65](https://github.com/rffrasca/PDFKeeper/issues/65)
### Changed
* When querying a document record, PDF contents will only be queried when the caller specifies that the PDF be included in the results. [#61](https://github.com/rffrasca/PDFKeeper/issues/61)
### Removed
* Removed AUTOINCREMENT keyword from create docs table statement when creating new SQLite database. [#43](https://github.com/rffrasca/PDFKeeper/issues/43)

## v11.0.1 - 2025-01-12
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.0.1).
### Added
* Added support for long path names up to 32,767 characters. [#54](https://github.com/rffrasca/PDFKeeper/issues/54) [#48](https://github.com/rffrasca/PDFKeeper/issues/48) [#50](https://github.com/rffrasca/PDFKeeper/issues/50)
### Fixed
* Fixed System.Runtime.InteropServices.COMException (0x8000001D): Activating a single-threaded class from MTA is not supported (Exception from HRESULT: 0x8000001D) [#49](https://github.com/rffrasca/PDFKeeper/issues/49)
* Fixed System.InvalidOperationException: SplitterDistance must be between Panel1MinSize and Width - Panel2MinSize. [#53](https://github.com/rffrasca/PDFKeeper/issues/53)

## v11.0.0 - 2025-01-02
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v11.0.0).
### Added
* Added SQL Server and SQL Server Express 2019 and higher compatibility. [#37](https://github.com/rffrasca/PDFKeeper/issues/37)
* Added MySQL Community Server 8.4.3 LTS and higher compatibility. [#41](https://github.com/rffrasca/PDFKeeper/issues/41)
* Added image and shortcut keys to "Copy PDF to Clipboard" menu item.
* Added "Copy PDF to Clipboard" tool strip button.
### Fixed
* Corrected text in "User Administration for Oracle Database" help topic.
* Fixed bug in Upload Directory Maintenance that was causing folders to be deleted that did not contain files but contained files in sub-folders. [#46](https://github.com/rffrasca/PDFKeeper/issues/46)

## v10.1.0 - 2024-11-23
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.1.0).
### Added
* Added feature to extract all attachments or embedded files from the PDF associated with the selected document to a ZIP file or folder. [#33](https://github.com/rffrasca/PDFKeeper/issues/33)
### Changed
* Replaced the "green clock" shown during the upload of PDF documents with a marquee style progress bar. [#40](https://github.com/rffrasca/PDFKeeper/issues/40)

## v10.0.1 - 2024-10-31
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.0.1).
### Fixed
* Fixed System.NullReferenceException: Object reference not set to an instance of an object. [#36](https://github.com/rffrasca/PDFKeeper/issues/36)

## v10.0.0 - 2024-10-26
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v10.0.0).
### Added
* Added Oracle Database Free compatibility.
* Added feature to change "date and time added" on selected document records. [#32](https://github.com/rffrasca/PDFKeeper/issues/32)
* Added option when using SQLite that will compact the database after deleting all selected documents.
* Created sections for each database platform in "Logging into PDFKeeper" help topic.
### Changed
* Improved Connection String handling.
* Improved PDF password handling.
* Flagged document check and auto refresh will not occur when the main form is minimized to reduce database traffic.
* Improved error handling when Oracle ODP.NET is required and not found.
* Changed PDFKeeper logo background color to white on Login Form and About Box.
* Setting of Oracle Wallet Path will no longer be called from the help file which could be blocked by some anti-virus programs.
* SQLite database will be created by PDFKeeper at startup instead of the help file calling a script which could be blocked by some anti-virus programs.
* Migrated Oracle Database client setup help topics to the new Client Setup for Multi-User Database section.
* Updated User Administration help topics for Oracle Database.
* Updated Oracle ODP.NET (Oracle.ManagedDataAccess) to 23.5.1 for connecting to Oracle Database and improved the installation that no longer requires administrative privileges.
* Updated Ghostscript to 10.04.0.
* Updated itext to 8.0.5.
* Updated itext.bouncy-castle-adapter to 8.0.5.
* Updated itext.font-asian to 8.0.5.
* Updated Magick.NET to 14.0.0.
### Removed
* Removed UploadConfigs folder migration logic.
* Removed UploadProfiles folder upgrade logic.
* Removed upgrade logic specific to user settings namespace and name changes.
* Removed "SQLite Error – database disk image is malformed" help topic and fix script.
* Removed Troubleshooting topic from help file.
* Removed SQLite Command Line Shell since PDFKeeper is now creating the SQLite database.
* Removed MultiUserDatabaseSchemaSetup.cmd that is no longer needed.
### Changed (Development)
* Converted PDFKeeper.Presentation to C# as PDFKeeper.WinForms.
* Changed all NotImplementedException references to NotSupportedException.
* Repository instance is now created using DatabaseSession.GetRepository.
* Repository interface and classes are now disposable to handle clearing the connection string builder object.
* Addressed Code Analysis warnings.
* Installer is now built with WiX Toolset 3.14.1.

## v9.2.0 - 2024-06-22
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.2.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.2.0).
### Added
* Added copying of PDF for selected document to the Clipboard.
* Added option to show All Documents on startup that is only available when using the SQLite local database platform.
### Changed
* Changed encoding used by Notes size rule check for Oracle platform to UTF-8 for better accuracy.
* Updated Magick.NET to 13.9.1.
### Fixed
* Fixed progress bar not resetting after each operation.

## v9.1.5 - 2024-06-02
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.5).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.5).
### Added
* Added rule when saving Notes that verifies the size of the Notes string does not exceed the maximum length of the DOC_NOTES column in the database when the database platform is Oracle.
### Changed
* Added predefined tokens to step 2 in the "Upload Profile Editor" help topic.
* Updated "Uploading PDF Documents from outside of PDFKeeper" and "Uploading PDF Documents using Upload Profile Folders" help topics to state that one or more folders that contain only PDF documents can be copied into PDFKeeper Upload.
* Updated Ghostscript to 10.03.1.
* Updated itext to 8.0.4.
* Updated itext.bouncy-castle-adapter to 8.0.4.
* Updated itext.font-asian to 8.0.4.
* Updated Magick.NET to 13.8.0.
### Fixed
* Specific error message will be displayed in place of unhandled System.IO.DirectoryNotFoundException when selecting or attempting to upload PDF that contains % and/or + in the filename.
* System.InvalidOperationException will be handled when document that triggers this exception is selected.
* Corrected location of PDFKeeper Upload shortcut in "Uploading PDF Documents from outside of PDFKeeper" help topic.
* Menu and toolbar items that should be disabled will now be disabled following a find operation that returns no results.

## v9.1.4 - 2024-04-14
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.4).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.4).
### Changed
* Updated "Installing Oracle Data Provider for .NET" help topic.
* Updated Donate help topic to align with website.
* Updated Ghostscript to 10.03.0.
### Fixed
* Selecting (checking) a document in DataGridView no longer requires two clicks.
* When an unhandled exception occurs in MainPresenter.BurstCurrentDocumentPdf, the exception will now be shown; however, an UnauthorizedAccessException will be caught.
* Fixed System.InvalidOperationException: Collection was modified; enumeration operation may not execute.

## v9.1.3 - 2024-03-09
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.3).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.3).
### Changed
* Updated itext to 8.0.3.
* Updated itext.bouncy-castle-adapter to 8.0.3.
* Updated itext.font-asian to 8.0.3.
* Updated Magick.NET to 13.6.0.
### Fixed
* Error message box will no longer show behind Login form when form does not have focus.
* Double clicking on document in documents DataGridView will only open current document not all selected (checked) documents.

## v9.1.2 - 2024-02-18
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.2).
### Changed
* Updated Features list.
* Updated Donate help topic to align with website.
* Updated iText to 8.0.2.
* Updated iText.Font-Asian to 8.0.2.
* Updated Magick.NET to 13.5.0.
* Updated System.Data.SQLite to 1.0.118.
* Updated SQLite Command Line Shell to 3.42.0.
### Fixed
* Documents Find menu and toolbar items are disabled when Notes have changed.
### Changed (Development)
* Created scripts to update copyright year range.

## v9.1.1 - 2023-12-28
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.1).
### Changed
* Updated Donate help topic to align with website.
### Fixed
* Fixed System.NullReferenceException: Object reference not set to an instance of an object when trying to upload a PDF that is missing a Title, Author, or Subject. [#23](https://github.com/rffrasca/PDFKeeper/issues/23)
* Fixed issue when double clicking on document in documents list results in a PDF being opened for a different document.

## v9.1.0 - 2023-12-09
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.1.0).
### Added
* Added feature to set the title on selected document records. [#16](https://github.com/rffrasca/PDFKeeper/issues/16)
* Added feature to set the author on selected document records. [#16](https://github.com/rffrasca/PDFKeeper/issues/16)
* Added feature to set the subject on selected document records. [#16](https://github.com/rffrasca/PDFKeeper/issues/16)
* Added new separators to the Documents menu.
* Added "When prompted to Run or Save, select Run." to all help topics that call a batch file. [#18](https://github.com/rffrasca/PDFKeeper/issues/18)
### Changed
* Changed "Set Tax Year" menu item shortcut keys to Ctrl+Shift+Y.
* Changed "Append Date/Time" menu item shortcut keys to Ctrl+Alt+D.
* Changed "Append Text" menu item shortcut keys to Ctrl+Alt+T.
* Changed Documents Select menu Text property to Se&lect.
* Updated Ghostscript to 10.02.1.
* Updated Sumatra PDF to 3.5.2.
### Fixed
* Fixed FileNotFoundException: The system cannot find the file specified. (Exception from HRESULT: 0x80070002) on startup when Windows Defender blocks the creation of the "PDFKeeper Upload" shortcut in the Documents folder. The shortcut will be created on the Desktop instead. [#15](https://github.com/rffrasca/PDFKeeper/issues/15)
* Fixed text extraction issue when uploading PDF documents that contains one or more image pages. [#20](https://github.com/rffrasca/PDFKeeper/issues/20)
### Changed (Development)
* Removed unused private member from MainPresenter.
* Updated Microsoft.CodeAnalysis.NetAnalyzers to 8.0.0.

## v9.0.0 - 2023-11-24
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v9.0.0).
### Added
* Added auto refresh every thirty seconds to the Documents list when the database platform is Oracle.
* Created Documents menu on the Main form that contains items related to the multi-selecting of Documents in the list. 
* Added status bar image that will be displayed while the Documents list is being refreshed.
### Changed
* Redesigned the layout of the Main form which included the removal of all Document Retrieval functionality that was replaced by the Find Documents dialog.
* Redesigned the Add PDF dialog.
* Redesigned the Upload Profiles dialog which included the removal of all Upload Profile editing functionality that was replaced by the Upload Profile Editor dialog.
* Moved Text from the Insert menu on the main form to the Edit menu and renamed to Append Text.
* Renamed Date/Time in the Edit menu on the main form to Append Date/Time.
* Renamed "List flagged documents on startup" to "Find flagged documents on startup" on the Options dialog.
* Changed Upload Directory Maintenance to execute directly before the Upload.
* Changed Rejected PDF files check to execute directly after the Upload.
* Renamed RemoveListAllDocuments policy to HideAllDocuments.
* Changed class name in the Upload Profile XML schema.
* Updated Magick.NET to 13.4.0. [CVE-2023-4863](https://github.com/advisories/GHSA-j7hp-h8jx-5ppr)
### Removed
* Removed Refresh from the View menu on the Main form.
* Removed Update PDF Text Columns from the Tools menu on the Main form.
* Removed Oracle Database Schema Upgrade help topic and script support.
* Removed the copying of BouncyCastle.Crypto.dll to BouncyCastle.Crypto.dll.bak during setup and the restore command class that is no longer needed since BouncyCastle.Crypto.dll is no longer being deleted during the upgrade of PDFKeeper.
### Fixed
* ArgumentException: The parameter is incorrect. Image dimensions are too large! This exception will be prevented by skipping each PDF page that is to be processed by OCR with a pixel width or pixel height that exceeds the maximum image pixel dimensions supported by the Windows OCR engine. [#13](https://github.com/rffrasca/PDFKeeper/issues/13)
### Changed (Development)
* Implemented the Model-View-Presenter-ViewModel pattern to improve maintainability.
* Created Components and User Controls to eliminate redundancy and improve modularity.
* Rewrote all non-UI code in C# and consolidated into PDFKeeper.Core.
* Rewrote all code related to PDF viewing in C# and consolidated into PDFKeeper.PDFViewer.
* Updated Microsoft.CodeAnalysis.NetAnalyzers to 7.0.4.

## v8.1.2 - 2023-04-16
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.2).
### Changed
- Empty folders in UploadRejected will be deleted.
- Upload Folder Maintenance timer interval was set to 30 seconds.
- Updated iText and iText.Font-Asian to 7.2.5.
- Updated SQLite Command Line Shell to 3.41.1.
- Updated Magick.NET to 13.0.0.
### Fixed
- Added missing ellipsis to Insert > Text menu item text.
- Added missing ellipsis to Tools > Update PDF Text Columns menu item text.
- Added missing ellipsis to Tools > Move Database menu item text.
- iText.IO.Exceptions.IOException will be caught during Save in Add PDF dialog.
- iText.IO.Exceptions.IOException will be caught during an Upload, and then the offending PDF will be moved to the UploadRejected folder.
### Changed (Development)
- Created scripts to set version in all references.
- Updated Microsoft.CodeAnalysis.NetAnalyzers to 7.0.1.

## v8.1.1 - 2023-01-07
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.1).
### Changed
- Flag Document menu item will be disabled when notes have been modified.
- Updated System.Data.SQLite to 1.0.117.
- Updated Magick.NET to 12.2.2.
### Fixed
- Auto Update will no longer prompt for an admin account to install new version.

## v8.1.0 - 2022-12-17
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.1.0).
### Added
- Added Mutual TLS (mTLS) authentication for connecting to Oracle Autonomous Database.
- Added link control to the Upload Profiles dialog that sets the Profile Name to the combined values of Author and Subject.
### Changed
- Updated "SQLite Error - database disk image is malformed" help topic to call fix that will correct triggers in SQLite database to address and prevent the malformed database disk image condition.
- Updated Welcome, Getting Support, and Donate help topics to align with home page/readme.
- Updated Magick.NET to 12.2.1.
- Updated Ghostscript to 10.0.0.
- Updated SQLite Command Line Shell to 3.40.0.
- Updated iText and iText.Font-Asian to 7.2.4.
### Removed
- Removed "Rebuild Full-Text Search Index" menu function.
### Fixed
- ErrorProvider will be displayed instead of an Oracle Text error when Find button is selected and Search Term starts with an asterisk (*).
- Corrected help topic referenced in error message that is displayed when Oracle Data Provider for .NET is missing.
- Corrected triggers in SQLite database setup script to prevent the malformed database disk image condition.
- Created fix to correct triggers in SQLite database to address and prevent the malformed database disk image condition.
### Changed (Development)
- Implemented GlobalAssemblyInfo.vb to simplify version change.

## v8.0.0 - 2022-08-27
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v8.0.0).
### Added
- Added condition requiring Windows 10 or higher for PDFKeeper to install.
- Added Oracle Database 21c (including Express Edition) compatibility.
- Added Oracle Cloud Autonomous Database compatibility (TLS authentication only).
- Added opening of PDF documents for all selected (checked) documents in the Documents list up to a maximum of 12.
- Added Burst for selected document PDF.
- Added option to OCR each PDF page containing text and image data to the Add PDF Dialog, Upload Profiles Dialog, and to the Update PDF Text Columns prompt.
- Added display of Search Term Snippets for the selected document when using Find Documents by Search Term. [#7](https://github.com/rffrasca/PDFKeeper/issues/7)
- Added text extraction for image-only pages in PDF when PDF contains both text and image-only pages.
### Changed
- Updated Oracle Data Provider .NET dependency to the version contained in Oracle Data Access Components 21.4.
- Updated Hash algorithm implementation used to compute file hashes to SHA512.
- Renamed Upload Folder Configurations to Upload Profiles and the folder where Upload Profiles are stored has been changed to %APPDATA%\Robert F. Frasca\PDFKeeper\UploadProfiles.
- Renamed Add PDF Documents dialog to Add PDF and redesigned the dialog to close after adding the PDF.
- Renamed Set Preview Image Resolution dialog and menu item to Set Preview Pixel Density.
- Renamed Select last row when displaying Search Results option to Select last row when listing documents.
- Renamed Open PDF documents with default application option to Show PDF documents with default application.
- Renamed Show Flagged Documents on startup option to List flagged documents on startup.
- Renamed Update PDF Text Annotations and Text in Database menu item to Update PDF Text Columns.
- Renamed Search to Document Retrieval.
- Renamed Documents by Text to Find Documents by Search Terms.
- Renamed Documents by Selections to Find Documents by Selections.
- Renamed Documents by Date Added to Find Documents by Date Added.
- Renamed Flagged Documents to List Flagged Documents.
- Renamed All Documents to List All Documents.
- Find Text is now referred to as Search Term.
- PDF documents that cannot be uploaded will now be moved to %APPDATA%\Robert F. Frasca\PDFKeeper\UploadRejected.
- Changed temporary directory used by PDFKeeper and Magick.NET to %TMP%\PDFKeeper. This folder will be emptied on application shutdown.
- PDFKeeper will now wait for an upload to finish before closing.
- Renamed DisableQueryAllDocuments policy to RemoveListAllDocuments.
- Renamed About menu item to About PDFKeeper.
- Downloads folder in the user profile is retrieved without using a third-party library.
- Ellipsis corrections have been made to some menu items.
- Replaced text formatted license for PDFKeeper in the help file with an HTML formatted copy.
- Replaced Third-Party Attribution in the help file with Third-Party Notices. The notices file was rewritten as THIRD-PARTY-NOTICES.txt that is also compiled into the help file in HTML format.
- Updated SQLite Command Line Shell to 3.39.2.
- Updated Magick.NET to 12.0.1.
- Updated iText and iText.Font-Asian to 7.2.3.
- Updated Ghostscript to 9.56.1.
- Updated Sumatra PDF to 3.4.6.
- Updated System.Data.SQLite to 1.0.116.
### Removed
- Dropped Oracle Database 11g Express Edition compatibility.
- Removed Oracle Database 11g and lower support from OracleDatabaseSchemaSetup.sql and OracleDatabaseSchemaUpgrade.sql.
- Removed Alternate Text Extraction Strategy from PDF Text Extractor. OCR will be performed when iText is unable to extract text from a PDF page.
- Removed unnecessary task completion messages.
### Changed (Development)
- Removed CompileHelp.cmd from Solution.
- Moved all SQL scripts from Help folder to Config folder.
- Rearchitected PDFKeeper.WindowsApplication into separate layers.
- Migrated from Package.Config to PackageReference for NuGet packages.

## v7.2.3 - 2021-12-18
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.3).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.3).
### Changed
- Updated Magick.NET to 8.4.0.
- Updated System.Data.SQLite to 1.0.115.5.
### Fixed
- Fixed Unhandled IOException “The CMap iText.IO.Font.Cmap.UniJIS-UTF16-H was not found.” during PDF text extraction prior to uploading.
- Fixed issue with "Add PDF Documents dialog" hiding behind Main form after Search Results is automatically refreshed. (rework)
- Unhandled ArithmeticException “Overflow or underflow in the arithmetic operation.” during PDF to TIFF extraction prior to uploading PDF will now be handled; however, no text will be extracted from the TIFF image.

## v7.2.2 - 2021-11-06
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.2).
### Added
- Menu function was added to repair the full-text search index when the “database disk image is malformed” error is encountered.
### Changed
- Search Results will no longer be refreshed after an Upload cycle when document records in Search Results are checked.
- Added 30 second sleep after the Upload of PDF files when TMP files exist in the Upload folder.
- Updated Magick.NET to 8.3.3.
- Updated PdfPig to 0.1.5.
- Updated Ghostscript to 9.55.0.
### Fixed
- Fixed issue with "Add PDF Documents dialog" hiding behind Main form after Search Results is automatically refreshed. (rework)
- Fixed Unhandled IOException "The process cannot access the file because it is being used by another process." when deleting a temporary TIFF file following OCR processing.
- Fixed Unhandled NullReferenceException “Object reference not set to an instance of an object.” when selecting "Edit" and then "Select All" following a search.

## v7.2.1 - 2021-09-25
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.1).
### Added
- Added Connection Timeout=60 to the Oracle Database Connection String to mitigate connection timeouts.
### Changed
- Improved PDF image extraction performance and OCR accuracy.
- Updated Magick.NET to 8.3.0.
- Updated System.Data.SQLite to 1.0.115.
### Fixed
- Fixed UploadRunningToolStripStatusLabel not being visible during an upload cycle and Search Results not refreshing after completing.
- Login form error message will no longer appear behind the Login form making it impossible to acknowledge.
- iText.IO.IOException when reading an invalid PDF is now handled.
- iText.Kernel.Pdf.Canvas.Parser.Util.InlineImageParsingUtils.InlineImageParseException when extracting text from PDF is now handled and ignored.
- Fixed application hang when extracting text from an “image-only” PDF while running "Update PDF Text Annotations and Text in Database".

## v7.2.0 - 2021-09-05
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.2.0).
### Added
- Added option to show Flagged Documents on startup of PDFKeeper.
- Added the creation of PDFKeeper Upload shortcut in the Downloads folder that will be available while PDFKeeper is open.
### Changed
- Renamed the “Populate New Database Table Columns” menu item to “Update PDF Text Annotations and Text in Database” and moved it to the Tools menu where it will be enabled when document records are selected in Search Results.
- Changed Search Results to be refreshed after each Upload cycle.
- Modified Manage Upload Folder Configurations dialog to delete selected Upload Folder Configuration file to the Windows Recycle Bin.
- Updated Magick.NET to 8.1.0.
- Updated iText to 7.1.16.
- Updated SumatraPDF to 3.3.3.
- Updated AutoUpdater.NET to 1.7.0.
- Updated Getting Support help topic.
- Updated Donate help topic.
### Fixed
- Add PDF Documents dialog is no longer being hidden after Upload cycle has completed.
- Upload Folder Configurations drop down list on the Manage Upload Folder Configurations dialog will now update when configurations are added, renamed, or deleted outside of PDFKeeper while open.

## v7.1.1 - 2021-07-18
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.1.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.1.1).
### Changed
- Renamed New menu and tool bar items to Add on main form.
- Tax Year combo box drop down list on the Add PDF Documents, Set Tax Year, and Manage Upload Folder Configurations dialogs will only display the last ten years and one year into the future, sorted in descending order.
- Changed "Upload folder contains files that were not uploaded" status message on the main form to "Upload folder contains one or more files that were not uploaded".
- Changed "UploadStaging folder contains files that were not uploaded" status message on the main form to "UploadStaging folder contains one or more files that were not uploaded".
- Changed "Database contains document records that are flagged" status message on the main form to "Database contains one or more document records that are flagged".
- Updated System.Data.SQLite to 1.0.114.4.
- Updated Ghostscript to 9.54.0.
- Updated SQLite Command Line Shell to 3.36.0.
- Updated copyright year for Magick.NET in Third-Party Attribution help topic.
- Updated support methods in Getting Support help topic.
- Changed “Programs and Features” to “Programs and Features or Apps and Features” in the Uninstalling PDFKeeper help topic.
### Fixed
- Upload Service will now delete empty, non-configured folders from the Upload folder.
- UploadFolderErrorToolStripStatusLabel is no longer visible on the status bar after a successful upload cycle.

## v7.1.0 - 2021-06-26
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.1.0).
### Added
- Text from image-only PDF will be extracted using OCR before uploading, and then stored in an indexed database column. (requires Windows 10)
- Menu function was added to move local SQLite database to an alternate location.
### Changed
- Migrated from iTextSharp to iText Core/Community 7.1.15.
- PdfPig 0.1.4 was added to handle text extraction when iText throws an ArgumentException while trying to extract text from a PDF that contains an invalid encoding.
- Magick.NET was updated to 7.24.1.0.
### Fixed
- Fixed License not opening from About box.
### Changed (Development)
- Improvements were made to the File Type and PDF Text Extractor classes.
- Help file is now compiled manually outside of build process to verify help file is not corrupt.

## v7.0.0 - 2021-03-28
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v7.0.0).
### Added
- Added a local, single user database option using SQLite.
- Added Tax Year feature for document records.
- Text annotations will be extracted from the PDF before uploading and stored in an indexed database column.
- Text from the PDF will be extracted before uploading and stored in an indexed database column.
### Changed
- Consolidated Third-Party Attribution help pages into single page.
- Modified Save As to retrieve the selected PDF Title from the database instead of PDF Metadata.
- Modified Export to rewrite an exported PDF with Title, Author, Subject, and Keywords from the database document record when the Title, Author, Subject, and Keywords in the PDF Metadata do not match.
- Updated SumatraPDF to 3.2.
- Migrated from pdftopng from Xpdf Tools to Magick.NET 7.22.2.2 and Ghostscript 9.53.3.
- Moved Search Results to right side of form and moved right Tab Control (Notes, Keywords, Preview, and Text) to left side of form under Search Group Box.
### Removed
- Oracle Database 18c (not XE), 12c, and 11g (not XE) has been dropped from the compatibility list.
### Fixed
- Unhandled InlineImageParseException: "Could not find image data or EI" during text extraction of an older PDF that does not contain text.
### Changed (Development)
- Source code was reorganized, and name changes were made.
- Created script that will download and extract SumatraPDF during the build process if missing or not the required version, replacing manual steps in Build Instructions.

## v6.1.1 - 2020-11-27
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.1.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.1.1).
### Fixed
- NullReferenceException at PDFKeeper.WindowsApplication.FileSelectDisplayService.Dispose(Boolean disposing) that is being logged to PDFKeeper.log during application shutdown.
- Preview Picture Box not always displaying correct image for selected document record.
- Cache folder not always clearing on application shutdown along with System.NullReferenceException or System.IndexOutOfRangeException being logged to Windows Application Event Log.

## v6.1.0 - 2020-11-21
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.1.0).
### Added
- Added import from text file into Notes text box with option to delete text file after importing.
### Changed
- Modified Export to save PDF files and supplemental data into a folder structure organized by Author and Subject and named []<Title>.PDF and []<Title>.XML for corresponding supplemental data.
- User will be prompted to refresh Search Results when Flagged Documents are being listed and flag is removed from selected document record.
- Search Results will be refreshed after PDF files are uploaded when Documents by Date Added and current date are being listed and Document Notes for the selected document record are not being edited.
- Search Results will be refreshed after flagged PDF files are uploaded when Flagged Documents are being listed and Document Notes for the selected document record are not being edited.
- Search Results will be refreshed after PDF files are uploaded when All Documents are being listed and Document Notes for the selected document record are not being edited.
- Updated AutoUpdater.NET to 1.6.4.
### Fixed
- Selected document record in Search Results not always visible after refreshing.
- InvalidOperationException at System.Drawing.Image.get_Width() or System.Drawing.Image.get_FrameDimensionsList() when document record is selected resulting in preview image failing to load into Preview Picture Box.

## v6.0.1 - 2020-10-16
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.1).
### Changed
- Updated iTextSharp to 5.5.13.2.
- Updated AutoUpdater.NET to 1.6.3.
### Fixed
- NullReferenceException at PDFKeeper.WindowsApplication.CommonViewPresenter.Dispose(Boolean disposing) that is being logged to PDFKeeper.log during application shutdown.
- Help topic not opening when F1 is pressed while Set Category dialog is open.
- Welcome Help Topic opening instead of "Configuring an Upload Folder" topic when F1 is pressed while Manage Upload Folder Configurations dialog is open after New or Edit is selected.
- Upload in progress status bar icon is being displayed when no PDF files need to be uploaded but other, unsupported file types exist in the Upload folder.
- UploadFolderErrorToolStripStatusLabel will remain visible on the status bar after all of the files that cannot be uploaded were removed from the Upload folder and will remain visible until the next upload cycle runs that uploads one or more PDF files.
- Wait cursor display delay when Select All or Deselect All is selected from the File menu.
- Progress bar on Main Form not updating properly during a lengthy Search Results operation (Delete, Export, or setting of a Category).
- "Failed to create restore point (Process = C:\Windows\system32\msiexec.exe /V; Description = Installed/Removed PDFKeeper; Error = 0x80042306)" is logged to Windows Application Event Log during an install and uninstall.

## v6.0.0 - 2020-09-26
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.0).
### Changed
- .NET Framework 4.8 is now targeted.
- Converted to 64-bit; Windows (32-bit) is no longer supported!
- Upload Service will copy each PDF in the Upload folder, appending a GUID to the file name, and then changing the extension on the original PDF to "delete" to be deleted during a future upload cycle and avoid a file in use prompt when deleting to the Windows Recycle Bin.
- Main form was redesigned to provide more flexible searching and reduce clicks.
- Database schema and Oracle Data Provider for .NET upgrade notification on startup has been reworked.
- Secondary application description was changed to "PDFKeeper is free, open source software that integrates with a compatible database to provide a centralized storage and management solution for PDF documents."
- "Third-Party Components" help topic was renamed to "Third-Party Attribution" and the introduction was changed to "This version of PDFKeeper uses third-party libraries or other resources that may be distributed under licenses different than the PDFKeeper software."
- Legal notice on Welcome page in help file was changed to "PDFKeeper is OSI Certified Open Source Software, licensed under the terms of the GNU General Public License (GPL) Version 3" and added Open Source Initiative and GPLv3 logos.
- About box contains a link to the PDFKeeper Website.
- Help file contains a Donate topic and About box contains a link to the help topic.
- Oracle Database 11g Express Edition compatibility testing has been discontinued.
### Fixed
- "Access to the path <PDF_PATH_NAME> is denied" when a document is selected in Search Results following PDFKeeper being closed while Sumatra PDF was displaying one or more selected PDF documents and PDFKeeper was opened again without closing Sumatra PDF first.
- Unhandled Oracle Exception when the Upload Service is trying to process a PDF that was moved from the Upload folder to the Upload Staging folder that is missing one or more required information property values (Title, Author, Subject).
### Changed (Development)
- Visual Studio 2019 is now used to build PDFKeeper.
- Build Instructions have been edited to instruct the developer to use Manage Extensions in Visual Studio to install WiX Toolset Build Tools, Wix Toolset Visual Studio 2019 Extension, and Wax.
- Application source folders have been reorganized; classes and interfaces have been renamed and refactored.

## v5.0.3 - 2019-12-27
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.3).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.3).
### Changed
- When Search Results is refreshed, the previously selected document record will be selected.
### Fixed
- Fixed an issue with Add PDF Documents dialog not having focus after selecting a PDF document.
- Fixed left panel of main form not always opening full width after Search Results grid view is filled when the width of the columns exceeded the width of Search Results grid view.
- Fixed Search Results grid view Category column not sizing correctly after selecting Toggle Right Panel to show right panel and then selecting Toggle Right Panel again to hide right panel.
- Fixed selected Search tab on main form not always being visible after the right panel collapsed state has changed.
When Search Results is refreshed, the previously selected document record will be selected.

## v5.0.2 - 2019-11-30
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.2).
### Changed
- Database connection pooling is now a setting in PDFKeeper.exe.config that can be set to True or False.
### Fixed
- Fixed a memory and handle leak.
- Corrected issue with how Oracle secure password handling was implemented, allowing database connection pooling to be enabled after being disabled in version 5.0.0.

## v5.0.1 - 2019-11-16
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.1).
### Changed
- Modified PDF file and application generated supplemental data will now be permanently deleted from the UploadStaging folder after being uploaded.
- Updated pdftopng from Xpdf Tools to 4.02.
- Updated AutoUpdater.NET to 1.5.8.
### Fixed
- Fixed "PDFKeeper has detected that you've upgraded from an older version" message box from being displayed when PDFKeeper was upgraded from version 5.0.0 or above.
- Upload status no longer shows running after one or more PDF documents fail to upload.
- Fixed User Interface hanging while the Upload process is running.
- Fixed unhandled InvalidOperationException during an upload that needs to be caught without displaying an error.

## v5.0.0 - 2019-10-24
- This is a major release and the 10th year anniversary edition, officially released on the 10th year anniversary of PDFKeeper.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.0).
### Added
- Added compatibility for Oracle Database 18c Express Edition and 19c.
- Flag Document feature has been added for setting the flag state on a selected document record and when PDF documents are uploaded as a way to mark for follow-up.
- Category feature has been added for setting or clearing the category on one or more selected document records.
- Optimistic concurrency has been added to Document Notes editing for preventing data loss during a save operation.
### Changed
- Logo for PDFKeeper has been changed.
- Description of PDFKeeper has been changed to "Open Source PDF Document Management System".
- Manage Upload Folder Configurations form has replaced the Upload Folders and Upload Folder Configuration forms.
- Add PDF Documents form has been redesigned to allow adding more than one PDF document without leaving the form.
- Updated help file to instruct user to not restore documents from the recycle bin while PDFKeeper is open.
- Improvements have been made to the handling of SecureStrings.
- Oracle Data Provider for .NET, included in Oracle Data Access Components Runtime 19.3 is now required offering better performance and security.
- Updated AutoUpdater.NET to 1.5.7.
- Updated iTextSharp to 5.5.13.1.
- Updated pdftopng from Xpdf Tools to 4.01.01.
### Fixed
- All SQL statements that accept parameters have been parameterized to address SQL Injection concerns flagged by Code Analysis.
- Oracle Database user password is now being passed securely when connecting to the database preventing exposure in a page file swap or crash dump.
- Fixed issue with PDFKeeper incorrectly detecting that an upgrade was performed from a prior version when no user settings exist from a prior version.
### Changed (Development)
- All source code has been refactored and reorganized.

## v4.1.0 - 2018-12-24
- This is a minor release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.1.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.1.0).
### Added
- Added compatibility for Oracle Database 18c, 12c, and 11g Release 2.
### Changed
- Replaced the numeric datatype with number in the database schema creation script for Oracle as numeric is deprecated.
- Replaced all usage and references to the Oracle Database connect and resource roles in the schema creation script and Help file with only the required privileges as the connect and resource roles have been deprecated by Oracle.
- Exception type is now displayed and logged during an unhandled exception event.
### Fixed
- Sumatra PDF is now being closed by the Add PDF Document form after View Original or Preview has been selected while Sumatra PDF was open.
- Fixed unhandled exception that would occur when PDFKeeper is unable to delete the "PDFKeeper Upload" shortcut when closing.
- Fixed unhandled exception that would occur when exporting a PDF from a document record that no longer exists.
- Fixed unhandled exception that would occur when selecting a document record in the Search Results DataGridView that no longer exists.

## v4.0.2 - 2018-09-08
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.2).
### Changed
- Updated links in the Oracle Database Express Edition (XE) Database Setup and Database User Administration help topics.
- The View Original button is now selected in place of the Title text box	when the Add PDF Document dialog loads.
- Default file name in Save As dialog is now set to the title of the PDF for the selected document record.
- Updated iTextSharp to 5.5.13.
- Updated AutoUpdater.NET to 1.4.11.
### Fixed
- Fixed an Unhandled Exception that would occur when saving a PDF for the	selected document record to a file system and the target file name already existed.
- Combo box on "Search by String" tab is no longer selecting the first string in the drop-down list that contains the text in text box, overwriting the text in the text box when the Search button is selected.

## v4.0.1 - 2018-08-04
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.1).
### Added
- Added support to migrate user settings from prior version starting with version 4.0.0.
### Changed
- Horizontal slide bar is now being displayed on the search results data grid view when the combined length of the search results exceed the width of the main form.
- Empty sub-folder(s) are now being removed from configured upload folder(s) by the upload process.
- Search Results Data Grid View now gets focus after a search.
- Author Combo Box on the "Search by Author" tab now drops down when a key is pressed after receiving focus.
- Subject Combo Box on the "Search by Subject" tab now drops down when a key is pressed after receiving focus.
- Author and Subject Combo Boxes on the "Search by Author and Subject" tab now drops down when down arrow key is pressed after receiving focus.
- Subject combo box drop down on the "Search by Author and Subject" tab will now always show correct subjects for the selected author when navigating with the keyboard.
- "Improper usage of query operators and/or characters" error notification on the "Search by String" tab is no longer being displayed for strings that end in characters that match a character based query operator.
- Paste menu and tool bar items are now always enabled when Notes text box has focus and the clipboard contains text.
- Corrected the Sumatra PDF usage description on its help topic.

## v4.0.0 - 2018-03-31
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.0).
### Added
- User interface has been completely redesigned, adding additional search capabilities.
- Conversion script was created to switch the lexer in an existing database schema to the WORLD_LEXER where the lexer was changed.
### Changed
- .NET Framework 4.6.1 is now targeted.
- Installation is now per-user, no longer requiring elevated rights to install.
- PDF documents can now be opened from the Search Results list with the default PDF viewer or Sumatra PDF.
- Document Capture and Direct Upload have been replaced by a single integrated Upload process.
- All User Documentation is now contained within the help file.
- All database setup scripts are now called from the help file.
- WORLD_LEXER is now the default lexer for new database schemas.
- Updated iTextSharp to 5.5.12.
- PDF Preview images are now generated by pdf2png from Xpdf Tools 4.00, replacing GhostScript.
- Updated Sumatra PDF to 3.1.2.
- Application update is now handled by AutoUpdater.NET 1.4.7.
- Nini is no longer used to read and write XML configuration files and has been removed.  This functionality is now performed using .NET Framework serialization.
### Changed (Development)
- Visual Studio 2013 is now used to build PDFKeeper.
- All code has been completely rewritten to address maintainability issues.
  
## v3.1.2 - 2015-07-11
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.2).
- Binary release is no longer available.
### Changed
- Updated Ghostscript to 9.16.
### Fixed
- Document Search: listview not scrolling when selected item is off the screen.
- Document Search: selected listview item becomes deselected after refresh.

## v3.1.1 - 2015-03-21
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.1).
- Binary release is no longer available.
### Fixed
- Losing focus from Document Search list view when arrowing up or down.
- Document Capture: Unhandled Exception.

## v3.1.0 - 2015-02-07
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.0).
- Binary release is no longer available.
### Added
- Document Preview to Main Form.
- Document Text View to Main Form.
### Changed
- Updated iTextSharp to 5.5.2.
- Updated Sumatra PDF to 3.0.
### Removed
- XPS and HTML converters.

## v3.0.1 - 2014-11-22
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.1).
- Binary release is no longer available.
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

## v3.0.0 - 2013-12-21
- This is a major release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.0).
- Binary release is no longer available.
### Added
- Prompt for password when PDF document contains an OWNER password.
- Window-level help system for application forms replacing the User Guide.
- Seamless XPS to PDF document conversion to Document Capture using GhostXPS.
- Seamed HTML to PDF document conversion to Document Capture using wkhtmltopdf.
- Create "Document Capture"; a centralized, document intake process.
- Menu item for reporting new issue.
- Created "Direct Upload" that will replace the "PDF Document Upload folder watcher.
### Changed
- .NET Framework 4.0 Client Profile is now targeted.
- Trap all unhandled exceptions and display to user.
- Modify update check to use new project site for verification.
- Migrate installation from InnoSetup to Windows Installer.
- Oracle Data Provider for .NET, Managed Driver is now required in place of Oracle Data Access Components.
- Enable database connection pooling to improve performance.
- Updated Sumatra PDF to 2.4.
- Updated iTextSharp to 5.4.5.
- Redesign About Form.
- Set form font to SystemFonts.MessageBoxFont to improve appearance on Windows Vista and later.
### Fixed
- Update links in source and documentation to new project site.
- Using mouse scroll wheel following search results in scrolling in Search Text combo box instead of list view.
- Database Setup: 'sqlplus' is not recognized as an internal or external command, operable program or batch file.
- Improve Database Connection form password handling.
- Main Form status bar getting cut off when vertical screen resolution value is 800.

## v2.7.0 - 2012-09-29
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.7.0).
- Binary release is no longer available.
### Added
- Main Form: Add a folder watcher that will directly upload PDF documents.
### Changed
- Set InitialDirectory property to last folder selected in Open and Save dialogs.
- Updated Sumatra PDF to 2.1.1.
- Updated iTextSharp to 5.3.0.
- Prompt to process existing PDF's when folder watchers are enabled.
- Installer: Move uninstall of existing version to just before start of installing files.
- Information Properties Editor and folder watcher; upload folder watcher file saving modifications.
### Fixed
- Main Form: ListView sort order being reset on new search.
- Main Form: Form flicker during a lengthy operation.
- Information Properties Editor Form: Form flicker when selecting Author or Subject combo box.
- Information Properties Editor Folder Watcher: PDF not found as file or resource.

## v2.6.0 - 2012-06-30
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.6.0).
- Binary release is no longer available.
### Added
- Information Properties Editor Form: Add checkbox that when checked will upload PDF document to the database after saving.
- Implement logging for the PDF upload process.
- Main Form and Information Properties Editor Form: Add option to delete PDF document after being uploaded.
- Information Properties Editor Form: Add checkbox that will delete the source file after saving the target file.
- Main Form: Information Properties Editor Folder Watcher: Add event to status bar icon to open watched folder.
- Main Form: Implement a centralized document upload process.
### Changed
- Updated Sumatra PDF to 2.0.1.
- Updated iTextSharp to 5.2.1.
- Installer to prompt user to close applications in use during installation on Windows Vista and newer.
### Fixed
- PDFKeeper not responsive after opening PDF document.
- PDFKeeper not responsive after opening Help or Windows Explorer on non-English version of Windows.

## v2.5.1 - 2012-04-14
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.1).
- Binary release is no longer available.
### Fixed
- Information Properties Editor Folder Watcher detecting duplicate events.
- Information Properties Editor Folder Watcher launching Information Properties Editor while PDF is being written.
- Folder Watcher log file only contains the last error logged.
- Folder Watcher Unhandled Exception: System.ArgumentOutOfRangeException: startIndex can not be less than zero.

## v2.5.0 - 2012-03-24
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.0).
- Binary release is no longer available.
### Added
- Encrypt PDF document when retrieved from the database during an "open" operation on EFS supported operating systems.
- Main Form: Add a folder watcher that will call the Information Properties Editor.
### Changed
- Store user settings in an XML file instead of the registry.
- Information Properties Editor: remember the last state of the "After saving, open PDF document in viewer" check box.
- Updated iTextSharp to 5.2.0.
### Fixed
- Disable some of Sumatra PDF's functionality.
- PdfKeeper.exe is not removed if in use during uninstall.
- "DeleteFile failed; code 5. Access is denied." during upgrade if PDFKeeper is in use.
- Main Form: (Not Responding) after opening PDF document.
- Move PDF document cache to location in user profile that remains local.
- Information Properties Editor not waiting for user to close PDF viewer when "After saving, open PDF document in viewer." is checked.
- Information Properties Editing: Modify to not rename original file and to append "modified_by_pdfkeeper" to the filename of the new copy.

## v2.4.0 - 2011-12-24
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.4.0).
- Binary release is no longer available.
### Changed
- Information Properties Editor: Make form modeless.
- Use Cache folder instead of TEMP folder for opened PDF documents.
- Enable caching of opened PDF documents.
- Update documentation for ODAC 11.2 Release 3 (11.2.0.2.1).
- Update documentation for Oracle Database Express Edition 11g Release 2.
- Updated Sumatra PDF to 1.9.
- Updated iTextSharp to 5.1.3.
### Fixed
- Main Form: Display Document Upload results on status bar, not in message box.
- About Form: Graphic, text, and OK button not centered.

## v2.3.0 - 2011-09-24
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.3.0).
- Binary release is no longer available.
### Changed
- Main Form: Enhance the Upload process to execute on a separate thread.
- Updated Sumatra PDF to 1.7.
- Updated iTextSharp to 5.1.2.
### Fixed
- Main Form: Document Notes text is highlighted after update.
- Main Form: Check for update not detecting newer version on project site.

## v2.2.0 - 2011-06-25
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.2.0).
- Binary release is no longer available.
### Changed
- Updated Sumatra PDF to 1.6.
- Consolidate Document Notes and Document Keywords functionality into Main Form.
- Updated iTextSharp to 5.1.1.
### Fixed
- Information Properties Editor: Unhandled Exception: Access to the path is denied.
- Information Properties Editor: On occasion, the Author has to be chosen twice.
- Main Form: Search Text combo box does not resize.
- Database Setup: Only works if either Oracle XE server or client is installed.

## v2.1.0 - 2011-03-25
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.1.0).
- Binary release is no longer available.
### Added
- Document Notes Form: Add Date/Time stamp feature.
- Main Form: Add "Search Text" history feature.
- Add check for update feature.
- Document Notes Form: Add print feature.
- Information Properties Editor: Force close PDF file on save or cancel, if open with bundled viewer.
### Changed
- Updated iTextSharp to 5.0.6.
- Updated Sumatra PDF to 1.4.
- Information Properties Editor: Replace prompt to view document after saving with a checkbox on form.
### Fixed
- Main Form: Clicking on a list view column after clearing search text triggers an Oracle Text parser error.
- Database Connection Form: Unhandled exception following second failed logon attempt.
- Main Form: When opening a PDF document already open, a second window is opening.

## v2.0.0 - 2010-11-22
- This is a major release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.0.0).
- Binary release is no longer available.
### Added
- Add document upload capability to the client.
### Changed
- Updated Sumatra PDF to 1.1.
- The Database Setup will now build the database schema with the BASIC_LEXER, replacing the WORLD_LEXER.
- pdfinfo.exe from Xpdf and pdftk have been replaced by iTextSharp.
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
- Removed server-side upload components.
- Rexx/SQL is no longer used and has been removed.
### Changed (Development)
- Application was redesigned and rewritten in VB.NET using SharpDevelop for .NET Framework 3.5 SP1; Open Object Rexx 4.0.0 is no longer being used.

## v1.1.0 - 2010-04-29
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.1.0).
- Binary release is no longer available.
### Added
- Add functionality to "Check" and "Uncheck" all list view items on Search dialog.
- PDFKeeper Summary Editor: Add check for vertical pipe '|' symbol(s).
- Add more examples on the "about" operator to the User Guide.
- Add upload folder integrity check to document loader.
### Changed
- Updated Sumatra PDF to 1.0.1.
- Updated pdfinfo.exe to 3.02pl4.
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

## v1.0.0 - 2009-10-24
- First, full release of this product to the public.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.0.0).
- Binary release is no longer available.
