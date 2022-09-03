<a name="readme-top"></a>

<!-- PROJECT INTRO AND SHIELDS -->
<br />
<div align="center">
  <a href="https://github.com/rffrasca/pdfkeeper">
    <img src="src/Resources/Logo/PDFKeeper_100x100.png" alt="Logo" width="100" height="100">
  </a>

<h1 align="center">PDFKeeper</h1>
<h3 align="center">Open Source PDF Document Management</h3>
  
  <p align="center">
    <br />

[![Downloads][downloads-shield]][downloads-url]
[![Downloads Latest][downloads-latest-shield]][downloads-latest-url]
[![Twitter][twitter-shield]][twitter-url]
    <br />
    <br />
    <a href="https://github.com/rffrasca/pdfkeeper/issues">Report Bug</a>
    ·
    <a href="https://github.com/rffrasca/pdfkeeper/issues">Request Feature</a>
    ·
    <a href="https://github.com/rffrasca/pdfkeeper/issues">Request adding Compatible Database</a>
  </p>
</div>


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#building-from-source">Building from Source</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

PDFKeeper is free, open source software that provides a storage and management solution for PDF documents.

![Product Name Screen Shot][product-screenshot]

User Interface in version 8.0.0.

### Features
* Store and manage PDF documents in a single-user or compatible, multi-user (on-prem or cloud) database where they’re indexed to provide full-text search functionality.
* Upload PDF documents individually or in bulk. In addition, Upload Profile folders can be setup to allow for integration and automation.
* Apply a category/tax year to selected documents and when PDF documents are uploaded.
* Set the flag state on a selected document and when PDF documents are uploaded to mark for follow-up.
* Add notes to a selected document that can include the date, time, and user account name. All notes can be edited and are indexed by the database.
* Find documents by Search Term, Selections (Author/Subject/Category/Tax Year), or Date Added. In addition, flagged documents or all documents can be listed.
* With PDFKeeper, the following functions can be performed on a selected document: PDF viewing with the bundled or default viewer; PDF bursting; flag document state management; notes viewing and editing; keywords, PDF preview, PDF text, and search term snippets (when applicable) are also displayed for viewing.
* Export selected PDF documents with their category, tax year, notes, and flag state from the database for easy importing.

These features are available in version 8.0.0. 

For release history, please see the [Changelog](https://github.com/rffrasca/PDFKeeper/blob/master/docs/Changelog.md).

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Installation

- Client prerequisites and compatible Database Management Systems are listed on the release page.
- Download and install the latest version of PDFKeeper from [here](https://github.com/rffrasca/PDFKeeper/releases/latest) or install using Windows Package Manager (winget install pdfkeeper).
- Note, PDFKeeper is installed per-user.
- Database setup instructions are available in the Help file that can be viewed post-install.

### Building from Source

Build Instructions for version 8.0.0 is available [here](https://github.com/rffrasca/PDFKeeper/blob/master/docs/Build-Instructions-8.0.0.md).

Build Instructions for version 7.x.x is available [here](https://github.com/rffrasca/PDFKeeper/blob/master/docs/Build-Instructions-7.x.x.md).

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

PDFKeeper is distributed under the terms of the [GNU General Public License (GPL) Version 3](https://github.com/robertfrasca/PDFKeeper/blob/master/COPYING).

[![Logo](https://www.gnu.org/graphics/gplv3-with-text-136x68.png)](https://github.com/robertfrasca/PDFKeeper/blob/master/COPYING)

[![Logo](https://opensource.org/trademarks/osi-certified/web/osi-certified-120x100.png)](https://opensource.org/licenses)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

[Robert F. Frasca](mailto:rffrasca@gmail.com) - Project Owner and Maintainer

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Best-README-Template](https://github.com/othneildrew/Best-README-Template)
* [ThirdPartyLibraries](https://github.com/max-ieremenko/ThirdPartyLibraries)
* [Third-Party software included with PDFKeeper](https://github.com/rffrasca/PDFKeeper/blob/master/THIRD-PARTY-NOTICES.txt)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[downloads-shield]: https://img.shields.io/github/downloads/rffrasca/PDFKeeper/total?style=for-the-badge
[downloads-url]: https://img.shields.io/github/downloads/rffrasca/PDFKeeper/total?style=for-the-badge
[downloads-latest-shield]: https://img.shields.io/github/downloads/rffrasca/pdfkeeper/latest/total?style=for-the-badge
[downloads-latest-url]: https://img.shields.io/github/downloads/rffrasca/pdfkeeper/latest/total?style=for-the-badge
[twitter-shield]: https://img.shields.io/twitter/follow/PDFKeeper?style=for-the-badge
[twitter-url]: https://twitter.com/PDFKeeper
[product-screenshot]: https://github.com/rffrasca/pdfkeeper/blob/master/docs/Screenshot-8.0.0.png
