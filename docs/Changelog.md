# PDFKeeper Changelog
All notable changes to PDFKeeper will be documented in this file.

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
- Updated Magick.NET to 8.1.0
- Updated iText to 7.1.16
- Updated SumatraPDF to 3.3.3
- Updated AutoUpdater.NET to 1.7.0
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
- Updated System.Data.SQLite to 1.0.114.4
- Updated Ghostscript to 9.54.0
- Updated SQLite Command Line Shell to 3.36.0
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
- Migrated from iTextSharp to iText Core/Community 7.1.15
- PdfPig 0.1.4 was added to handle text extraction when iText throws an ArgumentException while trying to extract text from a PDF that contains an invalid encoding.
- Magick.NET was updated to 7.24.1.0
- Improvements were made to the File Type and PDF Text Extractor classes.
- Help file is now compiled manually outside of build process to verify help file is not corrupt.
### Fixed
- Fixed License not opening from About box.

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
- Updated SumatraPDF to 3.2
- Migrated from pdftopng from Xpdf Tools to Magick.NET 7.22.2.2 and Ghostscript 9.53.3
- Moved Search Results to right side of form and moved right Tab Control (Notes, Keywords, Preview, and Text) to left side of form under Search Group Box.
- Source code was reorganized and name changes were made.
- Created script that will download and extract SumatraPDF during the build process if missing or not the required version, replacing manual steps in Build Instructions.
### Removed
- Oracle Database 18c (not XE), 12c, and 11g (not XE) has been dropped from the compatibility list.
### Fixed
- Unhandled InlineImageParseException: "Could not find image data or EI" during text extraction of an older PDF that does not contain text.

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
- Updated AutoUpdater.NET to 1.6.4
### Fixed
- Selected document record in Search Results not always visible after refreshing.
- InvalidOperationException at System.Drawing.Image.get_Width() or System.Drawing.Image.get_FrameDimensionsList() when document record is selected resulting in preview image failing to load into Preview Picture Box.

## v6.0.1 - 2020-10-16
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v6.0.1).
### Changed
- Updated iTextSharp to 5.5.13.2
- Updated AutoUpdater.NET to 1.6.3
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
- Converted to 64-bit; Windows (32-bit) is no longer supported!
- Upload Service will copy each PDF in the Upload folder, appending a GUID to the file name, and then changing the extension on the original PDF to "delete" to be deleted during a future upload cycle and avoid a file in use prompt when deleting to the Windows Recycle Bin.
- Main form was redesigned to provide more flexible searching and reduce clicks.
- Visual Studio 2019 is used to build PDFKeeper.
- .NET Framework 4.8 is targeted during the build.
- Build Instructions have been edited to instruct the developer to use Manage Extensions in Visual Studio to install WiX Toolset Build Tools, Wix Toolset Visual Studio 2019 Extension, and Wax.
- Application source folders have been reorganized; classes and interfaces have been renamed and refactored to better align with the MVP (Model-View-Presenter) pattern.
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

## v5.0.3 - 2019-12-27 (legacy)
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

## v5.0.2 - 2019-11-30 (legacy)
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.2).
### Changed
- Database connection pooling is now a setting in PDFKeeper.exe.config that can be set to True or False.
### Fixed
- Fixed a memory and handle leak.
- Resolved issue with how Oracle secure password handling was implemented, allowing database connection pooling to be enabled after being disabled in version 5.0.0.

## v5.0.1 - 2019-11-16 (legacy)
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.1).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v5.0.1).
### Changed
- Modified PDF file and application generated supplemental data will now be permanently deleted from the UploadStaging folder after being uploaded.
- pdftopng from Xpdf Tools was updated to 4.02 in the binary release.
- AutoUpdater.NET was updated to 1.5.8 in the binary release.
### Fixed
- Fixed "PDFKeeper has detected that you've upgraded from an older version" message box from being displayed when PDFKeeper was upgraded from version 5.0.0 or above.
- Upload status no longer shows running after one or more PDF documents fail to upload.
- Fixed User Interface hanging while the Upload process is running.
- Fixed unhandled InvalidOperationException during an upload that needs to be caught without displaying an error.

## v5.0.0 - 2019-10-24 (legacy)
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
- Entire application source code has been refactored and reorganized.
- AutoUpdater.NET was updated to 1.5.7 in the binary release.
- iTextSharp was updated to 5.5.13.1 in the binary release.
- pdftopng from Xpdf Tools was updated to 4.01.01 in the binary release.
### Fixed
- All SQL statements that accept parameters have been parameterized to address SQL Injection concerns flagged by Code Analysis.
- Oracle Database user password is now being passed securely when connecting to the database preventing exposure in a page file swap or crash dump.
- Fixed issue with PDFKeeper incorrectly detecting that an upgrade was performed from a prior version when no user settings exist from a prior version.

## v4.1.0 - 2018-12-24 (legacy)
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

