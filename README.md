![Logo](https://github.com/robertfrasca/PDFKeeper/blob/master/src/Resources/Logo/PDFKeeper.bmp) 
# PDFKeeper
Open Source PDF Document Storage Solution

## Overview
PDFKeeper integrates with a compatible, relational database management system to provide an upload, index, and search solution for PDF documents.

## Features
* PDF documents are stored and indexed in a compatible, relational database where they can be searched by String, Author, Subject, Author and Subject, Date Added, and by querying all documents.
* PDF documents can be uploaded individually or in bulk, with or without using configured Upload folders.
* Document record functions include, PDF document viewing with the bundled or default viewer, Notes editing, Keywords viewing, PDF document Preview image viewing from 10 to 600 dots per inch (DPI), and viewing of Text extracted from the PDF document.
* Notes can be added to document records, including a Date and Time stamp that includes the database user account name. All Notes can be edited and are indexed by the database, making the text searchable. Notes can be saved to a text file, printed, and can be uploaded with a name matching PDF document.
* Text extracted from the PDF document can be printed and saved to a text file.
* PDF documents and their Notes can be exported from the database.
* Querying of the entire database can be disabled by applying a policy to each client.

## Client Deployment Requirements
* .NET Framework 4.6.1 or higher
* Windows 7 SP1 (32 or 64 bit) or newer
* Oracle ODP.NET, Managed Driver

## Supported Database Management Systems
* Oracle Database 11g Express Edition

At this time, PDFKeeper has been designed for use with the Oracle Database and only the free, Oracle Database Express Edition (XE) has been tested. Future releases will include support for additional relational database management systems. For a database to be considered, please open an new issue, unless one was already opened. If your database has already been requested, please update the existing issue.

## Building
TODO

## Documentation and Support
* [Build Requirements and Instructions](https://github.com/robertfrasca/PDFKeeper/blob/master/Source/BUILD-README.txt)
* [PDFKeeper Home Page](https://bit.ly/pdfkeeper)
* Status updates, visit [Google+](https://plus.google.com/103180603238817050437)

## Deployment Requirements
* Microsoft Windows (XP SP3 or newer)
* Microsoft .NET Framework 4.0 (Client or Extended) or above
* Oracle ODP.NET, Managed Driver

Database Management System
* Oracle Database Express Edition

## Downloads
* [Supported stable releases](https://pdfkeeper.codeplex.com/releases/view/616109)

## License
* PDFKeeper is licensed under the terms of the [GPL v3](https://github.com/robertfrasca/PDFKeeper/blob/master/Source/LICENSE.txt).