## v4.0.2 - 2018-09-08 (legacy)
- This is a maintenance release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.2).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.2).
### Changed
- Updated links in the Oracle Database Express Edition (XE) Database Setup and Database User Administration help topics.
- The View Original button is now selected in place of the Title text box	when the Add PDF Document dialog loads.
- Default file name in Save As dialog is now set to the title of the PDF for the selected document record.
- iTextSharp was updated to 5.5.13 in the binary release.
- AutoUpdater.NET was updated to 1.4.11 in the binary release.
### Fixed
- Fixed an Unhandled Exception that would occur when saving a PDF for the	selected document record to a file system and the target file name already existed.
- Combo box on "Search by String" tab is no longer selecting the first string in the drop down list that contains the text in text box, overwriting the text in the text box when the Search button is selected.

## v4.0.1 - 2018-08-04 (legacy)
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

## v4.0.0 - 2018-03-31 (legacy)
- This is a major release.
- Source code is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.0).
- Binary release is available [here](https://github.com/rffrasca/PDFKeeper/releases/tag/v4.0.0).
### Added
- User interface has been completely redesigned, adding additional search capabilities.
- Conversion script was created to switch the lexer in an existing database schema to the WORLD_LEXER where the lexer was changed.
### Changed
- Installation is now per-user, no longer requiring elevated rights to install.
- Microsoft .NET Framework 4.6.1 or above is now required.
- PDF documents can now be opened from the Search Results list with the default PDF viewer or Sumatra PDF viewer (included in the binary  release).
- Document Capture and Direct Upload have been replaced by a single integrated Upload process.
- All code has been completely rewritten to address maintainability issues.
- All User Documentation is now contained within the help file.
- All database setup scripts are now called from the help file.
- WORLD_LEXER is now the default lexer for new database schemas.
- iTextSharp was updated to 5.5.12 in the binary release.
- PDF Preview images are now generated by pdf2png from Xpdf Tools v4.00, replacing GhostScript in the binary release.
- Sumatra PDF was updated to 3.1.2 in the binary release.
- Application update is now handled by AutoUpdater.NET 1.4.7 in the binary release.
- Nini is no longer used to read and write XML configuration files and has been removed from the binary release.  This functionality is now performed using .NET Framework serialization.

## v3.1.2 - 2015-07-11 (retired)
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.2).
- Binary release is no longer available for distribution.
### Changed
- Third-Party software update in binary release: Ghostscript 9.16.
### Fixed
- Document Search: listview not scrolling when selected item is off the screen.
- Document Search: selected listview item becomes deselected after refresh.

## v3.1.1 - 2015-03-21 (retired)
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.1).
- Binary release is no longer available for distribution.
### Fixed
- Losing focus from Document Search list view when arrowing up or down.
- Document Capture: Unhandled Exception.

## v3.1.0 - 2015-02-07 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.1.0).
- Binary release is no longer available for distribution.
### Added
- Document Preview to Main Form.
- Document Text View to Main Form.
### Changed
- Third-Party software update in binary release: iTextSharp 5.5.2.
- Third-Party software update in binary release: Sumatra PDF 3.0.
### Removed
- XPS and HTML converters.

## v3.0.1 - 2014-11-22 (retired)
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.1).
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

## v3.0.0 - 2013-12-21 (retired)
- This is a major release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/3.0.0).
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

## v2.7.0 - 2012-09-29 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.7.0).
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

## v2.6.0 - 2012-06-30 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.6.0).
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

## v2.5.1 - 2012-04-14 (retired)
- This is a maintenance release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.1).
- Binary release is no longer available for distribution.
### Fixed
- Information Properties Editor Folder Watcher detecting duplicate events.
- Information Properties Editor Folder Watcher launching Information Properties Editor while PDF is being written.
- Folder Watcher log file only contains the last error logged.
- Folder Watcher Unhandled Exception: System.ArgumentOutOfRangeException: startIndex can not be less than zero.

## v2.5.0 - 2012-03-24 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.5.0).
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

## v2.4.0 - 2011-12-24 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.4.0).
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

## v2.3.0 - 2011-09-24 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.3.0).
- Binary release is no longer available for distribution.
### Changed
- Main Form: Enhance the Upload process to execute on a separate thread.
- Third-Party software update in binary release: Sumatra PDF 1.7.
- Third-Party software update in binary release: iTextSharp 5.1.2.
### Fixed
- Main Form: Document Notes text is highlighted after update.
- Main Form: Check for update not detecting newer version on project site.

## v2.2.0 - 2011-06-25 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.2.0).
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

## v2.1.0 - 2011-03-25 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.1.0).
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

## v2.0.0 - 2010-11-22 (retired)
- This is a major release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/2.0.0).
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

## v1.1.0 - 2010-04-29 (retired)
- This is a minor release.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.1.0).
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

## v1.0.0 - 2009-10-24 (retired)
- First, full release of this product to the public.
- Source code is archived [here](https://github.com/rffrasca/PDFKeeper-Source-Archive/tree/master/1.0.0).
- Binary release is no longer available for distribution.
